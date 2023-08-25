using BusinessCheckBook.Validation;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCheckBook.DataStore
{
    // used to help parse journal entries from an export
    internal class Journal
    {
        public const int TransNumCol = 2;
        public const int TypeCol = 4;
        public const int DateCol = 6;
        public const int NumCol = 8;
        public const int NameCol = 10;
        public const int MemoCol = 12;
        public const int AccountCol = 14;
        public const int DebitCol = 16;
        public const int CreditCol = 18;

        public static bool ValidateCheck (IXLWorksheet JournalWorksheet, int Row, SheetFormat LedgerFormat, out string ErrorMessage)
        {
            string FieldValue;

            ErrorMessage = "";
            FieldValue = JournalWorksheet.Cell(Row, DateCol).GetString();
            if (!LedgerEntry.ValidateWhen(FieldValue, LedgerFormat)) { ErrorMessage = "Invalid Date:" + FieldValue; return false; }
            FieldValue = JournalWorksheet.Cell(Row, NameCol).GetString();
            if (!LedgerEntry.ValidateToWhom(FieldValue, LedgerFormat)) { ErrorMessage = "Invalid To Whom:" + FieldValue; return false; }
            FieldValue = JournalWorksheet.Cell(Row, NumCol).GetString();
            if (!LedgerEntry.ValidateCheckNumber(FieldValue, LedgerFormat)) { ErrorMessage = "Invalid Check Number:" + FieldValue; return false; }
            FieldValue = JournalWorksheet.Cell(Row, AccountCol).GetString();
            if (!LedgerEntry.ValidateAccount(FieldValue, LedgerFormat)) { ErrorMessage = "Invalid Account:" + FieldValue; return false; }
            FieldValue = JournalWorksheet.Cell(Row, DebitCol).GetString();
            if (!LedgerEntry.ValidateDebit(FieldValue, LedgerFormat)) { ErrorMessage = "Invalid Debit Amount:" + FieldValue; return false; }
            FieldValue = JournalWorksheet.Cell(Row, CreditCol).GetString();
            if (!LedgerEntry.ValidateCredit(FieldValue, LedgerFormat)) { ErrorMessage = "Invalid Credit Amount:" + FieldValue; return false; }
            FieldValue = JournalWorksheet.Cell(Row, MemoCol).GetString();
            if (!LedgerEntry.ValidateMemo(FieldValue, LedgerFormat)) { ErrorMessage = "Invalid Memo:" + FieldValue; return false; }

            // validate the breakdown
            int NextRow = Row + 1;
            while (NextRow < JournalWorksheet.RowsUsed().Count() && JournalWorksheet.Cell(NextRow, TransNumCol).GetString().Trim().Length == 0)
            {
                FieldValue = JournalWorksheet.Cell(Row, AccountCol).GetString();
                if (!LedgerEntryBreakdown.ValidateAccountName(FieldValue, LedgerFormat.SubFormats[0])) { ErrorMessage = "Invalid Sub Account Name:" + FieldValue; return false; }
                FieldValue = JournalWorksheet.Cell(Row, DebitCol).GetString();
                if (!LedgerEntryBreakdown.ValidateAmount(FieldValue, LedgerFormat.SubFormats[0])) { ErrorMessage = "Invalid Sub Debit Amount:" + FieldValue; return false; }
                FieldValue = JournalWorksheet.Cell(Row, MemoCol).GetString();
                if (!LedgerEntryBreakdown.ValidateMemo(FieldValue, LedgerFormat.SubFormats[0])) { ErrorMessage = "Invalid Sub Memo:" + FieldValue; return false; }
                NextRow++;
            }

            return true;
        }

        public static bool ValidatePayment(IXLWorksheet JournalWorksheet, int Row, SheetFormat LedgerFormat)
        {
            string FieldValue;
            FieldValue = JournalWorksheet.Cell(Row, DateCol).GetString();
            if (!LedgerEntry.ValidateWhen(FieldValue, LedgerFormat)) return false;
            FieldValue = JournalWorksheet.Cell(Row, NameCol).GetString();
            if (!LedgerEntry.ValidateToWhom(FieldValue, LedgerFormat)) return false;
            FieldValue = JournalWorksheet.Cell(Row, NumCol).GetString();
            if (!LedgerEntry.ValidateCheckNumber(FieldValue, LedgerFormat)) return false;
            FieldValue = JournalWorksheet.Cell(Row, AccountCol).GetString();
            if (!LedgerEntry.ValidateAccount(FieldValue, LedgerFormat)) return false;
            FieldValue = JournalWorksheet.Cell(Row, DebitCol).GetString();
            if (!LedgerEntry.ValidateDebit(FieldValue, LedgerFormat)) return false;
            FieldValue = JournalWorksheet.Cell(Row, CreditCol).GetString();
            if (!LedgerEntry.ValidateCredit(FieldValue, LedgerFormat)) return false;
            FieldValue = JournalWorksheet.Cell(Row, MemoCol).GetString();
            if (!LedgerEntry.ValidateMemo(FieldValue, LedgerFormat)) return false;

            return true;
        }


        internal bool ValidateInvoice(IXLWorksheet JournalWorksheet, int Row, SheetFormat InvoiceFormat)
        {
            string FieldValue;
            FieldValue = JournalWorksheet.Cell(Row, DateCol).GetString();
            if (!Invoice.ValidateBillingDate(FieldValue, InvoiceFormat)) return false;
            FieldValue = JournalWorksheet.Cell(Row, NameCol).GetString();
            if (!Invoice.ValidateCustomerID(FieldValue, InvoiceFormat)) return false;
            FieldValue = JournalWorksheet.Cell(Row, NumCol).GetString();
            if (!Invoice.ValidateInvoiceNumber(FieldValue, InvoiceFormat)) return false;
            FieldValue = JournalWorksheet.Cell(Row, AccountCol).GetString();
            // the account should always be "Accounts Receivable" and we are not tracking that
            //if (!Invoice.ValidateAccount(FieldValue, InvoiceFormat)) return false;
            FieldValue = JournalWorksheet.Cell(Row, DebitCol).GetString();
            if (!Invoice.ValidateTotal(FieldValue, InvoiceFormat)) return false;
            FieldValue = JournalWorksheet.Cell(Row, CreditCol).GetString();
            if (FieldValue != null && FieldValue.Length > 0)
            {
                if (!Invoice.ValidateTotal(FieldValue, InvoiceFormat)) return false;
            }
            FieldValue = JournalWorksheet.Cell(Row, MemoCol).GetString();
            if (!Invoice.ValidateCustomerMemo(FieldValue, InvoiceFormat)) return false;

            // validate the breakdown
            int NextRow = Row + 1;
            while (NextRow < JournalWorksheet.RowsUsed().Count() && JournalWorksheet.Cell(NextRow, TransNumCol).GetString().Trim().Length == 0)
            {
                FieldValue = JournalWorksheet.Cell(Row, AccountCol).GetString();
                if (!InvoiceItem.ValidateAccount(FieldValue, InvoiceFormat.SubFormats[0])) return false;
                FieldValue = JournalWorksheet.Cell(Row, DebitCol).GetString();
                if (!InvoiceItem.ValidateAmount(FieldValue, InvoiceFormat.SubFormats[0])) return false;
                FieldValue = JournalWorksheet.Cell(Row, MemoCol).GetString();
                if (!InvoiceItem.ValidateMemo(FieldValue, InvoiceFormat.SubFormats[0])) return false;
                NextRow++;
            }

            return true;
        }

        public static bool ParseCheck(LedgerEntry LE, IXLWorksheet JournalWorksheet, ref int Row)
        {
            ParseMainPartOfCheck(LE, JournalWorksheet, ref Row);

            LE.SubAccounts = new();

            // see if there is a breakdown to the check
            int NextRow = Row + 1;
            while (NextRow < JournalWorksheet.RowsUsed().Count() && JournalWorksheet.Cell(NextRow, TransNumCol).GetString().Trim().Length == 0)
            {
                if ((JournalWorksheet.Cell(NextRow, AccountCol).GetString().Trim().Length == 0)
                    && (JournalWorksheet.Cell(NextRow, MemoCol).GetString().Trim().Length == 0)
                    && (JournalWorksheet.Cell(NextRow, NameCol).GetString().Trim().Length == 0))
                    break;
                LedgerEntryBreakdown LEB = new()
                {
                    AccountName = JournalWorksheet.Cell(NextRow, AccountCol).GetString(),
                    Amount = GetDecimalValue(JournalWorksheet.Cell(NextRow, DebitCol).GetString()),
                    Memo = JournalWorksheet.Cell(NextRow, MemoCol).GetString()
                };
                // sometimes, we have a credit in the breakdown instead of a debit
                if (JournalWorksheet.Cell(NextRow, DebitCol).GetString().Trim().Length == 0)
                    LEB.Amount = 0.00M - GetDecimalValue(JournalWorksheet.Cell(NextRow, CreditCol).GetString());
                LE.SubAccounts.Add(LEB);
                NextRow++;
            }
            Row = NextRow;
            return true;
        }

        public static bool ParsePayCheck(LedgerEntry LE, IXLWorksheet JournalWorksheet, ref int Row)
        {
            ParseMainPartOfCheck(LE, JournalWorksheet, ref Row);

            LE.SubAccounts = new();

            // see if there is a breakdown to the check
            // With a paycheck, don't save breakdowns that are zero dollars
            // and don't save payroll liabilities as those are simply transfers between accounts
            int NextRow = Row + 1;
            while (NextRow < JournalWorksheet.RowsUsed().Count() && JournalWorksheet.Cell(NextRow, TransNumCol).GetString().Trim().Length == 0)
            {
                if ((JournalWorksheet.Cell(NextRow, AccountCol).GetString().Trim().Length == 0)
                    && (JournalWorksheet.Cell(NextRow, MemoCol).GetString().Trim().Length == 0)
                    && (JournalWorksheet.Cell(NextRow, NameCol).GetString().Trim().Length == 0))
                    break;
                if (JournalWorksheet.Cell(NextRow, DebitCol).GetString().Trim().Length > 0)
                {
                    decimal breakdownAmount = GetDecimalValue(JournalWorksheet.Cell(NextRow, DebitCol).GetString());
                    if (breakdownAmount > 0.00M)
                    {
                        LedgerEntryBreakdown LEB = new()
                        {
                            AccountName = JournalWorksheet.Cell(NextRow, AccountCol).GetString(),
                            Amount = breakdownAmount,
                            Memo = JournalWorksheet.Cell(NextRow, MemoCol).GetString()
                        };
                        LE.SubAccounts.Add(LEB);
                    }
                }
                NextRow++;
            }
            Row = NextRow;
            return true;
        }

        private static void ParseMainPartOfCheck(LedgerEntry LE, IXLWorksheet JournalWorksheet, ref int Row)
        {
            LE.When = DateTime.Parse(JournalWorksheet.Cell(Row, DateCol).GetString());
            LE.ToWhom = JournalWorksheet.Cell(Row, NameCol).GetString();
            LE.CheckNumber = JournalWorksheet.Cell(Row, NumCol).GetString();
            LE.Cleared = false;
            LE.Account = JournalWorksheet.Cell(Row, AccountCol).GetString();
            LE.Debit = GetDecimalValue(JournalWorksheet.Cell(Row, DebitCol).GetString());
            LE.Credit = GetDecimalValue(JournalWorksheet.Cell(Row, CreditCol).GetString());
            LE.Memo = JournalWorksheet.Cell(Row, MemoCol).GetString();

            // since it is a check, the credit column needs to move to the debit
            if (LE.Debit == 0.00M && LE.Credit > 0.00M)
            {
                LE.Debit = LE.Credit;
                LE.Credit = 0.00M;
            }
            LE.Amount = 0.00M - LE.Debit;

        }


        public static bool ParsePayment(LedgerEntry LE, IXLWorksheet JournalWorksheet, ref int Row)
        {
            LE.When = DateTime.Parse(JournalWorksheet.Cell(Row, DateCol).GetString());
            LE.ToWhom = JournalWorksheet.Cell(Row, NameCol).GetString();
            LE.CheckNumber = "";
            LE.Cleared = false;
            LE.Account = JournalWorksheet.Cell(Row, AccountCol).GetString();
            LE.Debit = GetDecimalValue(JournalWorksheet.Cell(Row, DebitCol).GetString());
            LE.Credit = GetDecimalValue(JournalWorksheet.Cell(Row, CreditCol).GetString());
            LE.Memo = JournalWorksheet.Cell(Row, MemoCol).GetString();
            LE.Amount = LE.Debit;

            // the payment breakdown only is a line debiting accounts payable. Ignore it.

            // since it is a payment, the debit column needs to move to credit
            if (LE.Credit == 0.00M && LE.Debit > 0.00M)
            {
                LE.Credit = LE.Debit;
                LE.Debit = 0.00M;
            }
            Row ++;
            return true;
        }

        public static bool ParseInvoice(Invoice Inv, IXLWorksheet JournalWorksheet, ref int Row)
        {
            Inv.BillingDate = JournalWorksheet.Cell(Row, DateCol).GetString();
            Inv.CustomerIdentifier = JournalWorksheet.Cell(Row, NameCol).GetString();
            Inv.InvoiceNumber = Int32.Parse(JournalWorksheet.Cell(Row, NumCol).GetString());
            Inv.Paid = false;
            Inv.Total = GetDecimalValue(JournalWorksheet.Cell(Row, DebitCol).GetString());
            Inv.AmountPaid = GetDecimalValue(JournalWorksheet.Cell(Row, CreditCol).GetString());
            Inv.CustomerMemo = JournalWorksheet.Cell(Row, MemoCol).GetString();

            Inv.InvoiceBreakdown = new();

            // see if there is a breakdown to the invoice
            int NextRow = Row + 1;
            while (NextRow < JournalWorksheet.RowsUsed().Count() && JournalWorksheet.Cell(NextRow, TransNumCol).GetString().Trim().Length == 0)
            {
                if ((JournalWorksheet.Cell(NextRow, AccountCol).GetString().Trim().Length == 0)
                    && (JournalWorksheet.Cell(NextRow, MemoCol).GetString().Trim().Length == 0)
                    && (JournalWorksheet.Cell(NextRow, NameCol).GetString().Trim().Length == 0))
                    break;
                InvoiceItem IIB = new()
                {
                    Account = JournalWorksheet.Cell(NextRow, AccountCol).GetString(),
                    ItemPrice = GetDecimalValue(JournalWorksheet.Cell(NextRow, DebitCol).GetString()),
                    ItemDescription = JournalWorksheet.Cell(NextRow, MemoCol).GetString()
                };
                if (IIB.ItemPrice == 0.00M)
                    IIB.ItemPrice = GetDecimalValue(JournalWorksheet.Cell(NextRow, CreditCol).GetString());
                Inv.InvoiceBreakdown.Add(IIB);
                NextRow++;
            }
            Row = NextRow;
            return true;
        }


        private static decimal GetDecimalValue (string? TestValue)
        {
            if (TestValue == null) return 0.00M;
            if (TestValue.Length == 0) return 0.00M;
            return decimal.Parse(TestValue);
        }
    }
}
