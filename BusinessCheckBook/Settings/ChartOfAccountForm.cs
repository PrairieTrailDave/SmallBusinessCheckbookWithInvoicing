//**********************************************************************
//
//          Copyright © 2023 Prairie Trail Software, Inc.
//
//**********************************************************************

using System.Data;
using BusinessCheckBook.DataStore;

namespace BusinessCheckBook.Settings
{
    public partial class ChartOfAccountForm : Form
    {
        public ChartOfAccounts CurrentAccounts = new();
        Account ThisAccount = new();

        public ChartOfAccountForm()
        {
            InitializeComponent();
            AddButton.Enabled = false;
            SaveChangesButton.Enabled = false;
        }
        public void Setup(MyCheckbook activeBook)
        {
            CurrentAccounts = activeBook.CurrentAccounts;
            CurrentAccountsDataGridView.DataSource = CurrentAccounts.GetCurrentList();
            CurrentAccountsDataGridView.Columns[0].Width = 30;// AutoResizeColumn(1);
            CurrentAccountsDataGridView.AutoResizeColumn(1);
            CurrentAccountsDataGridView.AutoResizeColumn(2);
            CurrentAccountsDataGridView.AutoResizeColumn(3);
            CurrentAccountsDataGridView.Columns[5].Visible = false;
            SetMainAccountsDropDown();

            Fed1120MappingComboBox.DataSource = Fed1120.F1120Fields;
        }

        private void CurrentAccountsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int CurrentRow = e.RowIndex;

            if (CurrentRow > -1)
            {
                ThisAccount = CurrentAccounts.GetThisAccount(CurrentRow);
                AccountNameTextBox.Text = ThisAccount.Name;
                AccountDescriptionTextBox.Text = ThisAccount.Description;
                AccountTypeComboBox.Text = ThisAccount.WhatType.ToString();
                SubAccountComboBox.Text = ThisAccount.SubAccountOf;
                Fed1120MappingComboBox.Text = ThisAccount.Fed1120Mapping;
                SaveChangesButton.Enabled = true;
                AddButton.Enabled = false;
                if (ThisAccount.IsActive) { DisableButton.Enabled = true; EnableButton.Enabled = false; }
                else { DisableButton.Enabled = false; EnableButton.Enabled = true; }
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            AccountNameTextBox.Text = "";
            AccountDescriptionTextBox.Text = "";
            AccountTypeComboBox.Text = "";
            Fed1120MappingComboBox.Text = "";
            SaveChangesButton.Enabled = false;
            AddButton.Enabled = true;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            // validate the entries first

            if (ValidAccountEntries())
            {
                ThisAccount = new();
                ThisAccount.Name = AccountNameTextBox.Text;
                ThisAccount.Description = AccountDescriptionTextBox.Text;
                ThisAccount.WhatType = Account.ParseType(AccountTypeComboBox.Text);
                ThisAccount.SubAccountOf = SubAccountComboBox.Text;
                ThisAccount.Fed1120Mapping = Fed1120MappingComboBox.Text;

                CurrentAccounts.InsertAccount(ThisAccount);
                CurrentAccountsDataGridView.DataSource = CurrentAccounts.GetCurrentList();
                CurrentAccountsDataGridView.AutoResizeColumn(0);
                CurrentAccountsDataGridView.Columns[5].Visible = false;
                SetMainAccountsDropDown();

                SaveChangesButton.Enabled = true;
                AddButton.Enabled = false;
            }
        }

        private void SaveChangesButton_Click(object sender, EventArgs e)
        {
            if (ValidAccountEntries())
            {
                ThisAccount.Name = AccountNameTextBox.Text;
                ThisAccount.Description = AccountDescriptionTextBox.Text;
                ThisAccount.WhatType = Account.ParseType(AccountTypeComboBox.Text);
                ThisAccount.SubAccountOf = SubAccountComboBox.Text;
                ThisAccount.Fed1120Mapping = Fed1120MappingComboBox.Text;
            }
        }

        private void DisableButton_Click(object sender, EventArgs e)
        {
            ThisAccount.IsActive = false;
        }

        private void EnableButton_Click(object sender, EventArgs e)
        {
            ThisAccount.IsActive = true;
        }


        private void SetMainAccountsDropDown()
        {
            List<string> MainAccounts = new();
            MainAccounts = (from acc in CurrentAccounts.GetCurrentList()
                            where acc.WhatType != Account.Type.SubAccount
                            select acc.Name).ToList();
            MainAccounts.Insert(0, "");
            SubAccountComboBox.Items.Clear();
            foreach (string account in MainAccounts) { SubAccountComboBox.Items.Add(account); }
        }

        private bool ValidAccountEntries()
        {
            return CurrentAccounts.ValidAccountFields(
                AccountTypeComboBox.Text,
                AccountNameTextBox.Text,
                AccountDescriptionTextBox.Text,
                SubAccountComboBox.Text,
                Fed1120MappingComboBox.Text
                );
        }


    }
}
