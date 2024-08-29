using BusinessCheckBook.Validation;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCheckBook.DataStore
{
    internal class TimeSheets
    {
        // storage of the timesheets

        List<TimeSheetEntry> CurrentTimeSheets = new();

        internal SheetFormat TimeSheetFormat { get; set; } = new();

        private bool Changed = false;

        public TimeSheets() 
        {
            CurrentTimeSheets.Clear();
            Changed = false;
            SetSheetFormat();
        }



        // public methods

        public bool IfChanged() { return Changed; }
        public void HasChanged() { Changed = true; }
        public void ClearChanged() { Changed = false; }



        public void Add(TimeSheetEntry entry)
        {
            CurrentTimeSheets.Add(entry);
        }

        public List<TimeSheetEntry> GetTimeSheets() { return CurrentTimeSheets; }
        public List<TimeSheetEntry> GetProjectBillableTimeSheets (string Project)
        {
            List<TimeSheetEntry> result = new();

            foreach (TimeSheetEntry TimeSheet in CurrentTimeSheets)
            {
                if (TimeSheet.ProjectID == Project)
                {
                    if (!TimeSheet.HasBeenInvoiced)
                    {
                        result.Add(TimeSheet);
                    }
                }
            }
            return result;
        }



        public bool Validate(XLWorkbook CheckBookXlsx, out string ErrorMessage)
        {
            ErrorMessage = "";
            IXLWorksheet TimeSheetsWorksheet;

            // a workbook might not support project list
            // a missing worksheet is not an error

            if (CheckBookXlsx.TryGetWorksheet(TimeSheetFormat.SheetName, out TimeSheetsWorksheet))
            {
                // the sheet exists. check all the data on the sheet

                try
                {
                    if (!TimeSheetEntry.ValidateColumnHeaders(TimeSheetsWorksheet, TimeSheetFormat, out ErrorMessage))
                        return false;

                    // validate all the entries in the columns for validity
                    // at this point, we have all the columns that are required

                    int RowsUsedCount = TimeSheetsWorksheet.RowsUsed().Count();
                    for (int Row = 2; Row < RowsUsedCount; Row++)
                    {
                        IXLRow XRow = TimeSheetsWorksheet.Row(Row);
                        if (!TimeSheetEntry.ValidateExcelRow(XRow, TimeSheetFormat, out ErrorMessage))
                        {
                            ErrorMessage += " in row " + Row.ToString();
                            return false;
                        }
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error in reading a cell " + ex.Message);
                    return false;
                }


            }
            return true;
        }


        public bool ReadFromExcelFile(XLWorkbook CheckBookXlsx)
        {
            CurrentTimeSheets = new();
            IXLWorksheet TimeSheetsWorksheet;

            // make this able to handle when the worksheet doesn't exist

            if (CheckBookXlsx.TryGetWorksheet(TimeSheetFormat.SheetName, out TimeSheetsWorksheet))
            {
                if (TimeSheetsWorksheet != null)
                {
                    int Row = 0;
                    foreach (var row in TimeSheetsWorksheet.RowsUsed())
                    {
                        // no header row

                        Row++;

                        // Pull off the TimeSheet Values

                        TimeSheetEntry NTimeSheet = new();
                        IXLRow xlRow = TimeSheetsWorksheet.Row(Row);

                        NTimeSheet.ParseExcelRow(xlRow, TimeSheetFormat);

                        CurrentTimeSheets.Add(NTimeSheet);
                    }
                    ClearChanged();
                    return true;
                }
            }
            return true;
        }





        internal void WriteXLTimeSheets(XLWorkbook CheckBookXlsx)
        {
            // TimeSheets is an optional sheet. Don't write if no TimeSheets defined.

            if (CurrentTimeSheets.Count > 0)
            {
                // add the worksheet
                CheckBookXlsx.AddWorksheet(TimeSheetFormat.SheetName);
                IXLWorksheet TimeSheetsWorksheet = CheckBookXlsx.Worksheet(TimeSheetFormat.SheetName);

                // add any column formatting needed

                // first build the header
                TimeSheetEntry.WriteXLHeader(TimeSheetsWorksheet, TimeSheetFormat);

                // then add all the rows

                int Row = 1;
                foreach (TimeSheetEntry RTimeSheet in CurrentTimeSheets)
                {
                    IXLRow xlRow = TimeSheetsWorksheet.Row(Row);
                    RTimeSheet.WriteExcelRow(xlRow, TimeSheetFormat);
                    Row++;
                }
            }
        }






        // support routines

        private void SetSheetFormat()
        {
            TimeSheetFormat = new();
            TimeSheetFormat.SheetName = "TimeSheets";
            // because this version uses dynamic row format, 
            // it doesn't set the column formats

        }
    }
}
