//
//  Copyright 2023 David Randolph
//
using BusinessCheckBook.Validation;
using ClosedXML.Excel;


namespace BusinessCheckBook.DataStore
{
    public class LedgerEntryBreakdown
    {
        public string AccountName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Memo { get; set; } = string.Empty;

        public const int NumberOfColumnsInLedgerEntryBreakdown = 3;

        // Excel column names
        // Column names and header values

        private const string XLAccountName = "AccountName";
        private const string XLAmount = "Amount";
        private const string XLMemo = "Memo";




        internal static bool ValidateAccountName(string TAccountName, SheetFormat SubColumnsFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = SubColumnsFormat.Column(XLAccountName)!;
            if (!ThisColumn.Valid(TAccountName))
                return false;
            return true;
        }
        internal static bool ValidateMemo(string Memo, SheetFormat SubColumnsFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = SubColumnsFormat.Column(XLMemo)!;
            if (!ThisColumn.Valid(Memo))
                return false;
            return true;
        }
        internal static bool ValidateAmount(string TAmount, SheetFormat SubColumnsFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = SubColumnsFormat.Column(XLAmount)!;
            if (!ThisColumn.Valid(TAmount))
                return false;
            return true;
        }

        internal static bool ValidateBreakdownHeader (IXLWorksheet TransactionsWorksheet, int LastColumn, SheetFormat BreakdownColumnsFormat, out string ErrorMessage, out int BreakdownColumnCount)
        {
            ErrorMessage = "";
            BreakdownColumnCount = NumberOfColumnsInLedgerEntryBreakdown;
            for (int ColumnNum = 0; ColumnNum < NumberOfColumnsInLedgerEntryBreakdown; ColumnNum++)
            {
                ColumnFormat col = BreakdownColumnsFormat.Column(ColumnNum);
                if (col == null) continue;

                bool headerNotFound = true;
                string HeaderValue;


                // go through all columns to find it

                for (int ColNum = LastColumn; ColNum < LastColumn + NumberOfColumnsInLedgerEntryBreakdown; ColNum++)
                {
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
            return true;
        }

        internal static bool ValidateExcelColumns(IXLRow XRow, ref int NextColumn, SheetFormat SubColumnsFormat, out string ErrorMessage)
        {
            ColumnFormat ThisColumn;

            ErrorMessage = "";

            // check the Account Name

            ThisColumn = SubColumnsFormat.Column(XLAccountName)!;
            int Col = ThisColumn.ColumnNumber;
            string TAccountName = XRow.Cell(Col).GetString();
            if (TAccountName.Length == 0) return true;
            if (!ThisColumn.Valid(TAccountName))
            {
                ErrorMessage = "Invalid AccountName "  + TAccountName;
                return false;
            }


            // check the Amount

            ThisColumn = SubColumnsFormat.Column(XLAmount)!;
            Col = ThisColumn.ColumnNumber;
            string TAmount = XRow.Cell(Col).GetString();
            if (!ThisColumn.Valid(TAmount))
            {
                ErrorMessage = "Invalid Amount " + TAmount;
                return false;
            }

            // check the Memo field

            ThisColumn = SubColumnsFormat.Column(XLMemo)!;
                Col = ThisColumn.ColumnNumber;
                string TMemo = XRow.Cell(Col).GetString();
            if (!ThisColumn.Valid(TMemo))
            {
                ErrorMessage = "Invalid Memo " + TMemo;
                return false;
            }

            NextColumn += NumberOfColumnsInLedgerEntryBreakdown;
            return true;
        }
        internal int ParseExcelColumns(IXLRow XRow, SheetFormat SubColumnsFormat)
        {
            ColumnFormat? Col;
            Col = SubColumnsFormat.Column(XLAccountName)!;
            AccountName = XRow.Cell(Col.ColumnNumber ).GetString();
            Col = SubColumnsFormat.Column(XLAmount)!;
            Amount = Decimal.Parse(XRow.Cell(Col.ColumnNumber).GetString());
            Col = SubColumnsFormat.Column(XLMemo)!;
            Memo = XRow.Cell(Col.ColumnNumber).GetString();
            return NumberOfColumnsInLedgerEntryBreakdown;
        }
        internal int WriteExcelColumns(IXLRow XRow, SheetFormat SubColumnsFormat)
        {
            ColumnFormat? Col = SubColumnsFormat.Column(XLAccountName)!;
            XRow.Cell(Col.ColumnNumber).Value = AccountName;
            Col = SubColumnsFormat.Column(XLAmount)!;
            XRow.Cell(Col.ColumnNumber).Value = Amount;
            Col = SubColumnsFormat.Column(XLMemo)!;
            XRow.Cell(Col.ColumnNumber).Value = Memo;
            return NumberOfColumnsInLedgerEntryBreakdown;
        }
        internal static void AddSheetColumns(SheetFormat SubColumnsFormat)
        {
            AddSheetColumns(SubColumnsFormat, 0);
        }

        internal static void AddSheetColumns(SheetFormat SubColumnsFormat, int offset)
        {
            SubColumnsFormat.Add(new NameColumn(offset + 1, XLAccountName, 50, ColumnFormat.ColumnIsOptional, ColumnFormat.FieldIsOptional));
            SubColumnsFormat.Add(new DecimalColumn(offset + 2, XLAmount, 20, ColumnFormat.ColumnIsOptional, ColumnFormat.FieldIsOptional));
            SubColumnsFormat.Add(new TextColumn(offset + 3, XLMemo, 255, ColumnFormat.ColumnIsOptional, ColumnFormat.FieldIsOptional));

        }
    }
}
