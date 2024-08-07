﻿//**********************************************************************
//
//          Copyright © 2023 Prairie Trail Software, Inc.
//
//**********************************************************************

using BusinessCheckBook.DataStore;

namespace BusinessCheckBook.Settings
{
    public partial class CustomerListForm : Form
    {
        public class CustList
        {
            public string CustomerIdentifier { get; set; } = string.Empty;
            public string AccountName { get; set; } = string.Empty;
            public string BusinessName { get; set; } = string.Empty;
            public string City { get; set; } = string.Empty;
        }


        // passed down data store

        MyCheckbook ActiveBook = new();

        // internal storage

        Customer TCustomer = new();

        public CustomerListForm()
        {
            InitializeComponent();
            StateComboBox.DataSource = Address.GetStateAbreviations();
        }
        internal void SetUp(MyCheckbook PassedActiveBook)
        {
            ActiveBook = PassedActiveBook;
        }

        private void CustomerListForm_Shown(object sender, EventArgs e)
        {
            ShowCurrentCustomerList();
        }


        internal void PutPassedCustomerOnScreen(Customer TCust)
        {
            DisplayThisCustomer(TCust);
        }

        // Grid click
        private void CustDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int RowNum = e.RowIndex;
            if (RowNum > -1)
            {
                string customerID = (string)CustDataGridView.Rows[RowNum].Cells[0].Value;
                TCustomer = ActiveBook.Customers.GetCustomerByID(customerID)!;
                if (TCustomer != null)
                    DisplayThisCustomer(TCustomer);
                else
                    ClearCustomerDisplay();
            }
        }


        // button clicks

        private void ShowAllCustomersButton_Click(object sender, EventArgs e)
        {
            ShowAllCurrentCustomerList();
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            ClearCustomerDisplay();
            TCustomer = new();
        }

        private void InActiveButton_Click(object sender, EventArgs e)
        {
            if (TCustomer.IsActive) TCustomer.IsActive = false;
            else TCustomer.IsActive = true;
            ActiveBook.Customers.HasChanged();
            ShowCurrentCustomerList();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (ValidateCustomerEntry())
            {
                TCustomer = new()
                {
                    CustomerIdentifier = CustomerIDTextBox.Text,
                    AccountName = AccountNameTextBox.Text,
                    BusinessName = BusinessNameTextBox.Text,
                    Address = AddressTextBox.Text,
                    Address2 = Address2TextBox.Text,
                    City = CityTextBox.Text,
                    State = StateComboBox.Text,
                    ZipCode = ZipCodeTextBox.Text,
                    ContactPerson = ContactPersonTextBox.Text,
                    Phone = PhoneTextBox.Text,
                    EmailAddress = EmailAddressTextBox.Text,
                    Taxable = SalesTaxCheckBox.Checked,
                    TaxID = TaxIDTextBox.Text
                };
                ActiveBook.Customers.AddCustomer(TCustomer);
                ShowCurrentCustomerList();
            }
        }



        private void UpdateButton_Click(object sender, EventArgs e)
        {
            if (ValidateCustomerEntry())
            {
                TCustomer.CustomerIdentifier = CustomerIDTextBox.Text;
                TCustomer.AccountName = AccountNameTextBox.Text;
                TCustomer.BusinessName = BusinessNameTextBox.Text;
                TCustomer.Address = AddressTextBox.Text;
                TCustomer.Address2 = Address2TextBox.Text;
                TCustomer.City = CityTextBox.Text;
                TCustomer.State = StateComboBox.Text;
                TCustomer.ZipCode = ZipCodeTextBox.Text;
                TCustomer.ContactPerson = ContactPersonTextBox.Text;
                TCustomer.Phone = PhoneTextBox.Text;
                TCustomer.EmailAddress = EmailAddressTextBox.Text;
                TCustomer.Taxable = SalesTaxCheckBox.Checked;
                TCustomer.TaxID = TaxIDTextBox.Text;
                ActiveBook.Customers.HasChanged();
            }
        }



        // Field edit routines

        private void CustomerIDTextBox_Leave(object sender, EventArgs e)
        {
            if (TCustomer != null)
            {
                if (TCustomer.CustomerIdentifier.Length > 0)
                {
                    if (TCustomer.CustomerIdentifier != CustomerIDTextBox.Text)
                    {
                        if (MessageBox.Show("This may cause problems matching up data. Are you sure?",
                            "Changing This?", MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            CustomerIDTextBox.Text = TCustomer.CustomerIdentifier;
                        }
                    }
                }
            }
        }



        // Support Routines

        private void ClearCustomerDisplay()
        {
            CustomerIDTextBox.Text = "";
            AccountNameTextBox.Text = "";
            BusinessNameTextBox.Text = "";
            AddressTextBox.Text = "";
            Address2TextBox.Text = "";
            CityTextBox.Text = "";
            StateComboBox.Text = "";
            ZipCodeTextBox.Text = "";
            ContactPersonTextBox.Text = "";
            PhoneTextBox.Text = "";
            EmailAddressTextBox.Text = "";
            SalesTaxCheckBox.Checked = false;
            TaxIDTextBox.Text = "";

        }
        private void ShowCurrentCustomerList()
        {
            CustomerList Customers = ActiveBook.Customers;
            List<CustList> DisplayList = new();
            foreach (var Customer in Customers.GetCurrentList())
            {
                if (Customer.IsActive)
                {
                    CustList DisplayCust = new()
                    {
                        CustomerIdentifier = Customer.CustomerIdentifier,
                        AccountName = Customer.AccountName,
                        BusinessName = Customer.BusinessName,
                        City = Customer.City
                    };
                    DisplayList.Add(DisplayCust);
                }
            }
            CustDataGridView.DataSource = DisplayList;
            CustDataGridView.AutoResizeColumn(1);
            CustDataGridView.AutoResizeColumn(2);
        }

        private void ShowAllCurrentCustomerList()
        {
            CustomerList Customers = ActiveBook.Customers;
            List<CustList> DisplayList = new();
            foreach (var Customer in Customers.GetCurrentList())
            {
                CustList DisplayCust = new()
                {
                    CustomerIdentifier = Customer.CustomerIdentifier,
                    AccountName = Customer.AccountName,
                    BusinessName = Customer.BusinessName,
                    City = Customer.City
                };
                DisplayList.Add(DisplayCust);
            }
            CustDataGridView.DataSource = DisplayList;
            CustDataGridView.AutoResizeColumn(1);
            CustDataGridView.AutoResizeColumn(2);
        }

        private void DisplayThisCustomer(Customer TCust)
        {
            CustomerIDTextBox.Text = TCust.CustomerIdentifier;
            AccountNameTextBox.Text = TCust.AccountName;
            BusinessNameTextBox.Text = TCust.BusinessName;
            AddressTextBox.Text = TCust.Address;
            Address2TextBox.Text = TCust.Address2;
            CityTextBox.Text = TCust.City;
            StateComboBox.Text = TCust.State;
            ZipCodeTextBox.Text = TCust.ZipCode;
            ContactPersonTextBox.Text = TCust.ContactPerson;
            PhoneTextBox.Text = TCust.Phone;
            EmailAddressTextBox.Text = TCust.EmailAddress;
            SalesTaxCheckBox.Checked = TCust.Taxable;
            TaxIDTextBox.Text = TCust.TaxID;
            if (TCust.IsActive)
            {
                InActiveButton.Text = "Make InActive";
            }
            else
            {
                InActiveButton.Text = "Make Active";
            }
        }

        private bool ValidateCustomerEntry()
        {
            if (!Customer.ValidateCustomerID(CustomerIDTextBox.Text, ActiveBook.Customers.CustomerListFormat))
            {
                MessageBox.Show("Invalid Customer ID");
                return false;
            }
            if (!Customer.ValidateAccountName(AccountNameTextBox.Text, ActiveBook.Customers.CustomerListFormat))
            {
                MessageBox.Show("Invalid Account Name");
                return false;
            }
            if (!Customer.ValidateBusinessName(BusinessNameTextBox.Text, ActiveBook.Customers.CustomerListFormat))
            {
                MessageBox.Show("Invalid Business Name");
                return false;
            }
            if (!Customer.ValidateAddress(AddressTextBox.Text, ActiveBook.Customers.CustomerListFormat))
            {
                MessageBox.Show("Invalid Address");
                return false;
            }
            if (!Customer.ValidateAddress(Address2TextBox.Text, ActiveBook.Customers.CustomerListFormat))
            {
                MessageBox.Show("Invalid Address2");
                return false;
            }
            if (!Customer.ValidateCity(CityTextBox.Text, ActiveBook.Customers.CustomerListFormat))
            {
                MessageBox.Show("Invalid City");
                return false;
            }
            if (!Customer.ValidateState(StateComboBox.Text, ActiveBook.Customers.CustomerListFormat))
            {
                MessageBox.Show("Invalid State");
                return false;
            }
            if (!Customer.ValidateZipCode(ZipCodeTextBox.Text, ActiveBook.Customers.CustomerListFormat))
            {
                MessageBox.Show("Invalid ZipCode");
                return false;
            }
            if (!Customer.ValidateContactPerson(ContactPersonTextBox.Text, ActiveBook.Customers.CustomerListFormat))
            {
                MessageBox.Show("Invalid ContactPerson");
                return false;
            }
            if (!Customer.ValidatePhone(PhoneTextBox.Text, ActiveBook.Customers.CustomerListFormat))
            {
                MessageBox.Show("Invalid Phone");
                return false;
            }
            if (!Customer.ValidateEmailAddress(EmailAddressTextBox.Text, ActiveBook.Customers.CustomerListFormat))
            {
                MessageBox.Show("Invalid EmailAddress");
                return false;
            }
            if (!Customer.ValidateTaxID(TaxIDTextBox.Text, ActiveBook.Customers.CustomerListFormat))
            {
                MessageBox.Show("Invalid TaxID");
                return false;
            }

            return true;
        }

    }

}
