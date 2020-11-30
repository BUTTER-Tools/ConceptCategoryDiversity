using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using PluginContracts;
using OutputHelperLib;
using System.Windows.Forms;
using System.Reflection;

namespace ConceptCatDiv
{
    public class ConceptCategoryDiversity : Plugin
    {


        public string[] InputType { get; } = { "Tokens" };
        public string OutputType { get; } = "OutputArray";

        public Dictionary<int, string> OutputHeaderData { get; set; } = new Dictionary<int, string>() { { 0, "TokenizedText" } };
        public bool InheritHeader { get; } = false;

        #region Plugin Details and Info

        public string PluginName { get; } = "Concept-Category Diversity";
        public string PluginType { get; } = "Language Analysis";
        public string PluginVersion { get; } = "1.0.1";
        public string PluginAuthor { get; } = "Ryan L. Boyd (ryan@ryanboyd.io)";
        public string PluginDescription { get; } = "This plugin is designed primarily to quantify the \"active emotion vocabulary\" of a text. This code is derived from an earlier, stand-alone program called \"Vocabulate\" which itself implemented the underlying method for the following paper: " + Environment.NewLine + Environment.NewLine +
                                                   "Vine, V., Boyd, R. L., & Pennebaker, J. W. (2020). Natural emotion vocabularies as windows on distress and well-being. Nature Communications, 11(4525), 1-9. https://doi.org/10.1038/s41467-020-18349-0" + Environment.NewLine + Environment.NewLine +
                                                   "By default, this plugin uses the concept-category map from the above paper. However, it is also possible to use this plugin for other dictionaries of the same structure to expand the analysis outside of our original, specific emotional domains." + Environment.NewLine + Environment.NewLine +
                                                   "Currently, this plugin only loads/uses one dictionary at a time. If there is a demand for the ability to process with multiple dictionaries concurrently, this feature will be added." + Environment.NewLine + Environment.NewLine +
                                                        "Output Information" + Environment.NewLine +
                                                        "----------------------------" + Environment.NewLine +
                                                        "WC:\tWord Count" + Environment.NewLine +
                                                        "TC:\tToken Count" + Environment.NewLine +
                                                        "TTR:\tType/Token Ratio" + Environment.NewLine +
                                                        "CWR:\tConcept/Word Ratio" + Environment.NewLine +
                                                        "CCR:\tConcept/Category Ratio";

        public string PluginTutorial { get; } = "Coming Soon";
        public bool TopLevel { get; } = false;


        public Icon GetPluginIcon
        {
            get
            {
                return Properties.Resources.icon;
            }
        }

        #endregion









        char quote { get; set; } = '"';
        char delim { get; set; } = ',';
        bool outputRawCategoryCounts { get; set; } = true;
        bool outputCapturedText { get; set; } = false;

        private static string dictionaryPath = Path.Combine(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory),
                                        "Plugins" + Path.DirectorySeparatorChar +
                                        "Dependencies" + Path.DirectorySeparatorChar +
                                        "ConceptCatDivDicts");

        private static string defaultDictFilename = "2019-07-30 - AEV_Dict.csv";

        private string defaultEncoding = System.Text.Encoding.UTF8.BodyName;
        private Dictionary<string, int> OutputMap { get; set; }

        
        private DictionaryData dictData { get; set; }
        




        public void ChangeSettings()
        {

            using (var form = new SettingsForm_ConceptCatDiv(quote, delim, outputRawCategoryCounts, outputCapturedText, defaultEncoding, dictData))
            {

                form.Icon = Properties.Resources.icon;
                form.Text = PluginName;


                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    quote = form.quote;
                    delim = form.delim;
                    outputRawCategoryCounts = form.outputRawCategoryCounts;
                    outputCapturedText = form.outputCapturedText;
                    defaultEncoding = form.defaultEncoding;
                    dictData = form.dictData;
                }
            }

        }





        public Payload RunPlugin(Payload Input)
        {



            Payload pData = new Payload();
            pData.FileID = Input.FileID;
            pData.SegmentID = Input.SegmentID;
            pData.SegmentNumber = Input.SegmentNumber;
    
            //this is the textanalyzer that we're going to use
            TextAnalyzer textAnalyzer = new TextAnalyzer(dictData);

            for (int i = 0; i < Input.StringArrayList.Count; i++)
            {




                Dictionary<string, string> analysisResults = textAnalyzer.Analyze(Input.StringArrayList[i], outputCapturedText, outputRawCategoryCounts);

                //create an empty output array here
                string[] outputArray = new string[OutputHeaderData.Count];
                for (int j = 0; j < outputArray.Length; j++) outputArray[j] = "";

                //now we fill up our output array
                for (int j = 0; j < outputArray.Length; j++)
                {
                    outputArray[OutputMap[OutputHeaderData[j]]] = analysisResults[OutputHeaderData[j]];
                }

                pData.StringArrayList.Add(outputArray);

            }

            return (pData);

        }



        public void Initialize() 
        {

            OutputHeaderData = new Dictionary<int, string>();
            OutputHeaderData.Add(0, "TokenCount");
            OutputHeaderData.Add(1, "DictPct");
            OutputHeaderData.Add(2, "TTR_Overall");
            OutputHeaderData.Add(3, "TTR_NonDict");

            OutputMap = new Dictionary<string, int>();
            foreach (int key in OutputHeaderData.Keys) OutputMap.Add(OutputHeaderData[key], key);


            //output headers for the Concept-constrained Concept-Word Ratio (CWR)
            for (int i = 0; i < dictData.NumCats; i++)
            {
                int indexToAdd = OutputHeaderData.Count;
                OutputHeaderData.Add(indexToAdd, dictData.CatNames[i] + "_CWR");
                OutputMap.Add(dictData.CatNames[i] + "_CWR", indexToAdd);
            }

            //output headers for the Concept-Category Ratio (CCR)
            for (int i = 0; i < dictData.NumCats; i++)
            {
                int indexToAdd = OutputHeaderData.Count;
                OutputHeaderData.Add(indexToAdd, dictData.CatNames[i] + "_CCR");
                OutputMap.Add(dictData.CatNames[i] + "_CCR", indexToAdd);
            }

            //if they want the raw category counts, then we add those to the header as well
            if (outputRawCategoryCounts)
            {
                for (int i = 0; i < dictData.NumCats; i++)
                {
                    int indexToAdd = OutputHeaderData.Count;
                    OutputHeaderData.Add(indexToAdd, dictData.CatNames[i] + "_Count");
                    OutputMap.Add(dictData.CatNames[i] + "_Count", indexToAdd);
                }

                for (int i = 0; i < dictData.NumCats; i++)
                {
                    int indexToAdd = OutputHeaderData.Count;
                    OutputHeaderData.Add(indexToAdd, dictData.CatNames[i] + "_Unique");
                    OutputMap.Add(dictData.CatNames[i] + "_Unique", indexToAdd);
                }

            }

            if (outputCapturedText)
            {
                int indexToAdd = OutputHeaderData.Count;
                OutputHeaderData.Add(indexToAdd, "CapturedText");
                OutputMap.Add("CapturedText", indexToAdd);
            }


        }




        public bool InspectSettings()
        {
            if (dictData.DictionaryLoaded == false)
            {
                MessageBox.Show("The \"" + PluginName + "\" plugin does not currently have a dictionary loaded. This plugin requires a dictionary to be loaded in order to run.");
                return false;
            }
            return true;
        }

        public Payload FinishUp(Payload Input)
        {
            return (Input);
        }







        public ConceptCategoryDiversity()
        {

            dictData = new DictionaryData();
            DictDataLoader dictLoad = new DictDataLoader();

            //try { 
                dictData = dictLoad.LoadDictionaryFile(Path.Combine(dictionaryPath, defaultDictFilename), Encoding.GetEncoding(defaultEncoding), delim, quote);
            //}
            //catch
            //{
            //    dictData = dictLoad.ParseDictionary(Properties.Resources._2019_07_30___AEV_Dict, delim, quote);
            //}

        }



        #region Import/Export Settings
        public void ImportSettings(Dictionary<string, string> SettingsDict)
        {
            quote = SettingsDict["quote"][0];
            delim = SettingsDict["delim"][0];
            outputRawCategoryCounts = Boolean.Parse(SettingsDict["outputRawCategoryCounts"]);
            outputCapturedText = Boolean.Parse(SettingsDict["outputCapturedText"]);
            defaultEncoding = SettingsDict["defaultEncoding"];

            DictDataLoader dictLoader = new DictDataLoader();
            string rawDictionaryText = SettingsDict["dictData"];
            dictData = dictLoader.ParseDictionary(rawDictionaryText, delim, quote);

        }

        public Dictionary<string, string> ExportSettings(bool suppressWarnings)
        {
            Dictionary<string, string> SettingsDict = new Dictionary<string, string>();

            
            SettingsDict.Add("quote", quote.ToString());
            SettingsDict.Add("delim", delim.ToString());
            SettingsDict.Add("outputRawCategoryCounts", outputRawCategoryCounts.ToString());
            SettingsDict.Add("outputCapturedText", outputCapturedText.ToString());
            SettingsDict.Add("defaultEncoding", defaultEncoding);
            SettingsDict.Add("dictData", dictData.RawDictionaryText);

            return (SettingsDict);
        }
        #endregion

    }
}
