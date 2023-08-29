//**********************************************************************
//
//          Copyright © 2023 Prairie Trail Software, Inc.
//
//**********************************************************************


namespace BusinessCheckBook
{
    public partial class InitialBalanceForm : Form
    {
        public decimal InitialBalance { get; set; }
        public int FirstCheckNumber { get; set; }
        public int FirstInvoiceNumber { get; set; }

        public InitialBalanceForm()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            decimal tryBalance;
            if (!decimal.TryParse(StartingBalanceTextBox.Text, out tryBalance) || (StartingBalanceTextBox.Text.Length == 0))
            {
                MessageBox.Show("Please enter a valid amount");
                StartingBalanceTextBox.Focus();
                return;
            }
            InitialBalance = tryBalance;

            int tryNumber;
            if (FirstCheckNumberTextBox.Text.Length == 0) { MessageBox.Show("Please enter a starting check number"); FirstCheckNumberTextBox.Focus(); return; }
            if (!Int32.TryParse(FirstCheckNumberTextBox.Text, out tryNumber))
            {
                MessageBox.Show("Please enter a valid starting check number");
                FirstCheckNumberTextBox.Focus();
                return;
            }
            FirstCheckNumber = tryNumber;

            if (FirstInvoiceNumberTextBox.Text.Length == 0) { MessageBox.Show("Please enter a starting invoice number"); FirstInvoiceNumberTextBox.Focus(); return; }
            if (!Int32.TryParse(FirstInvoiceNumberTextBox.Text, out tryNumber))
            {
                MessageBox.Show("Please enter a valid starting invoice number");
                FirstInvoiceNumberTextBox.Focus();
                return;
            }
            FirstInvoiceNumber = tryNumber;
            Close();
        }
    }
}
