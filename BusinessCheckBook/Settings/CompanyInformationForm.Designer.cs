namespace BusinessCheckBook.Settings
{
    partial class CompanyInformationForm
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
            label1 = new Label();
            groupBox1 = new GroupBox();
            CompanyZipTextBox = new TextBox();
            CompanyStateComboBox = new ComboBox();
            CompanyCityTextBox = new TextBox();
            label3 = new Label();
            CompanyAddress2TextBox = new TextBox();
            CompanyAddressTextBox = new TextBox();
            label2 = new Label();
            CompanyNameTextBox = new TextBox();
            groupBox2 = new GroupBox();
            CompanyPhoneTextBox = new TextBox();
            label5 = new Label();
            CompanyEINTextBox = new TextBox();
            label4 = new Label();
            SaveButton = new Button();
            label6 = new Label();
            label7 = new Label();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(15, 35);
            label1.Name = "label1";
            label1.Size = new Size(42, 15);
            label1.TabIndex = 0;
            label1.Text = "Name:";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(CompanyZipTextBox);
            groupBox1.Controls.Add(CompanyStateComboBox);
            groupBox1.Controls.Add(CompanyCityTextBox);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(CompanyAddress2TextBox);
            groupBox1.Controls.Add(CompanyAddressTextBox);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(CompanyNameTextBox);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(39, 52);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(586, 178);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Name and Address";
            // 
            // CompanyZipTextBox
            // 
            CompanyZipTextBox.Location = new Point(452, 118);
            CompanyZipTextBox.Name = "CompanyZipTextBox";
            CompanyZipTextBox.Size = new Size(128, 23);
            CompanyZipTextBox.TabIndex = 8;
            // 
            // CompanyStateComboBox
            // 
            CompanyStateComboBox.FormattingEnabled = true;
            CompanyStateComboBox.Location = new Point(409, 118);
            CompanyStateComboBox.Name = "CompanyStateComboBox";
            CompanyStateComboBox.Size = new Size(37, 23);
            CompanyStateComboBox.TabIndex = 7;
            // 
            // CompanyCityTextBox
            // 
            CompanyCityTextBox.Location = new Point(106, 118);
            CompanyCityTextBox.Name = "CompanyCityTextBox";
            CompanyCityTextBox.Size = new Size(297, 23);
            CompanyCityTextBox.TabIndex = 6;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(15, 121);
            label3.Name = "label3";
            label3.Size = new Size(83, 15);
            label3.TabIndex = 5;
            label3.Text = "City, State Zip:";
            // 
            // CompanyAddress2TextBox
            // 
            CompanyAddress2TextBox.Location = new Point(106, 90);
            CompanyAddress2TextBox.Name = "CompanyAddress2TextBox";
            CompanyAddress2TextBox.Size = new Size(474, 23);
            CompanyAddress2TextBox.TabIndex = 4;
            // 
            // CompanyAddressTextBox
            // 
            CompanyAddressTextBox.Location = new Point(106, 61);
            CompanyAddressTextBox.Name = "CompanyAddressTextBox";
            CompanyAddressTextBox.Size = new Size(474, 23);
            CompanyAddressTextBox.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(15, 64);
            label2.Name = "label2";
            label2.Size = new Size(52, 15);
            label2.TabIndex = 2;
            label2.Text = "Address:";
            // 
            // CompanyNameTextBox
            // 
            CompanyNameTextBox.Location = new Point(106, 32);
            CompanyNameTextBox.Name = "CompanyNameTextBox";
            CompanyNameTextBox.Size = new Size(474, 23);
            CompanyNameTextBox.TabIndex = 1;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(CompanyPhoneTextBox);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(CompanyEINTextBox);
            groupBox2.Controls.Add(label4);
            groupBox2.Location = new Point(39, 258);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(298, 100);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "Other Settings";
            // 
            // CompanyPhoneTextBox
            // 
            CompanyPhoneTextBox.Location = new Point(58, 27);
            CompanyPhoneTextBox.Name = "CompanyPhoneTextBox";
            CompanyPhoneTextBox.Size = new Size(122, 23);
            CompanyPhoneTextBox.TabIndex = 3;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(11, 30);
            label5.Name = "label5";
            label5.Size = new Size(44, 15);
            label5.TabIndex = 2;
            label5.Text = "Phone:";
            // 
            // CompanyEINTextBox
            // 
            CompanyEINTextBox.Location = new Point(58, 71);
            CompanyEINTextBox.Name = "CompanyEINTextBox";
            CompanyEINTextBox.Size = new Size(122, 23);
            CompanyEINTextBox.TabIndex = 1;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(11, 74);
            label4.Name = "label4";
            label4.Size = new Size(28, 15);
            label4.TabIndex = 0;
            label4.Text = "EIN:";
            // 
            // SaveButton
            // 
            SaveButton.Location = new Point(97, 402);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(75, 23);
            SaveButton.TabIndex = 3;
            SaveButton.Text = "Save";
            SaveButton.UseVisualStyleBackColor = true;
            SaveButton.Click += SaveButton_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(40, 19);
            label6.Name = "label6";
            label6.Size = new Size(268, 15);
            label6.TabIndex = 4;
            label6.Text = "All fields but the second addressl line are required";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(39, 4);
            label7.Name = "label7";
            label7.Size = new Size(218, 15);
            label7.TabIndex = 7;
            label7.Text = "Copyright 2023 Prarie Trail Software, Inc.";
            label7.Visible = false;
            // 
            // CompanyInformationForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(841, 504);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(SaveButton);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "CompanyInformationForm";
            Text = "Company Information";
            Shown += CompanyInformationForm_Shown;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private GroupBox groupBox1;
        private TextBox CompanyZipTextBox;
        private ComboBox CompanyStateComboBox;
        private TextBox CompanyCityTextBox;
        private Label label3;
        private TextBox CompanyAddress2TextBox;
        private TextBox CompanyAddressTextBox;
        private Label label2;
        private TextBox CompanyNameTextBox;
        private GroupBox groupBox2;
        private TextBox CompanyEINTextBox;
        private Label label4;
        private Button SaveButton;
        private TextBox CompanyPhoneTextBox;
        private Label label5;
        private Label label6;
        private Label label7;
    }
}