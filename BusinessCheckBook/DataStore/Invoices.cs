//**********************************************************************
//
//          Copyright © 2023 Prairie Trail Software, Inc.
//
//**********************************************************************

using BusinessCheckBook.Validation;
using ClosedXML.Excel;

namespace BusinessCheckBook.DataStore
{
    internal class Invoices
    {
        // storage of the invoices

        List<Invoice> CurrentInvoices = new();

        // what the Excel sheet is supposed to look like
        internal SheetFormat InvoiceListFormat { get; set; } = new();

        public Invoices() 
        {
            CurrentInvoices = new();
            SetSheetFormat();
        }


        internal int GetlastInvoiceNumber ()
        {
            Invoice? LInvoice = (from ci in CurrentInvoices
                                 select ci).LastOrDefault();
            if (LInvoice == null) { return 0; }
            return LInvoice.InvoiceNumber;
        }

        internal List<Invoice> GetInvoicesForACustomer(string CustomerID)
        {
            List<Invoice> FoundInvoices = new();
            foreach(Invoice inv in  CurrentInvoices)
            {
                if (inv.CustomerIdentifier == CustomerID)
                    FoundInvoices.Add(inv);
            }
            return FoundInvoices;
        }

        internal List<Invoice> GetOutstandingInvoicesForACustomer(string CustomerID)
        {
            List<Invoice> FoundInvoices = new();
            foreach (Invoice inv in CurrentInvoices)
            {
                if (inv.CustomerIdentifier == CustomerID)
                    if (!inv.Paid)
                        FoundInvoices.Add(inv);
            }
            return FoundInvoices;
        }

        internal void ApplyPayment (string CustomerID, decimal Payment)
        {
            List<Invoice> OutstandingInvoices = (from inv in CurrentInvoices
                                                 where inv.CustomerIdentifier == CustomerID
                                                 && !inv.Paid
                                                 select inv).ToList();
            foreach (Invoice inv in OutstandingInvoices)
            {
                decimal AmountDue = inv.Total - inv.AmountPaid;
                if (AmountDue <= Payment)
                {
                    inv.AmountPaid = inv.Total;
                    inv.Paid = true;
                    Payment -= AmountDue;
                    if (Payment == 0.00M) break;
                }
                else
                {
                    inv.AmountPaid += Payment;
                    Payment = 0.00M;
                    break;
                }
            }
        }


        internal List<Invoice> GetInvoicesForAYear(int Year)
        {
            return (from inv in CurrentInvoices
                    where DateTime.Parse(inv.BillingDate).Year == Year
                    select inv).ToList();
        }






        internal void AddInvoice (Invoice inv)
        {
            CurrentInvoices.Add(inv);
        }


        internal void AddJournalInvoice(Invoice Inv)
        {
            // before adding the invoice, see if this invoice number is already in the list

            bool Handled = false;
            foreach (Invoice Entry in CurrentInvoices)
            {
                if (Entry.InvoiceNumber > 0)
                {
                    if (Entry.InvoiceNumber == Inv.InvoiceNumber)
                    {
                        if (MessageBox.Show("The journal invoice has the same number " + Inv.InvoiceNumber.ToString() + " as an existing invoice. Replace?",
                            "Duplicate entry", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            // replace the current entry with this new one
                            int InvoiceIndex = CurrentInvoices.IndexOf(Entry);
                            Inv.AmountPaid = Entry.AmountPaid;
                            CurrentInvoices.Insert(InvoiceIndex, Inv);
                            CurrentInvoices.Remove(Entry);

                            Handled = true;
                            break;
                        }
                    }
                }
            }
            if (!Handled)
            {
                CurrentInvoices.Add(Inv);
            }
        }




        #region Validation

        public bool Validate(XLWorkbook CheckBookXlsx, out string ErrorMessage)
        {
            ErrorMessage = "";
            IXLWorksheet InvoiceWorksheet;
            try
            {
                InvoiceWorksheet = CheckBookXlsx.Worksheet(InvoiceListFormat.SheetName);
            }
            catch (Exception ex)
            {
                ErrorMessage = "Missing the Invoice list sheet " + ex.Message;
                return false;
            }

            // the sheet exists. check all the headings and find which column is which

            try
            {
                if (!Invoice.ValidateColumnHeaders(InvoiceWorksheet, InvoiceListFormat, out ErrorMessage))
                    return false;

                // validate all the entries in the columns for validity
                // at this point, we have all the columns that are required

                int RowsUsedCount = InvoiceWorksheet.RowsUsed().Count();
                for (int Row = 2; Row < RowsUsedCount; Row++)
                {
                    IXLRow XRow = InvoiceWorksheet.Row(Row);
                    if (!Invoice.ValidateExcelRow(XRow, InvoiceListFormat, out ErrorMessage))
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


        // used on user entry
        internal bool ValidateAccountName(string TAccountName)
        {
            return Customer.ValidateAccountName(TAccountName, InvoiceListFormat);
        }
        internal bool ValidateBusinessName(string TBusinessName)
        {
            return Customer.ValidateBusinessName(TBusinessName, InvoiceListFormat);
        }

        internal bool ValidateAddress(string TAddress)
        {
            return Customer.ValidateAddress(TAddress, InvoiceListFormat);
        }
        internal bool ValidateAddress2(string TAddress2)
        {
            return Customer.ValidateAddress2(TAddress2, InvoiceListFormat);
        }
        internal bool ValidateCity(string TCity)
        {
            return Customer.ValidateCity(TCity, InvoiceListFormat);
        }
        internal bool ValidateState(string TState)
        {
            return Customer.ValidateState(TState, InvoiceListFormat);
        }
        internal bool ValidateZipCode(string TZipCode)
        {
            return Customer.ValidateZipCode(TZipCode, InvoiceListFormat);
        }
        internal bool ValidateCountry(string TCountry)
        {
            return Customer.ValidateCountry(TCountry, InvoiceListFormat);
        }
        internal bool ValidateContactPerson(string TContactPerson)
        {
            return Customer.ValidateContactPerson(TContactPerson, InvoiceListFormat);
        }
        internal bool ValidatePhone(string TPhone)
        {
            return Customer.ValidatePhone(TPhone, InvoiceListFormat);
        }
        internal bool ValidateEmailAddress(string TEmailAddress)
        {
            return Customer.ValidateEmailAddress(TEmailAddress, InvoiceListFormat);
        }
        internal bool ValidateTaxID(string TTaxID)
        {
            return Customer.ValidateTaxID(TTaxID, InvoiceListFormat);
        }

        #endregion Validation


        public bool ReadFromExcelFile(XLWorkbook CheckBookXlsx)
        {
            CurrentInvoices = new();
            IXLWorksheet InvoicesWorksheet = CheckBookXlsx.Worksheet(InvoiceListFormat.SheetName);
            if (InvoicesWorksheet != null)
            {
                int Row = 0;
                foreach (var row in InvoicesWorksheet.RowsUsed())
                {
                    // The header row was processed in the validation stage and we can use those values

                    Row++;
                    if (Row == 1) continue;

                    // Pull off the Invoice Values

                    Invoice NInvoice = new();
                    IXLRow xlRow = InvoicesWorksheet.Row(Row);

                    NInvoice.ParseExcelRow(xlRow, InvoiceListFormat);

                    CurrentInvoices.Add(NInvoice);
                }
                return true;
            }
            return false;
        }





        internal void WriteXLInvoices(XLWorkbook CheckBookXlsx)
        {
            // add the chart of accounts worksheet
            CheckBookXlsx.AddWorksheet(InvoiceListFormat.SheetName);
            IXLWorksheet InvoicesWorksheet = CheckBookXlsx.Worksheet(InvoiceListFormat.SheetName);

            // first build the header
            int breakdowns = GetMaxBreakdowns();
            Invoice.WriteXLHeader(InvoicesWorksheet, InvoiceListFormat, breakdowns);

            // then add all the rows

            int Row = 2;
            foreach (Invoice RInvoice in CurrentInvoices)
            {
                IXLRow xlRow = InvoicesWorksheet.Row(Row);
                RInvoice.WriteExcelRow(xlRow, InvoiceListFormat);
                Row++;
            }
        }

        internal int GetMaxBreakdowns()
        {
            int breakdowns = 0;
            foreach (Invoice RInvoice in CurrentInvoices)
            {
                if (RInvoice.BreakdownCount() > breakdowns) breakdowns = RInvoice.BreakdownCount();
            }
            return breakdowns;
        }




        internal void SetSheetFormat()
        {
            InvoiceListFormat = new();
            InvoiceListFormat.SheetName = "Invoices";
            Invoice.AddSheetColumns(InvoiceListFormat);
        }

    }
}
