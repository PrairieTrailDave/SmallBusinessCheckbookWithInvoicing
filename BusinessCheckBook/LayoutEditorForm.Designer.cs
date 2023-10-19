namespace BusinessCheckBook
{
    partial class LayoutEditorForm
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
            panel1 = new Panel();
            textBox3 = new TextBox();
            label5 = new Label();
            textBox2 = new TextBox();
            label4 = new Label();
            textBox1 = new TextBox();
            label3 = new Label();
            ItemComboBox = new ComboBox();
            label2 = new Label();
            WhichLayoutLabel = new Label();
            label1 = new Label();
            WhereToPlacePanel = new Panel();
            label6 = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(textBox3);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(textBox2);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(textBox1);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(ItemComboBox);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(WhichLayoutLabel);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(937, 70);
            panel1.TabIndex = 0;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(676, 30);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(79, 23);
            textBox3.TabIndex = 9;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(618, 33);
            label5.Name = "label5";
            label5.Size = new Size(39, 15);
            label5.TabIndex = 8;
            label5.Text = "Width";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(526, 30);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(64, 23);
            textBox2.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(457, 33);
            label4.Name = "label4";
            label4.Size = new Size(63, 15);
            label4.TabIndex = 6;
            label4.Text = "X Position:";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(385, 30);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(54, 23);
            textBox1.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(319, 33);
            label3.Name = "label3";
            label3.Size = new Size(60, 15);
            label3.TabIndex = 4;
            label3.Text = "Y Position";
            // 
            // ItemComboBox
            // 
            ItemComboBox.FormattingEnabled = true;
            ItemComboBox.Location = new Point(114, 30);
            ItemComboBox.Name = "ItemComboBox";
            ItemComboBox.Size = new Size(167, 23);
            ItemComboBox.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 33);
            label2.Name = "label2";
            label2.Size = new Size(105, 15);
            label2.TabIndex = 2;
            label2.Text = "Item Being placed:";
            // 
            // WhichLayoutLabel
            // 
            WhichLayoutLabel.AutoSize = true;
            WhichLayoutLabel.Location = new Point(95, 9);
            WhichLayoutLabel.Name = "WhichLayoutLabel";
            WhichLayoutLabel.Size = new Size(22, 15);
            WhichLayoutLabel.TabIndex = 1;
            WhichLayoutLabel.Text = "Ch";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 9);
            label1.Name = "label1";
            label1.Size = new Size(86, 15);
            label1.TabIndex = 0;
            label1.Text = "Which Layout: ";
            // 
            // WhereToPlacePanel
            // 
            WhereToPlacePanel.Location = new Point(12, 97);
            WhereToPlacePanel.Name = "WhereToPlacePanel";
            WhereToPlacePanel.Size = new Size(1728, 632);
            WhereToPlacePanel.TabIndex = 1;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, -6);
            label6.Name = "label6";
            label6.Size = new Size(218, 15);
            label6.TabIndex = 7;
            label6.Text = "Copyright 2023 Prarie Trail Software, Inc.";
            label6.Visible = false;
            // 
            // LayoutEditorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1752, 702);
            Controls.Add(label6);
            Controls.Add(WhereToPlacePanel);
            Controls.Add(panel1);
            Name = "LayoutEditorForm";
            Text = "Layout Editor";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Label label2;
        private Label WhichLayoutLabel;
        private Label label1;
        private TextBox textBox3;
        private Label label5;
        private TextBox textBox2;
        private Label label4;
        private TextBox textBox1;
        private Label label3;
        private ComboBox ItemComboBox;
        private Panel WhereToPlacePanel;
        private Label label6;
    }
}