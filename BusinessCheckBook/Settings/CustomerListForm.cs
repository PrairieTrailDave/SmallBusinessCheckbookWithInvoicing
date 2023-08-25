using BusinessCheckBook.DataStore;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            CustDataGridView.AutoResizeColumns();
        }

        internal void PutPassedCustomerOnScreen (Customer TCust)
        {
            DisplayThisCustomer(TCust);
        }

        // Grid click
        private void CustDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int RowNum = e.RowIndex;
            if (RowNum > -1)
            {
                TCustomer = ActiveBook.Customers.GetThisCustomer(RowNum);
                DisplayThisCustomer(TCustomer);
            }
        }


        // button clicks

        private void ClearButton_Click(object sender, EventArgs e)
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

        private void DisableButton_Click(object sender, EventArgs e)
        {
            TCustomer.IsActive = false;
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
            }
        }





        // Support Routines



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
