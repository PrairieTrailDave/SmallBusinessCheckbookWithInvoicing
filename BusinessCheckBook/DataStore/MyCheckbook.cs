//**********************************************************************
//
//          Copyright © 2023 Prairie Trail Software, Inc.
//
//**********************************************************************

using ClosedXML.Excel;

namespace BusinessCheckBook.DataStore
{
    public class MyCheckbook
    {
        // Internal data storage
        // for various reasons, I'm using a single data store.
        // If this were a more capable system, it might make sense to break up the data store into components
        internal TransactionLedger CurrentTransactionLedger { get; set; } = new();
        internal ChartOfAccounts CurrentAccounts { get; set; } = new();
        internal Invoices CurrentInvoices { get; set; } = new();
        internal CustomerList Customers { get; set; } = new();
        internal PayToList ToPayTo { get; set; } = new();
        internal CompanyParameters CompanyInformation { get; set; } = new();


        // other variables

        ActivityLogger Logger { get; set; } = new("");

        public MyCheckbook()
        {
        }
        public MyCheckbook(ActivityLogger myLog)
        {
            Logger = myLog;
        }


        internal void CreateNewCheckBook(decimal InitialBalance, int FirstCheckNumber, int FirstInvoiceNumber)
        {
            CurrentAccounts = new();
            CurrentAccounts.CreateChartOfAccounts();
            Customers = new();
            CurrentInvoices = new();
            CurrentTransactionLedger = new();
            CurrentTransactionLedger.AddInitialBalance(InitialBalance);
            CurrentTransactionLedger.SetFirstCheckNumber(FirstCheckNumber);
            ToPayTo = new();
            CompanyInformation = new();
            CompanyInformation.PutParameter(CompanyParameters.FirstInvoiceNumber, FirstInvoiceNumber.ToString());
        }

        public bool FileIsValid(XLWorkbook CurrentWorkbook, out string ErrorMessage)
        {
            // first validate the Chart of Accounts

            if (!CurrentAccounts.Validate(CurrentWorkbook, out ErrorMessage))
            {
                ErrorMessage = "The Chart of Accounts is not valid" + ErrorMessage;
                return false;
            }
            if (!Customers.Validate(CurrentWorkbook, out ErrorMessage))
            {
                ErrorMessage = "The customer list is not valid " + ErrorMessage;
                return false;
            }
            if (!CurrentTransactionLedger.Validate(CurrentWorkbook, out ErrorMessage))
            {
                ErrorMessage = "The transaction list is not valid " + ErrorMessage;
                return false;
            }
            if (!CurrentInvoices.Validate(CurrentWorkbook, out ErrorMessage))
            {
                ErrorMessage = "The invoices are not valid " + ErrorMessage;
                return false;
            }

            if (!ToPayTo.Validate(CurrentWorkbook, out ErrorMessage))
            {
                ErrorMessage = "The list of people to pay is not valid " + ErrorMessage;
                return false;
            }

            if (!CompanyInformation.Validate(CurrentWorkbook, out ErrorMessage))
            {
                ErrorMessage = "The Company Information is not valid " + ErrorMessage;
                return false;
            }

            return true;
        }









        public bool ReadExcelFile(XLWorkbook CurrentWorkbook)
        {
            try
            {
                CurrentTransactionLedger = new();
                CurrentTransactionLedger.BuildBreakdownColumnFormats(CurrentWorkbook);
                CurrentAccounts = new();
                Customers = new();

                CurrentAccounts.ReadFromExcelFile(CurrentWorkbook);
                Customers.ReadFromExcelFile(CurrentWorkbook);   
                CurrentTransactionLedger.ReadFromExcelFile(CurrentWorkbook);
                CurrentInvoices.ReadFromExcelFile(CurrentWorkbook);
                ToPayTo.ReadFromExcelFile(CurrentWorkbook);
                CompanyInformation.ReadFromExcelFile(CurrentWorkbook) ;

                CheckFileForConsistency();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("We had an error in reading the file" + ex.Message);
                return false;
            }
        }


        internal bool ReadIIFFile(string FileName)
        {
            string ErrorMessage;
            using (StreamReader sr = new StreamReader(FileName))
            {
                int Row = 0;
                while (sr.Peek() > -1)
                {
                    string? InputLine = sr.ReadLine();
                    if (InputLine != null)
                    {
                        Row++;
                        string[] fields = InputLine.Split('\t');
                        if (fields.Length > 0)
                        {
                            switch (fields[0])
                            {
                                case "!HDR": break;
                                case "!ACCNT":
                                    CurrentAccounts = new();
                                    break;
                                case "ACCNT":
                                    if(CurrentAccounts.ValidIIFAccountFields(fields, out ErrorMessage))
                                        CurrentAccounts.AddIIFAccount(fields);
                                    else
                                    {
                                        ErrorMessage += " in row " + Row.ToString();
                                        MessageBox.Show(ErrorMessage);
                                    }
                                    break;
                                case "!CTYPE": break;
                                case "!CUSTNAMEDICT": break;
                                case "CUSTNAMEDICT": break;
                                case "!ENDCUSTNAMEDICT": break;
                                case "ENDCUSTNAMEDICT": break;
                                case "!CUST": break;
                                case "CUST":
                                    if (Customers.ValidateIIFCustomer(fields, out ErrorMessage))
                                        Customers.AddIIFCustomer(fields);
                                    else
                                    {
                                        ErrorMessage += " in row " + Row.ToString();
                                        MessageBox.Show(ErrorMessage);
                                    }
                                    break;
                                case "!VTYPE": break;
                                case "VTYPE": break;
                                case "!VEND": break;
                                case "VEND": 
                                    if (ToPayTo.ValidateIIFVendor(fields, out ErrorMessage))
                                        ToPayTo.AddIIFVendor(fields);
                                    else
                                    {
                                        ErrorMessage += " in row " + Row.ToString();
                                        MessageBox.Show(ErrorMessage);
                                    }
                                    break;
                                case "!OTHERNAME": break;
                                case "OTHERNAME":
                                    if (ToPayTo.ValidateIIFOtherName(fields, out ErrorMessage))
                                        ToPayTo.AddIIFOtherName(fields);
                                    else
                                    {
                                        ErrorMessage += " in row " + Row.ToString();
                                        MessageBox.Show(ErrorMessage);
                                    }
                                    break;
                                case "!SALESTAXCODE": break;
                                case "SALESTAXCODE": break;
                            }
                        }
                    }
                }
            }
            // Since this function only reads in names and accounts, don't check for consistency
            return true;
        }


        // the reading of the journal could result in duplicate entries
        // so each new entry needs to be validated against existing and 
        // let the user decide if to replace the existing entry
        internal bool ReadJournalFile(string FileName)
        {
            string ErrorMessage;
            Dictionary<string, string> AccountConversion = new();
            
            XLWorkbook JournalWorkbook = new XLWorkbook(FileName);
            IXLWorksheet JournalWorksheet = JournalWorkbook.Worksheet("Sheet1");

            int Row = 2;

            while (Row < JournalWorksheet.RowsUsed().Count())
            {
                string EntryType = JournalWorksheet.Cell(Row, Journal.TypeCol).GetString();
                switch (EntryType)
                {
                    case "Check":
                        if (CurrentTransactionLedger.ValidateJournalCheck(JournalWorksheet, Row, out ErrorMessage))
                        {
                            LedgerEntry LE = new();
                            if (!Journal.ParseCheck(LE, JournalWorksheet, ref Row)) return false;

                            // make sure primary account is in the chart of accounts

                            // because we can add to the list of current accounts, 
                            // get a new copy for each check

                            List<Account> CurrentAccountList = CurrentAccounts.GetCurrentList();
                            string PrimaryAccount = LE.GetPrimaryAccount();
                            if (!CurrentAccounts.IsThisAccountValid(PrimaryAccount))
                            {
                                MessageBox.Show("The primary account is not in the Chart of Accounts. It will be added.");
                                Account NAccount = ChartOfAccounts.CreateNewAccount(PrimaryAccount, Account.Type.CheckingAccount);
                                CurrentAccounts.InsertAccount(NAccount);
                            }

                            // check the payee to make sure is in the list of payee

                            string payee = LE.ToWhom;
                            if (!ToPayTo.IsValidPayTo(payee))
                            {
                                PayTo nPay = PayToList.CreateNewPayTo(payee);
                                ToPayTo.AddPayTo(nPay);
                            }

                            // check all the sub accounts to make sure they are in the chart of accounts / converted to valid account

                            foreach(LedgerEntryBreakdown LEB in LE.SubAccounts)
                            {
                                string EntryAccount = LEB.AccountName;
                                if (!CurrentAccounts.IsThisAccountValid(EntryAccount))
                                {
                                    if (AccountConversion.ContainsKey(EntryAccount))
                                    {
                                        LEB.AccountName = AccountConversion[EntryAccount];
                                    }
                                    else
                                    {
                                        ErrorMessage = "The account " + EntryAccount
                                            + " is not found in the chart of accounts for "
                                            + LE.CheckNumber + " dated: " + LE.When.ToShortDateString()
                                            + " breakdown part " + LEB.Amount.ToString("0.00");
                                        MessageBox.Show(ErrorMessage);
                                        ChangeAccountForm CAF = new();
                                        CAF.Setup(CurrentAccountList, EntryAccount);
                                        CAF.ShowDialog();
                                        if (CAF.ToChangeTo.Length > 0)
                                        {
                                            EntryAccount = CAF.ToChangeTo;
                                            AccountConversion.Add(LEB.AccountName, CAF.ToChangeTo);
                                            LEB.AccountName = EntryAccount;
                                        }
                                        // If not converted, leave for sanity tests
                                    }
                                }

                            }

                            // add this check to the ledger
                            CurrentTransactionLedger.AddCheckEntry(LE);

                        }
                        else
                        {
                            MessageBox.Show("The journal check in row " + Row.ToString() + " failed validation. " + ErrorMessage);
                            Row = JournalWorksheet.RowsUsed().Count();
                        }
                        break;
                    case "Payment":
                        if (CurrentTransactionLedger.ValidateJournalPayment(JournalWorksheet, Row))
                        {
                            LedgerEntry? LE = CurrentTransactionLedger.ReadJournalPaymentEntry(JournalWorksheet, ref Row);

                            // apply the payment to existing invoices for this client
                            if (LE != null)
                            {
                                decimal Payment = LE.Credit;
                                string Customer = LE.ToWhom;
                                CurrentInvoices.ApplyPayment(Customer, Payment);

                                LE.Account = ChartOfAccounts.MainIncomeAccount;
                            }
                        }
                        else
                        {
                            MessageBox.Show("The journal payment in row " + Row.ToString() + " failed validation");
                            Row = JournalWorksheet.RowsUsed().Count();
                        }
                        break;
                    case "Invoice":
                        ReadJournalInvoice(JournalWorksheet, ref Row);
                        break;
                    // handle paychecks separately as we want to make sure that the accounting is set up 
                    // for the tax reporting
                    case "Paycheck":
                        if (CurrentTransactionLedger.ValidateJournalCheck(JournalWorksheet, Row, out ErrorMessage))
                        {
                            LedgerEntry LE = new();
                            if (!Journal.ParsePayCheck(LE, JournalWorksheet, ref Row)) return false;

                            // make sure primary account is in the chart of accounts

                            // because we can add to the list of current accounts, 
                            // get a new copy for each check

                            List<Account> CurrentAccountList = CurrentAccounts.GetCurrentList();
                            string PrimaryAccount = LE.GetPrimaryAccount();
                            if (!CurrentAccounts.IsThisAccountValid(PrimaryAccount))
                            {
                                MessageBox.Show("The primary account is not in the Chart of Accounts. It will be added.");
                                Account NAccount = ChartOfAccounts.CreateNewAccount(PrimaryAccount, Account.Type.CheckingAccount);
                                CurrentAccounts.InsertAccount(NAccount);
                            }

                            //// check the payee to make sure is in the list of payee
                            string payee = LE.ToWhom;
                            if (!ToPayTo.IsValidPayTo(payee))
                            {
                                PayTo nPay = PayToList.CreateNewPayTo(payee);
                                ToPayTo.AddPayTo(nPay);
                            }

                            // check all the sub accounts to make sure they are in the chart of accounts / converted to valid account

                            foreach (LedgerEntryBreakdown LEB in LE.SubAccounts)
                            {
                                string EntryAccount = LEB.AccountName;
                                if (!CurrentAccounts.IsThisAccountValid(EntryAccount))
                                {
                                    if (AccountConversion.ContainsKey(EntryAccount))
                                    {
                                        LEB.AccountName = AccountConversion[EntryAccount];
                                    }
                                    else
                                    {
                                        ErrorMessage = "The account " + EntryAccount
                                            + " is not found in the chart of accounts for "
                                            + LE.CheckNumber + " dated: " + LE.When.ToShortDateString()
                                            + " breakdown part " + LEB.Amount.ToString("0.00");
                                        MessageBox.Show(ErrorMessage);
                                        ChangeAccountForm CAF = new();
                                        CAF.Setup(CurrentAccountList, EntryAccount);
                                        CAF.ShowDialog();
                                        if (CAF.ToChangeTo.Length > 0)
                                        {
                                            // don't use the dictionary as we may need to split the sections
                                            // into different accounts - officer and employee payroll
                                            EntryAccount = CAF.ToChangeTo;
                                            //AccountConversion.Add(LEB.AccountName, CAF.ToChangeTo);
                                            LEB.AccountName = EntryAccount;
                                        }
                                        // If not converted, leave for sanity tests
                                    }
                                }

                            }

                            // add this check to the ledger
                            CurrentTransactionLedger.AddCheckEntry(LE);
                        }
                        else
                        {
                            MessageBox.Show("The journal check in row " + Row.ToString() + " failed validation. " + ErrorMessage);
                            Row = JournalWorksheet.RowsUsed().Count();
                        }
                        break;
                    case "Liability Check":
                        if (CurrentTransactionLedger.ValidateJournalCheck(JournalWorksheet, Row, out ErrorMessage))
                        {
                            LedgerEntry LE = new();
                            if (!Journal.ParsePayCheck(LE, JournalWorksheet, ref Row)) return false;

                            // make sure primary account is in the chart of accounts
                            // tax liabilities can go to state, federal, or local entities

                            // because we can add to the list of current accounts, 
                            // get a new copy for each check

                            List<Account> CurrentAccountList = CurrentAccounts.GetCurrentList();
                            string PrimaryAccount = LE.GetPrimaryAccount();
                            if (!CurrentAccounts.IsThisAccountValid(PrimaryAccount))
                            {
                                MessageBox.Show("The primary account is not in the Chart of Accounts. It will be added.");
                                Account NAccount = ChartOfAccounts.CreateNewAccount(PrimaryAccount, Account.Type.CheckingAccount);
                                CurrentAccounts.InsertAccount(NAccount);
                            }

                            // check the payee to make sure is in the list of payee

                            string payee = LE.ToWhom;
                            if (!ToPayTo.IsValidPayTo(payee))
                            {
                                PayTo nPay = PayToList.CreateNewPayTo(payee);
                                ToPayTo.AddPayTo(nPay);
                            }

                            // check all the sub accounts to make sure they are in the chart of accounts / converted to valid account

                            foreach (LedgerEntryBreakdown LEB in LE.SubAccounts)
                            {
                                string EntryAccount = LEB.AccountName;
                                if (!CurrentAccounts.IsThisAccountValid(EntryAccount))
                                {
                                    if (AccountConversion.ContainsKey(EntryAccount))
                                    {
                                        LEB.AccountName = AccountConversion[EntryAccount];
                                    }
                                    else
                                    {
                                        ErrorMessage = "The account " + EntryAccount
                                                + " is not found in the chart of accounts for "
                                                + LE.CheckNumber + " dated: " + LE.When.ToShortDateString()
                                                + " breakdown part " + LEB.Amount.ToString("0.00");
                                        MessageBox.Show(ErrorMessage);
                                        ChangeAccountForm CAF = new();
                                        CAF.Setup(CurrentAccountList, EntryAccount);
                                        CAF.ShowDialog();
                                        if (CAF.ToChangeTo.Length > 0)
                                        {
                                            // don't use the dictionary as we may need to split the sections
                                            // into different accounts - SS and Medicare
                                            EntryAccount = CAF.ToChangeTo;
                                            //AccountConversion.Add(LEB.AccountName, CAF.ToChangeTo);
                                            LEB.AccountName = EntryAccount;
                                        }
                                        // If not converted, leave for sanity tests
                                    }
                                }

                            }

                            // add this check to the ledger
                            CurrentTransactionLedger.AddCheckEntry(LE);
                        }
                        else
                        {
                            MessageBox.Show("The journal check in row " + Row.ToString() + " failed validation. " + ErrorMessage);
                            Row = JournalWorksheet.RowsUsed().Count();
                        }
                        break;
                    default: Row ++; break;

                }
            }

            // check file for consistency after reading
            CheckFileForConsistency();
            return true;
        }


        internal bool ReadJournalInvoice(IXLWorksheet JournalWorksheet, ref int Row)
        {
            Invoice Inv = new();
            if (!Journal.ParseInvoice(Inv, JournalWorksheet, ref Row)) return false;

            // Fill in the billing address from the customer list

            Customer? TCustomer = Customers.GetCustomerByID(Inv.CustomerIdentifier);
            // TBD - before adding the invoice, see if this invoice number is already in the list

            // make sure that the customer is on file
            if (TCustomer == null)
            {
                if (MessageBox.Show("Invoice is for a customer not on file," + Inv.CustomerIdentifier + " Add?", "Unknown Customer", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    // since this is on a different thread and this is not a user facing class,
                    // can't display the customer list management screen here
                    TCustomer = new()
                    {
                        CustomerIdentifier = Inv.CustomerIdentifier,
                        AccountName = Inv.CustomerIdentifier,
                        Address = "Unknown"
                    };
                    Customers.AddCustomer(TCustomer);
                    Inv.BillingAddress1 = Inv.CustomerIdentifier;
                    Inv.BillingAddress2 = "Unknown";
                    Inv.BillingAddress3 = "Unknown";
                }
                else
                {
                    Inv.BillingAddress1 = Inv.CustomerIdentifier;
                    Inv.BillingAddress2 = "Unknown";
                    Inv.BillingAddress3 = "Unknown";
                }

            }
            else
            {
                Inv.BillingAddress1 = TCustomer.AccountName;
                Inv.BillingAddress2 = TCustomer.Address;
                Inv.BillingAddress3 = TCustomer.City + ", " + TCustomer.State + " " + TCustomer.ZipCode;
            }

            CurrentInvoices.AddJournalInvoice(Inv); 
            return true;
        }

//        internal bool ReadCSVFile (string FileName)
//        {
            // this is from another program but is here for information use
            //using (StreamReader sr = new StreamReader(FileName))
            //{
            //    CsvHelper.Configuration.CsvConfiguration config = new CsvHelper.Configuration.CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture);

            //    config.Delimiter = ",";
            //    config.MissingFieldFound = null;
            //    config.TrimOptions = CsvHelper.Configuration.TrimOptions.Trim;
            //    config.HeaderValidated = null;

            //    using (var csvFile = new CsvReader(sr, config))
            //    {
            //        if (csvFile.Read())
            //        {
            //            csvFile.ReadHeader();
            //            while (csvFile.Read())
            //            {
            //                string test = csvFile.GetField(0).Trim();
            //                if (test == "AccountsEnd")
            //                    break;
            //                ActiveBook.Accounts.Add(csvFile.GetRecord<AccountCategory>());
            //            }
            //        }

            //        if (csvFile.Read())
            //        {
            //            csvFile.ReadHeader();
            //            int recordCount = 0;
            //            while (csvFile.Read())
            //            {
            //                int FieldCount = csvFile.Parser.Count;
            //                int FieldID = 0;
            //                LedgerEntry LE = new LedgerEntry();
            //                // temp code to read after latest bug
            //                //string fldContents = csvFile.GetField(FieldID);
            //                //DateTime TryDate;
            //                //if (!DateTime.TryParse(fldContents, out TryDate))
            //                //    FieldID++;
            //                LE.When = DateTime.Parse(csvFile.GetField(FieldID++));
            //                LE.CheckNumber = csvFile.GetField(FieldID++);
            //                LE.ToWhom = csvFile.GetField(FieldID++);
            //                LE.Cleared = Boolean.Parse(csvFile.GetField(FieldID++));
            //                LE.Debit = Decimal.Parse(csvFile.GetField(FieldID++));
            //                LE.Credit = Decimal.Parse(csvFile.GetField(FieldID++));
            //                LE.Balance = Decimal.Parse(csvFile.GetField(FieldID++));
            //                LE.Amount = Decimal.Parse(csvFile.GetField(FieldID++));
            //                LE.Account = csvFile.GetField(FieldID++);
            //                LE.ID = recordCount + 1;
            //                recordCount++;

            //                // if there are any sub categories, add them
            //                if (!string.IsNullOrEmpty(csvFile.GetField(FieldID)))
            //                {
            //                    LE.SubAccounts = new List<CategoryEntry>();

            //                    // field count is not accurate
            //                    while (FieldID < FieldCount)
            //                    {
            //                        CategoryEntry CE = new CategoryEntry();
            //                        CE.AccountName = csvFile.GetField(FieldID++);
            //                        CE.Notes = csvFile.GetField(FieldID++);
            //                        // test if all three fields are blank, if so, quit
            //                        string testDecimal = csvFile.GetField(FieldID++);
            //                        if ((CE.AccountName.Trim() == "") && (CE.Notes.Trim() == "") && (testDecimal.Trim() == ""))
            //                            break;
            //                        decimal realDecimal;
            //                        if (Decimal.TryParse(testDecimal, out realDecimal))
            //                        {
            //                            CE.Amount = realDecimal;
            //                            LE.SubAccounts.Add(CE);
            //                        }
            //                        else
            //                        {
            //                            MessageBox.Show("Invalid amount in subcategory, column#:", (FieldID - 1).ToString());
            //                            break;
            //                        }
            //                    }
            //                }

            //                ActiveBook.CurrentLedger.Add(LE);
            //            }
            //        }
            //    }
            //}

//            return false;
//        }

        internal bool CheckFileForConsistency()
        {
            // check the chart of accounts to make sure all are unique names

            if (!CurrentAccounts.CheckForConsistency()) return false;

            // check if all transactions are in sequential date order
            // check if all transaction balances match running total to that point
            if (!CurrentTransactionLedger.CheckForConsistency()) return false;

            // make sure all amounts are allocated to an account
            // insure all accounts are found in the Chart of Accounts

            if (!CurrentTransactionLedger.CheckAccounts(CurrentAccounts.GetCurrentList())) return false;


            // make sure all checks are written to someone in the PayTo list

            if (!CurrentTransactionLedger.CheckPayTo(ToPayTo.GetCurrentList())) return false;

            // TBD - make sure all deposits are from some entity in the Customer list

            // TBD - make sure all accounts listed on an invoice are in the chart of accounts

            return true;
        }

        internal int GetNextInvoiceNumber()
        {
            int LastInvoiceNumber = CurrentInvoices.GetlastInvoiceNumber();
            if (LastInvoiceNumber == 0)
                return Int32.Parse(CompanyInformation.GetParameter(CompanyParameters.FirstInvoiceNumber));
            return LastInvoiceNumber + 1;
        }



        internal void WriteCheckBook(XLWorkbook CurrentWorkbook)
        {
            CurrentAccounts.SetSheetFormat();
            CurrentAccounts.WriteChartOfAccounts(CurrentWorkbook);
            int MaxBreakdowns = CurrentTransactionLedger.GetMaxBreakdowns();
            CurrentTransactionLedger.SetSheetFormat(MaxBreakdowns);
            CurrentTransactionLedger.WriteXLLedger(CurrentWorkbook);
            CurrentInvoices.WriteXLInvoices(CurrentWorkbook);
            Customers.SetSheetFormat();
            Customers.WriteXLCustomerList(CurrentWorkbook);
            ToPayTo.SetSheetFormat();
            ToPayTo.WriteXLPayToList(CurrentWorkbook);
            CompanyInformation.SetSheetFormat();
            CompanyInformation.WriteXLParameterList(CurrentWorkbook);
        }
    }
}
