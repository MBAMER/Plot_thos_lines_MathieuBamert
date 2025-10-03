namespace Plot_The_Line_MathieuBamert
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
            panel1 = new Panel();
            button1 = new Button();
            checkedListBoxTemp = new CheckedListBox();
            label1 = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.Cyan;
            panel1.Location = new Point(380, 103);
            panel1.Name = "panel1";
            panel1.Size = new Size(740, 413);
            panel1.TabIndex = 0;
            // 
            // button1
            // 
            button1.Location = new Point(990, 12);
            button1.Name = "button1";
            button1.Size = new Size(187, 43);
            button1.TabIndex = 1;
            button1.Text = "Importer un Fichier CSV";
            button1.UseVisualStyleBackColor = true;
            button1.Click += ImporterFichierCSV;
            // 
            // checkedListBoxTemp
            // 
            checkedListBoxTemp.CheckOnClick = true;
            checkedListBoxTemp.Location = new Point(12, 103);
            checkedListBoxTemp.Name = "checkedListBoxTemp";
            checkedListBoxTemp.Size = new Size(290, 400);
            checkedListBoxTemp.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15F);
            label1.Location = new Point(710, 537);
            label1.Name = "label1";
            label1.Size = new Size(61, 28);
            label1.TabIndex = 3;
            label1.Text = "Dates";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15F);
            label2.ImageAlign = ContentAlignment.MiddleLeft;
            label2.Location = new Point(289, 59);
            label2.Name = "label2";
            label2.Size = new Size(121, 28);
            label2.TabIndex = 4;
            label2.Text = "Température";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1206, 653);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(checkedListBoxTemp);
            Controls.Add(button1);
            Controls.Add(panel1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Button button1;
        private CheckedListBox checkedListBoxTemp;
        private Label label1;
        private Label label2;
    }
}
