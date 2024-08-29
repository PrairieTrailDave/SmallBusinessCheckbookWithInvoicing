using BusinessCheckBook.Validation;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCheckBook.DataStore
{
    internal class TimeSheetEntry
    {
        // used to store the time sheet entries
        // This version stores each individual entry as its own record
        // This version uses a type column for how to decode each record type

        // because of the dynamic column assignment, 
        // this worksheet does not decode by column header

        public bool HasBeenInvoiced { get; set; } = false;
        public enum TimeEntryType { Time, SubContractor, OtherExpense }
        public TimeEntryType WhichType { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; } = DateTime.MinValue;

        public string ProjectID { get; set; } = string.Empty;
        public string Person { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public string SubcontractorName { get; set; } = string.Empty;
        public Decimal SubcontractorAmount { get; set; }
        public string OtherExpenseDescription { get; set; } = String.Empty;
        public Decimal OtherExpenseAmount { get; set; }


        // Excel column identifiers

        // layout of a person's timesheet entry
        private const int XLHasBeenInvoiced = 1;
        private const int XLEntryType = 2;
        private const int XLStartTime = 3;
        private const int XLEndTime = 4;
        private const int XLProjectID = 5;
        private const int XLPerson = 6;
        private const int XLAction = 7;

        private static readonly TrueFalseColumn HasBeenInvoicedColumn;
        private static readonly ListOfValuesColumn EntryTypeColumn;
        private static readonly DateColumn StartTimeColumn;
        private static readonly DateColumn EndTimeColumn;
        private static readonly TextColumn ProjectIDColumn;
        private static readonly TextColumn PersonColumn;
        private static readonly TextColumn ActionColumn;

        // layout of a subcontractor amount

        //private const int XLHasBeenInvoiced = 1;
        //private const int XLEntryType = 2;
        private const int XLSubContractorName = 3;
        private const int XLSubContractorAmount = 4;

        private static readonly TextColumn SubContractorNameColumn;
        private static readonly DecimalColumn SubContractorAmountColumn;

        // layout of an Other Expense amount

        //private const int XLHasBeenInvoiced = 1;
        //private const int XLEntryType = 2;
        private const int XLOtherExpenseDescription = 3;
        private const int XLOtherExpenseAmount = 4;

        private static readonly TextColumn OtherExpenseDescriptionColumn;
        private static readonly DecimalColumn OtherExpenseAmountColumn;

        static TimeSheetEntry()
        {
            // define the columns for a timesheet

            HasBeenInvoicedColumn = new TrueFalseColumn(XLHasBeenInvoiced, "HasBeenInvoiced", 7, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsRequired);
            EntryTypeColumn = new ListOfValuesColumn(XLEntryType, "EntryType",
                new List<string>() { "Time", "SubContractor", "OtherExpense" }, 20, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsRequired);
            StartTimeColumn = new DateColumn(XLStartTime, "StartTime", 20, ColumnFormat.ColumnIsOptional, ColumnFormat.FieldIsOptional);
            EndTimeColumn = new DateColumn(XLEndTime, "EndTime", 20, ColumnFormat.ColumnIsOptional, ColumnFormat.FieldIsOptional);
            ProjectIDColumn = new TextColumn(XLProjectID, "ProjectID", 40, ColumnFormat.ColumnIsOptional, ColumnFormat.FieldIsOptional);
            PersonColumn = new TextColumn(XLPerson, "Person", 100, ColumnFormat.ColumnIsOptional, ColumnFormat.FieldIsOptional); ;
            ActionColumn = new TextColumn(XLAction, "Action", 255, ColumnFormat.ColumnIsOptional, ColumnFormat.FieldIsOptional); ;

            // define the columns for a subcontractor amount

            SubContractorNameColumn = new TextColumn(XLSubContractorName, "SubContractorName", 255, ColumnFormat.ColumnIsOptional, ColumnFormat.FieldIsOptional);
            SubContractorAmountColumn = new DecimalColumn(XLSubContractorAmount, "SubContractorAmount", 10, ColumnFormat.ColumnIsOptional, ColumnFormat.FieldIsOptional);

            // Define the columns for an Other Expense amount


            OtherExpenseDescriptionColumn = new TextColumn(XLOtherExpenseDescription, "OtherExpense", 255, ColumnFormat.ColumnIsOptional, ColumnFormat.FieldIsOptional);
            OtherExpenseAmountColumn = new DecimalColumn(XLOtherExpenseAmount, "OtherExpenseAmt", 10, ColumnFormat.ColumnIsOptional, ColumnFormat.FieldIsOptional);

        }


        // validation


        // this sheet doesn't have column headers to validate
        internal static bool ValidateColumnHeaders(IXLWorksheet TimeSheetWorksheet, SheetFormat TimeSheetFormat, out string ErrorMessage)
        {
            ErrorMessage = "";
            return true;
        }



        internal static bool ValidateExcelRow(IXLRow XRow, SheetFormat TimeSheetFormat, out string ErrorMessage)
        {
            ErrorMessage = "";

            // check the HasBeenInvoiced flag - if no flag, then skip this row

            string THasBeenInvoiced = XRow.Cell(XLHasBeenInvoiced).GetString();
            if (THasBeenInvoiced.Length == 0) return true;
            if (!HasBeenInvoicedColumn.Valid(THasBeenInvoiced))
            {
                ErrorMessage = "Invalid has been invoiced flag " + THasBeenInvoiced;
                return false;
            }

            // check the type of row this is

            string RowType = XRow.Cell(XLEntryType).GetString();
            if (RowType.Length == 0) return true;
            if (!EntryTypeColumn.Valid(RowType))
            {
                ErrorMessage = "Invalid Row Type " + RowType;
                return false;
            }

            if (RowType == TimeEntryType.Time.ToString())
            {

                // check the start time

                DateTime TStartTime = XRow.Cell(XLStartTime).GetDateTime();
                if (!StartTimeColumn.Valid(TStartTime.ToString()))
                {
                    ErrorMessage = "Invalid start time " + TStartTime.ToString();
                    return false;
                }

                // check the End time

                DateTime TEndTime = XRow.Cell(XLEndTime).GetDateTime();
                if (!EndTimeColumn.Valid(TEndTime.ToString()))
                {
                    ErrorMessage = "Invalid end time " + TEndTime.ToString();
                    return false;
                }

                // check the project id

                string TProjectID = XRow.Cell(XLProjectID).GetString();
                if (!ProjectIDColumn.Valid(TProjectID))
                {
                    ErrorMessage = "Invalid project id " + TProjectID;
                    return false;
                }

                // check the Person 

                string PersonIdentifier = XRow.Cell(XLPerson).GetString();
                if (!PersonColumn.Valid(PersonIdentifier))
                {
                    ErrorMessage = "Invalid Person Identifier " + PersonIdentifier;
                    return false;
                }

                // Check the Action

                string TAction = XRow.Cell(XLAction).GetString();
                if (!ActionColumn.Valid(TAction))
                {
                    ErrorMessage = "Invalid time action " + TAction;
                    return false;
                }

            }

            // check the sub contractor row

            if (RowType == TimeEntryType.SubContractor.ToString())
            {
                // check the SubContractor

                string SubContractor = XRow.Cell(XLSubContractorName).GetString();
                if (!SubContractorNameColumn.Valid(SubContractor))
                {
                    ErrorMessage = "Invalid SubContractor " + SubContractor;
                    return false;
                }

                // Check the Amount

                string TAmount = XRow.Cell(XLSubContractorAmount).GetString();
                if (!SubContractorAmountColumn.Valid(TAmount))
                {
                    ErrorMessage = "Invalid SubContracTor Amount " + TAmount;
                    return false;
                }
            }
            // check the Other Expenses row

            if (RowType == TimeEntryType.OtherExpense.ToString())
            {
                // check the Description

                string Description = XRow.Cell(XLOtherExpenseDescription).GetString();
                if (!OtherExpenseDescriptionColumn.Valid(Description))
                {
                    ErrorMessage = "Invalid Other Expense description " + Description;
                    return false;
                }

                // Check the Amount

                string TOtherExpenseAmount = XRow.Cell(XLOtherExpenseAmount).GetString();
                if (!OtherExpenseAmountColumn.Valid(TOtherExpenseAmount))
                {
                    ErrorMessage = "Invalid Other Expense Amount " + TOtherExpenseAmount;
                    return false;
                }


            }

            return true;
        }



        internal void ParseExcelRow(IXLRow XRow, SheetFormat TimeSheetFormat)
        {
 
            HasBeenInvoiced = TrueFalseColumn.Parse(XRow.Cell(XLHasBeenInvoiced).GetString());
            string RowType = XRow.Cell(XLEntryType).GetString();

            if (RowType == TimeEntryType.Time.ToString())
            {
                WhichType = TimeEntryType.Time;
                DateTime ParsedTime;
                if (DateTime.TryParse(XRow.Cell(XLStartTime).GetString(), out ParsedTime))
                    StartTime = ParsedTime;
                if (DateTime.TryParse(XRow.Cell(XLEndTime).GetString(), out ParsedTime))
                    EndTime = ParsedTime;
                ProjectID = XRow.Cell(XLProjectID).GetString();
                Person = XRow.Cell(XLPerson).GetString();
                Action = XRow.Cell(XLAction).GetString();
            }

            if (RowType == TimeEntryType.SubContractor.ToString())
            {
                WhichType = TimeEntryType.SubContractor;
                SubcontractorName = XRow.Cell(XLSubContractorName).GetString();
                SubcontractorAmount = Decimal.Parse(XRow.Cell(XLSubContractorAmount).GetString());
            }

            if (RowType == TimeEntryType.OtherExpense.ToString())
            {
                WhichType = TimeEntryType.OtherExpense;
                OtherExpenseDescription = XRow.Cell(XLOtherExpenseDescription).GetString();
                OtherExpenseAmount = Decimal.Parse(XRow.Cell(XLOtherExpenseAmount).GetString());
            }
        }


        internal static void WriteXLHeader(IXLWorksheet TimeSheetWorksheet, SheetFormat TimeSheetFormat)
        {
            // does nothing. Simply here as every other sheet has this module
        }

        //IXLWorksheet TimeSheetWorksheet, SheetFormat TimeSheetFormat

        internal void WriteExcelRow(IXLRow XRow, SheetFormat ProjectFormat)
        {
            XRow.Cell(XLHasBeenInvoiced).Value = HasBeenInvoiced.ToString();
            XRow.Cell(XLEntryType).Value = WhichType.ToString();
            switch (WhichType)
            {
                case TimeEntryType.Time:
                    XRow.Cell(XLStartTime).Value = StartTime.ToString();
                    XRow.Cell(XLEndTime).Value = EndTime.ToString();
                    XRow.Cell(XLProjectID).Value = ProjectID;
                    XRow.Cell(XLPerson).Value = Person;
                    XRow.Cell(XLAction).Value = Action;
                    break;
                case TimeEntryType.SubContractor:
                    XRow.Cell(XLSubContractorName).Value = SubcontractorName;
                    XRow.Cell(XLSubContractorAmount).Value = SubcontractorAmount.ToString();
                    break;
                case TimeEntryType.OtherExpense:
                    XRow.Cell(XLOtherExpenseDescription).Value = OtherExpenseDescription;
                    XRow.Cell(XLOtherExpenseAmount).Value = OtherExpenseAmount.ToString();
                    break;
            }
        }

    }
}