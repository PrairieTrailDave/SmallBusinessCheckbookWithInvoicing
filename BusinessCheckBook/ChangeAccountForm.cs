//**********************************************************************
//
//          Copyright © 2023 Prairie Trail Software, Inc.
//
//**********************************************************************

using BusinessCheckBook.DataStore;


namespace BusinessCheckBook
{
    public partial class ChangeAccountForm : Form
    {
        List<Account> CurrentAccountList = new();
        public string ToChangeTo = string.Empty;

        public ChangeAccountForm()
        {
            InitializeComponent();
        }

        public void Setup(List<Account> currentAccountList, string AccountNotFound)
        {
            CurrentAccountList = currentAccountList;;
            AccountsListBox.Items.Clear();
            foreach (Account Taccount in CurrentAccountList)
            {
                string whatToShow = Taccount.Name;
                if (Taccount.SubAccountOf.Length > 0) 
                {
                    whatToShow = Taccount.SubAccountOf + ":" + Taccount.Name; 
                }
                AccountsListBox.Items.Add(whatToShow);
            }
            AccountNotFoundTextBox.Text = AccountNotFound;
        }


        private void AccountsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToChangeTo = AccountsListBox.Text;
            if (ToChangeTo.IndexOf(':') > -1)
            {
                ToChangeTo = ToChangeTo[(ToChangeTo.IndexOf(":")+1)..];
            }

            ChangeToTextBox.Text = ToChangeTo;
        }

        private void DoneButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
