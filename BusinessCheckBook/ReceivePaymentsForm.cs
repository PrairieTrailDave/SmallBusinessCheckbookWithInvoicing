//**********************************************************************
//
//          Copyright © 2023 Prairie Trail Software, Inc.
//
//**********************************************************************

using BusinessCheckBook.DataStore;

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
                CustomerComboBox.Items.Add(customer.AccountName);
            }
        }

        private void CustomerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // find that customer
            string SelectedCustomer = CustomerComboBox.Text;

            // build the current balance from all invoices minus all payments

            List<Invoice> OutstandingInvoices = ActiveBook.CurrentInvoices.GetOutstandingInvoicesForACustomer(SelectedCustomer);
            decimal CurrentBalance = 0.00M;
            foreach (Invoice inv in OutstandingInvoices)
            {
                CurrentBalance += inv.Total - inv.AmountPaid;
            }
            CurrnetBalanceTextBox.Text = CurrentBalance.ToString();

            // show outstanding invoices
            List<DisplayInvoice> DispInvoices = new();
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

        private void SaveButton_Click(object sender, EventArgs e)
        {
            // build ledger entry
            // mark invoice as paid
        }

        private void PartialPaymentButton_Click(object sender, EventArgs e)
        {
            // build ledger entry

        }

        private void DoneButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AmountPaidTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // allow only decimal keys
            if ((e.KeyChar != '.') && !Char.IsDigit(e.KeyChar)) { e.Handled = true; }
        }
    }
}
