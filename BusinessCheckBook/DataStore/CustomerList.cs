using BusinessCheckBook.Settings;
using BusinessCheckBook.Validation;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCheckBook.DataStore
{
    internal class CustomerList
    {
        // storage of the customers

        private List<Customer> Customers = new();

        // what the Excel sheet is supposed to look like
        internal SheetFormat CustomerListFormat { get; set; } = new();




        public CustomerList()
        {
            Customers = new();
            SetSheetFormat();
        }

        internal void AddCustomer (Customer customer)
        {
            Customers.Add(customer);
        }
        internal List<Customer> GetCurrentList()
        { 
            return Customers; 
        }

        internal Customer GetThisCustomer(int RowIndex)
        { 
            return Customers[RowIndex]; 
        }
        internal Customer? GetCustomerByID(string CustomerIDToFind)
        {
            foreach (Customer TCust in Customers)
            {
                if (TCust.CustomerIdentifier == CustomerIDToFind) return TCust;
            }
            return null;
        }

        internal Customer? GetCustomerByName (string AccountNameToFind)
        {
            foreach (Customer TCust in Customers)
            {
                if (TCust.AccountName == AccountNameToFind) return TCust;   
            }
            return null;
        }

        #region Validation

        public bool Validate(XLWorkbook CheckBookXlsx, out string ErrorMessage)
        {
            ErrorMessage = "";
            IXLWorksheet CustomersWorksheet;
            try
            {
                CustomersWorksheet = CheckBookXlsx.Worksheet(CustomerListFormat.SheetName);
            }
            catch (Exception ex)
            {
                ErrorMessage = "Missing the Customer list sheet " + ex.Message;
                return false;
            }

            // the sheet exists. check all the headings and find which column is which

            try
            {
                for (int CustomerNum = 0; CustomerNum < CustomerListFormat.Count(); CustomerNum++)
                {
                    ColumnFormat col = CustomerListFormat.Column(CustomerNum);
                    if (col == null) continue;
                    bool headerNotFound = true;
                    for (int ColNum = 1; ColNum < CustomersWorksheet.ColumnCount(); ColNum++)
                    {
                        string HeaderValue = CustomersWorksheet.Cell(1, ColNum).GetString();
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
                            ErrorMessage = "The column " + col.Name + " is not in the customer list";
                            return false;
                        }
                    }

                }

                // validate all the entries in the columns for validity
                // at this point, we have all the columns that are required

                int RowsUsedCount = CustomersWorksheet.RowsUsed().Count();
                for (int Row = 2; Row < RowsUsedCount; Row++)
                {
                    IXLRow XRow = CustomersWorksheet.Row(Row);
                    if (!Customer.ValidateExcelRow(XRow, CustomerListFormat, out ErrorMessage))
                    {
                        ErrorMessage +=  " in row " + Row.ToString();
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
            return Customer.ValidateAccountName(TAccountName, CustomerListFormat);
        }
        internal bool ValidateBusinessName(string TBusinessName)
        {
            return Customer.ValidateBusinessName(TBusinessName, CustomerListFormat);
        }

        internal bool ValidateAddress(string TAddress)
        {
            return Customer.ValidateAddress(TAddress, CustomerListFormat);
        }
        internal bool ValidateAddress2(string TAddress2)
        {
            return Customer.ValidateAddress2(TAddress2, CustomerListFormat);
        }
        internal bool ValidateCity(string TCity)
        {
            return Customer.ValidateCity(TCity, CustomerListFormat);
        }
        internal bool ValidateState(string TState)
        {
            return Customer.ValidateState(TState, CustomerListFormat);
        }
        internal bool ValidateZipCode(string TZipCode)
        {
            return Customer.ValidateZipCode(TZipCode, CustomerListFormat);
        }
        internal bool ValidateCountry(string TCountry)
        {
            return Customer.ValidateCountry(TCountry, CustomerListFormat);
        }
        internal bool ValidateContactPerson(string TContactPerson)
        {
            return Customer.ValidateContactPerson(TContactPerson, CustomerListFormat);
        }
        internal bool ValidatePhone(string TPhone)
        {
            return Customer.ValidatePhone(TPhone, CustomerListFormat);
        }
        internal bool ValidateEmailAddress(string TEmailAddress)
        {
            return Customer.ValidateEmailAddress(TEmailAddress, CustomerListFormat);
        }
        internal bool ValidateTaxID(string TTaxID)
        {
            return Customer.ValidateTaxID(TTaxID, CustomerListFormat);
        }

        #endregion Validation


        public bool ReadFromExcelFile(XLWorkbook CheckBookXlsx)
        {
            Customers = new();
            IXLWorksheet CustomersWorksheet = CheckBookXlsx.Worksheet(CustomerListFormat.SheetName);
            if (CustomersWorksheet != null)
            {
                int Row = 0;
                foreach (var row in CustomersWorksheet.RowsUsed())
                {
                    // The header row was processed in the validation stage and we can use those values

                    Row++;
                    if (Row == 1) continue;

                    // Pull off the Customer Values

                    Customer NCustomer = new();
                    IXLRow xlRow = CustomersWorksheet.Row(Row);

                    NCustomer.ParseExcelRow(xlRow, CustomerListFormat);

                    Customers.Add(NCustomer);
                }
                return true;
            }
            return false;
        }

        internal bool ValidateIIFCustomer(string[] fields, out string ErrorMessage)
        {
            return Customer.ValidateIIFFields(fields, CustomerListFormat, out ErrorMessage);
        }
        internal void AddIIFCustomer(string[] fields)
        {
            // only add active customers from the IIF file
            Customer NCustomer = new();
            NCustomer.ParseIIFFields(fields);
            //if (NCustomer.IsActive)
                Customers.Add(NCustomer);
        }



        internal void WriteXLCustomerList(XLWorkbook CheckBookXlsx)
        {
            // add the chart of accounts worksheet
            CheckBookXlsx.AddWorksheet(CustomerListFormat.SheetName);
            IXLWorksheet CustomersWorksheet = CheckBookXlsx.Worksheet(CustomerListFormat.SheetName);

            // first build the header

            for (int i = 0; i < CustomerListFormat.Count(); i++)
            {
                ColumnFormat Col = CustomerListFormat.Column(i);
                CustomersWorksheet.Cell(1, Col.ColumnNumber).Value = Col.Name;
            }
            // then add all the rows

            int Row = 2;
            foreach (Customer RCustomer in Customers)
            {
                IXLRow xlRow = CustomersWorksheet.Row(Row);
                RCustomer.WriteExcelRow(xlRow, CustomerListFormat);
                Row++;
            }
        }




        internal void SetSheetFormat()
        {
            CustomerListFormat = new();
            CustomerListFormat.SheetName = "CustomerList";
            Customer.AddSheetColumns(CustomerListFormat);
        }

    }
}
