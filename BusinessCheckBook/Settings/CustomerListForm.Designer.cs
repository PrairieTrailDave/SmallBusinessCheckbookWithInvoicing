namespace BusinessCheckBook.Settings
{
    partial class CustomerListForm
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
            CustDataGridView = new DataGridView();
            label1 = new Label();
            ClearButton = new Button();
            label2 = new Label();
            CustomerIDTextBox = new TextBox();
            label3 = new Label();
            AccountNameTextBox = new TextBox();
            label4 = new Label();
            BusinessNameTextBox = new TextBox();
            label5 = new Label();
            AddressTextBox = new TextBox();
            label6 = new Label();
            CityTextBox = new TextBox();
            ZipCodeTextBox = new TextBox();
            label7 = new Label();
            ContactPersonTextBox = new TextBox();
            label8 = new Label();
            PhoneTextBox = new TextBox();
            label9 = new Label();
            EmailAddressTextBox = new TextBox();
            label10 = new Label();
            SalesTaxCheckBox = new CheckBox();
            label11 = new Label();
            TaxIDTextBox = new TextBox();
            AddButton = new Button();
            UpdateButton = new Button();
            Address2TextBox = new TextBox();
            StateComboBox = new ComboBox();
            label12 = new Label();
            InActiveButton = new Button();
            ShowAllCustomersButton = new Button();
            ((System.ComponentModel.ISupportInitialize)CustDataGridView).BeginInit();
            SuspendLayout();
            // 
            // CustDataGridView
            // 
            CustDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            CustDataGridView.Location = new Point(34, 92);
            CustDataGridView.Margin = new Padding(4, 5, 4, 5);
            CustDataGridView.Name = "CustDataGridView";
            CustDataGridView.RowHeadersWidth = 62;
            CustDataGridView.RowTemplate.Height = 25;
            CustDataGridView.Size = new Size(896, 630);
            CustDataGridView.TabIndex = 0;
            CustDataGridView.CellContentClick += CustDataGridView_CellContentClick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(256, 38);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(203, 25);
            label1.TabIndex = 1;
            label1.Text = "List of Active Customers";
            // 
            // ClearButton
            // 
            ClearButton.Location = new Point(1061, 722);
            ClearButton.Margin = new Padding(4, 5, 4, 5);
            ClearButton.Name = "ClearButton";
            ClearButton.Size = new Size(107, 38);
            ClearButton.TabIndex = 2;
            ClearButton.Text = "Clear";
            ClearButton.UseVisualStyleBackColor = true;
            ClearButton.Click += ClearButton_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(958, 86);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(116, 25);
            label2.TabIndex = 3;
            label2.Text = "Customer ID:";
            // 
            // CustomerIDTextBox
            // 
            CustomerIDTextBox.Location = new Point(1116, 83);
            CustomerIDTextBox.Margin = new Padding(4, 5, 4, 5);
            CustomerIDTextBox.Name = "CustomerIDTextBox";
            CustomerIDTextBox.Size = new Size(595, 31);
            CustomerIDTextBox.TabIndex = 4;
            CustomerIDTextBox.Leave += CustomerIDTextBox_Leave;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(958, 128);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(133, 25);
            label3.TabIndex = 5;
            label3.Text = "Account Name:";
            // 
            // AccountNameTextBox
            // 
            AccountNameTextBox.Location = new Point(1116, 125);
            AccountNameTextBox.Margin = new Padding(4, 5, 4, 5);
            AccountNameTextBox.Name = "AccountNameTextBox";
            AccountNameTextBox.Size = new Size(595, 31);
            AccountNameTextBox.TabIndex = 6;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(958, 169);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(135, 25);
            label4.TabIndex = 7;
            label4.Text = "Business Name:";
            // 
            // BusinessNameTextBox
            // 
            BusinessNameTextBox.Location = new Point(1116, 166);
            BusinessNameTextBox.Margin = new Padding(4, 5, 4, 5);
            BusinessNameTextBox.Name = "BusinessNameTextBox";
            BusinessNameTextBox.Size = new Size(595, 31);
            BusinessNameTextBox.TabIndex = 8;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(958, 209);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(81, 25);
            label5.TabIndex = 9;
            label5.Text = "Address:";
            // 
            // AddressTextBox
            // 
            AddressTextBox.Location = new Point(1116, 206);
            AddressTextBox.Margin = new Padding(4, 5, 4, 5);
            AddressTextBox.Name = "AddressTextBox";
            AddressTextBox.Size = new Size(595, 31);
            AddressTextBox.TabIndex = 10;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(958, 301);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(120, 25);
            label6.TabIndex = 11;
            label6.Text = "City State Zip:";
            // 
            // CityTextBox
            // 
            CityTextBox.Location = new Point(1116, 298);
            CityTextBox.Margin = new Padding(4, 5, 4, 5);
            CityTextBox.Name = "CityTextBox";
            CityTextBox.Size = new Size(408, 31);
            CityTextBox.TabIndex = 12;
            // 
            // ZipCodeTextBox
            // 
            ZipCodeTextBox.Location = new Point(1580, 298);
            ZipCodeTextBox.Margin = new Padding(4, 5, 4, 5);
            ZipCodeTextBox.Name = "ZipCodeTextBox";
            ZipCodeTextBox.Size = new Size(144, 31);
            ZipCodeTextBox.TabIndex = 14;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(957, 383);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(135, 25);
            label7.TabIndex = 15;
            label7.Text = "Contact Person:";
            // 
            // ContactPersonTextBox
            // 
            ContactPersonTextBox.Location = new Point(1116, 385);
            ContactPersonTextBox.Margin = new Padding(4, 5, 4, 5);
            ContactPersonTextBox.Name = "ContactPersonTextBox";
            ContactPersonTextBox.Size = new Size(608, 31);
            ContactPersonTextBox.TabIndex = 16;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(958, 454);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(66, 25);
            label8.TabIndex = 17;
            label8.Text = "Phone:";
            // 
            // PhoneTextBox
            // 
            PhoneTextBox.Location = new Point(1116, 451);
            PhoneTextBox.Margin = new Padding(4, 5, 4, 5);
            PhoneTextBox.Name = "PhoneTextBox";
            PhoneTextBox.Size = new Size(608, 31);
            PhoneTextBox.TabIndex = 18;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(958, 514);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(128, 25);
            label9.TabIndex = 19;
            label9.Text = "Email Address:";
            // 
            // EmailAddressTextBox
            // 
            EmailAddressTextBox.Location = new Point(1116, 511);
            EmailAddressTextBox.Margin = new Padding(4, 5, 4, 5);
            EmailAddressTextBox.Name = "EmailAddressTextBox";
            EmailAddressTextBox.Size = new Size(608, 31);
            EmailAddressTextBox.TabIndex = 20;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(958, 596);
            label10.Margin = new Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new Size(150, 25);
            label10.TabIndex = 21;
            label10.Text = "Charge Sales Tax?";
            // 
            // SalesTaxCheckBox
            // 
            SalesTaxCheckBox.AutoSize = true;
            SalesTaxCheckBox.Location = new Point(1116, 591);
            SalesTaxCheckBox.Margin = new Padding(4, 5, 4, 5);
            SalesTaxCheckBox.Name = "SalesTaxCheckBox";
            SalesTaxCheckBox.Size = new Size(107, 29);
            SalesTaxCheckBox.TabIndex = 22;
            SalesTaxCheckBox.Text = "Sales Tax";
            SalesTaxCheckBox.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(957, 646);
            label11.Margin = new Padding(4, 0, 4, 0);
            label11.Name = "label11";
            label11.Size = new Size(141, 25);
            label11.TabIndex = 23;
            label11.Text = "Tax ID (EIN/SSN)";
            // 
            // TaxIDTextBox
            // 
            TaxIDTextBox.Location = new Point(1116, 643);
            TaxIDTextBox.Margin = new Padding(4, 5, 4, 5);
            TaxIDTextBox.Name = "TaxIDTextBox";
            TaxIDTextBox.Size = new Size(239, 31);
            TaxIDTextBox.TabIndex = 24;
            // 
            // AddButton
            // 
            AddButton.Location = new Point(1247, 722);
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
            UpdateButton.Location = new Point(1604, 722);
            UpdateButton.Margin = new Padding(4, 5, 4, 5);
            UpdateButton.Name = "UpdateButton";
            UpdateButton.Size = new Size(107, 38);
            UpdateButton.TabIndex = 26;
            UpdateButton.Text = "Update";
            UpdateButton.UseVisualStyleBackColor = true;
            UpdateButton.Click += UpdateButton_Click;
            // 
            // Address2TextBox
            // 
            Address2TextBox.Location = new Point(1116, 250);
            Address2TextBox.Margin = new Padding(4, 5, 4, 5);
            Address2TextBox.Name = "Address2TextBox";
            Address2TextBox.Size = new Size(595, 31);
            Address2TextBox.TabIndex = 28;
            // 
            // StateComboBox
            // 
            StateComboBox.FormattingEnabled = true;
            StateComboBox.Location = new Point(1513, 298);
            StateComboBox.Margin = new Padding(4, 5, 4, 5);
            StateComboBox.Name = "StateComboBox";
            StateComboBox.Size = new Size(78, 33);
            StateComboBox.TabIndex = 29;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(34, 13);
            label12.Margin = new Padding(4, 0, 4, 0);
            label12.Name = "label12";
            label12.Size = new Size(331, 25);
            label12.TabIndex = 30;
            label12.Text = "Copyright 2024 Prarie Trail Software, Inc.";
            label12.Visible = false;
            // 
            // InActiveButton
            // 
            InActiveButton.Location = new Point(1403, 724);
            InActiveButton.Name = "InActiveButton";
            InActiveButton.Size = new Size(156, 34);
            InActiveButton.TabIndex = 31;
            InActiveButton.Text = "Make InActive";
            InActiveButton.UseVisualStyleBackColor = true;
            InActiveButton.Click += InActiveButton_Click;
            // 
            // ShowAllCustomersButton
            // 
            ShowAllCustomersButton.Location = new Point(345, 742);
            ShowAllCustomersButton.Name = "ShowAllCustomersButton";
            ShowAllCustomersButton.Size = new Size(222, 34);
            ShowAllCustomersButton.TabIndex = 32;
            ShowAllCustomersButton.Text = "Show All Customers";
            ShowAllCustomersButton.UseVisualStyleBackColor = true;
            ShowAllCustomersButton.Click += ShowAllCustomersButton_Click;
            // 
            // CustomerListForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1756, 818);
            Controls.Add(ShowAllCustomersButton);
            Controls.Add(InActiveButton);
            Controls.Add(label12);
            Controls.Add(StateComboBox);
            Controls.Add(Address2TextBox);
            Controls.Add(UpdateButton);
            Controls.Add(AddButton);
            Controls.Add(TaxIDTextBox);
            Controls.Add(label11);
            Controls.Add(SalesTaxCheckBox);
            Controls.Add(label10);
            Controls.Add(EmailAddressTextBox);
            Controls.Add(label9);
            Controls.Add(PhoneTextBox);
            Controls.Add(label8);
            Controls.Add(ContactPersonTextBox);
            Controls.Add(label7);
            Controls.Add(ZipCodeTextBox);
            Controls.Add(CityTextBox);
            Controls.Add(label6);
            Controls.Add(AddressTextBox);
            Controls.Add(label5);
            Controls.Add(BusinessNameTextBox);
            Controls.Add(label4);
            Controls.Add(AccountNameTextBox);
            Controls.Add(label3);
            Controls.Add(CustomerIDTextBox);
            Controls.Add(label2);
            Controls.Add(ClearButton);
            Controls.Add(label1);
            Controls.Add(CustDataGridView);
            Margin = new Padding(4, 5, 4, 5);
            Name = "CustomerListForm";
            Text = "CustomerListForm";
            Shown += CustomerListForm_Shown;
            ((System.ComponentModel.ISupportInitialize)CustDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView CustDataGridView;
        private Label label1;
        private Button ClearButton;
        private Label label2;
        private TextBox CustomerIDTextBox;
        private Label label3;
        private TextBox AccountNameTextBox;
        private Label label4;
        private TextBox BusinessNameTextBox;
        private Label label5;
        private TextBox AddressTextBox;
        private Label label6;
        private TextBox CityTextBox;
        private TextBox ZipCodeTextBox;
        private Label label7;
        private TextBox ContactPersonTextBox;
        private Label label8;
        private TextBox PhoneTextBox;
        private Label label9;
        private TextBox EmailAddressTextBox;
        private Label label10;
        private CheckBox SalesTaxCheckBox;
        private Label label11;
        private TextBox TaxIDTextBox;
        private Button AddButton;
        private Button UpdateButton;
        private TextBox Address2TextBox;
        private ComboBox StateComboBox;
        private Label label12;
        private Button InActiveButton;
        private Button ShowAllCustomersButton;
    }
}