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
    public partial class CreateInvoiceForm : Form
    {
        // variables filled from the main window
        public MyCheckbook ActiveBook { get; set; } = new();

        // for printing
        internal Invoice CurrentInvoice = new();
        List<Invoice> InvoiceBatch = new();
        int BatchInvoiceNum = 0;
        InvoicePrintLayout CurrentPrintLayout = new();
        public InvoiceFormat CurrentInvoiceFormat { get; set; } = new();


        // internal variables

        decimal InvoiceTotal = 0.00M;
        int CurrentBreakdownRow = 0;
        int InvoiceNumber = 0;  // because we need to increment the invoice number before we add to ledger


        // identifiers for company information to print
        internal const string idCompanyName = "CompName";
        internal const string idCompanyAdr = "CompAdr";
        internal const string idCompanyAdr2 = "CompAdr2";
        internal const string idCompanyAdr3 = "CompAdr3";
        internal const string idCompanyEIN = "CompEIN";
        internal const string idCompanyPhone = "CompPhone";

        public CreateInvoiceForm()
        {
            InitializeComponent();
            BatchInvoiceNum = 0;
        }


        // called from main menu

        internal void SetUp(MyCheckbook activeBook)
        {
            ActiveBook = activeBook;
            InvoiceDateTimePicker.Text = DateTime.Now.ToShortDateString();
            DueDateDateTimePicker.Text = DateTime.Now.AddDays(14).ToShortDateString();

            ResetBreakdown();

            CustomerComboBox.Items.Clear();
            List<Customer> customerList = ActiveBook.Customers.GetCurrentList();
            foreach (Customer customer in customerList)
            {
                CustomerComboBox.Items.Add(new DropDownItem(customer.AccountName, customer.CustomerIdentifier));
            }
            InvoiceNumber = ActiveBook.GetNextInvoiceNumber();
            InvoiceNumberTextBox.Text = InvoiceNumber.ToString();
            List<string> Accounts = ActiveBook.CurrentAccounts.GetListOfAccounts();
            AccountListBox.Items.Clear();
            foreach (string account in Accounts)
                AccountListBox.Items.Add(account);

            CurrentInvoiceFormat = new(ActiveBook);

        }

        // button clicks

        private void PrintButton_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                Invoice InvoiceToSave = new();
                CurrentInvoice = new();
                // before allowing an invoice to be printed, make sure that there is an invoice to print

                if (CustomerComboBox.Text.Length > 0)
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
                            CurrentInvoice = new();
                            BuildInvoiceToSave(CurrentInvoice);
                            CurrentPrintLayout = InvoiceFormat.BuildInvoiceLayout();


                            PrintDocument pd = new();
                            pd.PrintPage += new PrintPageEventHandler(this.Pd_PrintPage);
                            pd.Print();
                            BuildInvoiceToSave(InvoiceToSave);
                            ActiveBook.CurrentInvoices.AddInvoice(InvoiceToSave);
                            InvoiceNumber = ActiveBook.GetNextInvoiceNumber();
                            ResetScreen();
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
        }
        private void SaveToBatchButton_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                Invoice InvoiceToSave = new();
                BuildInvoiceToSave(InvoiceToSave);
                InvoiceBatch.Add(InvoiceToSave);
                InvoiceNumber++;
                ResetScreen();
            }
        }

        private void PrintBatchButton_Click(object sender, EventArgs e)
        {
            // before allowing an invoice to be printed, make sure that there is an invoice to print

            if (InvoiceBatch.Count > 0)
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
                        BatchInvoiceNum = 0;

                        PrintDocument pd = new();
                        pd.PrintPage += new PrintPageEventHandler(this.Pd_PrintBatch);
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
                MessageBox.Show("Please enter an invoice in the batch before attempting to print.");
            }
        }

        private void SaveToHistoryButton_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                CurrentInvoice = new();
                BuildInvoiceToSave(CurrentInvoice);
                ActiveBook.CurrentInvoices.AddInvoice(CurrentInvoice);
                InvoiceNumber = ActiveBook.GetNextInvoiceNumber();
                CurrentInvoice = new();
                ResetScreen();
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            ResetScreen();
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

        // The PrintPage event is raised for each page to be printed.
        private void Pd_PrintBatch(object sender, PrintPageEventArgs ev)
        {
            // define a local variable to keep thread clean

            Invoice BatchInvoice;

            BatchInvoice = InvoiceBatch[BatchInvoiceNum++];

            // Format that Invoice
            CurrentInvoiceFormat.FormatInvoice(BatchInvoice, CurrentPrintLayout, ev);

            // save that invoice in the list of invoices

            ActiveBook.CurrentInvoices.AddInvoice(BatchInvoice);

            // If more lines exist, print another page.
            if (BatchInvoiceNum < InvoiceBatch.Count)
                ev.HasMorePages = true;
            else
            {
                ev.HasMorePages = false;
                InvoiceBatch = new();
                BatchInvoiceNum = 0;
            }
        }





        // Other User Interface Events

        private void CustomerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string? CustomerSelected = CustomerComboBox.SelectedItem?.ToString();
            if (CustomerSelected != null)
            {
                Customer? ChosenCustomer = ActiveBook.Customers.GetCustomerByName(CustomerSelected);
                if (ChosenCustomer != null)
                {
                    BillToAddress1TextBox.Text = ChosenCustomer.BusinessName;
                    BillToAddress2TextBox.Text = ChosenCustomer.Address;
                    if (ChosenCustomer.Address2.Length > 0)
                    {
                        BillToAddress3TextBox.Text = ChosenCustomer.Address2;
                        BillToAddress4TextBox.Text = ChosenCustomer.City + ", " + ChosenCustomer.State + " " + ChosenCustomer.ZipCode;
                    }
                    else
                    {
                        BillToAddress3TextBox.Text = ChosenCustomer.City + ", " + ChosenCustomer.State + " " + ChosenCustomer.ZipCode;
                        BillToAddress4TextBox.Text = "";
                    }
                    GetCustomerHistoryAndOpenBalance(ChosenCustomer);
                }
                else
                    MessageBox.Show("Unable to find that customer");
            }
        }

        private void InvoiceDetailDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int WhichRow = e.RowIndex;
            int WhichColumn = e.ColumnIndex;

            // Compute the price from the cost and quantity
            if ((WhichColumn == InvoiceDetailDataGridView.Columns["ItemCost"].Index) ||
                (WhichColumn == InvoiceDetailDataGridView.Columns["ItemQuantity"].Index))
            {
                DataGridViewRow dgvr = InvoiceDetailDataGridView.Rows[WhichRow];
                string? CostString = dgvr.Cells["ItemCost"].Value.ToString();
                string? QtyString = dgvr.Cells["ItemQuantity"].Value.ToString();
                if (CostString != null && QtyString != null)
                {
                    if (decimal.TryParse(CostString, out decimal CostEntered) &&
                        decimal.TryParse(QtyString, out decimal QtyEntered))
                    {
                        decimal Price = CostEntered * QtyEntered;
                        dgvr.Cells["ItemPrice"].Value = Price.ToString("0.00");
                    }
                }
            }

            // add up the prices and display the total 
            if (WhichColumn == InvoiceDetailDataGridView.Columns["ItemPrice"].Index)
            {
                InvoiceTotal = 0.00M;
                foreach (DataGridViewRow dgvr in InvoiceDetailDataGridView.Rows)
                {
                    string? PriceString = dgvr.Cells["ItemPrice"].Value.ToString();
                    if (PriceString != null)
                    {
                        if (decimal.TryParse(PriceString, out decimal PriceEntered))
                        {
                            InvoiceTotal += PriceEntered;
                        }
                        else
                        {
                            MessageBox.Show("The price entered is not valid.");
                            dgvr.Cells["ItemPrice"].Value = "0.00";
                        }
                    }
                }
                TotalTextBox.Text = InvoiceTotal.ToString("0.00");
                BalanceDueTextBox.Text = InvoiceTotal.ToString("0.00");
            }

        }


        private void InvoiceDetailDataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            int ColumnEntered = e.ColumnIndex;
            CurrentBreakdownRow = e.RowIndex;
            if (ColumnEntered == 0)
            {
                if (CustomerComboBox.Text.Length > 0)
                {
                    AccountListBox.Visible = true;
                    AccountListBox.Focus();
                }
            }
            else
                AccountListBox.Visible = false;

        }


        private void AccountListBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //if (e.KeyCode == Keys.Tab)
            //{
            //    InvoiceDetailDataGridView.Focus();
            //    InvoiceDetailDataGridView.Rows[CurrentBreakdownRow].Cells[1].Selected = true;
            //    InvoiceDetailDataGridView.BeginEdit(true);
            //}
        }

        private void AccountListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CurrentBreakdownRow > -1)
            {
                InvoiceDetailDataGridView.Rows[CurrentBreakdownRow].Cells[0].Value =
                    AccountListBox.Text;
                AccountListBox.Visible = false;
                InvoiceDetailDataGridView.Focus();
                InvoiceDetailDataGridView.Rows[CurrentBreakdownRow].Cells[1].Selected = true;
                InvoiceDetailDataGridView.BeginEdit(true);
            }
        }

        private void AccountListBox_Leave(object sender, EventArgs e)
        {
            if (CurrentBreakdownRow > -1)
            {
                InvoiceDetailDataGridView.Rows[CurrentBreakdownRow].Cells[0].Value =
                    AccountListBox.Text;
                AccountListBox.Visible = false;
            }
        }




        // Support Routines



        internal void ResetScreen()
        {
            CustomerComboBox.SelectedItem = "";
            CustomerComboBox.Text = "";
            BillToAddress1TextBox.Text = "";
            BillToAddress2TextBox.Text = "";
            BillToAddress3TextBox.Text = "";
            BillToAddress4TextBox.Text = "";
            BillToAddress5TextBox.Text = "";
            InvoiceNumberTextBox.Text = InvoiceNumber.ToString();

            ResetBreakdown();

            TaxTextBox.Text = "";
            TotalTextBox.Text = "";
            BalanceDueTextBox.Text = "";
            OpenBalanceTextBox.Text = "";
            RecentTransactionsTextBox.Text = "";
            InvoiceTotal = 0.00M;
        }

        internal void ResetBreakdown()
        {
            CurrentInvoice.InvoiceBreakdown = new()
            {
                new InvoiceItem(),
                new InvoiceItem(),
                new InvoiceItem(),
                new InvoiceItem(),
                new InvoiceItem(),
                new InvoiceItem(),
                new InvoiceItem(),
                new InvoiceItem(),
                new InvoiceItem(),
                new InvoiceItem(),
                new InvoiceItem(),
                new InvoiceItem()
            };
            InvoiceDetailDataGridView.DataSource = CurrentInvoice.InvoiceBreakdown;
            int DetailWindowSize = InvoiceDetailDataGridView.Width;
            InvoiceDetailDataGridView.Columns["ItemDescription"].Width = DetailWindowSize / 2;
            InvoiceDetailDataGridView.Columns["ItemCost"].Width = DetailWindowSize / 12;
            InvoiceDetailDataGridView.Columns["ItemQuantity"].Width = DetailWindowSize / 14;
            InvoiceDetailDataGridView.Columns["ItemTax"].Width = DetailWindowSize / 14;
            InvoiceDetailDataGridView.Columns["ItemPrice"].Width = DetailWindowSize / 12;
            InvoiceDetailDataGridView.Columns["ItemDescription"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            InvoiceDetailDataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            InvoiceTotal = 0.00M;
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


        internal bool ValidateInputs()
        {
            if (CustomerComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a customer");
                return false;
            }
            if (!Int32.TryParse(InvoiceNumberTextBox.Text, out _))
            {
                MessageBox.Show("Please enter a valid invoice number");
                return false;
            }
            if (TaxTextBox.Text.Length > 0)
            {
                if (!decimal.TryParse(TaxTextBox.Text, out _))
                {
                    MessageBox.Show("Please enter a valid tax amount");
                    return false;
                }
            }
            if (TotalTextBox.Text.Length > 0)
            {
                if (!decimal.TryParse(TotalTextBox.Text, out _))
                {
                    MessageBox.Show("Please enter a valid total amount");
                    return false;
                }
            }
            else
                TotalTextBox.Text = "0.00";
            if (BalanceDueTextBox.Text.Length > 0)
            {
                if (!decimal.TryParse(BalanceDueTextBox.Text, out _))
                {
                    MessageBox.Show("Please enter a valid balance due amount");
                    return false;
                }
            }
            else
                BalanceDueTextBox.Text = "0.00";
            return true;
        }
        internal void BuildInvoiceToSave(Invoice ToSave)
        {
            DropDownItem SelectededCustomer = (DropDownItem)CustomerComboBox.SelectedItem!;
            ToSave.CustomerIdentifier = SelectededCustomer.Value;
            ToSave.BillingAddress1 = BillToAddress1TextBox.Text;
            ToSave.BillingAddress2 = BillToAddress2TextBox.Text;
            ToSave.BillingAddress3 = BillToAddress3TextBox.Text;
            ToSave.BillingAddress4 = BillToAddress4TextBox.Text;
            ToSave.BillingAddress5 = BillToAddress5TextBox.Text;

            ToSave.BillingDate = InvoiceDateTimePicker.Value.ToShortDateString();
            ToSave.InvoiceNumber = Int32.Parse(InvoiceNumberTextBox.Text);
            ToSave.DueDate = DueDateDateTimePicker.Value.ToShortDateString();
            ToSave.CustomerMemo = CustomerMemoTextBox.Text;
            ToSave.Tax = 0.00M;
            if (TaxTextBox.Text.Length > 0)
                ToSave.Tax = decimal.Parse(TaxTextBox.Text);
            ToSave.Total = decimal.Parse(TotalTextBox.Text);
            ToSave.AmountPaid = 0.00M;
            ToSave.BalanceDue = decimal.Parse(BalanceDueTextBox.Text);

            // save the breakdown
            // first find out how many rows are actually used, allowing for empty rows for formatting
            int MaxRow = 0;
            for (int i = 0; i < InvoiceDetailDataGridView.Rows.Count; i++)
            {
                DataGridViewRow dgvr = InvoiceDetailDataGridView.Rows[i];
                string? Descr = dgvr.Cells["ItemDescription"].Value.ToString();
                if (Descr != null)
                {
                    if (Descr.Length > 0)
                        MaxRow = i + 1;
                }
            }

            // now move those rows to the new invoice

            ToSave.InvoiceBreakdown = new();

            for (int i = 0; i < MaxRow; i++)
            {
                DataGridViewRow dgvr = InvoiceDetailDataGridView.Rows[i];
                InvoiceItem IIT = new();
                IIT.Account = dgvr.Cells["Account"].Value?.ToString() ?? "";
                IIT.ItemDescription = dgvr.Cells["ItemDescription"].Value.ToString() ?? "";
                IIT.ItemCost = decimal.Parse(dgvr.Cells["ItemCost"].Value.ToString() ?? "0.00M");
                IIT.ItemQuantity = decimal.Parse(dgvr.Cells["ItemQuantity"].Value.ToString() ?? "0.00M");
                IIT.ItemTax = decimal.Parse(dgvr.Cells["ItemTax"].Value.ToString() ?? "0.00M");
                IIT.ItemPrice = decimal.Parse(dgvr.Cells["ItemPrice"].Value.ToString() ?? "0.00M");
                ToSave.InvoiceBreakdown.Add(IIT);
            }
        }

    }
}
