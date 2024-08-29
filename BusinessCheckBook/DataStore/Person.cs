using BusinessCheckBook.Validation;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCheckBook.DataStore
{
    internal class Person
    {
        public bool IsActive { get; set; }
        public string Name { get; set; } = string.Empty;


        // Excel column names
        // Column names and header values

        private const string XLIsActive = "IsActive";
        private const string XLName = "Name";

        public Person() { }


        internal static bool ValidateColumnHeaders(IXLWorksheet PersonWorksheet, SheetFormat PersonFormat, out string ErrorMessage)
        {
            string HeaderValue;
            ErrorMessage = "";
            for (int ColumnNum = 0; ColumnNum < PersonFormat.Count(); ColumnNum++)
            {
                ColumnFormat col = PersonFormat.Column(ColumnNum);
                if (col == null) continue;

                // first see if the column listed in the format matches

                bool headerNotFound = true;
                int WhichWorksheetColumn = col.ColumnNumber;

                if (WhichWorksheetColumn > 0)
                {
                    HeaderValue = PersonWorksheet.Cell(1, WhichWorksheetColumn).GetString().Trim();
                    if (col.Name.ToUpper() == HeaderValue.ToUpper())
                        continue;
                }

                // if it didn't match, go through all columns to find it

                for (int ColNum = 1; ColNum <= PersonFormat.Count(); ColNum++)
                {
                    if (ColNum > PersonWorksheet.ColumnsUsed().Count()) break;
                    HeaderValue = PersonWorksheet.Cell(1, ColNum).GetString().Trim();
                    if (col.Name.ToUpper() == HeaderValue.ToUpper())
                    {
                        headerNotFound = false;
                        col.ColumnNumber = ColNum;
                        break;
                    }
                }
                if (headerNotFound)
                {
                    ErrorMessage = "The column " + col.Name + " is not in the Person Sheet";
                    return false;
                }

            }
            return true;
        }


        internal static bool ValidateExcelRow(IXLRow XRow, SheetFormat PersonFormat, out string ErrorMessage)
        {
            ColumnFormat ThisColumn;

            ErrorMessage = "";

            // check the IsActive flag - if no flag, then skip this row

            ThisColumn = PersonFormat.Column(XLIsActive)!;
            string TIsActive = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            if (TIsActive.Length == 0) return true;
            if (!ThisColumn.Valid(TIsActive))
            {
                ErrorMessage = "Invalid IsActive flag " + TIsActive;
                return false;
            }

            // Check the Name

            ThisColumn = PersonFormat.Column(XLName)!;
            string TName = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            if (!ThisColumn.Valid(TName))
            {
                ErrorMessage = "Invalid Name " + TName;
                return false;
            }

            return true;
        }

        public static bool ValidateName (string ProposedName, SheetFormat PersonFormat)
        {
            ColumnFormat ThisColumn;
            ThisColumn = PersonFormat.Column(XLName)!;
            return ThisColumn.Valid(ProposedName);
        }




        internal static void WriteXLHeader(IXLWorksheet PersonWorksheet, SheetFormat PersonFormat)
        {
            // write the main headers

            for (int i = 0; i < PersonFormat.Count(); i++)
            {
                ColumnFormat Col = PersonFormat.Column(i);
                PersonWorksheet.Cell(1, Col.ColumnNumber).Value = Col.Name;
            }

        }


        internal void ParseExcelRow(IXLRow XRow, SheetFormat PersonFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = PersonFormat.Column(XLIsActive)!;
            IsActive = TrueFalseColumn.Parse(XRow.Cell(ThisColumn.ColumnNumber).GetString());
            ThisColumn = PersonFormat.Column(XLName)!;
            Name = XRow.Cell(ThisColumn.ColumnNumber).GetString();
        }

        internal void WriteExcelRow(IXLRow XRow, SheetFormat PersonFormat)
        {

            ColumnFormat? Col = PersonFormat.Column(XLIsActive)!;
            XRow.Cell(Col.ColumnNumber).Value = IsActive.ToString();
            Col = PersonFormat.Column(XLName)!;
            XRow.Cell(Col.ColumnNumber).Value = Name;
        }





        internal static void AddSheetColumns(SheetFormat PersonFormat)
        {
            PersonFormat.Add(new TrueFalseColumn(1, XLIsActive, 6, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsRequired));
            PersonFormat.Add(new TextColumn(2, XLName, 255, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsRequired));
        }


    }
}
