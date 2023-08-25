//
//  Copyright 2023 David Randolph
//
using BusinessCheckBook.Validation;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCheckBook.DataStore
{
    public class InvoiceItem
    {
        public string Account { get; set; } = string.Empty;
        [DisplayName("Description")]
        public string ItemDescription { get; set; } = string.Empty;
        [DisplayName("Cost")]
        public decimal ItemCost { get; set; }
        [DisplayName("QTY")]
        public decimal ItemQuantity { get; set; }
        [DisplayName("Tax")]
        public decimal ItemTax { get; set; }
        [DisplayName("Price")]
        public decimal ItemPrice { get; set; }

        public const int NumberOfColumnsInInvoiceItem = 6;


        // Excel column names
        // Column names and header values

        private const string XLAccount = "Account";
        private const string XLDescription = "Description";
        private const string XLCost = "Cost";
        private const string XLQuantity = "Quantity";
        private const string XLTax = "Tax";
        private const string XLPrice = "Price";

        static readonly List<string> ColumnNames = new()
        {
            XLAccount,
            XLDescription,
            XLCost,
            XLQuantity,
            XLTax,
            XLPrice
        };

        internal static bool ValidateInvoiceItemHeader(IXLWorksheet TransactionsWorksheet, int LastColumn, SheetFormat InvoiceItemColumnsFormat, out string ErrorMessage, out int InvoiceItemColumnCount)
        {
            ErrorMessage = "";
            InvoiceItemColumnCount = NumberOfColumnsInInvoiceItem;
            for (int ColumnNum = 0; ColumnNum < NumberOfColumnsInInvoiceItem; ColumnNum++)
            {
                ColumnFormat col = InvoiceItemColumnsFormat.Column(ColumnNum);
                if (col == null) continue;

                bool headerNotFound = true;
                string HeaderValue;


                // go through all columns to find it

                for (int ColNum = LastColumn + 1; ColNum < LastColumn + 1 + NumberOfColumnsInInvoiceItem; ColNum++)
                {
                    HeaderValue = TransactionsWorksheet.Cell(1, ColNum).GetString().Trim();
                    if (col.Name.ToUpper() == HeaderValue.ToUpper())
                    {
                        headerNotFound = false;
                        col.ColumnNumber = ColNum;  // save the real column number
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

            foreach(string ColumnName in ColumnNames)
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

            NextColumn += NumberOfColumnsInInvoiceItem;
            return true;
        }

        // routines used in validating Journal import entries
        internal static bool ValidateAccount (string AccountName, SheetFormat SubColumnsFormat) 
        {
            ColumnFormat ThisColumn;
            ThisColumn = SubColumnsFormat.Column(XLAccount)!;
            if (!ThisColumn.Valid(AccountName))
                return false;
            return true;
        }
        internal static bool ValidateMemo(string TMemo, SheetFormat SubColumnsFormat)
        {
            ColumnFormat ThisColumn;
            ThisColumn = SubColumnsFormat.Column(XLDescription)!;
            if (!ThisColumn.Valid(TMemo))
                return false;
            return true;
        }
        internal static bool ValidateAmount(string Amount, SheetFormat SubColumnsFormat)
        {
            ColumnFormat ThisColumn;
            ThisColumn = SubColumnsFormat.Column(XLPrice)!;
            if (!ThisColumn.Valid(Amount))
                return false;
            return true;
        }



        internal int ParseExcelColumns(IXLRow XRow, SheetFormat SubColumnsFormat)
        {
            ColumnFormat? Col;
            Col = SubColumnsFormat.Column(XLAccount)!;
            Account = XRow.Cell(Col.ColumnNumber).GetString();
            Col = SubColumnsFormat.Column(XLDescription)!;
            ItemDescription = XRow.Cell(Col.ColumnNumber).GetString();
            Col = SubColumnsFormat.Column(XLCost)!;
            ItemCost = Col.GetValue(XRow.Cell(Col.ColumnNumber).GetString());
            Col = SubColumnsFormat.Column(XLQuantity)!;
            ItemQuantity = Col.GetValue(XRow.Cell(Col.ColumnNumber).GetString());
            Col = SubColumnsFormat.Column(XLTax)!;
            ItemTax = Col.GetValue(XRow.Cell(Col.ColumnNumber).GetString());
            Col = SubColumnsFormat.Column(XLPrice)!;
            ItemPrice = Col.GetValue(XRow.Cell(Col.ColumnNumber).GetString());

            return NumberOfColumnsInInvoiceItem;
        }



        internal void WriteExcelRow(IXLRow XRow, SheetFormat InvoiceListFormat)
        {
            ColumnFormat? Col = InvoiceListFormat.Column(XLAccount)!;
            XRow.Cell(Col.ColumnNumber).Value = Account ?? "";
            Col = InvoiceListFormat.Column(XLDescription)!;
            XRow.Cell(Col.ColumnNumber).Value = ItemDescription;
            Col = InvoiceListFormat.Column(XLCost)!;
            XRow.Cell(Col.ColumnNumber).Value = ItemCost;
            Col = InvoiceListFormat.Column(XLQuantity)!;
            XRow.Cell(Col.ColumnNumber).Value = ItemQuantity;
            Col = InvoiceListFormat.Column(XLTax)!;
            XRow.Cell(Col.ColumnNumber).Value = ItemTax;
            Col = InvoiceListFormat.Column(XLPrice)!;
            XRow.Cell(Col.ColumnNumber).Value = ItemPrice;
        }

        internal static void AddSheetColumns(SheetFormat InvoiceListFormat, int offset)
        {
            InvoiceListFormat.Add(new TextColumn(1 + offset, XLAccount, 30, ColumnFormat.ColumnIsOptional, ColumnFormat.FieldIsOptional));
            InvoiceListFormat.Add(new TextColumn(2 + offset, XLDescription, 1000, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsOptional));
            InvoiceListFormat.Add(new DecimalColumn(3 + offset, XLCost, 20, ColumnFormat.ColumnIsOptional, ColumnFormat.FieldIsOptional));
            InvoiceListFormat.Add(new DecimalColumn(4 + offset, XLQuantity, 20, ColumnFormat.ColumnIsOptional, ColumnFormat.FieldIsOptional));
            InvoiceListFormat.Add(new DecimalColumn(5 + offset, XLTax, 20, ColumnFormat.ColumnIsOptional, ColumnFormat.FieldIsRequired));
            InvoiceListFormat.Add(new DecimalColumn(6 + offset, XLPrice, 20, ColumnFormat.ColumnIsOptional, ColumnFormat.FieldIsRequired));

        }
    }
}
