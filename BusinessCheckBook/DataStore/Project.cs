using BusinessCheckBook.Settings;
using BusinessCheckBook.Validation;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Vml;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCheckBook.DataStore
{
    // This class is used to manage the projects that can be tracked with the daily activity.
    // One idea is that a customer could have different projects with different bill rates.

    internal class Project
    {
        public bool IsActive { get; set; }
        public string AllocatedAccount { get; set; } = string.Empty;
        public string ProjectID { get; set; } = string.Empty;
        public string CustomerID { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal BillRate { get; set; }


        // Excel column names
        // Column names and header values

        private const string XLIsActive = "IsActive";
        private const string XLAllocatedAccount = "AllotAcnt";
        private const string XLProjectID = "ProjectID";
        private const string XLCustomerID = "CustomerID";
        private const string XLDescription = "Description";
        private const string XLBillRate = "BillRate";

        public Project() { }

        #region validate

        internal static bool ValidateColumnHeaders(IXLWorksheet ProjectWorksheet, SheetFormat ProjectFormat, out string ErrorMessage)
        {
            string HeaderValue;
            ErrorMessage = "";
            for (int ColumnNum = 0; ColumnNum < ProjectFormat.Count(); ColumnNum++)
            {
                ColumnFormat col = ProjectFormat.Column(ColumnNum);
                if (col == null) continue;

                // first see if the column listed in the format matches

                bool headerNotFound = true;
                int WhichWorksheetColumn = col.ColumnNumber;

                if (WhichWorksheetColumn > 0)
                {
                    HeaderValue = ProjectWorksheet.Cell(1, WhichWorksheetColumn).GetString().Trim();
                    if (col.Name.ToUpper() == HeaderValue.ToUpper())
                        continue;
                }

                // if it didn't match, go through all columns to find it

                for (int ColNum = 1; ColNum <= ProjectFormat.Count(); ColNum++)
                {
                    if (ColNum > ProjectWorksheet.ColumnsUsed().Count()) break;
                    HeaderValue = ProjectWorksheet.Cell(1, ColNum).GetString().Trim();
                    if (col.Name.ToUpper() == HeaderValue.ToUpper())
                    {
                        headerNotFound = false;
                        col.ColumnNumber = ColNum;
                        break;
                    }
                }
                if (headerNotFound)
                {
                    ErrorMessage = "The column " + col.Name + " is not in the Project Sheet";
                    return false;
                }

            }
            return true;
        }

        internal static bool ValidateExcelRow(IXLRow XRow, SheetFormat ProjectFormat, out string ErrorMessage)
        {
            ColumnFormat ThisColumn;

            ErrorMessage = "";

            // check the IsActive flag - if no flag, then skip this row

            ThisColumn = ProjectFormat.Column(XLIsActive)!;
            string TIsActive = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            if (TIsActive.Length == 0) return true;
            if (!ThisColumn.Valid(TIsActive))
            {
                ErrorMessage = "Invalid is active flag " + TIsActive;
                return false;
            }

            // check the allocated account

            ThisColumn = ProjectFormat.Column(XLProjectID)!;
            string TAllocatedAccount = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            if (!ThisColumn.Valid(TAllocatedAccount))
            {
                ErrorMessage = "Invalid allocated account " + TAllocatedAccount;
                return false;
            }


            // check the project id

            ThisColumn = ProjectFormat.Column(XLProjectID)!;
            string TProjectID = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            if (!ThisColumn.Valid(TProjectID))
            {
                ErrorMessage = "Invalid project id " + TProjectID;
                return false;
            }

            // check the Customer Identifier 

            ThisColumn = ProjectFormat.Column(XLCustomerID)!;
            string CustomerIdentifier = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            if (!ThisColumn.Valid(CustomerIdentifier))
            {
                ErrorMessage = "Invalid Customer Identifier " + CustomerIdentifier;
                return false;
            }

            // Check the Description

            ThisColumn = ProjectFormat.Column(XLDescription)!;
            string TDescription = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            if (!ThisColumn.Valid(TDescription))
            {
                ErrorMessage = "Invalid description " + TDescription;
                return false;
            }

            // Check the Bill Rate

            ThisColumn = ProjectFormat.Column(XLBillRate)!;
            string TBillRate = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            if (!ThisColumn.Valid(TBillRate))
            {
                ErrorMessage = "Invalid Bill Rate " + TBillRate;
                return false;
            }


            return true;
        }

        public static bool ValidateProjectName (string projectName, SheetFormat ProjectFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = ProjectFormat.Column(XLProjectID)!;
            return ThisColumn.Valid(projectName);
        }

        public static bool ValidateProjectDescription(string projectDescription, SheetFormat ProjectFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = ProjectFormat.Column(XLDescription)!;
            return ThisColumn.Valid(projectDescription);
        }

        public static bool ValidateCustomerID(string CustomerID, SheetFormat ProjectFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = ProjectFormat.Column(XLCustomerID)!;
            return ThisColumn.Valid(CustomerID);
        }

        public static bool ValidateBillRate(string BillRate, SheetFormat ProjectFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = ProjectFormat.Column(XLBillRate)!;
            return ThisColumn.Valid(BillRate);
        }

        #endregion Validate

        internal static void WriteXLHeader(IXLWorksheet ProjectWorksheet, SheetFormat ProjectFormat)
        {
            // write the main headers

            for (int i = 0; i < ProjectFormat.Count(); i++)
            {
                ColumnFormat Col = ProjectFormat.Column(i);
                ProjectWorksheet.Cell(1, Col.ColumnNumber).Value = Col.Name;
            }

        }


        internal void ParseExcelRow(IXLRow XRow, SheetFormat ProjectFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = ProjectFormat.Column(XLIsActive)!;
            IsActive = TrueFalseColumn.Parse(XRow.Cell(ThisColumn.ColumnNumber).GetString());
            ThisColumn = ProjectFormat.Column(XLAllocatedAccount)!;
            AllocatedAccount = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            ThisColumn = ProjectFormat.Column(XLProjectID)!;
            ProjectID = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            ThisColumn = ProjectFormat.Column(XLCustomerID)!;
            CustomerID = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            ThisColumn = ProjectFormat.Column(XLDescription)!;
            Description = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            ThisColumn = ProjectFormat.Column(XLBillRate)!;
            BillRate = Decimal.Parse(XRow.Cell(ThisColumn.ColumnNumber).GetString());
        }

        internal void WriteExcelRow(IXLRow XRow, SheetFormat ProjectFormat)
        {

            ColumnFormat? Col = ProjectFormat.Column(XLIsActive)!;
            XRow.Cell(Col.ColumnNumber).Value = IsActive.ToString();
            Col = ProjectFormat.Column(XLAllocatedAccount)!;
            XRow.Cell(Col.ColumnNumber).Value = AllocatedAccount;
            Col = ProjectFormat.Column(XLProjectID)!;
            XRow.Cell(Col.ColumnNumber).Value = ProjectID;
            Col = ProjectFormat.Column(XLCustomerID)!;
            XRow.Cell(Col.ColumnNumber).Value = CustomerID;
            Col = ProjectFormat.Column(XLDescription)!;
            XRow.Cell(Col.ColumnNumber).Value = Description;
            Col = ProjectFormat.Column(XLBillRate)!;
            XRow.Cell(Col.ColumnNumber).Value = BillRate.ToString();
        }





        internal static void AddSheetColumns(SheetFormat ProjectFormat)
        {
            ProjectFormat.Add(new TrueFalseColumn(1, XLIsActive, 6, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsRequired));
            ProjectFormat.Add(new TextColumn(2, XLAllocatedAccount, 50, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsRequired));
            ProjectFormat.Add(new TextColumn(3, XLProjectID, 50, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsRequired));
            ProjectFormat.Add(new TextColumn(4, XLCustomerID, 100, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsRequired));
            ProjectFormat.Add(new TextColumn(5, XLDescription, 255, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsRequired));
            ProjectFormat.Add(new DecimalColumn(6, XLBillRate, 10, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsRequired));
        }


    }
}
