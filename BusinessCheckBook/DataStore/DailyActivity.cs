using BusinessCheckBook.Validation;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Vml;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCheckBook.DataStore
{
    internal class DailyActivity
    {
        //public int id {  get; set; }
        public DateTime WhichDay { get; set; }
        public string CustomerID { get; set; } = string.Empty;
        public bool IsInvoiced { get; set; } = false;
        public string SubcontractorActivityDescription { get; set; } = string.Empty;
        public decimal SubcontractorInvoiceAmount { get; set; } = 0;
        public string OtherBillableExpenseDescription {  get; set; } = string.Empty;
        public decimal OtherBillableExpenseAmount { get; set; }
        public string ActionsTaken { get; set; } = string.Empty;
        public decimal Hours { get; set;} = 0;
        public List<HourlyActivity> HourlyActivityList { get; set; } = new List<HourlyActivity>();



        // Column names and header values

        private const string XLWhichDay = "WhichDay";
        private const string XLCustomerID = "CustomerID";
        private const string XLInvoiced = "Invoiced";
        private const string XLSubContractor = "Subcontractor";
        private const string XLSubAmount = "SubAmount";
        private const string XLOther = "Other";
        private const string XLOtherAmount = "OtherAmount";
        private const string XLActions = "Actions";
        private const string XLHours = "Hours";

        //static readonly List<string> ColumnNames = new()
        //{
        //    XLWhichDay,
        //    XLCustomerID,
        //    XLInvoiced,
        //    XLSubContractor,
        //    XLSubAmount,
        //    XLOther,
        //    XLOtherAmount,
        //    XLActions,
        //    XLHours
        //};

        //static int NumberOfColumnsInHourlyActivity = 9;

        // what the Excel sheet is supposed to look like
        internal SheetFormat DailyActivityFormat { get; set; } = new();

        public DailyActivity()
        {

        }

        internal static bool ValidateColumnHeaders(IXLWorksheet DailyActivityWorksheet, SheetFormat DailyActivityFormat, out string ErrorMessage)
        {
            string HeaderValue;
            ErrorMessage = "";
            for (int ColumnNum = 0; ColumnNum < DailyActivityFormat.Count(); ColumnNum++)
            {
                ColumnFormat col = DailyActivityFormat.Column(ColumnNum);
                if (col == null) continue;

                // first see if the column listed in the format matches

                bool headerNotFound = true;
                int WhichWorksheetColumn = col.ColumnNumber;

                if (WhichWorksheetColumn > 0)
                {
                    HeaderValue = DailyActivityWorksheet.Cell(1, WhichWorksheetColumn).GetString().Trim();
                    if (col.Name.ToUpper() == HeaderValue.ToUpper())
                        continue;
                }

                // if it didn't match, go through all columns to find it

                for (int ColNum = 1; ColNum <= DailyActivityFormat.Count(); ColNum++)
                {
                    if (ColNum > DailyActivityWorksheet.ColumnsUsed().Count()) break;
                    HeaderValue = DailyActivityWorksheet.Cell(1, ColNum).GetString().Trim();
                    if (col.Name.ToUpper() == HeaderValue.ToUpper())
                    {
                        headerNotFound = false;
                        col.ColumnNumber = ColNum;
                        break;
                    }
                }
                if (headerNotFound)
                {
                    ErrorMessage = "The column " + col.Name + " is not in the Daily Activity Sheet";
                    return false;
                }

            }


            // go through the columns for the breakdown 

            DailyActivityFormat.SubFormats = new();
            int LastColumn = DailyActivityFormat.Count();
            while (LastColumn < DailyActivityWorksheet.ColumnsUsed().Count())
            {
                HeaderValue = DailyActivityWorksheet.Cell(1, LastColumn + 1).GetString().Trim();
                if (HeaderValue.Length > 0)
                {
                    // add another set of breakdown formats

                    SheetFormat BreakdownColumnsFormat = new();
                    HourlyActivity.AddSheetColumns(BreakdownColumnsFormat, LastColumn);
                    DailyActivityFormat.SubFormats.Add(BreakdownColumnsFormat);

                    // and validate against those formats
                    if (!HourlyActivity.ValidateHourlyActivityHeader(DailyActivityWorksheet, LastColumn, BreakdownColumnsFormat, out ErrorMessage, out int BreakdownColumnCount))
                        return false;
                    LastColumn += BreakdownColumnCount;
                }
                else
                    break;
            }
            return true;

        }


        internal static bool ValidateExcelRow(IXLRow XRow, SheetFormat DailyActivityFormat, out string ErrorMessage)
        {
            ColumnFormat ThisColumn;

            ErrorMessage = "";

            // Check the Day column

            ThisColumn = DailyActivityFormat.Column(XLWhichDay)!;
            string TWhichDay = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            if (!string.IsNullOrEmpty(TWhichDay)) return true;
            if (!ThisColumn.Valid(TWhichDay))
            {
                ErrorMessage = "Invalid which day " + TWhichDay;
                return false;
            }

            // check the Customer Identifier 

            ThisColumn = DailyActivityFormat.Column(XLCustomerID)!;
            string CustomerIdentifier = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            if (!ThisColumn.Valid(CustomerIdentifier))
            {
                ErrorMessage = "Invalid Customer ID " + CustomerIdentifier;
                return false;
            }

            // Check the Invoiced flag

            ThisColumn = DailyActivityFormat.Column(XLInvoiced)!;
            string TInvoiced = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            if (!ThisColumn.Valid(TInvoiced))
            {
                ErrorMessage = "Invalid invoiced flag " + TInvoiced;
                return false;
            }

            // Check the subcontractor

            ThisColumn = DailyActivityFormat.Column(XLSubContractor)!;
            string TSubcontractor = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            if (!ThisColumn.Valid(TSubcontractor))
            {
                ErrorMessage = "Invalid Subcontractor " + TSubcontractor;
                return false;
            }

            // Check the Subcontractor amount

            ThisColumn = DailyActivityFormat.Column(XLSubAmount)!;
            string TSubcontractorAmount = XRow.Cell(ThisColumn.ColumnNumber).GetString();

            if (!ThisColumn.Valid(TSubcontractorAmount))
            {
                ErrorMessage = "Invalid subcontractor amount " + TSubcontractorAmount;
                return false;
            }

            // Check the other expenses column

            ThisColumn = DailyActivityFormat.Column(XLOther)!;
            string TOtherExpenses = XRow.Cell(ThisColumn.ColumnNumber).GetString();

            if (!ThisColumn.Valid(TOtherExpenses))
            {
                ErrorMessage = "Invalid Other Expense Description " + TOtherExpenses;
                return false;
            }

            // Check the Other Expense Amount

            ThisColumn = DailyActivityFormat.Column(XLOtherAmount)!;
            string TOtherExpensesAmount = XRow.Cell(ThisColumn.ColumnNumber).GetString();

            if (!ThisColumn.Valid(TOtherExpensesAmount))
            {
                ErrorMessage = "Invalid Other Expense Amount  " + TOtherExpensesAmount;
                return false;
            }

            // Check the Action Column

            ThisColumn = DailyActivityFormat.Column(XLActions)!;
            string TActions = XRow.Cell(ThisColumn.ColumnNumber).GetString();

            if (!ThisColumn.Valid(TActions))
            {
                ErrorMessage = "Invalid Actions " + TActions;
                return false;
            }


            // Check the Hours

            ThisColumn = DailyActivityFormat.Column(XLHours)!;
            string THours = XRow.Cell(ThisColumn.ColumnNumber).GetString();

            if (!ThisColumn.Valid(THours))
            {
                ErrorMessage = "Invalid Hours  " + THours;
                return false;
            }

            // check all the sub items

            int NextColumn = DailyActivityFormat.Count() + 1;
            int Breakdown = 0;
            while (XRow.Cell(NextColumn).GetString().Length > 0)
            {
                if (!HourlyActivity.ValidateExcelColumns(XRow, ref NextColumn, DailyActivityFormat.SubFormats[Breakdown++], out ErrorMessage))
                    return false;
            }
            return true;
        }

        internal static void WriteXLHeader(IXLWorksheet DailyActivityWorksheet, SheetFormat DailyActivityFormat, int breakdowns)
        {
            // write the main headers

            for (int i = 0; i < DailyActivityFormat.Count(); i++)
            {
                ColumnFormat Col = DailyActivityFormat.Column(i);
                DailyActivityWorksheet.Cell(1, Col.ColumnNumber).Value = Col.Name;
            }

            // write all the breakdown headers

            DailyActivityFormat.SubFormats = new();
            int Offset = DailyActivityFormat.Count();
            for (int br = 0; br < breakdowns; br++)
            {
                DailyActivityFormat.SubFormats.Add(new SheetFormat());
                HourlyActivity.AddSheetColumns(DailyActivityFormat.SubFormats[br], Offset);
                for (int i = 0; i < DailyActivityFormat.SubFormats[br].Count(); i++)
                {
                    ColumnFormat Col = DailyActivityFormat.SubFormats[br].Column(i);
                    DailyActivityWorksheet.Cell(1, Col.ColumnNumber).Value = Col.Name;
                    Offset++;
                }
            }
        }


        internal void ParseExcelRow(IXLRow XRow, SheetFormat DailyActivityFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = DailyActivityFormat.Column(XLWhichDay)!;
            WhichDay = XRow.Cell(ThisColumn.ColumnNumber).GetDateTime();
            ThisColumn = DailyActivityFormat.Column(XLCustomerID)!;
            CustomerID = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            ThisColumn = DailyActivityFormat.Column(XLInvoiced)!;
            IsInvoiced = XRow.Cell(ThisColumn.ColumnNumber).GetBoolean();
            ThisColumn = DailyActivityFormat.Column(XLSubContractor)!;
            SubcontractorActivityDescription = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            ThisColumn = DailyActivityFormat.Column(XLSubAmount)!;
            SubcontractorInvoiceAmount = Decimal.Parse(XRow.Cell(ThisColumn.ColumnNumber).GetString());
            ThisColumn = DailyActivityFormat.Column(XLOther)!;
            OtherBillableExpenseDescription = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            ThisColumn = DailyActivityFormat.Column(XLOtherAmount)!;
            OtherBillableExpenseAmount = Decimal.Parse(XRow.Cell(ThisColumn.ColumnNumber).GetString());
            ThisColumn = DailyActivityFormat.Column(XLActions)!;
            ActionsTaken = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            ThisColumn = DailyActivityFormat.Column(XLHours)!;
            Hours = Decimal.Parse(XRow.Cell(ThisColumn.ColumnNumber).GetString());

            // parse the breakdown

            int NextColumn = DailyActivityFormat.ColumnFormats.Count + 1;
            int breakdown = 0;
            while (XRow.Cell(NextColumn).GetString().Trim().Length > 0)
            {
                HourlyActivity HrA = new();
                int count = HrA.ParseExcelColumns(XRow, DailyActivityFormat.SubFormats[breakdown++]);
                HourlyActivityList.Add(HrA);
                NextColumn += count;
            }
        }

        internal void WriteExcelRow(IXLRow XRow, SheetFormat DailyActivityFormat)
        {

            ColumnFormat? Col = DailyActivityFormat.Column(XLWhichDay)!;
            XRow.Cell(Col.ColumnNumber).Value = WhichDay.ToShortDateString();
            Col = DailyActivityFormat.Column(XLCustomerID)!;
            XRow.Cell(Col.ColumnNumber).Value = CustomerID;
            Col = DailyActivityFormat.Column(XLInvoiced)!;
            XRow.Cell(Col.ColumnNumber).Value = IsInvoiced;
            Col = DailyActivityFormat.Column(XLSubContractor)!;
            XRow.Cell(Col.ColumnNumber).Value = SubcontractorActivityDescription;
            Col = DailyActivityFormat.Column(XLSubAmount)!;
            XRow.Cell(Col.ColumnNumber).Value = SubcontractorInvoiceAmount;
            Col = DailyActivityFormat.Column(XLOther)!;
            XRow.Cell(Col.ColumnNumber).Value = OtherBillableExpenseDescription;
            Col = DailyActivityFormat.Column(XLOtherAmount)!;
            XRow.Cell(Col.ColumnNumber).Value = OtherBillableExpenseAmount;
            Col = DailyActivityFormat.Column(XLActions)!;
            XRow.Cell(Col.ColumnNumber).Value = ActionsTaken;
            Col = DailyActivityFormat.Column(XLHours)!;
            XRow.Cell(Col.ColumnNumber).Value = Hours;

            // write the breakdown
            int br = 0;
            foreach (HourlyActivity HrA in HourlyActivityList)
            {
                if (DailyActivityFormat.SubFormats.Count == br) break;
                HrA.WriteExcelRow(XRow, DailyActivityFormat.SubFormats[br++]);
            }
        }




        internal static void AddSheetColumns(SheetFormat DailyActivityFormat)
        {
            DailyActivityFormat.Add(new DateColumn(1, XLWhichDay, 6, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsRequired));
            DailyActivityFormat.Add(new TextColumn(2, XLCustomerID, 50, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsRequired));
            DailyActivityFormat.Add(new TrueFalseColumn(3, XLInvoiced, 10, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsRequired));
            DailyActivityFormat.Add(new TextColumn(4, XLSubContractor, 255, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsOptional));
            DailyActivityFormat.Add(new DecimalColumn(5, XLSubAmount, 20, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsOptional));
            DailyActivityFormat.Add(new TextColumn(6, XLOther, 255, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsOptional));
            DailyActivityFormat.Add(new DecimalColumn(7, XLOtherAmount, 20, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsOptional));
            DailyActivityFormat.Add(new TextColumn(8, XLActions, 255, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsOptional));
            DailyActivityFormat.Add(new DecimalColumn(9, XLHours, 15, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsOptional));

            int LastColumn = DailyActivityFormat.Count();

            // add a breakdown so that we can do some validation

            DailyActivityFormat.SubFormats = new()
            {  new SheetFormat() };
            HourlyActivity.AddSheetColumns(DailyActivityFormat.SubFormats[0], LastColumn);
        }

    }
}
