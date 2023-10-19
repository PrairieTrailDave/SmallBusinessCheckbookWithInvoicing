//**********************************************************************
//
//          Copyright © 2023 Prairie Trail Software, Inc.
//
//**********************************************************************

using BusinessCheckBook.DataStore;

namespace BusinessCheckBook.Settings
{
    public partial class CompanyInformationForm : Form
    {
        // passed down data store

        MyCheckbook ActiveBook = new();
        internal CompanyParameters CompanyInformation = new();

        public CompanyInformationForm()
        {
            InitializeComponent();
            CompanyStateComboBox.DataSource = DataStore.Address.GetStateAbreviations();
        }
        internal void SetUp(MyCheckbook PassedActiveBook)
        {
            ActiveBook = PassedActiveBook;
            CompanyInformation = ActiveBook.CompanyInformation;
        }

        private void CompanyInformationForm_Shown(object sender, EventArgs e)
        {
            CompanyNameTextBox.Text = CompanyInformation.GetParameter(CompanyParameters.ParmCompanyName.Name);
            CompanyAddressTextBox.Text = CompanyInformation.GetParameter(CompanyParameters.ParmCompanyAddr.Name);
            CompanyAddress2TextBox.Text = CompanyInformation.GetParameter(CompanyParameters.ParmCompanyAdr2.Name);
            CompanyCityTextBox.Text = CompanyInformation.GetParameter(CompanyParameters.ParmCompanyCity.Name);
            CompanyStateComboBox.Text = CompanyInformation.GetParameter(CompanyParameters.ParmCompanyState.Name);
            CompanyZipTextBox.Text = CompanyInformation.GetParameter(CompanyParameters.ParmCompanyZip.Name);
            CompanyPhoneTextBox.Text = CompanyInformation.GetParameter(CompanyParameters.ParmCompanyPhone.Name);
            CompanyEINTextBox.Text = CompanyInformation.GetParameter(CompanyParameters.ParmCompanyEIN.Name);
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (ValidData())
            {
                CompanyInformation.PutParameter(CompanyParameters.ParmCompanyName.Name, CompanyNameTextBox.Text);
                CompanyInformation.PutParameter(CompanyParameters.ParmCompanyAddr.Name, CompanyAddressTextBox.Text);
                CompanyInformation.PutParameter(CompanyParameters.ParmCompanyAdr2.Name, CompanyAddress2TextBox.Text);
                CompanyInformation.PutParameter(CompanyParameters.ParmCompanyCity.Name, CompanyCityTextBox.Text);
                CompanyInformation.PutParameter(CompanyParameters.ParmCompanyState.Name, CompanyStateComboBox.Text);
                CompanyInformation.PutParameter(CompanyParameters.ParmCompanyZip.Name, CompanyZipTextBox.Text);
                CompanyInformation.PutParameter(CompanyParameters.ParmCompanyPhone.Name, CompanyPhoneTextBox.Text);
                CompanyInformation.PutParameter(CompanyParameters.ParmCompanyEIN.Name, CompanyEINTextBox.Text);
                Close();
            }
        }

        private bool ValidData()
        {
            if (CompanyNameTextBox.Text.Length == 0)
            {
                MessageBox.Show("Please enter a company name");
                CompanyNameTextBox.Focus();
                return false;
            }
            if (CompanyAddressTextBox.Text.Length == 0)
            {
                MessageBox.Show("Please enter a company Address");
                CompanyAddressTextBox.Focus();
                return false;
            }
            if (CompanyCityTextBox.Text.Length == 0)
            {
                MessageBox.Show("Please enter a company City");
                CompanyCityTextBox.Focus();
                return false;
            }

            if (CompanyStateComboBox.Text.Length == 0)
            {
                MessageBox.Show("Please enter a company state");
                CompanyStateComboBox.Focus();
                return false;
            }
            if (CompanyZipTextBox.Text.Length == 0)
            {
                MessageBox.Show("Please enter a company Zip Code");
                CompanyZipTextBox.Focus();
                return false;
            }
            if (CompanyPhoneTextBox.Text.Length == 0)
            {
                MessageBox.Show("Please enter a company Phone");
                CompanyPhoneTextBox.Focus();
                return false;
            }
            if (CompanyEINTextBox.Text.Length == 0)
            {
                MessageBox.Show("Please enter a company EIN");
                CompanyEINTextBox.Focus();
                return false;
            }



            if (!CompanyInformation.ValidateCompanyName(CompanyNameTextBox.Text))
            {
                MessageBox.Show("Please enter a valid company name");
                CompanyNameTextBox.Focus();
                return false;
            }
            if (!CompanyInformation.ValidateCompanyAddress(CompanyAddressTextBox.Text))
            {
                MessageBox.Show("Please enter a valid company address");
                CompanyAddressTextBox.Focus();
                return false;
            }
            if (!CompanyInformation.ValidateCompanyAddress2(CompanyAddress2TextBox.Text))
            {
                MessageBox.Show("Please enter a valid company address2");
                CompanyAddress2TextBox.Focus();
                return false;
            }
            if (!CompanyInformation.ValidateCompanyCity(CompanyCityTextBox.Text))
            {
                MessageBox.Show("Please enter a valid company City");
                CompanyCityTextBox.Focus();
                return false;
            }
            if (!CompanyInformation.ValidateCompanyState(CompanyStateComboBox.Text))
            {
                MessageBox.Show("Please enter a valid company state");
                CompanyStateComboBox.Focus();
                return false;
            }
            if (!CompanyInformation.ValidateCompanyZipCode(CompanyZipTextBox.Text))
            {
                MessageBox.Show("Please enter a valid company zip code");
                CompanyZipTextBox.Focus();
                return false;
            }
            if (!CompanyInformation.ValidateCompanyPhone(CompanyPhoneTextBox.Text))
            {
                MessageBox.Show("Please enter a valid company phone");
                CompanyPhoneTextBox.Focus();
                return false;
            }
            if (!CompanyInformation.ValidateCompanyEIN(CompanyEINTextBox.Text))
            {
                MessageBox.Show("Please enter a valid company EIN");
                CompanyEINTextBox.Focus();
                return false;
            }
            return true;
        }
    }
}
