namespace WinFormsApp2
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            buttonStartNumbers = new Button();
            buttonStartLetters = new Button();
            buttonStartSymbols = new Button();
            comboBoxNumberPriority = new ComboBox();
            comboBoxLetterPriority = new ComboBox();
            comboBoxSymbolPriority = new ComboBox();
            textBoxOutput = new TextBox();
            SuspendLayout();
            // 
            // buttonStartNumbers
            // 
            buttonStartNumbers.Location = new Point(51, 10);
            buttonStartNumbers.Margin = new Padding(2, 1, 2, 1);
            buttonStartNumbers.Name = "buttonStartNumbers";
            buttonStartNumbers.Size = new Size(81, 22);
            buttonStartNumbers.TabIndex = 0;
            buttonStartNumbers.Text = "button1";
            buttonStartNumbers.UseVisualStyleBackColor = true;
            buttonStartNumbers.Click += buttonStartNumbers_Click;
            // 
            // buttonStartLetters
            // 
            buttonStartLetters.Location = new Point(182, 10);
            buttonStartLetters.Margin = new Padding(2, 1, 2, 1);
            buttonStartLetters.Name = "buttonStartLetters";
            buttonStartLetters.Size = new Size(81, 22);
            buttonStartLetters.TabIndex = 1;
            buttonStartLetters.Text = "button1";
            buttonStartLetters.UseVisualStyleBackColor = true;
            buttonStartLetters.Click += buttonStartLetters_Click;
            // 
            // buttonStartSymbols
            // 
            buttonStartSymbols.Location = new Point(317, 10);
            buttonStartSymbols.Margin = new Padding(2, 1, 2, 1);
            buttonStartSymbols.Name = "buttonStartSymbols";
            buttonStartSymbols.Size = new Size(81, 22);
            buttonStartSymbols.TabIndex = 2;
            buttonStartSymbols.Text = "button2";
            buttonStartSymbols.UseVisualStyleBackColor = true;
            buttonStartSymbols.Click += buttonStartSymbols_Click;
            // 
            // comboBoxNumberPriority
            // 
            comboBoxNumberPriority.FormattingEnabled = true;
            comboBoxNumberPriority.Location = new Point(22, 34);
            comboBoxNumberPriority.Margin = new Padding(2, 1, 2, 1);
            comboBoxNumberPriority.Name = "comboBoxNumberPriority";
            comboBoxNumberPriority.Size = new Size(132, 23);
            comboBoxNumberPriority.TabIndex = 3;
            // 
            // comboBoxLetterPriority
            // 
            comboBoxLetterPriority.FormattingEnabled = true;
            comboBoxLetterPriority.Location = new Point(158, 34);
            comboBoxLetterPriority.Margin = new Padding(2, 1, 2, 1);
            comboBoxLetterPriority.Name = "comboBoxLetterPriority";
            comboBoxLetterPriority.Size = new Size(132, 23);
            comboBoxLetterPriority.TabIndex = 4;
            // 
            // comboBoxSymbolPriority
            // 
            comboBoxSymbolPriority.FormattingEnabled = true;
            comboBoxSymbolPriority.Location = new Point(294, 34);
            comboBoxSymbolPriority.Margin = new Padding(2, 1, 2, 1);
            comboBoxSymbolPriority.Name = "comboBoxSymbolPriority";
            comboBoxSymbolPriority.Size = new Size(132, 23);
            comboBoxSymbolPriority.TabIndex = 5;
            // 
            // textBoxOutput
            // 
            textBoxOutput.Location = new Point(22, 77);
            textBoxOutput.Margin = new Padding(2, 1, 2, 1);
            textBoxOutput.Multiline = true;
            textBoxOutput.Name = "textBoxOutput";
            textBoxOutput.ReadOnly = true;
            textBoxOutput.ScrollBars = ScrollBars.Vertical;
            textBoxOutput.Size = new Size(110, 124);
            textBoxOutput.TabIndex = 6;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(431, 211);
            Controls.Add(textBoxOutput);
            Controls.Add(comboBoxSymbolPriority);
            Controls.Add(comboBoxLetterPriority);
            Controls.Add(comboBoxNumberPriority);
            Controls.Add(buttonStartSymbols);
            Controls.Add(buttonStartLetters);
            Controls.Add(buttonStartNumbers);
            Margin = new Padding(2, 1, 2, 1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonStartNumbers;
        private Button buttonStartLetters;
        private Button buttonStartSymbols;
        private ComboBox comboBoxNumberPriority;
        private ComboBox comboBoxLetterPriority;
        private ComboBox comboBoxSymbolPriority;
        private TextBox textBoxOutput;
    }
}
