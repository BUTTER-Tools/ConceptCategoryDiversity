namespace ConceptCatDiv
{
    partial class SettingsForm_ConceptCatDiv
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.DictionarySettingsGroupBox = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CSVQuoteTextbox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.EncodingDropdown = new System.Windows.Forms.ComboBox();
            this.label41 = new System.Windows.Forms.Label();
            this.CSVDelimiterTextbox = new System.Windows.Forms.TextBox();
            this.LoadDictionaryButton = new System.Windows.Forms.Button();
            this.label42 = new System.Windows.Forms.Label();
            this.DictStructureTextBox = new System.Windows.Forms.TextBox();
            this.ProcessingGroupbox = new System.Windows.Forms.GroupBox();
            this.OutputCapturedWordsCheckbox = new System.Windows.Forms.CheckBox();
            this.RawWCCheckbox = new System.Windows.Forms.CheckBox();
            this.OKButton = new System.Windows.Forms.Button();
            this.DictionarySettingsGroupBox.SuspendLayout();
            this.ProcessingGroupbox.SuspendLayout();
            this.SuspendLayout();
            // 
            // DictionarySettingsGroupBox
            // 
            this.DictionarySettingsGroupBox.Controls.Add(this.label1);
            this.DictionarySettingsGroupBox.Controls.Add(this.CSVQuoteTextbox);
            this.DictionarySettingsGroupBox.Controls.Add(this.label4);
            this.DictionarySettingsGroupBox.Controls.Add(this.EncodingDropdown);
            this.DictionarySettingsGroupBox.Controls.Add(this.label41);
            this.DictionarySettingsGroupBox.Controls.Add(this.CSVDelimiterTextbox);
            this.DictionarySettingsGroupBox.Controls.Add(this.LoadDictionaryButton);
            this.DictionarySettingsGroupBox.Controls.Add(this.label42);
            this.DictionarySettingsGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DictionarySettingsGroupBox.Location = new System.Drawing.Point(372, 25);
            this.DictionarySettingsGroupBox.Name = "DictionarySettingsGroupBox";
            this.DictionarySettingsGroupBox.Size = new System.Drawing.Size(264, 247);
            this.DictionarySettingsGroupBox.TabIndex = 32;
            this.DictionarySettingsGroupBox.TabStop = false;
            this.DictionarySettingsGroupBox.Text = "Dictionary / File Settings";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(50, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 15);
            this.label1.TabIndex = 26;
            this.label1.Text = "Dictionary File CSV Properties";
            // 
            // CSVQuoteTextbox
            // 
            this.CSVQuoteTextbox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CSVQuoteTextbox.Location = new System.Drawing.Point(144, 145);
            this.CSVQuoteTextbox.MaxLength = 1;
            this.CSVQuoteTextbox.Name = "CSVQuoteTextbox";
            this.CSVQuoteTextbox.Size = new System.Drawing.Size(65, 20);
            this.CSVQuoteTextbox.TabIndex = 25;
            this.CSVQuoteTextbox.Text = "\"";
            this.CSVQuoteTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(33, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(198, 15);
            this.label4.TabIndex = 10;
            this.label4.Text = "Encoding of Dictionary && Text Files:";
            // 
            // EncodingDropdown
            // 
            this.EncodingDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EncodingDropdown.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EncodingDropdown.FormattingEnabled = true;
            this.EncodingDropdown.Location = new System.Drawing.Point(22, 53);
            this.EncodingDropdown.Name = "EncodingDropdown";
            this.EncodingDropdown.Size = new System.Drawing.Size(221, 23);
            this.EncodingDropdown.TabIndex = 9;
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label41.Location = new System.Drawing.Point(56, 127);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(60, 15);
            this.label41.TabIndex = 22;
            this.label41.Text = "Delimiter:";
            // 
            // CSVDelimiterTextbox
            // 
            this.CSVDelimiterTextbox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CSVDelimiterTextbox.Location = new System.Drawing.Point(57, 145);
            this.CSVDelimiterTextbox.MaxLength = 1;
            this.CSVDelimiterTextbox.Name = "CSVDelimiterTextbox";
            this.CSVDelimiterTextbox.Size = new System.Drawing.Size(65, 20);
            this.CSVDelimiterTextbox.TabIndex = 24;
            this.CSVDelimiterTextbox.Text = ",";
            this.CSVDelimiterTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // LoadDictionaryButton
            // 
            this.LoadDictionaryButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoadDictionaryButton.Location = new System.Drawing.Point(68, 190);
            this.LoadDictionaryButton.Name = "LoadDictionaryButton";
            this.LoadDictionaryButton.Size = new System.Drawing.Size(129, 32);
            this.LoadDictionaryButton.TabIndex = 20;
            this.LoadDictionaryButton.Text = "Load Dictionary";
            this.LoadDictionaryButton.UseVisualStyleBackColor = true;
            this.LoadDictionaryButton.Click += new System.EventHandler(this.LoadDictionaryButton_Click);
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label42.Location = new System.Drawing.Point(154, 127);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(43, 15);
            this.label42.TabIndex = 23;
            this.label42.Text = "Quote:";
            // 
            // DictStructureTextBox
            // 
            this.DictStructureTextBox.AcceptsReturn = true;
            this.DictStructureTextBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DictStructureTextBox.Location = new System.Drawing.Point(30, 25);
            this.DictStructureTextBox.MaxLength = 2147483647;
            this.DictStructureTextBox.Multiline = true;
            this.DictStructureTextBox.Name = "DictStructureTextBox";
            this.DictStructureTextBox.ReadOnly = true;
            this.DictStructureTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.DictStructureTextBox.Size = new System.Drawing.Size(313, 385);
            this.DictStructureTextBox.TabIndex = 31;
            this.DictStructureTextBox.WordWrap = false;
            // 
            // ProcessingGroupbox
            // 
            this.ProcessingGroupbox.Controls.Add(this.OutputCapturedWordsCheckbox);
            this.ProcessingGroupbox.Controls.Add(this.RawWCCheckbox);
            this.ProcessingGroupbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProcessingGroupbox.Location = new System.Drawing.Point(372, 298);
            this.ProcessingGroupbox.Name = "ProcessingGroupbox";
            this.ProcessingGroupbox.Size = new System.Drawing.Size(264, 112);
            this.ProcessingGroupbox.TabIndex = 33;
            this.ProcessingGroupbox.TabStop = false;
            this.ProcessingGroupbox.Text = "Processing Settings";
            // 
            // OutputCapturedWordsCheckbox
            // 
            this.OutputCapturedWordsCheckbox.AutoSize = true;
            this.OutputCapturedWordsCheckbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OutputCapturedWordsCheckbox.Location = new System.Drawing.Point(22, 70);
            this.OutputCapturedWordsCheckbox.Name = "OutputCapturedWordsCheckbox";
            this.OutputCapturedWordsCheckbox.Size = new System.Drawing.Size(177, 17);
            this.OutputCapturedWordsCheckbox.TabIndex = 29;
            this.OutputCapturedWordsCheckbox.Text = "Include Captured Text in Output";
            this.OutputCapturedWordsCheckbox.UseVisualStyleBackColor = true;
            // 
            // RawWCCheckbox
            // 
            this.RawWCCheckbox.AutoSize = true;
            this.RawWCCheckbox.Checked = true;
            this.RawWCCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.RawWCCheckbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RawWCCheckbox.Location = new System.Drawing.Point(22, 36);
            this.RawWCCheckbox.Name = "RawWCCheckbox";
            this.RawWCCheckbox.Size = new System.Drawing.Size(164, 17);
            this.RawWCCheckbox.TabIndex = 21;
            this.RawWCCheckbox.Text = "Output Raw Category Counts";
            this.RawWCCheckbox.UseVisualStyleBackColor = true;
            // 
            // OKButton
            // 
            this.OKButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OKButton.Location = new System.Drawing.Point(276, 443);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(118, 40);
            this.OKButton.TabIndex = 27;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // SettingsForm_ConceptCatDiv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(670, 495);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.DictionarySettingsGroupBox);
            this.Controls.Add(this.DictStructureTextBox);
            this.Controls.Add(this.ProcessingGroupbox);
            this.Name = "SettingsForm_ConceptCatDiv";
            this.Text = "SettingsForm_ConceptCatDiv";
            this.DictionarySettingsGroupBox.ResumeLayout(false);
            this.DictionarySettingsGroupBox.PerformLayout();
            this.ProcessingGroupbox.ResumeLayout(false);
            this.ProcessingGroupbox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox DictionarySettingsGroupBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox CSVQuoteTextbox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox EncodingDropdown;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.TextBox CSVDelimiterTextbox;
        private System.Windows.Forms.Button LoadDictionaryButton;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.TextBox DictStructureTextBox;
        private System.Windows.Forms.GroupBox ProcessingGroupbox;
        private System.Windows.Forms.CheckBox OutputCapturedWordsCheckbox;
        private System.Windows.Forms.CheckBox RawWCCheckbox;
        private System.Windows.Forms.Button OKButton;
    }
}