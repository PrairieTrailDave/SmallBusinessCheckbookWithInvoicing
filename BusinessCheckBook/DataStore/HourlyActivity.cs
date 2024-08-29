using BusinessCheckBook.Validation;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCheckBook.DataStore
{
    // This is a subset of the columns of the DailyActivity capturing the time sheet data
    internal class HourlyActivity
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string PersonsName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;



        // Excel column names
        // Column names and header values

        private const string XLStartTime = "StartTime";
        private const string XLEndTime = "EndTime";
        private const string XLPerson = "Person";
        private const string XLDescription = "Description";

        static readonly List<string> ColumnNames = new()
        {
            XLStartTime,
            XLEndTime,
            XLPerson,
            XLDescription
        };

        static int NumberOfColumnsInHourlyActivity = 4;

        internal static bool ValidateHourlyActivityHeader(IXLWorksheet ActivityWorksheet, int LastColumn, SheetFormat HourlyActivityColumnsFormat, out string ErrorMessage, out int HourlyActivityColumnCount)
        {
            ErrorMessage = "";
            HourlyActivityColumnCount = NumberOfColumnsInHourlyActivity;
            for (int ColumnNum = 0; ColumnNum < NumberOfColumnsInHourlyActivity; ColumnNum++)
            {
                ColumnFormat col = HourlyActivityColumnsFormat.Column(ColumnNum);
                if (col == null) continue;

                bool headerNotFound = true;
                string HeaderValue;


                // go through all columns to find it

                for (int ColNum = LastColumn + 1; ColNum < LastColumn + 1 + NumberOfColumnsInHourlyActivity; ColNum++)
                {
                    HeaderValue = ActivityWorksheet.Cell(1, ColNum).GetString().Trim();
                    if (col.Name.ToUpper() == HeaderValue.ToUpper())
                    {
                        headerNotFound = false;
                        col.ColumnNumber = ColNum;  // save the real column number
                        break;
                    }
                }
                if (headerNotFound)
                {
                    ErrorMessage = "The column " + col.Name + " is not in the Daily Activity Sheet";
                    return false;
                }

            }
            return true;
        }

        internal static bool ValidateExcelColumns(IXLRow XRow, ref int NextColumn, SheetFormat SubColumnsFormat, out string ErrorMessage)
        {
            ColumnFormat ThisColumn;

            ErrorMessage = "";

            foreach (string ColumnName in ColumnNames)
            {
                ThisColumn = SubColumnsFormat.Column(ColumnName)!;
                int Col = ThisColumn.ColumnNumber;
                string TColumnValue = XRow.Cell(Col).GetString();
                if (!ThisColumn.Valid(TColumnValue))
                {
                    ErrorMessage = "Invalid " + ColumnName + ": " + TColumnValue;
                    return false;
                }
            }

            NextColumn += NumberOfColumnsInHourlyActivity;
            return true;
        }


        internal int ParseExcelColumns(IXLRow XRow, SheetFormat HourlyActivityFormat)
        {
            ColumnFormat? Col;
            Col = HourlyActivityFormat.Column(XLStartTime)!;
            StartTime = XRow.Cell(Col.ColumnNumber).GetDateTime();
            Col = HourlyActivityFormat.Column(XLEndTime)!;
            EndTime = XRow.Cell(Col.ColumnNumber).GetDateTime();
            Col = HourlyActivityFormat.Column(XLPerson)!;
            PersonsName = XRow.Cell(Col.ColumnNumber).GetString();
            Col = HourlyActivityFormat.Column(XLDescription)!;
            Description = XRow.Cell(Col.ColumnNumber).GetString();

            return NumberOfColumnsInHourlyActivity;
        }



        internal void WriteExcelRow(IXLRow XRow, SheetFormat HourlyActivityFormat)
        {
            ColumnFormat? Col = HourlyActivityFormat.Column(XLStartTime)!;
            XRow.Cell(Col.ColumnNumber).Value = StartTime.ToShortTimeString();
            Col = HourlyActivityFormat.Column(XLEndTime)!;
            XRow.Cell(Col.ColumnNumber).Value = EndTime.ToShortTimeString();
            Col = HourlyActivityFormat.Column(XLPerson)!;
            XRow.Cell(Col.ColumnNumber).Value = PersonsName;
            Col = HourlyActivityFormat.Column(XLDescription)!;
            XRow.Cell(Col.ColumnNumber).Value = Description;
        }

        internal static void AddSheetColumns(SheetFormat HourlyActivityFormat, int offset)
        {
            HourlyActivityFormat.Add(new TimeColumn(1 + offset, XLStartTime, 10, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsOptional));
            HourlyActivityFormat.Add(new TimeColumn(2 + offset, XLEndTime, 10, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsOptional));
            HourlyActivityFormat.Add(new TextColumn(3 + offset, XLPerson, 100, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsOptional));
            HourlyActivityFormat.Add(new TextColumn(4 + offset, XLDescription, 255, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsOptional));

        }


    }
}
