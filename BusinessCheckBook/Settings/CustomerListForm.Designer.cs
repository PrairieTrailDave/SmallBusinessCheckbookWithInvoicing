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
            DisableButton = new Button();
            Address2TextBox = new TextBox();
            StateComboBox = new ComboBox();
            label12 = new Label();
            ((System.ComponentModel.ISupportInitialize)CustDataGridView).BeginInit();
            SuspendLayout();
            // 
            // CustDataGridView
            // 
            CustDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            CustDataGridView.Location = new Point(24, 55);
            CustDataGridView.Name = "CustDataGridView";
            CustDataGridView.RowTemplate.Height = 25;
            CustDataGridView.Size = new Size(664, 572);
            CustDataGridView.TabIndex = 0;
            CustDataGridView.CellContentClick += CustDataGridView_CellContentClick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(179, 23);
            label1.Name = "label1";
            label1.Size = new Size(135, 15);
            label1.TabIndex = 1;
            label1.Text = "List of Active Customers";
            // 
            // ClearButton
            // 
            ClearButton.Location = new Point(802, 558);
            ClearButton.Name = "ClearButton";
            ClearButton.Size = new Size(75, 23);
            ClearButton.TabIndex = 2;
            ClearButton.Text = "Clear";
            ClearButton.UseVisualStyleBackColor = true;
            ClearButton.Click += ClearButton_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(749, 48);
            label2.Name = "label2";
            label2.Size = new Size(76, 15);
            label2.TabIndex = 3;
            label2.Text = "Customer ID:";
            // 
            // CustomerIDTextBox
            // 
            CustomerIDTextBox.Location = new Point(951, 45);
            CustomerIDTextBox.Name = "CustomerIDTextBox";
            CustomerIDTextBox.Size = new Size(403, 23);
            CustomerIDTextBox.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(749, 73);
            label3.Name = "label3";
            label3.Size = new Size(90, 15);
            label3.TabIndex = 5;
            label3.Text = "Account Name:";
            // 
            // AccountNameTextBox
            // 
            AccountNameTextBox.Location = new Point(951, 70);
            AccountNameTextBox.Name = "AccountNameTextBox";
            AccountNameTextBox.Size = new Size(403, 23);
            AccountNameTextBox.TabIndex = 6;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(749, 98);
            label4.Name = "label4";
            label4.Size = new Size(90, 15);
            label4.TabIndex = 7;
            label4.Text = "Business Name:";
            // 
            // BusinessNameTextBox
            // 
            BusinessNameTextBox.Location = new Point(951, 95);
            BusinessNameTextBox.Name = "BusinessNameTextBox";
            BusinessNameTextBox.Size = new Size(403, 23);
            BusinessNameTextBox.TabIndex = 8;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(749, 122);
            label5.Name = "label5";
            label5.Size = new Size(52, 15);
            label5.TabIndex = 9;
            label5.Text = "Address:";
            // 
            // AddressTextBox
            // 
            AddressTextBox.Location = new Point(951, 119);
            AddressTextBox.Name = "AddressTextBox";
            AddressTextBox.Size = new Size(403, 23);
            AddressTextBox.TabIndex = 10;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(749, 177);
            label6.Name = "label6";
            label6.Size = new Size(80, 15);
            label6.TabIndex = 11;
            label6.Text = "City State Zip:";
            // 
            // CityTextBox
            // 
            CityTextBox.Location = new Point(951, 174);
            CityTextBox.Name = "CityTextBox";
            CityTextBox.Size = new Size(272, 23);
            CityTextBox.TabIndex = 12;
            // 
            // ZipCodeTextBox
            // 
            ZipCodeTextBox.Location = new Point(1276, 174);
            ZipCodeTextBox.Name = "ZipCodeTextBox";
            ZipCodeTextBox.Size = new Size(87, 23);
            ZipCodeTextBox.TabIndex = 14;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(748, 226);
            label7.Name = "label7";
            label7.Size = new Size(91, 15);
            label7.TabIndex = 15;
            label7.Text = "Contact Person:";
            // 
            // ContactPersonTextBox
            // 
            ContactPersonTextBox.Location = new Point(951, 226);
            ContactPersonTextBox.Name = "ContactPersonTextBox";
            ContactPersonTextBox.Size = new Size(412, 23);
            ContactPersonTextBox.TabIndex = 16;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(749, 269);
            label8.Name = "label8";
            label8.Size = new Size(44, 15);
            label8.TabIndex = 17;
            label8.Text = "Phone:";
            // 
            // PhoneTextBox
            // 
            PhoneTextBox.Location = new Point(951, 266);
            PhoneTextBox.Name = "PhoneTextBox";
            PhoneTextBox.Size = new Size(412, 23);
            PhoneTextBox.TabIndex = 18;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(749, 305);
            label9.Name = "label9";
            label9.Size = new Size(84, 15);
            label9.TabIndex = 19;
            label9.Text = "Email Address:";
            // 
            // EmailAddressTextBox
            // 
            EmailAddressTextBox.Location = new Point(951, 302);
            EmailAddressTextBox.Name = "EmailAddressTextBox";
            EmailAddressTextBox.Size = new Size(412, 23);
            EmailAddressTextBox.TabIndex = 20;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(749, 354);
            label10.Name = "label10";
            label10.Size = new Size(99, 15);
            label10.TabIndex = 21;
            label10.Text = "Charge Sales Tax?";
            // 
            // SalesTaxCheckBox
            // 
            SalesTaxCheckBox.AutoSize = true;
            SalesTaxCheckBox.Location = new Point(951, 350);
            SalesTaxCheckBox.Name = "SalesTaxCheckBox";
            SalesTaxCheckBox.Size = new Size(72, 19);
            SalesTaxCheckBox.TabIndex = 22;
            SalesTaxCheckBox.Text = "Sales Tax";
            SalesTaxCheckBox.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(748, 384);
            label11.Name = "label11";
            label11.Size = new Size(156, 15);
            label11.TabIndex = 23;
            label11.Text = "Customer's Tax ID (EIN/SSN)";
            // 
            // TaxIDTextBox
            // 
            TaxIDTextBox.Location = new Point(951, 381);
            TaxIDTextBox.Name = "TaxIDTextBox";
            TaxIDTextBox.Size = new Size(154, 23);
            TaxIDTextBox.TabIndex = 24;
            // 
            // AddButton
            // 
            AddButton.Location = new Point(974, 558);
            AddButton.Name = "AddButton";
            AddButton.Size = new Size(75, 23);
            AddButton.TabIndex = 25;
            AddButton.Text = "Add";
            AddButton.UseVisualStyleBackColor = true;
            AddButton.Click += AddButton_Click;
            // 
            // UpdateButton
            // 
            UpdateButton.Location = new Point(1182, 558);
            UpdateButton.Name = "UpdateButton";
            UpdateButton.Size = new Size(75, 23);
            UpdateButton.TabIndex = 26;
            UpdateButton.Text = "Update";
            UpdateButton.UseVisualStyleBackColor = true;
            UpdateButton.Click += UpdateButton_Click;
            // 
            // DisableButton
            // 
            DisableButton.Location = new Point(1345, 558);
            DisableButton.Name = "DisableButton";
            DisableButton.Size = new Size(75, 23);
            DisableButton.TabIndex = 27;
            DisableButton.Text = "Disable";
            DisableButton.UseVisualStyleBackColor = true;
            DisableButton.Click += DisableButton_Click;
            // 
            // Address2TextBox
            // 
            Address2TextBox.Location = new Point(951, 145);
            Address2TextBox.Name = "Address2TextBox";
            Address2TextBox.Size = new Size(403, 23);
            Address2TextBox.TabIndex = 28;
            // 
            // StateComboBox
            // 
            StateComboBox.FormattingEnabled = true;
            StateComboBox.Location = new Point(1229, 174);
            StateComboBox.Name = "StateComboBox";
            StateComboBox.Size = new Size(41, 23);
            StateComboBox.TabIndex = 29;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(24, 8);
            label12.Name = "label12";
            label12.Size = new Size(218, 15);
            label12.TabIndex = 30;
            label12.Text = "Copyright 2023 Prarie Trail Software, Inc.";
            label12.Visible = false;
            // 
            // CustomerListForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1451, 639);
            Controls.Add(label12);
            Controls.Add(StateComboBox);
            Controls.Add(Address2TextBox);
            Controls.Add(DisableButton);
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
            Name = "CustomerListForm";
            Text = "CustomerListForm";
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
        private Button DisableButton;
        private TextBox Address2TextBox;
        private ComboBox StateComboBox;
        private Label label12;
    }
}