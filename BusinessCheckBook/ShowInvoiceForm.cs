//**********************************************************************
//
//          Copyright © 2023 Prairie Trail Software, Inc.
//
//**********************************************************************

using System.Data;
using System.Drawing.Printing;
using System.Reflection;

using BusinessCheckBook.DataStore;
using BusinessCheckBook.Extensions;

namespace BusinessCheckBook
{
    public partial class ShowInvoiceForm : Form
    {
        // variables filled from the main window
        public MyCheckbook ActiveBook { get; set; } = new();

        public Invoice CurrentInvoice { get; set; } = new();
        public InvoiceFormat CurrentInvoiceFormat { get; set; } = new();
        InvoicePrintLayout CurrentPrintLayout { get; set; } = new();

        public ShowInvoiceForm()
        {
            InitializeComponent();
        }


        // called from main menu

        internal void SetUp(MyCheckbook activeBook, Invoice toPrint)
        {
            ActiveBook = activeBook;
            CurrentInvoice = toPrint;
            CurrentInvoiceFormat = new(ActiveBook);
            BuildScreen();
        }

        // button clicks

        private void PrintButton_Click(object sender, EventArgs e)
        {
            // before allowing an invoice to be printed, make sure that there is an invoice to print

            if (CustomerTextBox.Text.Length > 0)
            {

                // Allow the user to choose the page range he or she would
                // like to print.
                PrintInvoiceDialog.AllowSomePages = true;

                // Show the help button.
                PrintInvoiceDialog.ShowHelp = true;

                // Set the Document property to the PrintDocument for 
                // which the PrintPage Event has been handled. To display the
                // dialog, either this property or the PrinterSettings property 
                // must be set 
                PrintInvoiceDialog.PrinterSettings = new();
                //PrintCheckDialog.Document = docToPrint;

                DialogResult result = PrintInvoiceDialog.ShowDialog();

                // If the result is OK then print the document.
                if (result == DialogResult.OK)
                {

                    try
                    {
                        CurrentPrintLayout = InvoiceFormat.BuildInvoiceLayout();


                        PrintDocument pd = new();
                        pd.PrintPage += new PrintPageEventHandler(this.Pd_PrintPage);
                        pd.Print();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter an invoice before attempting to print.");
            }
        }


        // Event Handler
        // The PrintPage event is raised for each page to be printed.
        private void Pd_PrintPage(object sender, PrintPageEventArgs ev)
        {

            // Format the Invoice
            CurrentInvoiceFormat.FormatInvoice(CurrentInvoice, CurrentPrintLayout, ev);

            // If more lines exist, print another page.
            //if (line != null)
            //    ev.HasMorePages = true;
            //else
            ev.HasMorePages = false;
        }




        // Other User Interface Events




        // Support Routines



        internal void BuildScreen()
        {
            CustomerTextBox.Text = CurrentInvoice.CustomerIdentifier;
            BillToAddress1TextBox.Text = CurrentInvoice.BillingAddress1;
            BillToAddress2TextBox.Text = CurrentInvoice.BillingAddress2;
            BillToAddress3TextBox.Text = CurrentInvoice.BillingAddress3;
            BillToAddress4TextBox.Text = CurrentInvoice.BillingAddress4;
            BillToAddress5TextBox.Text = CurrentInvoice.BillingAddress5;
            InvoiceNumberTextBox.Text = CurrentInvoice.InvoiceNumber.ToString();
            InvoiceDateTextBox.Text = CurrentInvoice.BillingDate;
            InvoiceDueDateTextBox.Text = CurrentInvoice.DueDate;

            ResetBreakdown();

            TaxTextBox.Text = CurrentInvoice.Tax.ToString();
            TotalTextBox.Text = CurrentInvoice.Total.ToString();
            BalanceDueTextBox.Text = CurrentInvoice.BalanceDue.ToString();
            OpenBalanceTextBox.Text = "";
            RecentTransactionsTextBox.Text = "";
        }

        internal void ResetBreakdown()
        {
            InvoiceDetailDataGridView.DataSource = CurrentInvoice.InvoiceBreakdown;
            int DetailWindowSize = InvoiceDetailDataGridView.Width;
            InvoiceDetailDataGridView.Columns["ItemDescription"].Width = DetailWindowSize / 2;
            InvoiceDetailDataGridView.Columns["ItemCost"].Width = DetailWindowSize / 12;
            InvoiceDetailDataGridView.Columns["ItemQuantity"].Width = DetailWindowSize / 14;
            InvoiceDetailDataGridView.Columns["ItemTax"].Width = DetailWindowSize / 14;
            InvoiceDetailDataGridView.Columns["ItemPrice"].Width = DetailWindowSize / 12;
            InvoiceDetailDataGridView.Columns["ItemDescription"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            InvoiceDetailDataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }



        internal void GetCustomerHistoryAndOpenBalance(Customer ChosenCustomer)
        {
            List<Invoice> PastInvoices = ActiveBook.CurrentInvoices.GetInvoicesForACustomer(ChosenCustomer.AccountName);
            decimal OpenBalance = (from inv in PastInvoices
                                   where !inv.Paid
                                   select inv.Total).Sum();
            OpenBalanceTextBox.Text = OpenBalance.ToString();
            // get the last 5 transactions
            PastInvoices.Reverse();
            int count = 0;
            foreach (Invoice invoice in PastInvoices)
            {
                if (count == 5) break;

                string DisplayInvoice = invoice.BillingDate + " " + invoice.InvoiceNumber.ToString() + " " + invoice.Total.ToString();
                RecentTransactionsTextBox.Text += DisplayInvoice + Environment.NewLine;

                count++;
            }

        }


    }
}
