//**********************************************************************
//
//          Copyright © 2023 Prairie Trail Software, Inc.
//
//**********************************************************************

using BusinessCheckBook.DataStore;
using System.Data;

namespace BusinessCheckBook
{
    public partial class ViewInvoicesForm : Form
    {
        // variables filled from the main window
        public MyCheckbook ActiveBook { get; set; } = new();


        // class for displaying the invoices for the selected customer
        public class DisplayInvoice
        {
            public int Number { get; set; }
            public string Date { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public string Total { get; set; } = string.Empty;
            public string AmountPaid { get; set; } = string.Empty;

        }

        public ViewInvoicesForm()
        {
            InitializeComponent();
        }

        internal void SetUp(MyCheckbook activeBook)
        {
            ActiveBook = activeBook;
            SelectCustomerComboBox.Items.Clear();
            List<Customer> customerList = ActiveBook.Customers.GetCurrentList();
            foreach (Customer customer in customerList)
            {
                SelectCustomerComboBox.Items.Add(customer.CustomerIdentifier);
            }
        }
        private void SelectCustomerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<DisplayInvoice> ToShow = new();
            string CustomerID = SelectCustomerComboBox.Text;
            ToShow = (from inv in ActiveBook.CurrentInvoices.GetInvoicesForACustomer(CustomerID)
                      select new DisplayInvoice
                      {
                          Number = inv.InvoiceNumber,
                          Date = inv.BillingDate,
                          Description = inv.CustomerMemo,
                          Total = inv.Total.ToString("0.00"),
                          AmountPaid = inv.AmountPaid.ToString("0.00")
                      }).ToList();
            InvoicesDataGridView.DataSource = ToShow;
            InvoicesDataGridView.AutoResizeColumns();
        }

        private void InvoicesDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int GridRow = e.RowIndex;
            int InvoiceNumber = (int)InvoicesDataGridView.Rows[GridRow].Cells[0].Value;

            // get that invoice
            Invoice ToShow = ActiveBook.CurrentInvoices.GetInvoice(InvoiceNumber);

            // display on Show Invoice screen

            ShowInvoiceForm SIF = new();
            SIF.SetUp(ActiveBook, ToShow);
            SIF.ShowDialog();

        }
    }
}