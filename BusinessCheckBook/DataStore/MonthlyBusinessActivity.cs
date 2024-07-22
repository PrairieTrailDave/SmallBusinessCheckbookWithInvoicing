using BusinessCheckBook.Validation;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCheckBook.DataStore
{
    // this is an example of a specialty report entry

    internal class MonthlyBusinessActivity
    {
        public int Year { get; set; }
        public int Month { get; set; } 
        public decimal Invoices { get; set; }
        public decimal CashIn { get; set; }


        // the Excel sheet has repeated copies of the columns for each year that data is kept for

        // Excel column names
        // Column names and header values

        private const string XLMonth = "Month";
        private const string XLInvoices = "Invoices";
        private const string XLCashIn = "Cash In";

        private const string MmmYYFormat = "mmm-yy";
        private const string CurrencyFormat = "$ #,##0.00";

        static readonly List<string> ColumnNames =
        [
            XLMonth,
            XLInvoices,
            XLCashIn
        ];

        private static int NumberOfColumnsInMonthlyBusinessActivity = 3;
        private static readonly string[] MonthAbrevs = [
            "jan",
            "feb",
            "mar",
            "apr",
            "may",
            "jun",
            "jul",
            "aug",
            "sep",
            "oct",
            "nov",
            "dec"
        ];


        public MonthlyBusinessActivity()
        {
            Month = 0;
        }


        public static int ColumnsUsed () {  return NumberOfColumnsInMonthlyBusinessActivity; }


        public void AddInvoicesAndCash(MonthlyBusinessActivity MBA)
        {
            Invoices += MBA.Invoices;
            CashIn += MBA.CashIn;
        }

        internal static bool ValidateMonthlyActivityHeader(IXLWorksheet MonthlyBusinessActivityWorksheet, int LastColumn, SheetFormat MonthlyBusinessActivityColumnsFormat, out string ErrorMessage, out int MonthlyBusinessActivityColumnCount)
        {
            ErrorMessage = "";
            MonthlyBusinessActivityColumnCount = NumberOfColumnsInMonthlyBusinessActivity;
            for (int ColumnNum = 0; ColumnNum < NumberOfColumnsInMonthlyBusinessActivity; ColumnNum++)
            {
                ColumnFormat col = MonthlyBusinessActivityColumnsFormat.Column(ColumnNum);
                if (col == null) continue;

                bool headerNotFound = true;
                string HeaderValue;


                // go through all columns to find it

                for (int ColNum = LastColumn + 1; ColNum < LastColumn + 1 + NumberOfColumnsInMonthlyBusinessActivity; ColNum++)
                {
                    HeaderValue = MonthlyBusinessActivityWorksheet.Cell(1, ColNum).GetString().Trim();
                    if (col.Name.ToUpper() == HeaderValue.ToUpper())
                    {
                        headerNotFound = false;
                        col.ColumnNumber = ColNum;  // save the real column number
                        break;
                    }
                }
                if (headerNotFound)
                {
                    ErrorMessage = "The column " + col.Name + " is not in the Monthly Business Activity Sheet";
                    return false;
                }

            }
            return true;
        }

        internal static bool ValidateExcelColumns(IXLRow XRow, SheetFormat SubColumnsFormat, out string ErrorMessage)
        {
            ColumnFormat ThisColumn;

            ErrorMessage = "";

            foreach (string ColumnName in ColumnNames)
            {
                ThisColumn = SubColumnsFormat.Column(ColumnName)!;
                if (ThisColumn == null) {
                    ErrorMessage = "Missing " + ColumnName + " format ";
                    return false;
                }
                int Col = ThisColumn.ColumnNumber;
                string TColumnValue = XRow.Cell(Col).GetString();
                if (!ThisColumn.Valid(TColumnValue))
                {
                    ErrorMessage = "Invalid " + ColumnName + ": " + TColumnValue;
                    return false;
                }
            }

            return true;
        }




        internal int ParseExcelColumns(IXLRow XRow, SheetFormat ColumnsFormat)
        {
            ColumnFormat? Col;
            Col = ColumnsFormat.Column(XLMonth)!;
            string strMonth = XRow.Cell(Col.ColumnNumber).GetString();
            DateTime YearAndMonth;
            if (DateTime.TryParse(strMonth, out YearAndMonth)) 
            { 
                Year = YearAndMonth.Year;
                Month = YearAndMonth.Month;
            }
            Col = ColumnsFormat.Column(XLInvoices)!;
            Invoices = Col.GetValue(XRow.Cell(Col.ColumnNumber).GetString());
            Col = ColumnsFormat.Column(XLCashIn)!;
            CashIn = Col.GetValue(XRow.Cell(Col.ColumnNumber).GetString());

            return NumberOfColumnsInMonthlyBusinessActivity;
        }

        public static void WriteHeader(IXLWorksheet BusinessHistoryWorksheet, SheetFormat YearlyFormat)
        {
            IXLRow XRow = BusinessHistoryWorksheet.Row(1);
            ColumnFormat? Col = YearlyFormat.Column(XLMonth)!;
            XRow.Cell(Col.ColumnNumber).Value = XLMonth;
            Col = YearlyFormat.Column(XLInvoices)!;
            XRow.Cell(Col.ColumnNumber).Value = XLInvoices;
            Col = YearlyFormat.Column(XLCashIn)!;
            XRow.Cell(Col.ColumnNumber).Value = XLCashIn;
        }

        public void WriteExcelRow(IXLRow XRow, SheetFormat YearlyFormat)
        {
            ColumnFormat? Col = YearlyFormat.Column(XLMonth)!;
            if (Month > 0)
            {
                XRow.Cell(Col.ColumnNumber).Value = new DateTime(Year, Month, 1);
                XRow.Cell(Col.ColumnNumber).Style.NumberFormat.Format = MmmYYFormat;
            }
            Col = YearlyFormat.Column(XLInvoices)!;
            XRow.Cell(Col.ColumnNumber).Value = Invoices;
            XRow.Cell(Col.ColumnNumber).Style.NumberFormat.Format = CurrencyFormat;
            Col = YearlyFormat.Column(XLCashIn)!;
            XRow.Cell(Col.ColumnNumber).Value = CashIn;
            XRow.Cell(Col.ColumnNumber).Style.NumberFormat.Format = CurrencyFormat;
        }

        private string WriteMonth()
        {
            if (Month > 0)
                return MonthAbrevs[Month - 1] + "-" + Year % 100;
            return "";
        }

        internal void AddUnderline(IXLRow XRow, SheetFormat BusinessHistoryFormat)
        {
            ColumnFormat? Col = BusinessHistoryFormat.Column(XLMonth)!;
            XRow.Cell(Col.ColumnNumber).Style.Border.BottomBorder = XLBorderStyleValues.Medium;
            Col = BusinessHistoryFormat.Column(XLInvoices)!;
            XRow.Cell(Col.ColumnNumber).Style.Border.BottomBorder = XLBorderStyleValues.Medium;
            Col = BusinessHistoryFormat.Column(XLCashIn)!;
            XRow.Cell(Col.ColumnNumber).Style.Border.BottomBorder = XLBorderStyleValues.Medium;
        }
        internal void AddDoubleUnderLine(IXLRow XRow, SheetFormat BusinessHistoryFormat)
        {
            ColumnFormat? Col = BusinessHistoryFormat.Column(XLMonth)!;
            XRow.Cell(Col.ColumnNumber).Style.Border.BottomBorder = XLBorderStyleValues.Double;
            Col = BusinessHistoryFormat.Column(XLInvoices)!;
            XRow.Cell(Col.ColumnNumber).Style.Border.BottomBorder = XLBorderStyleValues.Double;
            Col = BusinessHistoryFormat.Column(XLCashIn)!;
            XRow.Cell(Col.ColumnNumber).Style.Border.BottomBorder = XLBorderStyleValues.Double;
        }

        internal static void AddSheetColumns(SheetFormat BusinessHistoryFormat, int offset)
        {
            BusinessHistoryFormat.Add(new DateColumn(1 + offset, XLMonth, 20, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsOptional));
            BusinessHistoryFormat.Add(new DecimalColumn(2 + offset, XLInvoices, 20, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsOptional));
            BusinessHistoryFormat.Add(new DecimalColumn(3 + offset, XLCashIn, 20, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsOptional));

        }


    }
}
