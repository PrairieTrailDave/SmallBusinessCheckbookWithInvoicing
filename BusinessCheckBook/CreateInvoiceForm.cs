//
//  Copyright 2023 David Randolph
//
using BusinessCheckBook.DataStore;
using System.Data;
using System.Drawing.Printing;
using System.Reflection;

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

        // internal variables

        decimal InvoiceTotal = 0.00M;
        int CurrentBreakdownRow = 0;


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
                CustomerComboBox.Items.Add(customer.AccountName);
            }
            InvoiceNumberTextBox.Text = ActiveBook.GetNextInvoiceNumber().ToString();
            List<string> Accounts = ActiveBook.CurrentAccounts.GetListOfAccounts();
            AccountListBox.Items.Clear();
            foreach (string account in Accounts)
                AccountListBox.Items.Add(account);
        }

        // button clicks

        private void PrintButton_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                CurrentInvoice = new();
                BuildInvoiceToSave(CurrentInvoice);
            }
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
                        BuildInvoiceLayout();


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
        private void SaveToBatchButton_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                CurrentInvoice = new();
                BuildInvoiceToSave(CurrentInvoice);
                InvoiceBatch.Add(CurrentInvoice);
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
                        BuildInvoiceLayout();
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
            FormatInvoice(CurrentInvoice, CurrentPrintLayout, ev);

            // If more lines exist, print another page.
            //if (line != null)
            //    ev.HasMorePages = true;
            //else
            ev.HasMorePages = false;
        }

        // The PrintPage event is raised for each page to be printed.
        private void Pd_PrintBatch(object sender, PrintPageEventArgs ev)
        {

            CurrentInvoice = InvoiceBatch[BatchInvoiceNum++];

            // Format that Invoice
            FormatInvoice(CurrentInvoice, CurrentPrintLayout, ev);

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
            string? CustomerSelected = CustomerComboBox.SelectedItem.ToString();
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
                TotalTextBox.Text = InvoiceTotal.ToString();
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
            InvoiceNumberTextBox.Text = ActiveBook.GetNextInvoiceNumber().ToString();

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
            ToSave.CustomerIdentifier = CustomerComboBox.Text;
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


        // printing routines


        internal void FormatInvoice(Invoice CurrentInvoiceToPrint, InvoicePrintLayout CurrentPrintLayout, PrintPageEventArgs ev)
        {
            // print the logo

            if (CurrentPrintLayout.LogoFileName.IfToPrintThisItem)
            {
                System.Drawing.Image img = System.Drawing.Image.FromFile(CurrentPrintLayout.LogoFileName.VariableNameToPrint);
                Rectangle Rex = new Rectangle(CurrentPrintLayout.LogoFileName.XPos,
                    CurrentPrintLayout.LogoFileName.YPos,
                    CurrentPrintLayout.LogoFileName.Width,
                    CurrentPrintLayout.LogoFileName.Height);
                ev.Graphics!.DrawImage(img, Rex);
            }

            // print company information

            foreach (PrintItemLayout PIL in CurrentPrintLayout.CompanyInformationItems)
            {
                // get what to print

                string WhatToPrint = "";
                switch (PIL.VariableNameToPrint)
                {
                    case idCompanyName:
                        WhatToPrint = ActiveBook.CompanyInformation.
                            GetParameter(CompanyParameters.ParmCompanyName);
                        break;
                    case idCompanyAdr:
                        WhatToPrint = ActiveBook.CompanyInformation.
                           GetAddressLine(1);
                        break;
                    case idCompanyAdr2:
                        WhatToPrint = ActiveBook.CompanyInformation.
                           GetAddressLine(2);
                        break;
                    case idCompanyAdr3:
                        WhatToPrint = ActiveBook.CompanyInformation.
                           GetAddressLine(3);
                        break;
                    case idCompanyEIN:
                        WhatToPrint = ActiveBook.CompanyInformation.
                            GetParameter(CompanyParameters.ParmCompanyEIN);
                        break;
                    case idCompanyPhone:
                        WhatToPrint = ActiveBook.CompanyInformation.
                            GetParameter(CompanyParameters.ParmCompanyPhone);
                        break;
                }

                if (PIL.IfToPrintThisItem)
                {
                    ev.Graphics!.DrawString(WhatToPrint, PIL.ItemFont, Brushes.Black, PIL.XPos, PIL.YPos, new StringFormat());
                }

            }

            // draw the boxes

            foreach (Rectangle rex in CurrentPrintLayout.BoxRects)
            {
                ev.Graphics!.DrawRectangle(new Pen(Brushes.Black, 1.0F), rex);
            }


            // print the standard text

            foreach (PrintItemLayout PIL in CurrentPrintLayout.StandardTexts)
            {
                if (PIL.IfToPrintThisItem)
                {
                    ev.Graphics!.DrawString(PIL.VariableNameToPrint, PIL.ItemFont, Brushes.Black, PIL.XPos, PIL.YPos, new StringFormat());
                }
            }

            // print the invoice text
            foreach (PrintItemLayout PIL in CurrentPrintLayout.TextToPrintLayouts)
            {
                if (PIL.IfToPrintThisItem)
                {
                    if (PIL.VariableNameToPrint == "BreakdownDescription")
                    {

                    }
                    else
                    {
                        if (PIL.VariableNameToPrint == "BreakdownAmount")
                        {

                        }
                        else
                        {
                            var InvoiceTextField = CurrentInvoiceToPrint.GetType().GetProperty(PIL.VariableNameToPrint, BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty);
                            if (InvoiceTextField != null)
                            {
                                var mthod = InvoiceTextField.GetGetMethod();
                                if (mthod != null)
                                {
                                    string fieldvalue = mthod.Invoke(CurrentInvoiceToPrint, null)?.ToString() ?? "";
                                    Rectangle Rex = new Rectangle(PIL.XPos, PIL.YPos, PIL.Width, PIL.Height);
                                    StringFormat SRF = new StringFormat();
                                    switch (PIL.HowToJustify)
                                    {
                                        case PrintItemLayout.Justification.LeftJustify: break;
                                        case PrintItemLayout.Justification.RightJustify:
                                            SRF.Alignment = StringAlignment.Far;
                                            break;
                                        case PrintItemLayout.Justification.Center:
                                            SRF.Alignment = StringAlignment.Center;
                                            break;
                                    }
                                    ev.Graphics!.DrawString(fieldvalue, PIL.ItemFont, Brushes.Black, Rex, SRF);
                                }
                            }
                        }
                    }
                }
            }
            FormatBreakdown(CurrentInvoiceToPrint, CurrentPrintLayout, ev);
        }

        internal void FormatBreakdown(Invoice CurrentInvoiceToPrint, InvoicePrintLayout CurrentPrintLayout, PrintPageEventArgs ev)
        {
            string WhatToPrint;
            int XPos;
            int YPos;
            int Height;
            // initialize these as don't print so that we can see if they are found
            PrintItemLayout PILDescr = new()
            {
                IfToPrintThisItem = false
            };
            PrintItemLayout PILAmt = new()
            {
                IfToPrintThisItem = false
            };

            // find the two layouts we need

            foreach (PrintItemLayout PIL in CurrentPrintLayout.TextToPrintLayouts)
            {
                if (PIL.VariableNameToPrint == "BreakdownDescription")
                    PILDescr = PIL;
                else
                    if (PIL.VariableNameToPrint == "BreakdownAmount")
                    PILAmt = PIL;
            }

            // initialize the line positions

            YPos = PILDescr.YPos;
            Height = PILDescr.Height;

            foreach (InvoiceItem invBreakdown in CurrentInvoiceToPrint.InvoiceBreakdown)
            {
                XPos = PILDescr.XPos;
                Rectangle Rex = new(XPos, YPos, PILDescr.Width, Height);
                WhatToPrint = invBreakdown.ItemDescription;
                ev.Graphics!.DrawString(WhatToPrint, PILDescr.ItemFont, Brushes.Black, Rex, new StringFormat());

                XPos = PILAmt.XPos;
                // YPos = PILAmt.YPos;
                Rectangle RexAmt = new(XPos, YPos, PILAmt.Width, Height);
                StringFormat SRF = new() { Alignment = StringAlignment.Far };
                ev.Graphics!.DrawString(invBreakdown.ItemPrice.ToString(), PILAmt.ItemFont, Brushes.Black, RexAmt, SRF);

                // figure out how much space was used by the text and adjust the spacing 

                SizeF TextSize = ev.Graphics.MeasureString(WhatToPrint, PILDescr.ItemFont);
                YPos += (int)(TextSize.Height * 1.3);  // with a margin
            }
        }


        internal void BuildInvoiceLayout()
        {
            CurrentPrintLayout = new();
            CurrentPrintLayout.LogoFileName = new()
            {
                IfToPrintThisItem = true,
                VariableNameToPrint = "Logo.BMP",
                YPos = 70,
                XPos = 40,
                Height = 40,
                Width = 535
            };

            // company information items

            CurrentPrintLayout.CompanyInformationItems = new()
            {
                new PrintItemLayout
                {
                    IfToPrintThisItem = true,
                    VariableNameToPrint = idCompanyAdr,
                    ItemFont = new Font("Times New Roman", 12),
                    YPos = 130,
                    XPos = 110,
                    Height = 15,
                    Width = 150
                },
                new PrintItemLayout
                {
                    IfToPrintThisItem = true,
                    VariableNameToPrint = idCompanyAdr2,
                    ItemFont = new Font("Times New Roman", 12),
                    YPos = 148,
                    XPos = 110,
                    Height = 15,
                    Width = 150
                },
                new PrintItemLayout
                {
                    IfToPrintThisItem = true,
                    VariableNameToPrint = idCompanyAdr3,
                    ItemFont = new Font("Times New Roman", 12),
                    YPos = 172,
                    XPos = 110,
                    Height = 15,
                    Width = 150
                }

            };

            // add boxes
            CurrentPrintLayout.BoxRects = new()
            {
                new Rectangle(585, 94, 88, 30), // date
                new Rectangle(673, 94, 93, 30), // invoice #
                new Rectangle(585, 124, 88, 30), // actual date
                new Rectangle(673, 124, 93, 30), // actual invoice #

                new Rectangle(110, 338, 324, 76), // bill to
                new Rectangle(674, 408, 93, 30),  // Due Date:
                new Rectangle(674, 438, 93, 27),  // actual due date

                new Rectangle(114, 465, 490, 30), // description
                new Rectangle(604, 465, 165, 30), // amount
                new Rectangle(114, 495, 490, 330), // invoice breakdown
                new Rectangle(604, 495, 165, 330), // breakdown amounts

                new Rectangle(114, 825, 392, 40),  // empty
                new Rectangle(506, 825, 263, 40),  // total

                new Rectangle(506, 915, 263, 30),  // payments/credits
                new Rectangle(506, 945, 263, 40)  // balance due
            };

            // add the standard texts

            CurrentPrintLayout.StandardTexts = new()
            {
                new PrintItemLayout
                {
                    IfToPrintThisItem = true,
                    VariableNameToPrint = "Invoice",
                    ItemFont = new Font("Lucida Sans", 20, FontStyle.Bold),
                    YPos = 50,
                    XPos = 650,
                    Height = 30,
                    Width = 200
                },
                new PrintItemLayout
                {
                    IfToPrintThisItem = true,
                    VariableNameToPrint = "Date",
                    ItemFont = new Font("Arial", 9),
                    YPos = 100,
                    XPos = 610,
                    Height = 12,
                    Width = 150
                },
                new PrintItemLayout
                {
                    IfToPrintThisItem = true,
                    VariableNameToPrint = "Invoice #",
                    ItemFont = new Font("Arial", 9),
                    YPos = 100,
                    XPos = 685,
                    Height = 12,
                    Width = 150
                },
                new PrintItemLayout
                {
                    IfToPrintThisItem = true,
                    VariableNameToPrint = "Bill To:",
                    ItemFont = new Font("Arial", 10),
                    YPos = 310,
                    XPos = 140,
                    Height = 12,
                    Width = 150
                },
                new PrintItemLayout
                {
                    IfToPrintThisItem = true,
                    VariableNameToPrint = "Due Date:",
                    ItemFont = new Font("Arial", 8),
                    YPos = 413,
                    XPos = 690,
                    Height = 12,
                    Width = 150
                },
                new PrintItemLayout
                {
                    IfToPrintThisItem = true,
                    VariableNameToPrint = "Description",
                    ItemFont = new Font("Arial", 8),
                    YPos = 475,
                    XPos = 325,
                    Height = 12,
                    Width = 150
                },
                new PrintItemLayout
                {
                    IfToPrintThisItem = true,
                    VariableNameToPrint = "Amount",
                    ItemFont = new Font("Arial", 8),
                    YPos = 475,
                    XPos = 685,
                    Height = 12,
                    Width = 150
                },
                new PrintItemLayout
                {
                    IfToPrintThisItem = true,
                    VariableNameToPrint = "Total",
                    ItemFont = new Font("Arial", 15, FontStyle.Bold),
                    YPos = 830,
                    XPos = 510,
                    Height = 12,
                    Width = 150
                },
                new PrintItemLayout
                {
                    IfToPrintThisItem = true,
                    VariableNameToPrint = "Payments/Credits",
                    ItemFont = new Font("Arial", 10, FontStyle.Bold),
                    YPos = 922,
                    XPos = 510,
                    Height = 12,
                    Width = 150
                },
                new PrintItemLayout
                {
                    IfToPrintThisItem = true,
                    VariableNameToPrint = "Balance Due:",
                    ItemFont = new Font("Arial", 15, FontStyle.Bold),
                    YPos = 952,
                    XPos = 510,
                    Height = 12,
                    Width = 150
                }
            };

            CurrentPrintLayout.TextToPrintLayouts = new()
            {
                new PrintItemLayout
                {
                    IfToPrintThisItem = true,
                    VariableNameToPrint = nameof(Invoice.BillingAddress1),
                    ItemFont = new Font("Times New Roman", 9),
                    YPos = 340,
                    XPos = 115,
                    Height = 12,
                    Width = 150
                },
                new PrintItemLayout
                {
                    IfToPrintThisItem = true,
                    VariableNameToPrint = nameof(Invoice.BillingAddress2),
                    ItemFont = new Font("Times New Roman", 9),
                    YPos = 355,
                    XPos = 115,
                    Height = 12,
                    Width = 150
                },
                new PrintItemLayout
                {
                    IfToPrintThisItem = true,
                    VariableNameToPrint = nameof(Invoice.BillingAddress3),
                    ItemFont = new Font("Times New Roman", 9),
                    YPos = 370,
                    XPos = 115,
                    Height = 12,
                    Width = 150
                },
                new PrintItemLayout
                {
                    IfToPrintThisItem = true,
                    VariableNameToPrint = nameof(Invoice.BillingDate),
                    ItemFont = new Font("Times New Roman", 9),
                    YPos = 131,
                    XPos = 590,
                    Height = 12,
                    Width = 75,
                    HowToJustify = PrintItemLayout.Justification.Center
                },
                new PrintItemLayout
                {
                    IfToPrintThisItem = true,
                    VariableNameToPrint = nameof(Invoice.InvoiceNumber),
                    ItemFont = new Font("Times New Roman", 9),
                    YPos = 131,
                    XPos = 680,
                    Height = 12,
                    Width = 75,
                    HowToJustify = PrintItemLayout.Justification.Center
                },
                new PrintItemLayout
                {
                    IfToPrintThisItem = true,
                    VariableNameToPrint = nameof(Invoice.DueDate),
                    ItemFont = new Font("Times New Roman", 9),
                    YPos = 442,
                    XPos = 680,
                    Height = 12,
                    Width = 75,
                    HowToJustify = PrintItemLayout.Justification.Center
                },
                new PrintItemLayout
                {
                    IfToPrintThisItem = true,
                    VariableNameToPrint = nameof(Invoice.Total),
                    ItemFont = new Font("Times New Roman", 12),
                    YPos = 833,
                    XPos = 600,
                    Height = 19,
                    Width = 150,
                    HowToJustify = PrintItemLayout.Justification.RightJustify
                },
                new PrintItemLayout
                {
                    IfToPrintThisItem = true,
                    VariableNameToPrint = nameof(Invoice.AmountPaid),
                    ItemFont = new Font("Times New Roman", 9),
                    YPos = 923,
                    XPos = 640,
                    Height = 12,
                    Width = 110,
                    HowToJustify = PrintItemLayout.Justification.RightJustify
                },
                new PrintItemLayout
                {
                    IfToPrintThisItem = true,
                    VariableNameToPrint = nameof(Invoice.BalanceDue),
                    ItemFont = new Font("Times New Roman", 12),
                    YPos = 955,
                    XPos = 640,
                    Height = 19,
                    Width = 110,
                    HowToJustify = PrintItemLayout.Justification.RightJustify
                },
                new PrintItemLayout
                {
                    IfToPrintThisItem = true,
                    VariableNameToPrint = "BreakdownDescription",
                    ItemFont = new Font("Times New Roman", 12),
                    YPos = 497,
                    XPos = 116,
                    Height = 325,
                    Width = 480
                },
                new PrintItemLayout
                {
                    IfToPrintThisItem = true,
                    VariableNameToPrint = "BreakdownAmount",
                    ItemFont = new Font("Times New Roman", 12),
                    YPos = 497,
                    XPos = 610,
                    Height = 325,
                    Width = 140
                }


            };
        }

    }
}
