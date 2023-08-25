using BusinessCheckBook.DataStore;
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
            CompanyNameTextBox.Text = CompanyInformation.GetParameter(CompanyParameters.ParmCompanyName);
            CompanyAddressTextBox.Text = CompanyInformation.GetParameter(CompanyParameters.ParmCompanyAddr);
            CompanyAddress2TextBox.Text = CompanyInformation.GetParameter(CompanyParameters.ParmCompanyAdr2);
            CompanyCityTextBox.Text = CompanyInformation.GetParameter(CompanyParameters.ParmCompanyCity);
            CompanyStateComboBox.Text = CompanyInformation.GetParameter(CompanyParameters.ParmCompanyState);
            CompanyZipTextBox.Text = CompanyInformation.GetParameter(CompanyParameters.ParmCompanyZip);
            CompanyPhoneTextBox.Text = CompanyInformation.GetParameter(CompanyParameters.ParmCompanyPhone);
            CompanyEINTextBox.Text = CompanyInformation.GetParameter(CompanyParameters.ParmCompanyEIN);
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (ValidData())
            {
                CompanyInformation.PutParameter(CompanyParameters.ParmCompanyName, CompanyNameTextBox.Text);
                CompanyInformation.PutParameter(CompanyParameters.ParmCompanyAddr, CompanyAddressTextBox.Text);
                CompanyInformation.PutParameter(CompanyParameters.ParmCompanyAdr2, CompanyAddress2TextBox.Text);
                CompanyInformation.PutParameter(CompanyParameters.ParmCompanyCity, CompanyCityTextBox.Text);
                CompanyInformation.PutParameter(CompanyParameters.ParmCompanyState, CompanyStateComboBox.Text);
                CompanyInformation.PutParameter(CompanyParameters.ParmCompanyZip, CompanyZipTextBox.Text);
                CompanyInformation.PutParameter(CompanyParameters.ParmCompanyPhone, CompanyPhoneTextBox.Text);
                CompanyInformation.PutParameter(CompanyParameters.ParmCompanyEIN, CompanyEINTextBox.Text);
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
            if (!CompanyInformation.ValidateCompanyAddress(CompanyAddress2TextBox.Text))
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
