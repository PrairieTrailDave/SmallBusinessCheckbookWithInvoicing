//**********************************************************************
//
//          Copyright © 2024 Prairie Trail Software, Inc.
//
//**********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessCheckBook.Validation;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing;

namespace BusinessCheckBook.DataStore
{

    // this reflects what I think are the most important metrics for my company - how many invoices are being sent out and what cash is coming in.
    // Other people may have a different idea of what is most important. 

    // This sheet can be regenerated each time.
    // This sheet does not need to exist when the file is read in.

    internal class BusinessActivities
    {

        // storage of the activites sheet

        List<YearlyBusinessActivity> BusinessHistory = [];

        // what the Excel sheet is supposed to look like
        internal SheetFormat BusinessHistoryFormat { get; set; } = new();


        public BusinessActivities() 
        { 
            BusinessHistory = [];
            SetSheetFormat();

        }

        public bool AnyActivitiesToWrite()
        {
            if (BusinessHistory.Count > 0)
                return true;
            return false;
        }


        public void UpdateThisYear(YearlyBusinessActivity YBA)
        {
            if (YBA.MonthlyBusinessActivities.Count > 0)
            {
                if (YBA.MonthlyBusinessActivities[0].Year == BusinessHistory[0].MonthlyBusinessActivities[0].Year)
                {
                    BusinessHistory.RemoveAt(0);
                    BusinessHistory.Insert(0, YBA);
                }
                else
                {
                    BusinessHistory.Insert(0, YBA);
                    YearlyBusinessActivity.AddAnotherSheetColumn(BusinessHistoryFormat);
                }
            }
        }

        public bool Validate(XLWorkbook CheckBookXlsx, out string ErrorMessage)
        {
            ErrorMessage = "";
            IXLWorksheet BusinessHistoryWorksheet;

            // if this sheet does not exist, that is not an error

            if (CheckBookXlsx.TryGetWorksheet(BusinessHistoryFormat.SheetName, out BusinessHistoryWorksheet))
            {

                // the sheet exists. check all the headings and find which column is which

                try
                {
                    BusinessHistoryFormat.SubFormats = [];
                    int NextCol = 0;
                    int LastCol = BusinessHistoryWorksheet.LastColumnUsed().ColumnNumber();
                    int NumberOfColumnsInBusinessHistory = 0;
                    int WhichYear = 0;

                    while (NextCol < BusinessHistoryWorksheet.ColumnsUsed().Count())
                    {
                        // define a format for the next set of columns

                        SheetFormat YearlyColumnsFormat = new();
                        MonthlyBusinessActivity.AddSheetColumns(YearlyColumnsFormat, NextCol);

                        // see if they are properly formatted

                        if (!YearlyBusinessActivity.ValidateColumnHeaders(NextCol, BusinessHistoryWorksheet, YearlyColumnsFormat, out ErrorMessage, out NumberOfColumnsInBusinessHistory))
                            return false;

                        // add columns to validation sheet

                        BusinessHistoryFormat.SubFormats.Add(YearlyColumnsFormat);

                        // and validate those columns

                        if (!YearlyBusinessActivity.ValidateExcelColumns(ref NextCol, BusinessHistoryWorksheet, YearlyColumnsFormat, out ErrorMessage))
                            return false;
                        WhichYear++;
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error in reading a cell " + ex.Message);
                    return false;
                }
            }
            else
                return true;
        }




        public bool ReadFromExcelFile(XLWorkbook CheckBookXlsx)
        {
            BusinessHistory = [];
            if (CheckBookXlsx.TryGetWorksheet(BusinessHistoryFormat.SheetName, out IXLWorksheet BusinessHistoryWorksheet))
            {
                foreach (SheetFormat YearlyColumnsFormat in BusinessHistoryFormat.SubFormats)
                {
                    // read off the monthly values from this year's columns

                    YearlyBusinessActivity YBA = new();
                    YBA.ReadMonthlyActivities(BusinessHistoryWorksheet, YearlyColumnsFormat);
                    BusinessHistory.Add(YBA);
                }
                return true;
            }
            return false;
        }




        public bool WriteToXLFile(XLWorkbook CheckBookXlsx)
        {
            // as this is an optional sheet.
            // Don't write it if nothing in memory

            if (BusinessHistory.Count > 0)
            {
                // add the business history worksheet
                CheckBookXlsx.AddWorksheet(BusinessHistoryFormat.SheetName);
                IXLWorksheet BusinessHistoryWorksheet = CheckBookXlsx.Worksheet(BusinessHistoryFormat.SheetName);

                // for each year saved, write it out

                int WhichYear = 0;
                foreach (YearlyBusinessActivity YBA in BusinessHistory)
                {
                    SheetFormat YearlyColumnsFormat = BusinessHistoryFormat.SubFormats[WhichYear];
                    YBA.WriteYearlyActivity(BusinessHistoryWorksheet, YearlyColumnsFormat);
                    WhichYear++;
                }
            }
            return true;
        }


        internal void SetSheetFormat()
        {
            BusinessHistoryFormat = new();
            BusinessHistoryFormat.SheetName = "BusinessActivity";
            YearlyBusinessActivity.AddSheetColumns(BusinessHistoryFormat);
        }

    }
}
