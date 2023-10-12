//**********************************************************************
//
//          Copyright © 2023 Prairie Trail Software, Inc.
//
//**********************************************************************

using BusinessCheckBook.Validation;
using ClosedXML.Excel;

namespace BusinessCheckBook.DataStore
{
    public class ChartOfAccounts
    {
        // storage of the chart of accounts
        List<Account> CurrentAccounts { get; set; } = new();

        // what the Excel sheet is supposed to look like
        private SheetFormat ChartOfAccountsFormat { get; set; } = new();

        // some standard accounts that get used in reconciliation

        public const string InterestEarnedAccount = "InterestEarned";
        public const string BankFeesAccount = "BankFee";
        public static string MainIncomeAccount = "Consulting";

        ActivityLogger? Logger { get; set; } = null;
        string sNewAccount = "New Account";
        //string sAccountModified = "Account Modified";
        string sAccount = "Account";

        public ChartOfAccounts()
        {
            Initialize();
        }

        public ChartOfAccounts(ActivityLogger logger)
        {
            Logger = logger;
            Initialize();
        }

        internal void Initialize()
        {
            CurrentAccounts = new List<Account>();
            SetSheetFormat();
        }

        internal List<Account> GetCurrentList()
        {
            return CurrentAccounts;
        }
        internal static Account CreateNewAccount(string AccountName, Account.Type WhichType) => 
            new ()
        {
            Name = AccountName,
            WhatType = WhichType,
            Description = AccountName
        };

        internal Account GetThisAccount(int row) { return CurrentAccounts[row]; }
        internal bool IsThisAccountValid (string AccountToTest)
        {
            foreach (Account account in CurrentAccounts)
            {
                if (account.Name == AccountToTest) return true;
            }
            if (AccountToTest.ToLower() == "split") return true;
            return false;   
        }

        internal List<string> GetMatchingPayToNames(string ToMatch)
        {
            // the version for individual only went back a year. 
            // For business simply use currently active accounts

            return (from ch in CurrentAccounts
                    where ch.Name.ToUpper().StartsWith(ToMatch.ToUpper())
                       && ch.IsActive
                    orderby ch.Name
                    select ch.Name).Distinct().ToList();
        }


        // don't use a simple add
        // The insert account does a better job of preserving the order
        //internal void Add(Account account)
        //{
        //    CurrentAccounts.Add(account);
        //    SortCurrentAccounts();
        //}

        internal void AddIIFAccount(string[] fields)
        {
            Account IIFAccount = new();
            if (IIFAccount.ParseIIFRow(fields) != null)
            {
                CurrentAccounts.Add(IIFAccount);
                Logger?.LogObject(sNewAccount, sAccount, IIFAccount);
            }
        }

        internal void InsertAccount (Account NewAccount)
        {
            // because we use the insert while reading the existing file
            // we don't want to log all those inserts every time
            // make sure the log file is null when reading the Excel file

            bool Inserted;
            switch (NewAccount.WhatType)
            {
                case Account.Type.CheckingAccount:
                    // put as the first account in the list
                    CurrentAccounts.Insert(0, NewAccount);
                    Logger?.LogObject(sNewAccount, sAccount, NewAccount);
                    break;
                case Account.Type.Income:
                    // find the income account that is more than this one and add before that

                    Inserted = false;
                    for (int i = 0; i < CurrentAccounts.Count; i ++)
                    {
                        Account Acc = CurrentAccounts[i];
                        if (Acc.WhatType == Account.Type.CheckingAccount) continue;
                        if (Acc.WhatType != Account.Type.Income)
                        {
                            CurrentAccounts.Insert (i, NewAccount);
                            Logger?.LogObject(sNewAccount, sAccount, NewAccount);
                            Inserted = true;
                            break;
                        }
                        if (string.Compare(NewAccount.Name, Acc.Name,StringComparison.CurrentCulture) < 0)
                        {
                            CurrentAccounts.Insert(i, NewAccount);
                            Logger?.LogObject(sNewAccount, sAccount, NewAccount);
                            Inserted = true;
                            break;
                        }
                    }
                    // if a place wasn't found, simply add this as that means that 
                    // there aren't any accounts in the chart
                    if (!Inserted)
                    {
                        CurrentAccounts.Add(NewAccount);
                        Logger?.LogObject(sNewAccount, sAccount, NewAccount);
                    }
                    break;
                case Account.Type.Expense:
                    // find the expense that is after this one
                    Inserted = false;
                    for (int i = 0; i < CurrentAccounts.Count; i++)
                    {
                        Account Acc = CurrentAccounts[i];
                        if (Acc.WhatType == Account.Type.CheckingAccount) continue;
                        if (Acc.WhatType == Account.Type.Income) continue;
                        if (Acc.WhatType == Account.Type.Expense)
                        {
                            if (string.Compare(NewAccount.Name, Acc.Name, StringComparison.CurrentCulture) < 0)
                            {
                                CurrentAccounts.Insert(i, NewAccount);
                                Logger?.LogObject(sNewAccount, sAccount, NewAccount);
                                Inserted = true;
                                break;
                            }
                        }
                    }
                    // add if not inserted
                    if (!Inserted)
                    {
                        CurrentAccounts.Add(NewAccount);
                        Logger?.LogObject(sNewAccount, sAccount, NewAccount);
                    }
                    break;
                case Account.Type.SubAccount:
                    // find the expense that this is a sub account to
                    Inserted = false;
                    for (int i = 0; i < CurrentAccounts.Count; i++)
                    {
                        Account Acc = CurrentAccounts[i];
                        if (Acc.WhatType == Account.Type.CheckingAccount) continue;
                        if (Acc.WhatType == Account.Type.Income) continue;
                        if (Acc.WhatType == Account.Type.Expense)
                        {
                            if (Acc.Name == NewAccount.SubAccountOf)
                            {
                                // find where in the list of subaccounts to add this one...
                                for (int j = i + 1; j < CurrentAccounts.Count; j++)
                                {
                                    Acc = CurrentAccounts[j];
                                    if (Acc.WhatType !=  Account.Type.SubAccount)
                                    {
                                        CurrentAccounts.Insert(j, NewAccount);
                                        Logger?.LogObject(sNewAccount, sAccount, NewAccount);
                                        Inserted = true;
                                        break;
                                    }
                                    if (string.Compare(NewAccount.Name, Acc.Name, StringComparison.CurrentCulture) < 0)
                                    {
                                        CurrentAccounts.Insert(j, NewAccount);
                                        Logger?.LogObject(sNewAccount, sAccount, NewAccount);
                                        Inserted = true;
                                        break;
                                    }
                                }
                                if (!Inserted)
                                {
                                    CurrentAccounts.Add(NewAccount);
                                    Logger?.LogObject(sNewAccount, sAccount, NewAccount);
                                }
                            }
                            }
                    }
                    break;
                case Account.Type.Withholdings:
                    CurrentAccounts.Add(NewAccount);
                    Logger?.LogObject(sNewAccount, sAccount, NewAccount);
                    break;
                default:
                    CurrentAccounts.Add(NewAccount);
                    Logger?.LogObject(sNewAccount, sAccount, NewAccount);
                    break;
            }
        }



        internal List<string> GetListOfAccounts()
        {
            List<string> results = new();
            foreach (Account account in CurrentAccounts)
            {
                if (account.IsActive)
                {
                    if (account.SubAccountOf?.Length > 0)
                    {
                        string fullname = account.SubAccountOf + ":" + account.Name;
                        Account? parentAccount = CurrentAccounts.FirstOrDefault(c => c.Name == account.SubAccountOf);
                        while ((parentAccount != null) && (parentAccount.SubAccountOf?.Length > 0))
                        {
                            fullname = parentAccount.SubAccountOf + ":" + fullname;
                            parentAccount = CurrentAccounts.FirstOrDefault(c => c.Name == parentAccount.SubAccountOf);
                        }
                        results.Add(fullname);
                    }
                    else
                    {
                        results.Add(account.Name);
                    }
                }
            }
            return results;
        }
        internal string? GetFirstExpenseAccount()
        {
            return (from act in CurrentAccounts
                    where act.WhatType == Account.Type.Expense
                    select act.Name).FirstOrDefault();
        }

        #region Validation

        // each account needs to have a unique name
        internal bool CheckForConsistency ()
        {
            for (int i = 0; i < CurrentAccounts.Count-1; i ++)
            {
                for (int j = i+1; j < CurrentAccounts.Count; j ++)
                {
                    if (CurrentAccounts[i].Name == CurrentAccounts[j].Name)
                    {
                        MessageBox.Show("There are two accounts with the same name " 
                            + CurrentAccounts[i].Name + " in the chart of accounts.");
                        return false;
                    }
                }
            }
            return true;
        }

        internal bool Validate(XLWorkbook CheckBookXlsx, out string ErrorMessage)
        {
            ErrorMessage = "";
            IXLWorksheet AccountsWorksheet;
            try
            {
                AccountsWorksheet = CheckBookXlsx.Worksheet(ChartOfAccountsFormat.SheetName);
            }
            catch (Exception ex)
            {
                ErrorMessage = "Missing the Chart of Account sheet " + ex.Message;
                return false;
            }

            // the sheet exists. check all the headings and find which column is which

            try
            {
                for (int AccountNum = 0; AccountNum < ChartOfAccountsFormat.Count(); AccountNum++)
                {
                    ColumnFormat col = ChartOfAccountsFormat.Column(AccountNum);
                    if (col == null) continue;
                    bool headerNotFound = true;
                    for (int ColNum = 1; ColNum < AccountsWorksheet.ColumnCount(); ColNum++)
                    {
                        string HeaderValue = AccountsWorksheet.Cell(1, ColNum).GetString();
                        if (col.Name == HeaderValue)
                        {
                            headerNotFound = false;
                            col.ColumnNumber = ColNum;
                            break;
                        }
                    }
                    if (headerNotFound)
                    {
                        if (col.RequiredColumn)
                        {
                            ErrorMessage = "The column " + col.Name + " is not in the chart of accounts";
                            return false;
                        }
                    }

                }

                // validate all the entries in the columns for validity
                // at this point, we have all the columns that are required

                int RowsUsedCount = AccountsWorksheet.RowsUsed().Count();
                for (int Row = 2; Row < RowsUsedCount; Row++)
                {
                    IXLRow XRow = AccountsWorksheet.Row(Row);
                    if (!Account.ValidateExcelRow(XRow, ChartOfAccountsFormat, out ErrorMessage))
                    {
                        ErrorMessage += " in row " + Row.ToString();
                        return false;
                    }
                }


                // make sure that the sub accounts reference an existing account
                string SubAccountColumnName = Account.GetSubAccountColumnName();
                string AccountColumnName = Account.GetAccountColumnName();  

                for (int Row = 2; Row < RowsUsedCount; Row++)
                {
                    // check if this row has a subaccountof value

                    ColumnFormat ThisColumn = ChartOfAccountsFormat.Column(SubAccountColumnName)!;
                    string TSubField = AccountsWorksheet.Cell(Row, ThisColumn.ColumnNumber).GetString();
                    if (TSubField != null)
                    {
                        if (TSubField.Length > 0)
                        {
                            bool NoAccountFound = true;
                            ThisColumn = ChartOfAccountsFormat.Column(AccountColumnName)!;
                            for (int i = 2; i < AccountsWorksheet.RowCount(); i++)
                            {
                                if (i == Row) continue; // don't allow subaccount of itself
                                string TName = AccountsWorksheet.Cell(i, ThisColumn.ColumnNumber).GetString();
                                if (TSubField.ToLower() == TName.ToLower())
                                {
                                    NoAccountFound = false; break;
                                }
                            }

                            if (NoAccountFound)
                            {
                                ErrorMessage = "Invalid account subfield in row " + Row.ToString() + " " + TSubField;
                                return false;
                            }

                        }
                    }
                }


                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = "Error in reading a cell " + ex.Message;
                return false;
            }
        }


        // used on user entry
        internal bool ValidAccountFields(string AccountType, string AccountName, string AccountDescription, string AccountSubField, string AccountFedField)
        {
            return Account.ValidAccountFields(ChartOfAccountsFormat, AccountType, AccountName, AccountDescription, AccountSubField, AccountFedField);
        }
        internal bool ValidIIFAccountFields(string[] fields, out string ErrorMessage)
        {
            return Account.ValidateIIFFields(fields, ChartOfAccountsFormat, out ErrorMessage);
        }

        #endregion Validation

        internal bool ReadFromExcelFile(XLWorkbook CheckBookXlsx)
        {
            CurrentAccounts = new();
            IXLWorksheet AccountsWorksheet = CheckBookXlsx.Worksheet(ChartOfAccountsFormat.SheetName);
            if (AccountsWorksheet != null)
            {
                int Row = 0;
                foreach (var row in AccountsWorksheet.RowsUsed())
                {
                    // The header row was processed in the validation stage and we can use those values

                    Row++;
                    if (Row == 1) continue;
                    IXLRow XRow = AccountsWorksheet.Row(Row);

                    // Pull off the Chart of Accounts Values

                    Account TAccount = new();
                    TAccount.ParseExcelRow(XRow, ChartOfAccountsFormat);
                    InsertAccount(TAccount);
                }
                return true;
            }
            return false;
        }


 
        // this sort didn't work very well. 
        // it needs to sort first on account type, then account name, then sort the sub accounts
        //private void SortCurrentAccounts()
        //{
        //    List<Account> NewList = new();
        //    AccountComparer CompareAccountNames = new();


        //    // first get all the top level accounts
        //    foreach (Account account in CurrentAccounts)
        //    {
        //        if (account.SubAccountOf == null || account.SubAccountOf.Length == 0)
        //            NewList.Add(account);
        //    }
        //    NewList.Sort(CompareAccountNames);

        //    // Now add in all the sub accounts for each top level account

        //    int AccountID = 0;
        //    while (AccountID < NewList.Count)
        //    {
        //        Account account = NewList[AccountID];
        //        if (account.WhatType != Account.Type.SubAccount)
        //        {
        //            // find all the sub accounts of this account
        //            List<Account> SubAccounts = new();
        //            foreach (Account possibleaccount in CurrentAccounts)
        //            {
        //                if (possibleaccount.SubAccountOf == account.Name)
        //                    SubAccounts.Add(possibleaccount);
        //            }
        //            if (SubAccounts.Count > 0)
        //            {
        //                SubAccounts.Sort(CompareAccountNames);

        //                int WhereToInsert = NewList.IndexOf(account) + 1; // do this as a previous insert may have changed the index
        //                NewList.InsertRange(WhereToInsert, SubAccounts);
        //            }
        //        }
        //        AccountID++;
        //    }

        //    CurrentAccounts = NewList;
        //}


        internal void CreateChartOfAccounts()
        {
            BuildDefaultAccounts();
        }

        internal void WriteChartOfAccounts(XLWorkbook CheckBookXlsx)
        {
            // add the chart of accounts worksheet
            CheckBookXlsx.AddWorksheet(ChartOfAccountsFormat.SheetName);
            IXLWorksheet AccountsWorksheet = CheckBookXlsx.Worksheet(ChartOfAccountsFormat.SheetName);

            // first build the header

            for (int i = 0; i < ChartOfAccountsFormat.Count(); i++)
            {
                ColumnFormat Col = ChartOfAccountsFormat.Column(i);
                AccountsWorksheet.Cell(1, Col.ColumnNumber).Value = Col.Name;
            }
            // then add all the rows

            int Row = 2;
            foreach (Account TAccount in CurrentAccounts)
            {
                IXLRow xlRow = AccountsWorksheet.Row(Row);
                TAccount.WriteExcelRow(xlRow, ChartOfAccountsFormat);
                Row++;
            }
        }

        internal void SetSheetFormat()
        {
            ChartOfAccountsFormat = new();
            ChartOfAccountsFormat.SheetName = "ChartOfAccounts";
            Account.AddSheetColumns(ChartOfAccountsFormat);
        }

        private void BuildDefaultAccounts()
        {
            CurrentAccounts.Clear();
            CurrentAccounts.Add(new Account(true, Account.Type.Income, "InitialBalance", "Initial Balance", "", "L1b. Cash"));
            CurrentAccounts.Add(new Account(true, Account.Type.Income, "CashSales", "Cash Sales", "", "1a. Gross Receipts"));
            CurrentAccounts.Add(new Account(true, Account.Type.Income, "Consulting", "Consulting", "", "1a. Gross Receipts"));
            CurrentAccounts.Add(new Account(true, Account.Type.Income, InterestEarnedAccount, "Interest Earned", "", "5. Interest"));
            CurrentAccounts.Add(new Account(true, Account.Type.Income, "Loan", "Borrowing", "", "1a. Gross Receipts"));
            CurrentAccounts.Add(new Account(true, Account.Type.Income, "ServiceIncome", "Income from services rendered", "", "1a. Gross Receipts"));
            CurrentAccounts.Add(new Account(true, Account.Type.Expense, BankFeesAccount, "Bank fees", "", "18. Interest"));
            CurrentAccounts.Add(new Account(true, Account.Type.Expense, "BorrowingExpenses", "Various Expenses with Loans", "", "18. Interest"));
            CurrentAccounts.Add(new Account(true, Account.Type.Expense, "Interest", "Interest on Loans", "BorrowingExpenses", "18. Interest"));
            CurrentAccounts.Add(new Account(true, Account.Type.Expense, "PointsAndFees", "Points or Fees on Loans", "BorrowingExpenses", "18. Interest"));
            CurrentAccounts.Add(new Account(true, Account.Type.Expense, "Repayment", "Repaying Loans", "BorrowingExpenses", ""));
            CurrentAccounts.Add(new Account(true, Account.Type.Expense, "Charity", "Donations", "", "19. Charitable contributions"));
            CurrentAccounts.Add(new Account(true, Account.Type.Expense, "CostOfGoods", "Cost of goods sold", "", "2. Cost Of Goods"));
            CurrentAccounts.Add(new Account(true, Account.Type.Expense, "CostOfService", "Cost of service resold", "", "26. Other deductions"));
            CurrentAccounts.Add(new Account(true, Account.Type.Expense, "CreditCardFees", "Merchant Service Fees", "", "26. Other deductions"));
            CurrentAccounts.Add(new Account(true, Account.Type.Expense, "Insurance", "Insurance", "", "26. Other deductions"));
            CurrentAccounts.Add(new Account(true, Account.Type.Expense, "LicenseAndPermits", "Licenses and Permits", "", "26. Other deductions"));
            CurrentAccounts.Add(new Account(true, Account.Type.Expense, "Marketing", "Marketing", "", "22. Advertising"));
            CurrentAccounts.Add(new Account(true, Account.Type.Expense, "Ads", "Advertising", "Marketing", "22. Advertising"));
            CurrentAccounts.Add(new Account(true, Account.Type.Expense, "Commissions", "Commissions to get sales", "Marketing", "22. Advertising"));
            CurrentAccounts.Add(new Account(true, Account.Type.Expense, "CustomerMeetings", "Costs of meeting with customer", "Marketing", "22. Advertising"));
            CurrentAccounts.Add(new Account(true, Account.Type.Expense, "Internal", "Internal marketing costs", "Marketing", "22. Advertising"));
            CurrentAccounts.Add(new Account(true, Account.Type.Expense, "Internet", "Internet", "Marketing", "22. Advertising"));
            CurrentAccounts.Add(new Account(true, Account.Type.Expense, "Phone", "Phone line costs", "Marketing", "22. Advertising"));
            CurrentAccounts.Add(new Account(true, Account.Type.Expense, "Shows", "Show expenses", "Marketing", "22. Advertising"));
            CurrentAccounts.Add(new Account(true, Account.Type.Expense, "Misc", "Miscellaneous", "", "26. Other deductions"));
            CurrentAccounts.Add(new Account(true, Account.Type.Expense, "Office", "Costs to keep office open", "", "26. Other deductions"));
            CurrentAccounts.Add(new Account(true, Account.Type.Expense, "Chamber", "Chamber Dues", "Office", "26. Other deductions"));
            CurrentAccounts.Add(new Account(true, Account.Type.Expense, "ExpenseRpt", "Expense reports", "Office", "26. Other deductions"));
            CurrentAccounts.Add(new Account(true, Account.Type.Expense, "Internet", "Internet", "Office", "26. Other deductions"));
            CurrentAccounts.Add(new Account(true, Account.Type.Expense, "Insurance", "Insurance", "Office", "26. Other deductions"));
            CurrentAccounts.Add(new Account(true, Account.Type.Expense, "MedicalInsurance", "Medical Insurance", "Office", "24. Employee Benefit programs"));
            CurrentAccounts.Add(new Account(true, Account.Type.Expense, "Phone", "Phone", "Office", "26. Other deductions"));
            CurrentAccounts.Add(new Account(true, Account.Type.Expense, "Rent", "Rent paid", "Office", "16. Rents"));
            CurrentAccounts.Add(new Account(true, Account.Type.Expense, "Repairs", "Repairs", "Office", "14. Repairs and maintenance"));
            CurrentAccounts.Add(new Account(true, Account.Type.Expense, "Subscriptions", "Subscriptions", "Office", "26. Other deductions"));
            CurrentAccounts.Add(new Account(true, Account.Type.Expense, "Stamps", "Stamps", "Office", "26. Other deductions"));
            CurrentAccounts.Add(new Account(true, Account.Type.Expense, "Supplies", "Supplies", "Office", "26. Other deductions"));
            CurrentAccounts.Add(new Account(true, Account.Type.Expense, "Temps", "Temporary Help", "Office", "13. Salaries and wages"));
            CurrentAccounts.Add(new Account(true, Account.Type.Expense, "Utillities", "Utillities", "Office", "26. Other deductions"));
            CurrentAccounts.Add(new Account(true, Account.Type.Expense, "Taxes", "Taxes", "", "17. Taxes & Licenses"));
            CurrentAccounts.Add(new Account(true, Account.Type.Expense, "Accountant", "Tax Accountant", "Taxes", "17. Taxes & Licenses"));
            CurrentAccounts.Add(new Account(true, Account.Type.Expense, "Federal", "Federal Taxes", "Taxes", "17. Taxes & Licenses"));
            CurrentAccounts.Add(new Account(true, Account.Type.Expense, "SocialSecurity", "Bus. Portion of Social Security", "Taxes", "17. Taxes & Licenses"));
            CurrentAccounts.Add(new Account(true, Account.Type.Expense, "Medicare", "Bus. Portion of Medicare", "Taxes", "17. Taxes & Licenses"));
            CurrentAccounts.Add(new Account(true, Account.Type.Expense, "State", "State Taxes", "Taxes", "17. Taxes & Licenses"));
            CurrentAccounts.Add(new Account(true, Account.Type.Expense, "Local", "Local Taxes", "Taxes", "17. Taxes & Licenses"));
            CurrentAccounts.Add(new Account(true, Account.Type.Expense, "Tools", "Tools", "", "20. Depreciation"));
            CurrentAccounts.Add(new Account(true, Account.Type.Expense, "TravelExpenses", "Expenses for travel", "", "26. Other deductions"));
            CurrentAccounts.Add(new Account(true, Account.Type.Withholdings, "Withholdings", "Withholding from Employee Paychecks", "", ""));
        }

    }
}
