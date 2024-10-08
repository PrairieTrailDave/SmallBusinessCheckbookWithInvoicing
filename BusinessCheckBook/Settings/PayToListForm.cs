//**********************************************************************
//
//          Copyright © 2023 Prairie Trail Software, Inc.
//
//**********************************************************************

using BusinessCheckBook.DataStore;


namespace BusinessCheckBook.Settings
{
    public partial class PayToListForm : Form
    {
        // passed down data store

        MyCheckbook ActiveBook = new();

        // internal storage

        PayTo TPayTo = new();

        public PayToListForm()
        {
            InitializeComponent();
            StateComboBox.DataSource = Address.GetStateAbreviations();
        }
        internal void SetUp(MyCheckbook PassedActiveBook)
        {
            ActiveBook = PassedActiveBook;
            PayToList TList = ActiveBook.ToPayTo;
            PayToDataGridView.DataSource = TList.GetCurrentList();
        }
        internal void SetBusinessName(string NewBusinessName)
        {
            BusinessNameTextBox.Text = NewBusinessName;
        }


        private void ClearButton_Click(object sender, EventArgs e)
        {
            AccountNameTextBox.Text = "";
            BusinessNameTextBox.Text = "";
            PrintAsTextBox.Text = "";
            AddressTextBox.Text = "";
            Address2TextBox.Text = "";
            CityTextBox.Text = "";
            StateComboBox.Text = "";
            ZipCodeTextBox.Text = "";
            CountryTextBox.Text = "";
            ContactPersonTextBox.Text = "";
            PhoneTextBox.Text = "";
            EmailAddressTextBox.Text = "";
            TaxableCheckBox.Checked = false;
            Send1099CheckBox.Checked = false;
            TaxIDTextBox.Text = "";
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (ValidatedEntries())
            {
                TPayTo = new();
                TPayTo.AccountName = AccountNameTextBox.Text;
                TPayTo.BusinessName = BusinessNameTextBox.Text;
                TPayTo.PrintAs = PrintAsTextBox.Text;
                TPayTo.Address = AddressTextBox.Text;
                TPayTo.Address2 = Address2TextBox.Text;
                TPayTo.City = CityTextBox.Text;
                TPayTo.State = StateComboBox.Text;
                TPayTo.ZipCode = ZipCodeTextBox.Text;
                TPayTo.Country = CountryTextBox.Text;
                TPayTo.ContactPerson = ContactPersonTextBox.Text;
                TPayTo.Phone = PhoneTextBox.Text;
                TPayTo.EmailAddress = EmailAddressTextBox.Text;
                TPayTo.Taxable = TaxableCheckBox.Checked;
                TPayTo.Send1099 = Send1099CheckBox.Checked;
                TPayTo.TaxID = TaxIDTextBox.Text;
                ActiveBook.ToPayTo.AddPayTo(TPayTo);
            }
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            if (TPayTo != null)
            {
                if (ValidatedEntries())
                {
                    TPayTo.AccountName = AccountNameTextBox.Text;
                    TPayTo.BusinessName = BusinessNameTextBox.Text;
                    TPayTo.PrintAs = PrintAsTextBox.Text;
                    TPayTo.Address = AddressTextBox.Text;
                    TPayTo.Address2 = Address2TextBox.Text;
                    TPayTo.City = CityTextBox.Text;
                    TPayTo.State = StateComboBox.Text;
                    TPayTo.ZipCode = ZipCodeTextBox.Text;
                    TPayTo.ContactPerson = ContactPersonTextBox.Text;
                    TPayTo.Phone = PhoneTextBox.Text;
                    TPayTo.EmailAddress = EmailAddressTextBox.Text;
                    TPayTo.Taxable = TaxableCheckBox.Checked;
                    TPayTo.Send1099 = Send1099CheckBox.Checked;
                    TPayTo.TaxID = TaxIDTextBox.Text;
                    ActiveBook.ToPayTo.HasChanged();
                }
            }
        }

        private void DisableButton_Click(object sender, EventArgs e)
        {
            if (TPayTo != null)
            {
                TPayTo.IsActive = false;
            }
        }

        private void DoneButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void PayToDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int Row = e.RowIndex;
            TPayTo = ActiveBook.ToPayTo.GetThisPayTo(Row);
            if (TPayTo != null)
            {
                AccountNameTextBox.Text = TPayTo.AccountName;
                BusinessNameTextBox.Text = TPayTo.BusinessName;
                PrintAsTextBox.Text = TPayTo.PrintAs;
                AddressTextBox.Text = TPayTo.Address;
                Address2TextBox.Text = TPayTo.Address2;
                CityTextBox.Text = TPayTo.City;
                StateComboBox.Text = TPayTo.State;
                ZipCodeTextBox.Text = TPayTo.ZipCode;
                CountryTextBox.Text = TPayTo.Country;
                ContactPersonTextBox.Text = TPayTo.ContactPerson;
                PhoneTextBox.Text = TPayTo.Phone;
                EmailAddressTextBox.Text = TPayTo.EmailAddress;
                TaxableCheckBox.Checked = TPayTo.Taxable;
                Send1099CheckBox.Checked = TPayTo.Send1099;
                TaxIDTextBox.Text = TPayTo.TaxID;
            }
        }

        private bool ValidatedEntries()
        {
            PayToList TPayToList = ActiveBook.ToPayTo;
            if (!TPayToList.ValidateAccountName(AccountNameTextBox.Text))
            {
                MessageBox.Show("Invalid Account Name");
                return false;
            }
            if (!TPayToList.ValidateBusinessName(BusinessNameTextBox.Text))
            {
                MessageBox.Show("Invalid Business Name");
                return false;
            }
            if (!TPayToList.ValidatePrintAs(PrintAsTextBox.Text))
            {
                MessageBox.Show("Invalid Print As");
                return false;
            }
            if (!TPayToList.ValidateAddress(AddressTextBox.Text))
            {
                MessageBox.Show("Invalid Address");
                return false;
            }
            if (!TPayToList.ValidateAddress2(Address2TextBox.Text))
            {
                MessageBox.Show("Invalid Address 2");
                return false;
            }
            if (!TPayToList.ValidateCity(CityTextBox.Text))
            {
                MessageBox.Show("Invalid City");
                return false;
            }
            if (!TPayToList.ValidateState(StateComboBox.Text))
            {
                MessageBox.Show("Invalid State");
                return false;
            }
            if (!TPayToList.ValidateZipCode(ZipCodeTextBox.Text))
            {
                MessageBox.Show("Invalid ZipCode");
                return false;
            }
            if (!TPayToList.ValidateCountry(CountryTextBox.Text))
            {
                MessageBox.Show("Invalid Country");
                return false;
            }
            if (!TPayToList.ValidateContactPerson(ContactPersonTextBox.Text))
            {
                MessageBox.Show("Invalid Contact Person");
                return false;
            }
            if (!TPayToList.ValidatePhone(PhoneTextBox.Text))
            {
                MessageBox.Show("Invalid Phone");
                return false;
            }
            if (!TPayToList.ValidateEmailAddress(EmailAddressTextBox.Text))
            {
                MessageBox.Show("Invalid Email Address");
                return false;
            }
            if (!TPayToList.ValidateTaxID(TaxIDTextBox.Text))
            {
                MessageBox.Show("Invalid TaxID");
                return false;
            }
            return true;
        }
    }
}
