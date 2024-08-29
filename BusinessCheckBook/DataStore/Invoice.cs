//**********************************************************************
//
//          Copyright © 2023 Prairie Trail Software, Inc.
//
//**********************************************************************

using BusinessCheckBook.Validation;
using ClosedXML.Excel;

namespace BusinessCheckBook.DataStore
{
    public class Invoice
    {
        public bool Paid { get; set; }
        public string CustomerIdentifier { get; set; } = string.Empty;
        public string BillingAddress1 { get; set; } = string.Empty;
        public string BillingAddress2 { get; set; } = string.Empty;
        public string BillingAddress3 { get; set; } = string.Empty;
        public string BillingAddress4 { get; set; } = string.Empty;
        public string BillingAddress5 { get; set; } = string.Empty;
        public string BillingDate { get; set; } = string.Empty;
        public int InvoiceNumber { get; set;}
        public string DueDate { get; set;} = string.Empty;
        public List<InvoiceItem> InvoiceBreakdown { get; set; } = new();
        public string CustomerMemo { get; set;} = string.Empty;
        public decimal Tax { get; set;} = decimal.Zero;
        public decimal Total { get; set;} = decimal.Zero;
        public decimal AmountPaid { get; set;}= decimal.Zero;
        public decimal BalanceDue { get; set; } = decimal.Zero;  // not saved in database, Included here so that we can print it

   
        // adding all this below allows one file to contain the names of all the fields and all the formats 

        // Excel column names
        // Column names and header values

        private const string XLPaid = "Paid";
        private const string XLCustomerIdentifier = "CustomerIdentifier";
        private const string XLBillingAddress1 = "BillingAddress1";
        private const string XLBillingAddress2 = "BillingAddress2";
        private const string XLBillingAddress3 = "BillingAddress3";
        private const string XLBillingAddress4 = "BillingAddress4";
        private const string XLBillingAddress5 = "BillingAddress5";
        private const string XLBillingDate = "BillingDate";
        private const string XLInvoiceNumber = "InvoiceNumber";
        private const string XLDueDate = "DueDate";
        private const string XLCustomerMemo = "CustomerMemo";
        private const string XLTax = "Tax";
        private const string XLTotal = "Total";
        private const string XLAmountPaid = "AmountPaid";
        

        public Invoice()
        {
            Paid = false;
        }


        internal static bool ValidateColumnHeaders(IXLWorksheet InvoiceWorksheet, SheetFormat InvoiceListFormat, out string ErrorMessage)
        {
            string HeaderValue;
            ErrorMessage = "";
            for (int ColumnNum = 0; ColumnNum < InvoiceListFormat.Count(); ColumnNum++)
            {
                ColumnFormat col = InvoiceListFormat.Column(ColumnNum);
                if (col == null) continue;

                // first see if the column listed in the format matches

                bool headerNotFound = true;
                int WhichWorksheetColumn = col.ColumnNumber;

                if (WhichWorksheetColumn > 0)
                {
                    HeaderValue = InvoiceWorksheet.Cell(1, WhichWorksheetColumn).GetString().Trim();
                    if (col.Name.ToUpper() == HeaderValue.ToUpper())
                        continue;
                }

                // if it didn't match, go through all columns to find it

                for (int ColNum = 1; ColNum <= InvoiceListFormat.Count(); ColNum++)
                {
                    if (ColNum > InvoiceWorksheet.ColumnsUsed().Count()) break;
                    HeaderValue = InvoiceWorksheet.Cell(1, ColNum).GetString().Trim();
                    if (col.Name.ToUpper() == HeaderValue.ToUpper())
                    {
                        headerNotFound = false;
                        col.ColumnNumber = ColNum;
                        break;
                    }
                }
                if (headerNotFound)
                {
                    ErrorMessage = "The column " + col.Name + " is not in the Invoices Sheet";
                    return false;
                }

            }


            // go through the columns for the breakdown 

            InvoiceListFormat.SubFormats = new();
            int LastColumn = InvoiceListFormat.Count();
            while (LastColumn < InvoiceWorksheet.ColumnsUsed().Count())
            {
                HeaderValue = InvoiceWorksheet.Cell(1, LastColumn + 1).GetString().Trim();
                if (HeaderValue.Length > 0)
                {
                    // add another set of breakdown formats

                    SheetFormat BreakdownColumnsFormat = new();
                    InvoiceItem.AddSheetColumns(BreakdownColumnsFormat, LastColumn);
                    InvoiceListFormat.SubFormats.Add(BreakdownColumnsFormat);

                    // and validate against those formats
                    if (!InvoiceItem.ValidateInvoiceItemHeader(InvoiceWorksheet, LastColumn, BreakdownColumnsFormat, out ErrorMessage, out int BreakdownColumnCount))
                        return false;
                    LastColumn += BreakdownColumnCount;
                }
                else
                    break;
            }
            return true;

        }


        internal static bool ValidateExcelRow(IXLRow XRow, SheetFormat InvoiceListFormat, out string ErrorMessage)
        {
            ColumnFormat ThisColumn;

            ErrorMessage = "";

            // check the Paid flag - if no flag, then skip this row

            ThisColumn = InvoiceListFormat.Column(XLPaid)!;
            string TPaid = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            if (TPaid.Length == 0) return true;
            if (!ThisColumn.Valid(TPaid))
            {
                ErrorMessage = "Invalid invoice paid flag " + TPaid;
                return false;
            }

            // check the Customer Identifier 

            ThisColumn = InvoiceListFormat.Column(XLCustomerIdentifier)!;
            string CustomerIdentifier = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            if (!ThisColumn.Valid(CustomerIdentifier))
            {
                ErrorMessage = "Invalid Customer Identifier " + CustomerIdentifier;
                return false;
            }

            // Check the BillingAddress1

            ThisColumn = InvoiceListFormat.Column(XLBillingAddress1)!;
            string TBillingAddress1 = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            if (!ThisColumn.Valid(TBillingAddress1))
            {
                ErrorMessage = "Invalid BillingAddress1 " + TBillingAddress1;
                return false;
            }

            // Check the BillingAddress2

            ThisColumn = InvoiceListFormat.Column(XLBillingAddress2)!;
            string TBillingAddress2 = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            if (!ThisColumn.Valid(TBillingAddress2))
            {
                ErrorMessage = "Invalid BillingAddress2 " + TBillingAddress2;
                return false;
            }

            // Check the BillingAddress3

            ThisColumn = InvoiceListFormat.Column(XLBillingAddress3)!;
            string TBillingAddress3 = XRow.Cell(ThisColumn.ColumnNumber).GetString();

            if (!ThisColumn.Valid(TBillingAddress3))
            {
                ErrorMessage = "Invalid BillingAddress3 " + TBillingAddress3;
                return false;
            }


            ThisColumn = InvoiceListFormat.Column(XLBillingAddress4)!;
            string TBillingAddress4 = XRow.Cell(ThisColumn.ColumnNumber).GetString();

            if (!ThisColumn.Valid(TBillingAddress4))
            {
                ErrorMessage = "Invalid BillingAddress4 " + TBillingAddress4;
                return false;
            }

            // Check the BillingAddress5

            ThisColumn = InvoiceListFormat.Column(XLBillingAddress5)!;
            string TBillingAddress5 = XRow.Cell(ThisColumn.ColumnNumber).GetString();

            if (!ThisColumn.Valid(TBillingAddress5))
            {
                ErrorMessage = "Invalid BillingAddress5  " + TBillingAddress5;
                return false;
            }

            // Check the BillingDate

            ThisColumn = InvoiceListFormat.Column(XLBillingDate)!;
            string TBillingDate = XRow.Cell(ThisColumn.ColumnNumber).GetString();

            if (!ThisColumn.Valid(TBillingDate))
            {
                ErrorMessage = "Invalid BillingDate " + TBillingDate;
                return false;
            }


            // Check the InvoiceNumber

            ThisColumn = InvoiceListFormat.Column(XLInvoiceNumber)!;
            string TInvoiceNumber = XRow.Cell(ThisColumn.ColumnNumber).GetString();

            if (!ThisColumn.Valid(TInvoiceNumber))
            {
                ErrorMessage = "Invalid InvoiceNumber  " + TInvoiceNumber;
                return false;
            }

            // Check the DueDate
            ThisColumn = InvoiceListFormat.Column(XLDueDate)!;
            string TDueDate = XRow.Cell(InvoiceListFormat.Column(XLDueDate)!.ColumnNumber).GetString();
            if (!ThisColumn.Valid(TDueDate))
            {
                ErrorMessage = "Invalid DueDate  " + TDueDate;
                return false;
            }

            // Check the CustomerMemo

            ThisColumn = InvoiceListFormat.Column(XLCustomerMemo)!;
            string TCustomerMemo = XRow.Cell(ThisColumn.ColumnNumber).GetString();

            if (!ThisColumn.Valid(TCustomerMemo))
            {
                ErrorMessage = "Invalid CustomerMemo " + TCustomerMemo;
                return false;
            }


            // Check the Tax

            ThisColumn = InvoiceListFormat.Column(XLTax)!;
            string TTax = XRow.Cell(ThisColumn.ColumnNumber).GetString();

            if (!ThisColumn.Valid(TTax))
            {
                ErrorMessage = "Invalid Tax " + TTax;
                return false;
            }


            // Check the Total

            ThisColumn = InvoiceListFormat.Column(XLTotal)!;
            string TTotal = XRow.Cell(ThisColumn.ColumnNumber).GetString();

            if (!ThisColumn.Valid(TTotal))
            {
                ErrorMessage = "Invalid Total " + TTotal;
                return false;
            }


            // Check the Amount Paid

            ThisColumn = InvoiceListFormat.Column(XLAmountPaid)!;
            string TAmountPaid = XRow.Cell(ThisColumn.ColumnNumber).GetString();

            if (!ThisColumn.Valid(TAmountPaid))
            {
                ErrorMessage = "Invalid Amount Paid " + TAmountPaid;
                return false;
            }

            // check all the sub items

            int NextColumn = InvoiceListFormat.Count() + 1;
            int Breakdown = 0;
            while (XRow.Cell(NextColumn).GetString().Length > 0)
            {
                if (!InvoiceItem.ValidateExcelColumns(XRow, ref NextColumn, InvoiceListFormat.SubFormats[Breakdown++], out ErrorMessage))
                    return false;
            }
            return true;
        }



        internal static bool ValidateCustomerID(string CustomerIdentifier, SheetFormat CustomerListFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = CustomerListFormat.Column(XLCustomerIdentifier)!;
            if (!ThisColumn.Valid(CustomerIdentifier))
                return false;
            return true;
        }
        internal static bool ValidateBillingAddress1(string BillingAddress1, SheetFormat CustomerListFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = CustomerListFormat.Column(XLBillingAddress1)!;
            if (!ThisColumn.Valid(BillingAddress1))
                return false;
            return true;
        }
        internal static bool ValidateBillingAddress2(string TBillingAddress2, SheetFormat CustomerListFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = CustomerListFormat.Column(XLBillingAddress2)!;
            if (!ThisColumn.Valid(TBillingAddress2))
                return false;
            return true;
        }
        internal static bool ValidateBillingAddress3(string TBillingAddress3, SheetFormat CustomerListFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = CustomerListFormat.Column(XLBillingAddress3)!;
            if (!ThisColumn.Valid(TBillingAddress3))
                return false;
            return true;
        }
        internal static bool ValidateBillingAddress4(string TBillingAddress4, SheetFormat CustomerListFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = CustomerListFormat.Column(XLBillingAddress4)!;
            if (!ThisColumn.Valid(TBillingAddress4))
                return false;
            return true;
        }
        internal static bool ValidateBillingAddress5(string TBillingAddress5, SheetFormat CustomerListFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = CustomerListFormat.Column(XLBillingAddress5)!;
            if (!ThisColumn.Valid(TBillingAddress5))
                return false;
            return true;
        }
        internal static bool ValidateBillingDate(string TBillingDate, SheetFormat CustomerListFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = CustomerListFormat.Column(XLBillingDate)!;
            if (!ThisColumn.Valid(TBillingDate))
                return false;
            return true;
        }
        internal static bool ValidateInvoiceNumber(string TInvoiceNumber, SheetFormat CustomerListFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = CustomerListFormat.Column(XLInvoiceNumber)!;
            if (!ThisColumn.Valid(TInvoiceNumber))
                return false;
            return true;
        }
        internal static bool ValidateDueDate(string TDueDate, SheetFormat CustomerListFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = CustomerListFormat.Column(XLDueDate)!;
            if (!ThisColumn.Valid(TDueDate))
                return false;
            return true;
        }
        internal static bool ValidateCustomerMemo(string TCustomerMemo, SheetFormat CustomerListFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = CustomerListFormat.Column(XLCustomerMemo)!;
            if (!ThisColumn.Valid(TCustomerMemo))
                return false;
            return true;
        }
        internal static bool ValidateTax(string TTax, SheetFormat CustomerListFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = CustomerListFormat.Column(XLTax)!;
            if (!ThisColumn.Valid(TTax))
                return false;
            return true;
        }
        internal static bool ValidateTotal(string TTotal, SheetFormat CustomerListFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = CustomerListFormat.Column(XLTotal)!;
            if (!ThisColumn.Valid(TTotal))
                return false;
            return true;
        }
        internal static bool ValidateAmountPaid(string TBalanceDue, SheetFormat CustomerListFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = CustomerListFormat.Column(XLAmountPaid)!;

            if (!ThisColumn.Valid(TBalanceDue))
                return false;
            return true;
        }






        internal int BreakdownCount()
        {
            int results = 0;
            foreach (InvoiceItem II in InvoiceBreakdown)
            {
                if (II.Account.Length > 0) results++;
            }
            return results;
        }


        internal static void WriteXLHeader(IXLWorksheet InvoicesWorksheet, SheetFormat InvoiceListFormat, int breakdowns)
        {
            // write the main headers

            for (int i = 0; i < InvoiceListFormat.Count(); i++)
            {
                ColumnFormat Col = InvoiceListFormat.Column(i);
                InvoicesWorksheet.Cell(1, Col.ColumnNumber).Value = Col.Name;
            }

            // write all the breakdown headers

            InvoiceListFormat.SubFormats = new();
            int Offset = InvoiceListFormat.Count();
            for (int br = 0; br < breakdowns; br++)
            {
                InvoiceListFormat.SubFormats.Add(new SheetFormat());
                InvoiceItem.AddSheetColumns(InvoiceListFormat.SubFormats[br], Offset);
                for (int i = 0; i < InvoiceListFormat.SubFormats[br].Count(); i++)
                {
                    ColumnFormat Col = InvoiceListFormat.SubFormats[br].Column(i);
                    InvoicesWorksheet.Cell(1, Col.ColumnNumber).Value = Col.Name;
                    Offset++;
                }
            }
        }


        internal void ParseExcelRow(IXLRow XRow, SheetFormat InvoiceListFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = InvoiceListFormat.Column(XLPaid)!;
            Paid = TrueFalseColumn.Parse(XRow.Cell(ThisColumn.ColumnNumber).GetString());
            ThisColumn = InvoiceListFormat.Column(XLCustomerIdentifier)!;
            CustomerIdentifier = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            ThisColumn = InvoiceListFormat.Column(XLBillingAddress1)!;
            BillingAddress1 = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            ThisColumn = InvoiceListFormat.Column(XLBillingAddress2)!;
            BillingAddress2 = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            ThisColumn = InvoiceListFormat.Column(XLBillingAddress3)!;
            BillingAddress3 = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            ThisColumn = InvoiceListFormat.Column(XLBillingAddress4)!;
            BillingAddress4 = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            ThisColumn = InvoiceListFormat.Column(XLBillingAddress5)!;
            BillingAddress5 = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            ThisColumn = InvoiceListFormat.Column(XLBillingDate)!;
            BillingDate = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            ThisColumn = InvoiceListFormat.Column(XLInvoiceNumber)!;
            InvoiceNumber = Int32.Parse(XRow.Cell(ThisColumn.ColumnNumber).GetString());
            ThisColumn = InvoiceListFormat.Column(XLDueDate)!;
            DueDate = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            ThisColumn = InvoiceListFormat.Column(XLCustomerMemo)!;
            CustomerMemo = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            ThisColumn = InvoiceListFormat.Column(XLTax)!;
            Tax = ThisColumn.GetValue(XRow.Cell(ThisColumn.ColumnNumber).GetString());
            ThisColumn = InvoiceListFormat.Column(XLTotal)!;
            Total = Decimal.Round(ThisColumn.GetValue(XRow.Cell(ThisColumn.ColumnNumber).GetString()), 2);
            ThisColumn = InvoiceListFormat.Column(XLAmountPaid)!;
            AmountPaid = Decimal.Round(ThisColumn.GetValue(XRow.Cell(ThisColumn.ColumnNumber).GetString()), 2);

                // parse the breakdown

            int NextColumn = InvoiceListFormat.ColumnFormats.Count + 1;
            int breakdown = 0;
            while (XRow.Cell(NextColumn).GetString().Trim().Length > 0)
            {
                InvoiceItem IIT = new();
                int count = IIT.ParseExcelColumns(XRow, InvoiceListFormat.SubFormats[breakdown++]);
                InvoiceBreakdown.Add(IIT);
                NextColumn += count;
            }
        }

        internal void WriteExcelRow(IXLRow XRow, SheetFormat InvoiceListFormat)
        {

            ColumnFormat? Col = InvoiceListFormat.Column(XLPaid)!;
            XRow.Cell(Col.ColumnNumber).Value = Paid.ToString();
            Col = InvoiceListFormat.Column(XLCustomerIdentifier)!;
            XRow.Cell(Col.ColumnNumber).Value = CustomerIdentifier.ToString();
            Col = InvoiceListFormat.Column(XLBillingAddress1)!;
            XRow.Cell(Col.ColumnNumber).Value = BillingAddress1;
            Col = InvoiceListFormat.Column(XLBillingAddress2)!;
            XRow.Cell(Col.ColumnNumber).Value = BillingAddress2;
            Col = InvoiceListFormat.Column(XLBillingAddress3)!;
            XRow.Cell(Col.ColumnNumber).Value = BillingAddress3;
            Col = InvoiceListFormat.Column(XLBillingAddress4)!;
            XRow.Cell(Col.ColumnNumber).Value = BillingAddress4;
            Col = InvoiceListFormat.Column(XLBillingAddress5)!;
            XRow.Cell(Col.ColumnNumber).Value = BillingAddress5;
            Col = InvoiceListFormat.Column(XLBillingDate)!;
            XRow.Cell(Col.ColumnNumber).Value = BillingDate;
            Col = InvoiceListFormat.Column(XLInvoiceNumber)!;
            XRow.Cell(Col.ColumnNumber).Value = InvoiceNumber;
            Col = InvoiceListFormat.Column(XLDueDate)!;
            XRow.Cell(Col.ColumnNumber).Value = DueDate;
            Col = InvoiceListFormat.Column(XLCustomerMemo)!;
            XRow.Cell(Col.ColumnNumber).Value = CustomerMemo;
            Col = InvoiceListFormat.Column(XLTax)!;
            XRow.Cell(Col.ColumnNumber).Value = Tax;
            Col = InvoiceListFormat.Column(XLTotal)!;
            XRow.Cell(Col.ColumnNumber).Value = Total;
            Col = InvoiceListFormat.Column(XLAmountPaid)!;
            XRow.Cell(Col.ColumnNumber).Value = AmountPaid;

            // write the breakdown
            int br = 0;
            foreach (InvoiceItem InvI in InvoiceBreakdown)
            {
                if (InvoiceListFormat.SubFormats.Count == br) break;
                InvI.WriteExcelRow(XRow, InvoiceListFormat.SubFormats[br++]);
            }
        }





        internal static void AddSheetColumns(SheetFormat InvoiceListFormat)
        {
            InvoiceListFormat.Add(new TrueFalseColumn(1, XLPaid, 6, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsRequired));
            InvoiceListFormat.Add(new TextColumn(2, XLCustomerIdentifier, 50, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsRequired));
            InvoiceListFormat.Add(new TextColumn(3, XLBillingAddress1, 60, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsRequired));
            InvoiceListFormat.Add(new TextColumn(4, XLBillingAddress2, 255, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsRequired));
            InvoiceListFormat.Add(new TextColumn(5, XLBillingAddress3, 255, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsOptional));
            InvoiceListFormat.Add(new TextColumn(6, XLBillingAddress4, 255, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsOptional));
            InvoiceListFormat.Add(new TextColumn(7, XLBillingAddress5, 50, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsOptional));
            InvoiceListFormat.Add(new DateColumn(8, XLBillingDate, 50, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsOptional));
            InvoiceListFormat.Add(new NumberColumn(9, XLInvoiceNumber, 15, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsOptional));
            InvoiceListFormat.Add(new DateColumn(10, XLDueDate, 30, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsOptional));
            InvoiceListFormat.Add(new TextColumn(11, XLCustomerMemo, 300, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsOptional));
            InvoiceListFormat.Add(new DecimalColumn(12, XLTax, 20, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsOptional));
            InvoiceListFormat.Add(new DecimalColumn(13, XLTotal, 20, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsOptional));
            InvoiceListFormat.Add(new DecimalColumn(14, XLAmountPaid, 20, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsRequired));

            int LastColumn = InvoiceListFormat.Count();

            // add a breakdown so that we can do some validation

            InvoiceListFormat.SubFormats = new()
            {  new SheetFormat() };
            InvoiceItem.AddSheetColumns(InvoiceListFormat.SubFormats[0], LastColumn);
        }
    }
}
