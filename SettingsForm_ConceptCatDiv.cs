using System;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace ConceptCatDiv
{
    public partial class SettingsForm_ConceptCatDiv : Form
    {




        internal char quote { get; set; }
        internal char delim { get; set; }
        internal bool outputRawCategoryCounts { get; set; }
        internal bool outputCapturedText { get; set; }
        internal string defaultEncoding { get; set; }
        internal DictionaryData dictData { get; set; }



        internal SettingsForm_ConceptCatDiv(char quote_in, char delim_in, bool outputRawCategoryCounts_in, bool outputCapturedText_in, string defaultEncoding_in, DictionaryData dictData_in)
        {
            InitializeComponent();

            foreach (var encoding in Encoding.GetEncodings())
            {
                EncodingDropdown.Items.Add(encoding.Name);
            }

            try
            {
                EncodingDropdown.SelectedIndex = EncodingDropdown.FindStringExact("utf-8");
            }
            catch
            {
                EncodingDropdown.SelectedIndex = EncodingDropdown.FindStringExact(defaultEncoding_in);
            }

            CSVDelimiterTextbox.Text = delim_in.ToString();
            CSVQuoteTextbox.Text = quote_in.ToString();

            if (outputRawCategoryCounts_in) RawWCCheckbox.Checked = true;
            if (outputCapturedText_in) OutputCapturedWordsCheckbox.Checked = true;

            dictData = dictData_in;
            DictStructureTextBox.Text = GeneratePreview(dictData);

        }

        private void LoadDictionaryButton_Click(object sender, EventArgs e)
        {
            dictData = new DictionaryData();

            DictStructureTextBox.Text = "";

            OpenFileDialog openFileDialog = new OpenFileDialog();
            FolderBrowserDialog FolderBrowser = new FolderBrowserDialog();

            openFileDialog.Title = "Please choose your dictionary file";

            if (openFileDialog.ShowDialog() != DialogResult.Cancel)
            {

                FolderBrowser.SelectedPath = System.IO.Path.GetDirectoryName(openFileDialog.FileName);

                //Load dictionary file now
                try
                {
                    Encoding SelectedEncoding = null;
                    SelectedEncoding = Encoding.GetEncoding(EncodingDropdown.SelectedItem.ToString());

                    DictDataLoader DictionaryLoader = new DictDataLoader();
                    dictData = DictionaryLoader.LoadDictionaryFile(openFileDialog.FileName, SelectedEncoding, CSVDelimiterTextbox.Text[0], CSVQuoteTextbox.Text[0]);
                    dictData.DictionaryLoaded = true;
                    DictStructureTextBox.Text = GeneratePreview(dictData);
                    MessageBox.Show("Your dictionary has been successfully loaded.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch
                {
                    MessageBox.Show("BUTTER is having trouble loading your dictionary file. The most common causes of this problem are:" + Environment.NewLine + Environment.NewLine +
                        "-> Your dictionary file is already being used by another application" + Environment.NewLine +
                        "-> Your dictionary is formatted incorrectly" + Environment.NewLine +
                        "-> You dictionary contains duplicate words (the same word appearing more than once)" + Environment.NewLine + Environment.NewLine +
                        "Please check to make sure that none of these issues exist in your dictionary file.",
                        "Dictionary Load Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dictData.DictionaryLoaded = false;
                    return;
                }

            }
            else
            {
                return;
            }
        }


        private void OKButton_Click(object sender, EventArgs e)
        {

            if (CSVQuoteTextbox.TextLength >= 1) 
            { 
                this.quote = CSVQuoteTextbox.Text[0];
            }
            else
            {
                this.quote = '"';
            }

            if (CSVDelimiterTextbox.TextLength >= 1)
            {
                this.delim = CSVDelimiterTextbox.Text[0];
            }
            else
            {
                this.delim = ',';
            }

            this.defaultEncoding = EncodingDropdown.SelectedItem.ToString();
            this.outputRawCategoryCounts = RawWCCheckbox.Checked;
            this.outputCapturedText = OutputCapturedWordsCheckbox.Checked;
            this.DialogResult = DialogResult.OK;

        }



        private string GeneratePreview(DictionaryData DictData)
        {

            //this is where we load up the dictionary preview
            StringBuilder DictPreview = new StringBuilder();

            DictPreview.AppendLine("TERM -> CONCEPT -> [CATEGORIES]");
            DictPreview.AppendLine("-------------------------------");

            foreach (string StemType in DictData.FullDictionaryMap.Keys)
            {
                foreach (int WordCountKey in DictData.FullDictionaryMap[StemType].Keys)
                {

                    foreach (var Word in DictData.FullDictionaryMap[StemType][WordCountKey])
                    {

                        DictPreview.AppendLine(Word.Key + " -> " + Word.Value + " -> [" + string.Join(", ", DictData.ConceptMap[Word.Value]) + "]");

                    }

                }
            }


            return DictPreview.ToString();

        }



    }

}
