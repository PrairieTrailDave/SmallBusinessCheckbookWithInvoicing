//**********************************************************************
//
//          Copyright © 2023 Prairie Trail Software, Inc.
//
//**********************************************************************

using BusinessCheckBook.Validation;
using ClosedXML.Excel;

namespace BusinessCheckBook.DataStore
{
    internal class TransactionLedger
    {
        internal List<LedgerEntry> CurrentLedger { get; set; } = new();
        internal SheetFormat LedgerFormat { get; set; } = new();

        internal int FirstCheckNumber { get; set; }

        readonly string SInitialBalance = "Initial Balance";

        private bool Changed;



        public TransactionLedger()
        {
            CurrentLedger = new List<LedgerEntry>();
            SetSheetFormat(0);
            Changed = false;

            FirstCheckNumber = 0;
        }

        public bool IfChanged() { return Changed; }
        public void HasChanged() { Changed = true; }
        public void ClearChanged() { Changed = false; }



        // the problem is that when we declare a new ledger,
        // we wipe out the breakdown formats that were built when
        // the validate step happens. We need to rebuild them.
        internal void BuildBreakdownColumnFormats (XLWorkbook CheckBookXlsx)
        {
            IXLWorksheet TransactionsWorksheet;
            TransactionsWorksheet = CheckBookXlsx.Worksheet(LedgerFormat.SheetName);
            _ = LedgerEntry.BuildSubFormats(TransactionsWorksheet, LedgerFormat, out _);

        }

        internal void AddInitialBalance (decimal InitialBalance)
        {
            if (CurrentLedger.Count == 0)
            {
                LedgerEntry IBalance = new();
                IBalance.When = DateTime.Now;
                IBalance.ID = 1;
                IBalance.ToWhom = SInitialBalance;
                IBalance.Amount = InitialBalance;
                IBalance.Account = "InitialBalance";
                IBalance.Balance = InitialBalance;
                CurrentLedger.Add(IBalance);
                HasChanged();
            }
            else
                throw new Exception("Trying to add an initial balance when transactions already exist in ledger");
        }

        internal void SetFirstCheckNumber (int nFirstCheckNumber)
        {
            if (CurrentLedger.Count == 0)
            {
                FirstCheckNumber = nFirstCheckNumber;
                HasChanged();
            }
            else
            {
                if (CurrentLedger[0].ToWhom == SInitialBalance)
                {
                    FirstCheckNumber = nFirstCheckNumber;
                    HasChanged();
                }
                else
                    throw new Exception("Trying to set the first check number when transactions already exist in ledger");
            }
        }
        internal List<LedgerEntry> GetCurrentList()
        { return CurrentLedger; }

        internal List<LedgerEntry> GetLedgerForAYear(int Year)
        {
            return (from ld in  CurrentLedger
                    where ld.When.Year == Year
                    select ld).ToList();
        }


        internal LedgerEntry GetLedgerEntry(int  index) { return CurrentLedger[index]; }

        internal decimal GetCurrentBalance ()
        { 
            if (CurrentLedger.Count > 1)
                return CurrentLedger[CurrentLedger.Count - 1].Balance; 
            if (CurrentLedger.Count == 1)
                return CurrentLedger[0].Balance;
            return 0.00M;
        }

        // this recalculates the reconciled balance each time
        internal decimal GetCurrentReceonciledBalance ()
        {
            return
            (from ch in CurrentLedger
             where ch.Cleared == true &&
                   ch.Credit > 0.00M
             select ch.Credit).Sum()
                                 - (from ch in CurrentLedger
                                    where ch.Cleared == true &&
                                          ch.Debit > 0.00M
                                    select ch.Debit).Sum();
        }
        internal int GetLastCheckNumber()
        {
            if (CurrentLedger.Count > 1)
            {
                string sLastCheckNumber = (from ch in CurrentLedger
                                           where ch.CheckNumber.Length > 0
                                              && Char.IsDigit(ch.CheckNumber[0])
                                           orderby ch.CheckNumber descending
                                           select ch.CheckNumber).First();
                return Int32.Parse(sLastCheckNumber);
            }
            if (CurrentLedger.Count == 1)
            {
                if (CurrentLedger[0].ToWhom == SInitialBalance)
                    return FirstCheckNumber;
                else
                    return Int32.Parse(CurrentLedger[0].CheckNumber);
            }
            return FirstCheckNumber;
        }

        internal LedgerEntry CreateLedgerEntry (DateTime when, string checkNumber, string toWhom, decimal debit, decimal credit, decimal balance, decimal amount,
            string account, List<LedgerEntryBreakdown>? subAccounts)
            
        {
            return new LedgerEntry
            {
                When = when,
                CheckNumber = checkNumber,
                ToWhom = toWhom,
                Cleared = false,
                Debit = debit,
                Credit = credit,
                Balance = balance,
                Amount = amount,
                Account = account,
                ID = CurrentLedger.Count + 1,
                SubAccounts = subAccounts ?? new()
            };
        }

        // used to find unreconciled checks for the reconciliation 
        internal List<LedgerEntry> GetOutstandingChecks ()
        {
            return (from ch in CurrentLedger
                    where ch.Cleared == false &&
                          ch.Debit > 0.00M
                    select ch).ToList();
        }

        // used to find unreconciled deposits for the reconciliation 
        internal List<LedgerEntry> GetOutstandingDeposits()
        {
            return (from ch in CurrentLedger
                    where ch.Cleared == false &&
                          ch.Credit > 0.00M
                    select ch).ToList();
        }


        internal LedgerEntry? FindLastTransactionFor (string ToPayTo)
        {
            return (from ch in CurrentLedger
                    where ch.ToWhom.ToUpper() == ToPayTo.ToUpper()
                    orderby ch.When descending
                    select ch).FirstOrDefault();
        }
        internal int InsertTransaction(LedgerEntry newEntry)
        {
            // if there are no transaction in the ledger,
            // simply add this transaction

            if (CurrentLedger.Count == 0)
            {
                newEntry.Balance = newEntry.Credit - newEntry.Debit;
                CurrentLedger.Add(newEntry);
                HasChanged();
                return CurrentLedger.Count - 1;
            }
            else
            {
                // find where to insert this transaction

                // date of this transaction

                DateTime DateOfThisTransaction = newEntry.When.Date;

                // get the first transaction after yesterday

                int LedgerIndex = 0;
                while (LedgerIndex < CurrentLedger.Count)
                {
                    if (CurrentLedger[LedgerIndex].When.Date < DateOfThisTransaction)
                        LedgerIndex++;
                    else
                        break;
                }

                // if no more entries today or later, then simply append

                if (LedgerIndex >= CurrentLedger.Count)
                {
                    newEntry.Balance = CurrentLedger[LedgerIndex - 1].Balance + newEntry.Credit - newEntry.Debit;
                    CurrentLedger.Add(newEntry);
                    HasChanged();
                    return newEntry.ID;
                }
                else
                {
                    // if this is a deposit, 
                    // insert it at the start of the day's transactions

                    if (newEntry.Credit > 0.00M)
                    {
                        newEntry.Balance = CurrentLedger[LedgerIndex - 1].Balance + newEntry.Credit - newEntry.Debit;
                        CurrentLedger.Insert(LedgerIndex, newEntry);
                        UpdateFollowingTransactionBalances(LedgerIndex);
                        HasChanged();
                        return newEntry.ID;
                    }
                    else
                    {
                        // if a check, if it has a check number, 
                        // insert after the prior check number of today
                        if (newEntry.CheckNumber.Length > 0)
                        {
                            // it is possible to put characters in the check number
                            // if it doesn't parse, skip this 

                            int EcheckNum;
                            if (int.TryParse(newEntry.CheckNumber, out EcheckNum))
                            {
                                int secondIndex = LedgerIndex;
                                while (secondIndex < CurrentLedger.Count)
                                {
                                    // if past today, insert at this point
                                    if (CurrentLedger[secondIndex].When.Date > DateOfThisTransaction)
                                        break;
                                    // if has a check number, compare that
                                    if (CurrentLedger[secondIndex].CheckNumber.Length > 0)
                                    {
                                        int LcheckNum;
                                        if (int.TryParse(CurrentLedger[secondIndex].CheckNumber, out LcheckNum))
                                        {
                                            if (EcheckNum < LcheckNum)
                                                break;
                                        }
                                        else
                                            break;
                                    }
                                    // if no check number, insert at this point
                                    else
                                        break;
                                    secondIndex++;
                                }
                                // if today is the last day in the ledger, 
                                // simply add it
                                if (secondIndex >= CurrentLedger.Count)
                                {
                                    newEntry.Balance = CurrentLedger[secondIndex - 1].Balance + newEntry.Credit - newEntry.Debit;
                                    CurrentLedger.Add(newEntry);
                                    HasChanged();
                                    return newEntry.ID;
                                }
                                else
                                {
                                    newEntry.Balance = CurrentLedger[secondIndex - 1].Balance + newEntry.Credit - newEntry.Debit;
                                    CurrentLedger.Insert(secondIndex, newEntry);
                                    UpdateFollowingTransactionBalances(secondIndex);
                                    HasChanged();
                                    return newEntry.ID;
                                }

                            }
                        }
                        else
                        // doesn't have a check number, 
                        // place it after all the check number entries for today
                        {
                            int secondIndex = LedgerIndex;
                            while (secondIndex < CurrentLedger.Count)
                            {
                                // if past today, insert at this point
                                if (CurrentLedger[secondIndex].When.Date > DateOfThisTransaction)
                                    break;
                                // if has a check number, keep going
                                if (CurrentLedger[secondIndex].CheckNumber.Length > 0)
                                {
                                }
                                // if no check number, insert at this point
                                else
                                    break;
                                secondIndex++;
                            }
                            // if today is the last day in the ledger, 
                            // simply add it
                            if (secondIndex >= CurrentLedger.Count)
                            {
                                newEntry.Balance = CurrentLedger[secondIndex - 1].Balance + newEntry.Credit - newEntry.Debit;
                                CurrentLedger.Add(newEntry);
                                HasChanged();
                                return newEntry.ID;
                            }
                            else
                            {
                                newEntry.Balance = CurrentLedger[secondIndex - 1].Balance + newEntry.Credit - newEntry.Debit;
                                CurrentLedger.Insert(secondIndex, newEntry);
                                UpdateFollowingTransactionBalances(secondIndex);
                                HasChanged();
                                return newEntry.ID;
                            }
                        }
                    }
                    // don't think it should get here, but if it does, simply add to end
                    newEntry.Balance = CurrentLedger[LedgerIndex - 1].Balance + newEntry.Credit - newEntry.Debit;
                    CurrentLedger.Add(newEntry);
                    HasChanged();
                    return newEntry.ID;
                }
            }
        }

        public void VoidThisTransaction(int LedgerEntryID)
        {
            // only act if there are any transactions in the ledger

            if (CurrentLedger.Count > 0)
            {
                // and only if this is a valid entry

                for (int EntryNumber = 0; EntryNumber < CurrentLedger.Count; EntryNumber++)
                {
                    LedgerEntry LE = CurrentLedger[EntryNumber];
                    if (LE.ID == LedgerEntryID)
                    {
                        CurrentLedger[EntryNumber].Amount = 0.00M;
                        CurrentLedger[EntryNumber].Debit = 0.00M;
                        CurrentLedger[EntryNumber].Credit = 0.00M;
                        CurrentLedger[EntryNumber].Cleared = true;
                        if (EntryNumber > 1)
                            CurrentLedger[EntryNumber].Balance = CurrentLedger[EntryNumber - 1].Balance;
                        else
                            CurrentLedger[EntryNumber].Balance = 0.00M;
                        UpdateFollowingTransactionBalances(EntryNumber);
                        HasChanged();
                        break;
                    }
                }
            }
        }

        public void UpdateThisTransaction(int LedgerEntryID, decimal NewValue)
        {
            // only act if there are any transactions in the ledger

            if (CurrentLedger.Count > 0)
            {
                // and only if this is a valid entry

                for (int EntryNumber = 0; EntryNumber < CurrentLedger.Count; EntryNumber++)
                {
                    LedgerEntry LE = CurrentLedger[EntryNumber];
                    if (LE.ID == LedgerEntryID)
                    {
                        if (CurrentLedger[EntryNumber].Debit > 0)
                        {
                            CurrentLedger[EntryNumber].Amount = 0.00M - NewValue;
                            CurrentLedger[EntryNumber].Debit = NewValue;
                        }
                        else
                        {
                            CurrentLedger[EntryNumber].Amount = NewValue;
                            CurrentLedger[EntryNumber].Credit = NewValue;
                        }

                        if (EntryNumber > 1)
                            CurrentLedger[EntryNumber].Balance = CurrentLedger[EntryNumber - 1].Balance
                                                                       + CurrentLedger[EntryNumber].Credit
                                                                       - CurrentLedger[EntryNumber].Debit;
                        else
                            CurrentLedger[EntryNumber].Balance = 0.00M
                                                                       + CurrentLedger[EntryNumber].Credit
                                                                       - CurrentLedger[EntryNumber].Debit;
                        UpdateFollowingTransactionBalances(EntryNumber);
                        HasChanged();
                        break;
                    }
                }
            }
        }

        internal void ReconcileThisLedgerEntry (int IDToReconcile, bool Cleared)
        {
            for (int index = 0; index < CurrentLedger.Count; index++)
            {
                if (CurrentLedger[index].ID == IDToReconcile)
                {
                    if (Cleared != CurrentLedger[index].Cleared)
                    {
                        CurrentLedger[index].Cleared = Cleared;
                        HasChanged();
                    }
                    break;
                }
            }
        }
        private void UpdateFollowingTransactionBalances(int startingPoint)
        {
            int LIndex = startingPoint + 1;
            decimal RunningBalance = CurrentLedger[startingPoint].Balance;
            while (LIndex < CurrentLedger.Count)
            {
                RunningBalance = RunningBalance + CurrentLedger[LIndex].Credit - CurrentLedger[LIndex].Debit;
                CurrentLedger[LIndex].Balance = RunningBalance;
                LIndex++;
            }
        }


        // check the ledger for date and check number consistency
        internal bool CheckForConsistency(out string ErrorMessage)
        {
            decimal RunningBalance;
            bool consistency = true;
            ErrorMessage = "";

            // only act if there are any transactions in the ledger
            // Note: we could have multiple errors in the ledger. Thus, report all of them in one pass.

            if (CurrentLedger.Count > 0)
            {
                for (int EntryNumber = 0; EntryNumber < CurrentLedger.Count; EntryNumber++)
                {

                    if (EntryNumber == 0) continue;  // skip the first one

                    LedgerEntry LE = CurrentLedger[EntryNumber];
                    LedgerEntry LastLedgerEntry = CurrentLedger[EntryNumber - 1];
                    if (LE.When < LastLedgerEntry.When)
                    {
                        MessageBox.Show("Transaction " + LE.ID + " is out of order date wise", "Inconsistent Ledger");
                        ErrorMessage = "Inconsistent Ledger";
                        consistency = false;
                    }
                    if ((LE.Credit > 0.00M) && (LE.Debit > 0.00M))
                    {
                        MessageBox.Show("Transaction " + LE.ID + " has both credit of " + LE.Credit.ToString("0.00") + 
                            " and debit of " + LE.Debit.ToString("0.00"), "Inconsistent Ledger");
                        ErrorMessage = "Inconsistent Ledger";
                        consistency = false;
                    }
                    RunningBalance = LastLedgerEntry.Balance + LE.Credit - LE.Debit;
                    if (LE.Balance != RunningBalance)
                    {
                        MessageBox.Show("Balance for transaction " + LE.ID + " does not match running balance "
                            + LE.Balance.ToString("0.00") + " != " + RunningBalance.ToString("0.00"), "Inconsistent Ledger");
                        ErrorMessage = "Inconsistent Ledger";
                        consistency = false;
                    }
                }
            }
            return consistency;
        }


        // check the ledger against the chart of accounts to make sure all accounts are valid
        internal bool CheckAccounts(List<Account> CurrentAccountList)
        {
            bool consistency = true;

            foreach(LedgerEntry LE in CurrentLedger) 
            {
                string EntryAccount = LE.GetPrimaryAccount();
                if (EntryAccount.Length == 0)
                {
                    string ErrorMessage =
                        LE.CheckNumber + " dated: " + LE.When.ToShortDateString()
                        + " is not allocated to an account";
                    MessageBox.Show(ErrorMessage);
                    return false;
                }

                Account? TAccount = (from tcc in CurrentAccountList
                                     where tcc.Name == EntryAccount
                                     select tcc).FirstOrDefault();
                if (TAccount == null && EntryAccount != "Split")
                {
                    string ErrorMessage = "The account " + EntryAccount +
                        " is not found in the chart of accounts for "
                        + LE.CheckNumber + " dated: " + LE.When.ToShortDateString();
                    MessageBox.Show(ErrorMessage);
                    ChangeAccountForm CAF = new();
                    CAF.Setup(CurrentAccountList, EntryAccount);
                    CAF.ShowDialog();
                    if (CAF.ToChangeTo.Length > 0)
                    {
                        EntryAccount = CAF.ToChangeTo;
                        LE.Account = EntryAccount;
                    }
                    else
                        return false;
                }

                if (LE.SubAccounts.Count > 0)
                {
                    foreach(LedgerEntryBreakdown LEB in LE.SubAccounts)
                    {
                        EntryAccount = LEB.AccountName;
                        if (EntryAccount.Length == 0)
                        {
                            string ErrorMessage =
                                LE.CheckNumber + " dated: " + LE.When.ToShortDateString()
                                + " has a breakdown " + LEB.Amount.ToString("0.00") 
                                + " not allocated to an account";
                            MessageBox.Show(ErrorMessage);
                            return false;
                        }
                        TAccount = (from tcc in CurrentAccountList
                                             where tcc.Name == EntryAccount
                                             select tcc).FirstOrDefault();
                        if (TAccount == null)
                        {
                            string ErrorMessage = "The account " + EntryAccount +
                                " is not found in the chart of accounts for "
                                + LE.CheckNumber + " dated: " + LE.When.ToShortDateString()
                                + " breakdown part " + LEB.Amount.ToString("0.00");
                            MessageBox.Show(ErrorMessage);
                            ChangeAccountForm CAF = new();
                            CAF.Setup(CurrentAccountList, EntryAccount);
                            CAF.ShowDialog();
                            if (CAF.ToChangeTo.Length > 0) 
                            { 
                                EntryAccount = CAF.ToChangeTo; 
                                LEB.AccountName = EntryAccount;
                            }
                            else
                            return false;
                        }

                    }
                }
            }
            return consistency;
        }


        internal bool CheckPayTo(List<PayTo> payToList)
        {
            foreach (LedgerEntry LE in CurrentLedger)
            {
                // only check the entries that are debits
                if (LE.Debit > 0.00M)
                {
                    string WhoPaid = LE.ToWhom;
                    bool found = false;
                    foreach(PayTo PT in  payToList)
                    {
                        if ((WhoPaid == PT.BusinessName)
                            ||(WhoPaid == PT.AccountName))
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        MessageBox.Show("Check written to someone not in payto list, " + WhoPaid);
                        return false;
                    }
                }
            }

            return true;
        }


        #region Validation

        public bool Validate(XLWorkbook CheckBookXlsx, out string ErrorMessage)
        {
            IXLWorksheet TransactionsWorksheet;
            try
            {
                TransactionsWorksheet = CheckBookXlsx.Worksheet(LedgerFormat.SheetName);
            }
            catch (Exception ex)
            {
                ErrorMessage = "Missing the Transactions sheet " + ex.Message;
                return false;
            }

            // the sheet exists. check all the headings and find which column is which

            try
            {
                if (!LedgerEntry.ValidateColumnHeaders(TransactionsWorksheet, LedgerFormat, out ErrorMessage))
                    return false;

                // validate all the entries in the columns for validity
                // at this point, we have all the columns that are required

                int RowsUsedCount = TransactionsWorksheet.RowsUsed().Count();
                for (int Row = 2; Row < RowsUsedCount; Row++)
                {
                    IXLRow XRow = TransactionsWorksheet.Row(Row);
                    if (!LedgerEntry.ValidateExcelRow(XRow, LedgerFormat, out ErrorMessage))
                    {
                        ErrorMessage += " in row " + Row.ToString();
                        return false;
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
        internal bool ValidateJournalCheck (IXLWorksheet JournalWorksheet, int Row, out string ErrorMessage)
        {
            return Journal.ValidateCheck(JournalWorksheet, Row, LedgerFormat, out ErrorMessage);
        }
        internal bool ValidateJournalPayment(IXLWorksheet JournalWorksheet, int Row)
        {
            return Journal.ValidatePayment(JournalWorksheet, Row, LedgerFormat);
        }

        // used on user entry
        internal bool ValidateWhen(string TWhen)
        {
            return LedgerEntry.ValidateWhen(TWhen, LedgerFormat);
        }
        internal bool ValidateCheckNumber(string TCheckNumber)
        {
            return LedgerEntry.ValidateCheckNumber(TCheckNumber, LedgerFormat);
        }
        internal bool ValidateToWhom(string TToWhom)
        {
            return LedgerEntry.ValidateToWhom(TToWhom, LedgerFormat);
        }
        internal bool ValidateCleared(string TCleared)
        {
            return LedgerEntry.ValidateCleared(TCleared, LedgerFormat);
        }
        internal bool ValidateDebit(string TDebit)
        {
            return LedgerEntry.ValidateDebit(TDebit, LedgerFormat);
        }
        internal bool ValidateCredit(string TCredit)
        {
            return LedgerEntry.ValidateCredit(TCredit, LedgerFormat);
        }
        internal bool ValidateBalance(string TBalance)
        {
            return LedgerEntry.ValidateBalance(TBalance, LedgerFormat);
        }
        internal bool ValidateAmount(string TAmount)
        {
            return LedgerEntry.ValidateAmount(TAmount, LedgerFormat);
        }
        internal bool ValidateAccount(string TAccount)
        {
            return LedgerEntry.ValidateAccount(TAccount, LedgerFormat);
        }
        internal bool ValidateSubAccountName(string TSubAccountName)
        {
            return LedgerEntry.ValidateSubAccountName(TSubAccountName, LedgerFormat);
        }
        internal bool ValidateSubNotes(string TSubNotes)
        {
            return LedgerEntry.ValidateSubNotes(TSubNotes, LedgerFormat);
        }
        internal bool ValidateTaxID(string TSubAmount)
        {
            return LedgerEntry.ValidateSubAmount(TSubAmount, LedgerFormat);
        }
        #endregion Validation


        public bool ReadFromExcelFile(XLWorkbook CheckBookXlsx)
        {
            CurrentLedger = new();
            IXLWorksheet TransactionsWorksheet = CheckBookXlsx.Worksheet(LedgerFormat.SheetName);
            if (TransactionsWorksheet != null)
            {
                int Row = 0;
                foreach (var row in TransactionsWorksheet.RowsUsed())
                {
                    // The header row was processed in the validation stage and we can use those values

                    Row++;
                    if (Row == 1) continue;

                    // Pull off the Transaction Values

                    LedgerEntry NEntry = new();
                    IXLRow xlRow = TransactionsWorksheet.Row(Row);

                    NEntry.ParseExcelRow(xlRow, LedgerFormat);

                    CurrentLedger.Add(NEntry);
                }
                Changed = false; // if we just read in a file, don't mark as changed
                return true;
            }
            return false;
        }

        internal void AddCheckEntry(LedgerEntry LE)
        {

            // before adding the check, see if this check number is already in the list

            bool Handled = false;
            foreach(LedgerEntry Entry in CurrentLedger)
            {
                if (Entry.CheckNumber.Length > 0)
                {
                    if (Entry.CheckNumber == LE.CheckNumber)
                    {
                        if (MessageBox.Show("The journal check has the same number " + LE.CheckNumber + " as an existing check. Replace?",
                            "Duplicate entry", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            // replace the current entry with this new one
                            int LedgerIndex = CurrentLedger.IndexOf(Entry);
                            LE.Balance = CurrentLedger[LedgerIndex - 1].Balance + LE.Credit - LE.Debit;
                            LE.ID = Entry.ID;
                            CurrentLedger.Insert(LedgerIndex, LE);
                            CurrentLedger.Remove(Entry);
                            UpdateFollowingTransactionBalances(LedgerIndex);
                            HasChanged();

                            Handled = true;
                            break;
                        }
                    }
                }
            }
            if (!Handled)
            {
                if (LE.When < CurrentLedger[^1].When)
                {
                    LE.ID = CurrentLedger.Count;
                    InsertTransaction(LE);
                    HasChanged();
                }
                else
                {
                    LE.Balance = CurrentLedger[^1].Balance + LE.Credit - LE.Debit;
                    LE.ID = CurrentLedger.Count;
                    CurrentLedger.Add(LE);
                    HasChanged();
                }
            }
        }
        internal LedgerEntry? ReadJournalPaymentEntry(IXLWorksheet JournalWorksheet, ref int Row)
        {
            LedgerEntry LE = new();
            if (!Journal.ParsePayment(LE, JournalWorksheet, ref Row)) return null;

            if (LE.When < CurrentLedger[^1].When)
            {
                LE.ID = CurrentLedger.Count;
                InsertTransaction(LE);
            }
            else
            {
                LE.Balance = CurrentLedger[^1].Balance + LE.Credit - LE.Debit;
                LE.ID = CurrentLedger.Count;
                CurrentLedger.Add(LE);
            }
            return LE;
        }

        internal void WriteXLLedger(XLWorkbook CheckBookXlsx)
        {
            // add the chart of accounts worksheet
            CheckBookXlsx.AddWorksheet(LedgerFormat.SheetName);
            IXLWorksheet LedgerWorksheet = CheckBookXlsx.Worksheet(LedgerFormat.SheetName);

            // first build the header

            int MaxBreakdowns = GetMaxBreakdowns();
            LedgerEntry.WriteXLHeader(LedgerWorksheet, LedgerFormat, MaxBreakdowns);

            // then add all the rows

            int Row = 2;
            foreach (LedgerEntry RLedger in CurrentLedger)
            {
                IXLRow xlRow = LedgerWorksheet.Row(Row);
                RLedger.WriteExcelRow(xlRow, LedgerFormat);
                Row++;
            }
        }

        internal int GetMaxBreakdowns()
        {
            int breakdowns = 0;
            foreach (LedgerEntry LE in CurrentLedger)
            {
                if (LE.BreakdownCount() > breakdowns) breakdowns = LE.BreakdownCount();
            }
            return breakdowns;
        }
        internal void SetSheetFormat(int MaxBreakdowns)
        {
            LedgerFormat = new();
            LedgerFormat.SheetName = "Transactions";
            LedgerEntry.AddSheetColumns(LedgerFormat);
            LedgerEntry.AddBreakdownSheetColumns(LedgerFormat, MaxBreakdowns);
        }
    }
}
