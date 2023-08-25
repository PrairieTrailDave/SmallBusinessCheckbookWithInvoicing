namespace BusinessCheckBook.Settings
{
    partial class PayToListForm
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
            PayToDataGridView = new DataGridView();
            label1 = new Label();
            label2 = new Label();
            AccountNameTextBox = new TextBox();
            label3 = new Label();
            BusinessNameTextBox = new TextBox();
            label4 = new Label();
            PrintAsTextBox = new TextBox();
            label5 = new Label();
            AddressTextBox = new TextBox();
            Address2TextBox = new TextBox();
            label6 = new Label();
            CityTextBox = new TextBox();
            StateComboBox = new ComboBox();
            ZipCodeTextBox = new TextBox();
            label7 = new Label();
            ContactPersonTextBox = new TextBox();
            label8 = new Label();
            PhoneTextBox = new TextBox();
            label9 = new Label();
            EmailAddressTextBox = new TextBox();
            TaxableCheckBox = new CheckBox();
            label10 = new Label();
            TaxIDTextBox = new TextBox();
            ClearButton = new Button();
            AddButton = new Button();
            UpdateButton = new Button();
            DisableButton = new Button();
            DoneButton = new Button();
            label11 = new Label();
            CountryTextBox = new TextBox();
            Send1099CheckBox = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)PayToDataGridView).BeginInit();
            SuspendLayout();
            // 
            // PayToDataGridView
            // 
            PayToDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            PayToDataGridView.Location = new Point(86, 120);
            PayToDataGridView.Margin = new Padding(4, 5, 4, 5);
            PayToDataGridView.Name = "PayToDataGridView";
            PayToDataGridView.RowHeadersWidth = 62;
            PayToDataGridView.RowTemplate.Height = 25;
            PayToDataGridView.Size = new Size(836, 797);
            PayToDataGridView.TabIndex = 0;
            PayToDataGridView.CellContentClick += PayToDataGridView_CellContentClick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(109, 60);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(118, 25);
            label1.TabIndex = 1;
            label1.Text = "List of Payees";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(1136, 147);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(133, 25);
            label2.TabIndex = 2;
            label2.Text = "Account Name:";
            // 
            // AccountNameTextBox
            // 
            AccountNameTextBox.Location = new Point(1300, 142);
            AccountNameTextBox.Margin = new Padding(4, 5, 4, 5);
            AccountNameTextBox.Name = "AccountNameTextBox";
            AccountNameTextBox.Size = new Size(493, 31);
            AccountNameTextBox.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(1136, 195);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(135, 25);
            label3.TabIndex = 4;
            label3.Text = "Business Name:";
            // 
            // BusinessNameTextBox
            // 
            BusinessNameTextBox.Location = new Point(1300, 190);
            BusinessNameTextBox.Margin = new Padding(4, 5, 4, 5);
            BusinessNameTextBox.Name = "BusinessNameTextBox";
            BusinessNameTextBox.Size = new Size(493, 31);
            BusinessNameTextBox.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(1136, 243);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(77, 25);
            label4.TabIndex = 6;
            label4.Text = "Print As:";
            // 
            // PrintAsTextBox
            // 
            PrintAsTextBox.Location = new Point(1300, 238);
            PrintAsTextBox.Margin = new Padding(4, 5, 4, 5);
            PrintAsTextBox.Name = "PrintAsTextBox";
            PrintAsTextBox.Size = new Size(493, 31);
            PrintAsTextBox.TabIndex = 7;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(1136, 292);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(81, 25);
            label5.TabIndex = 8;
            label5.Text = "Address:";
            // 
            // AddressTextBox
            // 
            AddressTextBox.Location = new Point(1300, 287);
            AddressTextBox.Margin = new Padding(4, 5, 4, 5);
            AddressTextBox.Name = "AddressTextBox";
            AddressTextBox.Size = new Size(493, 31);
            AddressTextBox.TabIndex = 9;
            // 
            // Address2TextBox
            // 
            Address2TextBox.Location = new Point(1300, 335);
            Address2TextBox.Margin = new Padding(4, 5, 4, 5);
            Address2TextBox.Name = "Address2TextBox";
            Address2TextBox.Size = new Size(493, 31);
            Address2TextBox.TabIndex = 10;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(1136, 388);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(128, 25);
            label6.TabIndex = 11;
            label6.Text = "City, State, Zip:";
            // 
            // CityTextBox
            // 
            CityTextBox.Location = new Point(1300, 383);
            CityTextBox.Margin = new Padding(4, 5, 4, 5);
            CityTextBox.Name = "CityTextBox";
            CityTextBox.Size = new Size(333, 31);
            CityTextBox.TabIndex = 12;
            // 
            // StateComboBox
            // 
            StateComboBox.FormattingEnabled = true;
            StateComboBox.Location = new Point(1643, 383);
            StateComboBox.Margin = new Padding(4, 5, 4, 5);
            StateComboBox.Name = "StateComboBox";
            StateComboBox.Size = new Size(73, 33);
            StateComboBox.TabIndex = 13;
            // 
            // ZipCodeTextBox
            // 
            ZipCodeTextBox.Location = new Point(1726, 383);
            ZipCodeTextBox.Margin = new Padding(4, 5, 4, 5);
            ZipCodeTextBox.Name = "ZipCodeTextBox";
            ZipCodeTextBox.Size = new Size(174, 31);
            ZipCodeTextBox.TabIndex = 14;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(1136, 498);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(135, 25);
            label7.TabIndex = 15;
            label7.Text = "Contact Person:";
            // 
            // ContactPersonTextBox
            // 
            ContactPersonTextBox.Location = new Point(1300, 493);
            ContactPersonTextBox.Margin = new Padding(4, 5, 4, 5);
            ContactPersonTextBox.Name = "ContactPersonTextBox";
            ContactPersonTextBox.Size = new Size(493, 31);
            ContactPersonTextBox.TabIndex = 16;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(1136, 547);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(66, 25);
            label8.TabIndex = 17;
            label8.Text = "Phone:";
            // 
            // PhoneTextBox
            // 
            PhoneTextBox.Location = new Point(1300, 542);
            PhoneTextBox.Margin = new Padding(4, 5, 4, 5);
            PhoneTextBox.Name = "PhoneTextBox";
            PhoneTextBox.Size = new Size(493, 31);
            PhoneTextBox.TabIndex = 18;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(1136, 595);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(128, 25);
            label9.TabIndex = 19;
            label9.Text = "Email Address:";
            // 
            // EmailAddressTextBox
            // 
            EmailAddressTextBox.Location = new Point(1300, 590);
            EmailAddressTextBox.Margin = new Padding(4, 5, 4, 5);
            EmailAddressTextBox.Name = "EmailAddressTextBox";
            EmailAddressTextBox.Size = new Size(493, 31);
            EmailAddressTextBox.TabIndex = 20;
            // 
            // TaxableCheckBox
            // 
            TaxableCheckBox.AutoSize = true;
            TaxableCheckBox.Location = new Point(1300, 677);
            TaxableCheckBox.Margin = new Padding(4, 5, 4, 5);
            TaxableCheckBox.Name = "TaxableCheckBox";
            TaxableCheckBox.Size = new Size(176, 29);
            TaxableCheckBox.TabIndex = 21;
            TaxableCheckBox.Text = "Charge Sales Tax?";
            TaxableCheckBox.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(1136, 737);
            label10.Margin = new Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new Size(145, 25);
            label10.TabIndex = 22;
            label10.Text = "Tax ID (EIN/SSN):";
            // 
            // TaxIDTextBox
            // 
            TaxIDTextBox.Location = new Point(1300, 732);
            TaxIDTextBox.Margin = new Padding(4, 5, 4, 5);
            TaxIDTextBox.Name = "TaxIDTextBox";
            TaxIDTextBox.Size = new Size(333, 31);
            TaxIDTextBox.TabIndex = 23;
            // 
            // ClearButton
            // 
            ClearButton.Location = new Point(1041, 805);
            ClearButton.Margin = new Padding(4, 5, 4, 5);
            ClearButton.Name = "ClearButton";
            ClearButton.Size = new Size(107, 38);
            ClearButton.TabIndex = 24;
            ClearButton.Text = "Clear";
            ClearButton.UseVisualStyleBackColor = true;
            ClearButton.Click += ClearButton_Click;
            // 
            // AddButton
            // 
            AddButton.Location = new Point(1300, 805);
            AddButton.Margin = new Padding(4, 5, 4, 5);
            AddButton.Name = "AddButton";
            AddButton.Size = new Size(107, 38);
            AddButton.TabIndex = 25;
            AddButton.Text = "Add";
            AddButton.UseVisualStyleBackColor = true;
            AddButton.Click += AddButton_Click;
            // 
            // UpdateButton
            // 
            UpdateButton.Location = new Point(1503, 805);
            UpdateButton.Margin = new Padding(4, 5, 4, 5);
            UpdateButton.Name = "UpdateButton";
            UpdateButton.Size = new Size(107, 38);
            UpdateButton.TabIndex = 26;
            UpdateButton.Text = "Update";
            UpdateButton.UseVisualStyleBackColor = true;
            UpdateButton.Click += UpdateButton_Click;
            // 
            // DisableButton
            // 
            DisableButton.Location = new Point(1754, 805);
            DisableButton.Margin = new Padding(4, 5, 4, 5);
            DisableButton.Name = "DisableButton";
            DisableButton.Size = new Size(107, 38);
            DisableButton.TabIndex = 27;
            DisableButton.Text = "Disable";
            DisableButton.UseVisualStyleBackColor = true;
            DisableButton.Click += DisableButton_Click;
            // 
            // DoneButton
            // 
            DoneButton.Location = new Point(1357, 913);
            DoneButton.Margin = new Padding(4, 5, 4, 5);
            DoneButton.Name = "DoneButton";
            DoneButton.Size = new Size(107, 38);
            DoneButton.TabIndex = 28;
            DoneButton.Text = "Done";
            DoneButton.UseVisualStyleBackColor = true;
            DoneButton.Click += DoneButton_Click;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(1136, 437);
            label11.Margin = new Padding(4, 0, 4, 0);
            label11.Name = "label11";
            label11.Size = new Size(79, 25);
            label11.TabIndex = 29;
            label11.Text = "Country:";
            // 
            // CountryTextBox
            // 
            CountryTextBox.Location = new Point(1671, 437);
            CountryTextBox.Margin = new Padding(4, 5, 4, 5);
            CountryTextBox.Name = "CountryTextBox";
            CountryTextBox.Size = new Size(188, 31);
            CountryTextBox.TabIndex = 30;
            // 
            // Send1099CheckBox
            // 
            Send1099CheckBox.AutoSize = true;
            Send1099CheckBox.Location = new Point(1500, 676);
            Send1099CheckBox.Name = "Send1099CheckBox";
            Send1099CheckBox.Size = new Size(123, 29);
            Send1099CheckBox.TabIndex = 31;
            Send1099CheckBox.Text = "Send 1099";
            Send1099CheckBox.UseVisualStyleBackColor = true;
            // 
            // PayToListForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1924, 1007);
            Controls.Add(Send1099CheckBox);
            Controls.Add(CountryTextBox);
            Controls.Add(label11);
            Controls.Add(DoneButton);
            Controls.Add(DisableButton);
            Controls.Add(UpdateButton);
            Controls.Add(AddButton);
            Controls.Add(ClearButton);
            Controls.Add(TaxIDTextBox);
            Controls.Add(label10);
            Controls.Add(TaxableCheckBox);
            Controls.Add(EmailAddressTextBox);
            Controls.Add(label9);
            Controls.Add(PhoneTextBox);
            Controls.Add(label8);
            Controls.Add(ContactPersonTextBox);
            Controls.Add(label7);
            Controls.Add(ZipCodeTextBox);
            Controls.Add(StateComboBox);
            Controls.Add(CityTextBox);
            Controls.Add(label6);
            Controls.Add(Address2TextBox);
            Controls.Add(AddressTextBox);
            Controls.Add(label5);
            Controls.Add(PrintAsTextBox);
            Controls.Add(label4);
            Controls.Add(BusinessNameTextBox);
            Controls.Add(label3);
            Controls.Add(AccountNameTextBox);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(PayToDataGridView);
            Margin = new Padding(4, 5, 4, 5);
            Name = "PayToListForm";
            Text = "PayToListForm";
            ((System.ComponentModel.ISupportInitialize)PayToDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView PayToDataGridView;
        private Label label1;
        private Label label2;
        private TextBox AccountNameTextBox;
        private Label label3;
        private TextBox BusinessNameTextBox;
        private Label label4;
        private TextBox PrintAsTextBox;
        private Label label5;
        private TextBox AddressTextBox;
        private TextBox Address2TextBox;
        private Label label6;
        private TextBox CityTextBox;
        private ComboBox StateComboBox;
        private TextBox ZipCodeTextBox;
        private Label label7;
        private TextBox ContactPersonTextBox;
        private Label label8;
        private TextBox PhoneTextBox;
        private Label label9;
        private TextBox EmailAddressTextBox;
        private CheckBox TaxableCheckBox;
        private Label label10;
        private TextBox TaxIDTextBox;
        private Button ClearButton;
        private Button AddButton;
        private Button UpdateButton;
        private Button DisableButton;
        private Button DoneButton;
        private Label label11;
        private TextBox CountryTextBox;
        private CheckBox Send1099CheckBox;
    }
}