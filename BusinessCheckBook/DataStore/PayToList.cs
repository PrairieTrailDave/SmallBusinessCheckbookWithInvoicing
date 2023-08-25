using BusinessCheckBook.Settings;
using BusinessCheckBook.Validation;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;

namespace BusinessCheckBook.DataStore
{
    internal class PayToList
    {
        // storage of the Payees

        private List<PayTo> PayTos = new();

        // what the Excel sheet is supposed to look like
        private SheetFormat PayToListFormat { get; set; } = new();

        public PayToList()
        {
            PayTos = new();
            SetSheetFormat();
        }

        internal static PayTo CreateNewPayTo(string PayToName) =>
            new()
            {
                AccountName = PayToName,
                BusinessName = PayToName,
                PrintAs = PayToName
            };
        internal void AddPayTo(PayTo TPayTo)
        {
            PayTos.Add(TPayTo);
        }
        internal List<PayTo> GetCurrentList()
        {
            return PayTos;
        }

        internal PayTo GetThisPayTo(int RowIndex)
        {
            return PayTos[RowIndex];
        }
        internal bool IsValidPayTo(string NameToMatch)
        {
            foreach (PayTo PT in  PayTos)
            {
                if (PT.AccountName == NameToMatch) return true;
                if (PT.BusinessName == NameToMatch) return true;
            }
            return false;
        }

        internal List<string> GetMatchingPayToNames(string ToMatch)
        {
            // the version for individual only went back a year. 
            // For business simply use currently active accounts

            return (from ch in PayTos
                    where ch.BusinessName.ToUpper().StartsWith(ToMatch.ToUpper())
                       && ch.IsActive
                    orderby ch.BusinessName
                    select ch.BusinessName).Distinct().ToList();
        }

        internal PayTo? GetMatchedPayTo (string ToMatch)
        {
            return (from ch in PayTos
                    where ch.BusinessName.ToUpper().StartsWith(ToMatch.ToUpper())
                       && ch.IsActive
                    select ch).FirstOrDefault();
        }

        #region Validation

        internal bool Validate(XLWorkbook CheckBookXlsx, out string ErrorMessage)
        {
            ErrorMessage = "";
            IXLWorksheet PayToListWorksheet;
            try
            {
                PayToListWorksheet = CheckBookXlsx.Worksheet(PayToListFormat.SheetName);
            }
            catch (Exception ex)
            {
                ErrorMessage = "Missing the Pay To list sheet " + ex.Message;
                return false;
            }

            // the sheet exists. check all the headings and find which column is which

            try
            {
                for (int PayToNum = 0; PayToNum < PayToListFormat.Count(); PayToNum++)
                {
                    ColumnFormat col = PayToListFormat.Column(PayToNum);
                    if (col == null) continue;
                    bool headerNotFound = true;
                    for (int ColNum = 1; ColNum < PayToListWorksheet.ColumnCount(); ColNum++)
                    {
                        string HeaderValue = PayToListWorksheet.Cell(1, ColNum).GetString();
                        if (col.Name == HeaderValue)
                        {
                            headerNotFound = false;
                            col.ColumnNumber = ColNum;
                            break;
                        }
                    }
                    if (headerNotFound)
                    {
                        if (col.RequiredColumn)
                        {
                            ErrorMessage = "The column " + col.Name + " is not in the pay to list";
                            return false;
                        }
                    }

                }

                // validate all the entries in the columns for validity
                // at this point, we have all the columns that are required

                int RowsUsedCount = PayToListWorksheet.RowsUsed().Count();
                for (int Row = 2; Row < RowsUsedCount; Row++)
                {
                    IXLRow XRow = PayToListWorksheet.Row(Row);
                    if (!PayTo.ValidateExcelRow(XRow, PayToListFormat, out ErrorMessage))
                    {
                        ErrorMessage += " in row " + Row.ToString();
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = "Error in reading a cell " + ex.Message;
                return false;
            }
        }


        // used on user entry
        internal bool ValidateAccountName(string TAccountName)
        {
            return PayTo.ValidateAccountName(TAccountName, PayToListFormat);
        }
        internal bool ValidateBusinessName(string TBusinessName)
        {
            return PayTo.ValidateBusinessName(TBusinessName, PayToListFormat);
        }
        internal bool ValidatePrintAs(string TPrintAs)
        {
            return PayTo.ValidatePrintAs(TPrintAs, PayToListFormat);
        }
        internal  bool ValidateAddress(string TAddress)
        {
            return PayTo.ValidateAddress(TAddress, PayToListFormat);
        }
        internal bool ValidateAddress2(string TAddress2)
        {
            return PayTo.ValidateAddress2(TAddress2, PayToListFormat);
        }
        internal bool ValidateCity(string TCity)
        {
            return PayTo.ValidateCity(TCity, PayToListFormat);
        }
        internal bool ValidateState(string TState)
        {
            return PayTo.ValidateState(TState, PayToListFormat);
        }
        internal bool ValidateZipCode(string TZipCode)
        {
            return PayTo.ValidateZipCode(TZipCode, PayToListFormat);
        }
        internal bool ValidateCountry(string TCountry)
        {
            return PayTo.ValidateCountry(TCountry, PayToListFormat);
        }
        internal bool ValidateContactPerson(string TContactPerson)
        {
            return PayTo.ValidateContactPerson(TContactPerson, PayToListFormat);
        }
        internal bool ValidatePhone(string TPhone)
        {
            return PayTo.ValidatePhone(TPhone, PayToListFormat);
        }
        internal bool ValidateEmailAddress(string TEmailAddress)
        {
            return PayTo.ValidateEmailAddress(TEmailAddress, PayToListFormat);
        }
        internal bool ValidateTaxID(string TTaxID)
        {
            return PayTo.ValidateTaxID(TTaxID, PayToListFormat);
        }
        #endregion Validation


        internal bool ReadFromExcelFile(XLWorkbook CheckBookXlsx)
        {
            PayTos = new();
            IXLWorksheet PayTosWorksheet = CheckBookXlsx.Worksheet(PayToListFormat.SheetName);
            if (PayTosWorksheet != null)
            {
                int Row = 0;
                foreach (var row in PayTosWorksheet.RowsUsed())
                {
                    // The header row was processed in the validation stage

                    Row++;
                    if (Row == 1) continue;

                    // Pull off the PayTo Values

                    PayTo NPayTo = new();
                    IXLRow xlRow = PayTosWorksheet.Row(Row);

                    NPayTo.ParseExcelRow(xlRow, PayToListFormat);

                    PayTos.Add(NPayTo);
                }
                return true;
            }
            return false;
        }

        internal bool ValidateIIFVendor(string[] fields, out string ErrorMessage)
        {
            return PayTo.ValidateVendorIIFFIelds(fields, PayToListFormat, out ErrorMessage);
        }
        internal void AddIIFVendor(string[] fields)
        {
            // only add active Vendors from the IIF file
            PayTo NPayTo = new();
            NPayTo.ParseVendorIIFFields(fields);
            //if (NPayTo.IsActive)
                PayTos.Add(NPayTo);
        }

        internal bool ValidateIIFOtherName(string[] fields, out string ErrorMessage)
        {
            return PayTo.ValidateOtherNameIIFFIelds(fields, PayToListFormat, out ErrorMessage);
        }
        internal void AddIIFOtherName(string[] fields)
        {
            // only add active other names from the IIF file
            PayTo NPayTo = new();
            NPayTo.ParseOtherNameIIFFields(fields);
            //if (NPayTo.IsActive)
                PayTos.Add(NPayTo);
        }


        internal void WriteXLPayToList(XLWorkbook CheckBookXlsx)
        {
            // add the pay to worksheet
            CheckBookXlsx.AddWorksheet(PayToListFormat.SheetName);
            IXLWorksheet PayTosWorksheet = CheckBookXlsx.Worksheet(PayToListFormat.SheetName);

            // first build the header

            for (int i = 0; i < PayToListFormat.Count(); i++)
            {
                ColumnFormat Col = PayToListFormat.Column(i);
                PayTosWorksheet.Cell(1, Col.ColumnNumber).Value = Col.Name;
            }
            // then add all the rows

            int Row = 2;
            foreach (PayTo RPayTo in PayTos)
            {
                IXLRow xlRow = PayTosWorksheet.Row(Row);
                RPayTo.WriteExcelRow(xlRow, PayToListFormat);
                Row++;
            }
        }




        internal void SetSheetFormat()
        {
            PayToListFormat = new();
            PayToListFormat.SheetName = "PayToList";
            PayTo.AddSheetColumns(PayToListFormat);
        }

    }
}
