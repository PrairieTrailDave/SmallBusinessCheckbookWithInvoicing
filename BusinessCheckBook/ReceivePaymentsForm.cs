//**********************************************************************
//
//          Copyright © 2023 Prairie Trail Software, Inc.
//
//**********************************************************************

using BusinessCheckBook.DataStore;
using BusinessCheckBook.Extensions;

namespace BusinessCheckBook
{
    public partial class ReceivePaymentsForm : Form
    {

        public class DisplayInvoice
        {
            public bool Paid { get; set; }
            public string InvoiceNumber { get; set; } = string.Empty;
            public string DueDate { get; set; } = string.Empty;
            public decimal Total { get; set; } = decimal.Zero;
            public decimal AmountPaid { get; set; } = decimal.Zero;

        }


        // variables filled from the main window
        public MyCheckbook ActiveBook { get; set; } = new();


        // local variables

        string CurrentPayer = string.Empty;
        decimal AmountPaid;
        List<Invoice> OutstandingInvoices = new();
        List<DisplayInvoice> DispInvoices = new();

        public ReceivePaymentsForm()
        {
            InitializeComponent();
        }
        internal void SetUp(MyCheckbook activeBook)
        {
            ActiveBook = activeBook;
            PostingDateTimePicker.Text = DateTime.Now.ToShortDateString();
            CustomerComboBox.Items.Clear();
            List<Customer> customerList = ActiveBook.Customers.GetCurrentList();
            foreach (Customer customer in customerList)
            {
                CustomerComboBox.Items.Add(new DropDownItem(customer.AccountName, customer.CustomerIdentifier));
            }
        }

        private void CustomerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // find that customer
            if (CustomerComboBox.SelectedItem == null) return;

            DropDownItem SelectedCustomer = (DropDownItem)CustomerComboBox.SelectedItem;
            CurrentPayer = SelectedCustomer.Value;

            // build the current balance from all invoices minus all payments

            OutstandingInvoices = ActiveBook.CurrentInvoices.GetOutstandingInvoicesForACustomer(CurrentPayer);
            decimal CurrentBalance = 0.00M;
            foreach (Invoice inv in OutstandingInvoices)
            {
                CurrentBalance += inv.Total - inv.AmountPaid;
            }
            CurrnetBalanceTextBox.Text = CurrentBalance.ToString();

            // show outstanding invoices
            DispInvoices = new();
            foreach (Invoice inv in OutstandingInvoices)
            {
                DispInvoices.Add(new DisplayInvoice()
                {
                    Paid = inv.Paid,
                    InvoiceNumber = inv.InvoiceNumber.ToString(),
                    DueDate = inv.DueDate,
                    Total = inv.Total,
                    AmountPaid = inv.AmountPaid
                });
            }

            OutstandingInvoicesDataGridView.DataSource = DispInvoices;
            OutstandingInvoicesDataGridView.AutoResizeColumns();
            OutstandingInvoicesDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }


        // Button clicks


        private void PostPaymentButton_Click(object sender, EventArgs e)
        {
            if (CurrentPayer != null)
            {
                AmountPaid = Decimal.Parse(AmountPaidTextBox.Text);

                // find the invoice this amount matches or is more than
                // first go through and find an exact match

                foreach (Invoice Inv in OutstandingInvoices)
                {
                    if ((Inv.Total - Inv.AmountPaid) == AmountPaid)
                    {
                        // build ledger entry
                        ActiveBook.CurrentTransactionLedger.InsertTransaction(BuildLedgerEntry(AmountPaid, Inv));

                        // mark invoice as paid

                        ActiveBook.CurrentInvoices.ApplyPaymentToInvoice(Inv.InvoiceNumber, AmountPaid);

                        // mark invoice on screen as paid

                        UpdateDisplayInvoice(Inv.InvoiceNumber, AmountPaid, true);
                        return;
                    }
                }

                // otherwise, start at the first invoice and start applying payment to the invoices

                foreach (Invoice Inv in OutstandingInvoices)
                {
                    decimal BalanceDue = Inv.Total - Inv.AmountPaid;
                    if (BalanceDue <= AmountPaid)
                    {
                        // build ledger entry
                        ActiveBook.CurrentTransactionLedger.InsertTransaction(BuildLedgerEntry(BalanceDue, Inv));

                        // mark invoice as paid

                        ActiveBook.CurrentInvoices.ApplyPaymentToInvoice(Inv.InvoiceNumber, BalanceDue);

                        // mark invoice on screen as paid

                        UpdateDisplayInvoice(Inv.InvoiceNumber, BalanceDue, true);

                        AmountPaid -= BalanceDue;
                    }
                    if (AmountPaid == 0.00M)
                        return;
                }
                if (AmountPaid > 0)
                {
                    MessageBox.Show("You have more payment than outstanding invoices. Give the customer credit.");
                }

            }
        }

        private void PartialPaymentButton_Click(object sender, EventArgs e)
        {
            Invoice Inv = OutstandingInvoices[0];
            if (Inv != null)
            {
                // build ledger entry
                ActiveBook.CurrentTransactionLedger.InsertTransaction(BuildLedgerEntry(AmountPaid, Inv));

                // post as partial payment

                ActiveBook.CurrentInvoices.ApplyPaymentToInvoice(Inv.InvoiceNumber, AmountPaid);

                // mark invoice on screen as paid

                UpdateDisplayInvoice(Inv.InvoiceNumber, AmountPaid, false);
            }
        }

        private void DoneButton_Click(object sender, EventArgs e)
        {
            Close();
        }


        // Key Presses

        private void AmountPaidTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // allow only decimal keys
            if ((e.KeyChar != '.') && !Char.IsDigit(e.KeyChar)) { e.Handled = true; }
        }


        // support routines

        internal LedgerEntry BuildLedgerEntry(decimal AmountToPost, Invoice Inv)
        {
            string Account;
            if (Inv.InvoiceBreakdown.Count > 0)
                Account = Inv.InvoiceBreakdown[0].Account ?? ChartOfAccounts.MainIncomeAccount;
            else
                Account = ChartOfAccounts.MainIncomeAccount;

            return ActiveBook.CurrentTransactionLedger.CreateLedgerEntry
                    (
                        PostingDateTimePicker.Value.Date,
                        "",                 // check number
                        CurrentPayer,
                        0.00M,              // debit amount - should always be zero
                        AmountToPost,
                        0.00M,              // resulting balance get recalculated
                        AmountToPost,
                        Account,
                        null                // ledger breakdown
                    );

        }
        internal void UpdateDisplayInvoice(int InvoiceNumber, decimal AmountPaid, bool ifPaid)
        {
            OutstandingInvoicesDataGridView.DataSource = null;
            string strInvNum = InvoiceNumber.ToString();
            foreach (DisplayInvoice DI in DispInvoices)
            {
                if (DI.InvoiceNumber == strInvNum)
                {
                    DI.Paid = ifPaid;
                    DI.AmountPaid += AmountPaid;
                }
            }
            OutstandingInvoicesDataGridView.DataSource = DispInvoices;
            OutstandingInvoicesDataGridView.AutoResizeColumns();
            OutstandingInvoicesDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }
    }
}
