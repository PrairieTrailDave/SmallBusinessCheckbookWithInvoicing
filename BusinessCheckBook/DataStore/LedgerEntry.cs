//**********************************************************************
//
//          Copyright © 2023 Prairie Trail Software, Inc.
//
//**********************************************************************

using BusinessCheckBook.Validation;
using ClosedXML.Excel;


namespace BusinessCheckBook.DataStore
{
    public class LedgerEntry
    {
        public int ID { get; set; }
        public DateTime When { get; set; }
        public string CheckNumber { get; set; } = string.Empty;
        public string ToWhom { get; set; } = string.Empty;
        public bool Cleared { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal Balance { get; set; }
        public decimal Amount { get; set; }
        public string Account { get; set; } = string.Empty;
        public string Memo { get; set; } = string.Empty;
        public List<LedgerEntryBreakdown> SubAccounts { get; set; } = new();

        static int LastRegularColumn = 0;

        //static List<SheetFormat> BreakdownColumnsFormats = new();


        // adding all this below allows one file to contain the names of all the fields and all the formats 

        // Excel column names
        // Column names and header values

        private const string XLID = "ID";
        private const string XLWhen = "When";
        private const string XLCheckNumber = "CheckNumber";
        private const string XLToWhom = "ToWhom";
        private const string XLCleared = "Cleared";
        private const string XLDebit = "Debit";
        private const string XLCredit = "Credit";
        private const string XLBalance = "Balance";
        private const string XLAmount = "Amount";
        private const string XLAccount = "Account";
        private const string XLMemo = "Memo";



        internal string GetPrimaryAccount()
        {
            return Account;
        }




        internal static bool ValidateColumnHeaders(IXLWorksheet TransactionsWorksheet, SheetFormat LedgerFormat, out string ErrorMessage)
        {
            string HeaderValue;
            ErrorMessage = "";
            for (int ColumnNum = 0; ColumnNum < LedgerFormat.Count(); ColumnNum++)
            {
                ColumnFormat col = LedgerFormat.Column(ColumnNum);
                if (col == null) continue;

                // first see if the column listed in the format matches

                bool headerNotFound = true;
                int WhichWorksheetColumn = col.ColumnNumber;

                if (WhichWorksheetColumn > 0)
                {
                    HeaderValue = TransactionsWorksheet.Cell(1, WhichWorksheetColumn).GetString().Trim();
                    if (col.Name.ToUpper() == HeaderValue.ToUpper())
                        continue;
                }

                // if it didn't match, go through all columns to find it

                for (int ColNum = 1; ColNum <= LedgerFormat.Count(); ColNum++)
                {
                    if (ColNum > TransactionsWorksheet.ColumnsUsed().Count()) break;
                    HeaderValue = TransactionsWorksheet.Cell(1, ColNum).GetString().Trim();
                    if (col.Name.ToUpper() == HeaderValue.ToUpper())
                    {
                        headerNotFound = false;
                        col.ColumnNumber = ColNum;
                        break;
                    }
                }
                if (headerNotFound)
                {
                    ErrorMessage = "The column " + col.Name + " is not in the Transaction Sheet";
                    return false;
                }

            }


            // go through the columns for the breakdown 
            if (!BuildSubFormats(TransactionsWorksheet, LedgerFormat, out ErrorMessage)) return false;
            return true;

        }

        internal static bool BuildSubFormats(IXLWorksheet TransactionsWorksheet, SheetFormat LedgerFormat, out string ErrorMessage)
        {
            ErrorMessage = "";
            int LastColumn = LedgerFormat.Count() + 1;
            LedgerFormat.SubFormats = new();
            while (LastColumn < TransactionsWorksheet.ColumnsUsed().Count())
            {
                string HeaderValue = TransactionsWorksheet.Cell(1, LastColumn).GetString().Trim();
                if (HeaderValue.Length > 0)
                {
                    // add another set of breakdown formats

                    SheetFormat BreakdownColumnsFormat = new();
                    LedgerEntryBreakdown.AddSheetColumns(BreakdownColumnsFormat, LastColumn);
                    LedgerFormat.SubFormats.Add(BreakdownColumnsFormat);

                    // and validate against those formats
                    if (!LedgerEntryBreakdown.ValidateBreakdownHeader(TransactionsWorksheet, LastColumn, BreakdownColumnsFormat, out ErrorMessage, out int BreakdownColumnCount))
                        return false;
                    LastColumn += BreakdownColumnCount;
                }
                else
                    break;
            }
            return true;
        }



        internal static bool ValidateWhen(string When, SheetFormat LedgerFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = LedgerFormat.Column(XLWhen)!;
            if (!ThisColumn.Valid(When))
                return false;
            return true;
        }
        internal static bool ValidateCheckNumber(string TCheckNumber, SheetFormat LedgerFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = LedgerFormat.Column(XLCheckNumber)!;
            if (!ThisColumn.Valid(TCheckNumber))
                return false;
            return true;
        }
        internal static bool ValidateToWhom(string TToWhom, SheetFormat LedgerFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = LedgerFormat.Column(XLToWhom)!;
            if (!ThisColumn.Valid(TToWhom))
                return false;
            return true;
        }
        internal static bool ValidateCleared(string TCleared, SheetFormat LedgerFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = LedgerFormat.Column(XLCleared)!;
            if (!ThisColumn.Valid(TCleared))
                return false;
            return true;
        }
        internal static bool ValidateDebit(string TDebit, SheetFormat LedgerFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = LedgerFormat.Column(XLDebit)!;
            if (!ThisColumn.Valid(TDebit))
                return false;
            return true;
        }
        internal static bool ValidateCredit(string TCredit, SheetFormat LedgerFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = LedgerFormat.Column(XLCredit)!;
            if (!ThisColumn.Valid(TCredit))
                return false;
            return true;
        }
        internal static bool ValidateBalance(string TBalance, SheetFormat LedgerFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = LedgerFormat.Column(XLBalance)!;
            if (!ThisColumn.Valid(TBalance))
                return false;
            return true;
        }
        internal static bool ValidateAmount(string TAmount, SheetFormat LedgerFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = LedgerFormat.Column(XLAmount)!;
            if (!ThisColumn.Valid(TAmount))
                return false;
            return true;
        }
        internal static bool ValidateAccount(string TAccount, SheetFormat LedgerFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = LedgerFormat.Column(XLAccount)!;
            if (!ThisColumn.Valid(TAccount))
                return false;
            return true;
        }
        internal static bool ValidateMemo(string TMemo, SheetFormat LedgerFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = LedgerFormat.Column(XLMemo)!;
            if (!ThisColumn.Valid(TMemo))
                return false;
            return true;
        }
        internal static bool ValidateSubAccountName(string TAccount, SheetFormat LedgerFormat)
        {
            return LedgerEntryBreakdown.ValidateAccountName(TAccount, LedgerFormat.SubFormats[0]);
        }
        internal static bool ValidateSubNotes(string TNotes, SheetFormat LedgerFormat)
        {
            return LedgerEntryBreakdown.ValidateMemo(TNotes, LedgerFormat.SubFormats[0]);
        }
        internal static bool ValidateSubAmount(string TAmount, SheetFormat LedgerFormat)
        {
            return LedgerEntryBreakdown.ValidateAmount(TAmount, LedgerFormat.SubFormats[0]);
        }








        internal static bool ValidateExcelRow(IXLRow XRow, SheetFormat LedgerFormat, out string ErrorMessage)
        {
            ColumnFormat ThisColumn;

            ErrorMessage = "";

            // check the ID - if no ID, then skip this row

            ThisColumn = LedgerFormat.Column(XLID)!;
            string TID = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            if (TID.Length == 0) return true;
            if (!ThisColumn.Valid(TID))
            {
                ErrorMessage = "Invalid Ledger ID " +  TID;
                return false;
            }


            // Check the When

            ThisColumn = LedgerFormat.Column(XLWhen)!;
            string TWhen = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            if (!ThisColumn.Valid(TWhen))
            {
                ErrorMessage = "Invalid Ledger When " +  TWhen;
                return false;
            }

            // Check the Check Number

            ThisColumn = LedgerFormat.Column(XLCheckNumber)!;
            string TCheckNumber = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            if (!ThisColumn.Valid(TCheckNumber))
            {
                ErrorMessage = "Invalid Ledger Check Number " + TCheckNumber;
                return false;
            }

            // Check the ToWhom

            ThisColumn = LedgerFormat.Column(XLToWhom)!;
            string TToWhom = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            if (!ThisColumn.Valid(TToWhom))
            {
                ErrorMessage = "Invalid Ledger To Whom " + TToWhom;
                return false;
            }

            // check the Cleared

            ThisColumn = LedgerFormat.Column(XLCleared)!;
            string TCleared = XRow.Cell(ThisColumn.ColumnNumber).GetString();

            if (!ThisColumn.Valid(TCleared))
            {
                ErrorMessage = "Invalid Ledger Cleared " + TCleared;
                return false;
            }

            // Check the Debit

            ThisColumn = LedgerFormat.Column(XLDebit)!;
            string TDebit = XRow.Cell(ThisColumn.ColumnNumber).GetString();

            if (!ThisColumn.Valid(TDebit))
            {
                ErrorMessage = "Invalid Ledger Debit " + TDebit;
                return false;
            }

            // Check the Credit

            ThisColumn = LedgerFormat.Column(XLCredit)!;
            string TCredit = XRow.Cell(ThisColumn.ColumnNumber).GetString();

            if (!ThisColumn.Valid(TCredit))
            {
                ErrorMessage = "Invalid Ledger Credit " + TCredit;
                return false;
            }

            // Check the Balance

            ThisColumn = LedgerFormat.Column(XLBalance)!;
            string TBalance = XRow.Cell(ThisColumn.ColumnNumber).GetString();

            if (!ThisColumn.Valid(TBalance))
            {
                ErrorMessage = "Invalid Ledger Balance " + TBalance;
                return false;
            }


            // Check the Amount

            ThisColumn = LedgerFormat.Column(XLAmount)!;
            string TAmount = XRow.Cell(ThisColumn.ColumnNumber).GetString();

            if (!ThisColumn.Valid(TAmount))
            {
                ErrorMessage = "Invalid Ledger Amount " +  TAmount;
                return false;
            }


            // Check the Account

            ThisColumn = LedgerFormat.Column(XLAccount)!;
            string TAccount = XRow.Cell(ThisColumn.ColumnNumber).GetString();

            if (!ThisColumn.Valid(TAccount))
            {
                ErrorMessage = "Invalid Ledger Account " + TAccount;
                return false;
            }

            // Check the Memo

            ThisColumn = LedgerFormat.Column(XLMemo)!;
            string TMemo = XRow.Cell(ThisColumn.ColumnNumber).GetString();

            if (!ThisColumn.Valid(TMemo))
            {
                ErrorMessage = "Invalid Memo Field " + TMemo;
                return false;
            }


            // check all the sub items

            int NextColumn = LastRegularColumn + 1;
            int Breakdown = 0;
            while (XRow.Cell(NextColumn).GetString().Length > 0)
            {
                if (Breakdown >= LedgerFormat.SubFormats.Count) break;
                if (!LedgerEntryBreakdown.ValidateExcelColumns(XRow, ref NextColumn, LedgerFormat.SubFormats[Breakdown++], out ErrorMessage))
                    return false;
            }

            return true;
        }

        internal int BreakdownCount()
        {
            int results = 0;
            foreach(LedgerEntryBreakdown LE in SubAccounts)
            {
                if (LE.AccountName.Length > 0)  results++; 
            }
            return results;
        }

        internal static void WriteXLHeader(IXLWorksheet LedgerWorksheet, SheetFormat LedgerFormat, int breakdowns)
        {
            // write the main headers

            for (int i = 0; i < LedgerFormat.Count(); i++)
            {
                ColumnFormat Col = LedgerFormat.Column(i);
                LedgerWorksheet.Cell(1, Col.ColumnNumber).Value = Col.Name;
            }

            // write all the breakdown headers

            for (int br = 0; br < breakdowns; br++)
            {
                for (int i = 0; i < LedgerFormat.SubFormats[br].Count(); i++)
                {
                    ColumnFormat Col = LedgerFormat.SubFormats[br].Column(i);
                    LedgerWorksheet.Cell(1, Col.ColumnNumber).Value = Col.Name;
                }
            }
        }

        // Note: when Excel modifies an amount, there might be some small decimal digits on that amount.
        // Thus, when reading the file, round to two decimal digits to clean out those digits 
        internal void ParseExcelRow(IXLRow XRow, SheetFormat LedgerFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = LedgerFormat.Column(XLID)!;
            ID = Int32.Parse(XRow.Cell(ThisColumn.ColumnNumber).GetString());
            ThisColumn = LedgerFormat.Column(XLWhen)!;
            When = DateTime.Parse(XRow.Cell(ThisColumn.ColumnNumber).GetString());
            ThisColumn = LedgerFormat.Column(XLCheckNumber)!;
            CheckNumber = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            ThisColumn = LedgerFormat.Column(XLToWhom)!;
            ToWhom = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            ThisColumn = LedgerFormat.Column(XLCleared)!;
            Cleared = TrueFalseColumn.Parse(XRow.Cell(ThisColumn.ColumnNumber).GetString());
            ThisColumn = LedgerFormat.Column(XLDebit)!;
            string debitstr = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            if (debitstr.Length == 0) debitstr = "0.00";
            Debit = Decimal.Round(Decimal.Parse(debitstr), 2);
            ThisColumn = LedgerFormat.Column(XLCredit)!;
            string creditstr = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            if (creditstr.Length == 0) creditstr = "0.00";
            Credit = Decimal.Round(Decimal.Parse(creditstr), 2);
            ThisColumn = LedgerFormat.Column(XLBalance)!;
            string balancestr = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            if (balancestr.Length == 0) balancestr = "0.00";
            Balance = Decimal.Round(Decimal.Parse(balancestr), 2);
            ThisColumn = LedgerFormat.Column(XLAmount)!;
            string amountstr = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            if (amountstr.Length == 0) amountstr = "0.00";
            Amount = Decimal.Round(Decimal.Parse(amountstr), 2);
            ThisColumn = LedgerFormat.Column(XLAccount)!;
            Account = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            ThisColumn = LedgerFormat.Column(XLMemo)!;
            Memo = XRow.Cell(ThisColumn.ColumnNumber).GetString();

            SubAccounts = new();
            int NextColumn = LastRegularColumn + 1;
            int Breakdown = 0;
            int LastColumToCheck = XRow.LastCellUsed().Address.ColumnNumber;
            while (NextColumn < LastColumToCheck)
            {
                if (XRow.Cell(NextColumn).GetString().Length > 0)
                {
                    LedgerEntryBreakdown CE = new();
                    NextColumn += CE.ParseExcelColumns(XRow, LedgerFormat.SubFormats[Breakdown++]);
                    SubAccounts.Add(CE);
                }
            }
        }

        internal void WriteExcelRow(IXLRow XRow, SheetFormat LedgerFormat)
        {

            ColumnFormat? Col = LedgerFormat.Column(XLID)!;
            XRow.Cell(Col.ColumnNumber).Value = ID;
            Col = LedgerFormat.Column(XLWhen)!;
            XRow.Cell(Col.ColumnNumber).Value = When.ToShortDateString();
            Col = LedgerFormat.Column(XLCheckNumber)!;
            XRow.Cell(Col.ColumnNumber).Value = CheckNumber;
            Col = LedgerFormat.Column(XLToWhom)!;
            XRow.Cell(Col.ColumnNumber).Value = ToWhom;
            Col = LedgerFormat.Column(XLCleared)!;
            XRow.Cell(Col.ColumnNumber).Value = Cleared.ToString();
            Col = LedgerFormat.Column(XLDebit)!;
            XRow.Cell(Col.ColumnNumber).Value = Debit;
            Col = LedgerFormat.Column(XLCredit)!;
            XRow.Cell(Col.ColumnNumber).Value = Credit;
            Col = LedgerFormat.Column(XLBalance)!;
            XRow.Cell(Col.ColumnNumber).Value = Balance;
            Col = LedgerFormat.Column(XLAmount)!;
            XRow.Cell(Col.ColumnNumber).Value = Amount;
            Col = LedgerFormat.Column(XLAccount)!;
            XRow.Cell(Col.ColumnNumber).Value = Account;
            Col = LedgerFormat.Column(XLMemo)!;
            XRow.Cell(Col.ColumnNumber).Value = Memo;
            int Breakdown = 0;
            foreach(LedgerEntryBreakdown CE in SubAccounts)
            {
                if (CE.AccountName.Length == 0) break;
                CE.WriteExcelColumns(XRow, LedgerFormat.SubFormats[Breakdown++]);
            }
        }





        internal static void AddSheetColumns(SheetFormat LedgerFormat)
        {
            LedgerFormat.Add(new NumberColumn(1, XLID, 8, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsRequired));
            LedgerFormat.Add(new DateColumn(2, XLWhen, 15, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsRequired));
            LedgerFormat.Add(new NumberColumn(3, XLCheckNumber, 8, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsOptional));
            LedgerFormat.Add(new NameColumn(4, XLToWhom, 100, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsRequired));
            LedgerFormat.Add(new TrueFalseColumn(5, XLCleared, 8, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsOptional));
            LedgerFormat.Add(new DecimalColumn(6, XLDebit, 50, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsOptional));
            LedgerFormat.Add(new DecimalColumn(7, XLCredit, 50, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsOptional));
            LedgerFormat.Add(new DecimalColumn(8, XLBalance, 50, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsOptional));
            LedgerFormat.Add(new DecimalColumn(9, XLAmount, 50, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsOptional));
            LedgerFormat.Add(new NameColumn(10, XLAccount, 100, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsOptional));
            LedgerFormat.Add(new TextColumn(11, XLMemo, 150, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsOptional));
            LastRegularColumn = 11;
            LedgerFormat.SubFormats = new();
            SheetFormat BreakdownColumnsFormat = new();
            LedgerEntryBreakdown.AddSheetColumns(BreakdownColumnsFormat, 0);
            LedgerFormat.SubFormats.Add(BreakdownColumnsFormat);
        }

        internal static void AddBreakdownSheetColumns(SheetFormat LedgerFormat, int MaxBreakdowns)
        {
            int Breakdown = 0;
            LedgerFormat.SubFormats = new();
            int offset = LastRegularColumn;
            while (Breakdown < MaxBreakdowns)
            {
                SheetFormat BreakdownColumnsFormat = new();
                LedgerEntryBreakdown.AddSheetColumns(BreakdownColumnsFormat, offset);
                LedgerFormat.SubFormats.Add(BreakdownColumnsFormat);
                Breakdown++;
                offset += LedgerEntryBreakdown.NumberOfColumnsInLedgerEntryBreakdown;
            }
        }

    }

}
