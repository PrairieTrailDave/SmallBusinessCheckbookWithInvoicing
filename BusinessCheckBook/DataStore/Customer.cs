using BusinessCheckBook.Settings;
using BusinessCheckBook.Validation;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCheckBook.DataStore
{
    public class Customer
    {
        public bool IsActive = true;
        public string CustomerIdentifier { get; set; } = string.Empty;
        public string AccountName { get; set; } = string.Empty;
        public string BusinessName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Address2 { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string Country { get; set;  } = string.Empty;
        public string ContactPerson { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public bool Taxable { get; set; } = false;
        public string TaxID { get; set; } = string.Empty;


        // adding all this below allows one file to contain the names of all the fields and all the formats 

        // Excel column names
        // Column names and header values

        private const string XLIsActive = "IsActive";
        private const string XLCustomerIdentifier = "CustomerIdentifier";
        private const string XLAccountName = "AccountName";
        private const string XLBusinessName = "BusinessName";
        private const string XLAddress = "Address";
        private const string XLAddress2 = "Address2";
        private const string XLCity = "City";
        private const string XLState = "State";
        private const string XLZipCode = "ZipCode";
        private const string XLCountry = "Country";
        private const string XLContactPerson = "ContactPerson";
        private const string XLPhone = "Phone";
        private const string XLEmailAddress = "EmailAddress";
        private const string XLTaxable = "Taxable";
        private const string XLTaxID = "TaxID";



        // IIF column names

        private const int IIFNAME = 1;
        //private const int IIFREFNUM = 2;
        //private const int IIFTIMESTAMP = 3;
        private const int IIFBADDR1 = 4;
        private const int IIFBADDR2 = 5;
        private const int IIFBADDR3 = 6;
        private const int IIFBADDR4 = 7;
        //private const int IIFBADDR5 = 8;
        //private const int IIFSADDR1 = 9;
        //private const int IIFSADDR2 = 10;
        //private const int IIFSADDR3 = 11;
        //private const int IIFSADDR4 = 12;
        //private const int IIFSADDR5 = 13;
        //private const int IIFPHONE1 = 14;
        //private const int IIFPHONE2 = 15;
        //private const int IIFFAXNUM = 16;
        private const int IIFEMAIL = 17;
        //private const int IIFNOTE = 18;
        //private const int IIFCONT1 = 19;
        //private const int IIFCONT2 = 20;
        //private const int IIFCTYPE = 21;
        //private const int IIFTERMS = 22;
        private const int IIFTAXABLE = 23;
        //private const int IIFSALESTAXCODE = 24;
        //private const int IIFLIMIT = 25;
        //private const int IIFRESALENUM = 26;
        //private const int IIFREP = 27;
        //private const int IIFTAXITEM = 28;
        //private const int IIFNOTEPAD = 29;
        //private const int IIFSALUTATION = 30;
        private const int IIFCOMPANYNAME = 31;
        private const int IIFFIRSTNAME = 32;
        //private const int IIFMIDINIT = 33;
        private const int IIFLASTNAME = 34;
        //private const int IIFCUSTFLD1 = 35;
        //private const int IIFCUSTFLD2 = 36;
        //private const int IIFCUSTFLD3 = 37;
        //private const int IIFCUSTFLD4 = 38;
        //private const int IIFCUSTFLD5 = 39;
        //private const int IIFCUSTFLD6 = 40;
        //private const int IIFCUSTFLD7 = 41;
        //private const int IIFCUSTFLD8 = 42;
        //private const int IIFCUSTFLD9 = 43;
        //private const int IIFCUSTFLD10 = 44;
        //private const int IIFCUSTFLD11 = 45;
        //private const int IIFCUSTFLD12 = 46;
        //private const int IIFCUSTFLD13 = 47;
        //private const int IIFCUSTFLD14 = 48;
        //private const int IIFCUSTFLD15 = 49;
        //private const int IIFJOBDESC = 50;
        //private const int IIFJOBTYPE = 51;
        //private const int IIFJOBSTATUS = 52;
        //private const int IIFJOBSTART = 53;
        //private const int IIFJOBPROJEND = 54;
        //private const int IIFJOBEND = 55;
        private const int IIFHIDDEN = 56;
        //private const int IIFDELCOUNT = 57;
        //private const int IIFPRICELEVEL = 58;


        internal static bool ValidateIIFFields(string[] fields, SheetFormat CustomerListFormat, out string ErrorMessage)
        {
            ErrorMessage = "";
            string TCustomerIdentifier = fields[IIFNAME];
            if (!ValidateCustomerID(TCustomerIdentifier, CustomerListFormat))
            {
                ErrorMessage = "Invalid Account ID in IIF " + TCustomerIdentifier;
                return false;
            }
            string TAccountName = fields[IIFCOMPANYNAME];
            if (TAccountName.Length < 1) TAccountName = fields[IIFNAME];
            if (!ValidateAccountName(TAccountName, CustomerListFormat))
            {
                ErrorMessage = "Invalid Account Name in IIF " + TAccountName;
                return false;
            }
            string TBusinessName = fields[IIFBADDR1];
            if (TBusinessName.Length > 0)
            {
                if (!ValidateBusinessName(TBusinessName, CustomerListFormat))
                {
                    ErrorMessage = "Invalid Business Name in IIF " + TBusinessName;
                    return false;
                }
            }
            string[] combinedcitystate = DataStore.Address.ParseAddressCityStateZipCountry(fields[IIFBADDR2], fields[IIFBADDR3], fields[IIFBADDR4]);
            string TAddress = combinedcitystate[0];
            if (!ValidateAddress(TAddress, CustomerListFormat))
            {
                ErrorMessage = "Invalid Address in IIF " + TAddress;
                return false;
            }
            string TAddress2 = combinedcitystate[1];
            if (!ValidateAddress2(TAddress2, CustomerListFormat))
            {
                ErrorMessage = "Invalid Address2 in IIF " + TAddress2;
                return false;
            }
            string TCity = combinedcitystate[2];
            if (!ValidateCity(TCity, CustomerListFormat))
            {
                ErrorMessage = "Invalid City in IIF " + TCity;
                return false;
            }
            string TState = combinedcitystate[3];
            if (!ValidateState(TState, CustomerListFormat))
            {
                ErrorMessage = "Invalid State in IIF " + TState;
                return false;
            }
            string TZipCode = combinedcitystate[4];
            if (!ValidateZipCode(TZipCode, CustomerListFormat))
            {
                ErrorMessage = "Invalid ZipCode in IIF " + TZipCode;
                return false;
            }
            string TCountry = combinedcitystate[5];
            if (!ValidateCountry(TCountry, CustomerListFormat))
            {
                ErrorMessage = "Invalid Country in IIF " + TCountry;
                return false;
            }
            string TContactPerson = fields[IIFFIRSTNAME] + " " + fields[IIFLASTNAME];
            if (!ValidateContactPerson(TContactPerson, CustomerListFormat))
            {
                ErrorMessage = "Invalid ContactPerson in IIF " + TContactPerson;
                return false;
            }
            string TEmailAddress = fields[IIFEMAIL];
            if (!ValidateEmailAddress(TEmailAddress, CustomerListFormat))
            {
                ErrorMessage = "Invalid EmailAddress in IIF " + TEmailAddress;
                return false;
            }
            return true;
        }
        internal void ParseIIFFields(string[] fields)
        {
            IsActive = true;
            if (fields[IIFHIDDEN] == "Y") IsActive = false;
            CustomerIdentifier = fields[IIFNAME];
            AccountName = fields[IIFCOMPANYNAME];
            if (AccountName.Length < 1) AccountName = fields[IIFNAME];
            BusinessName = fields[IIFBADDR1];
            if (BusinessName.Length < 1) BusinessName = AccountName;

            
            string[] combinedcitystate = DataStore.Address.ParseAddressCityStateZipCountry(fields[IIFBADDR2], fields[IIFBADDR3], fields[IIFBADDR4]);
            Address = combinedcitystate[0];
            Address2 = combinedcitystate[1];
            City = combinedcitystate[2];
            State = combinedcitystate[3];
            ZipCode = combinedcitystate[4];
            Country = combinedcitystate[5];
            ContactPerson = fields[IIFFIRSTNAME] + " " + fields[IIFLASTNAME];
            EmailAddress = fields[IIFEMAIL];
            Taxable = true;
            if (fields[IIFTAXABLE] == "N")
                Taxable = false;
            TaxID = "";
        }


        internal static bool ValidateExcelRow(IXLRow XRow, SheetFormat CustomerListFormat, out string ErrorMessage)
        {
            ColumnFormat ThisColumn;

            ErrorMessage = "";

            // check the IsActive flag - if no isactive, then skip this row

            ThisColumn = CustomerListFormat.Column(XLIsActive)!;
            string TActive = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            if (TActive.Length == 0) return true;
            if (!ThisColumn.Valid(TActive))
            {
                ErrorMessage = "Invalid account active flag " + TActive;
                return false;
            }
            
            // check the Customer Identifier 

            ThisColumn = CustomerListFormat.Column(XLCustomerIdentifier)!;
            string CustomerIdentifier = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            if (!ThisColumn.Valid(CustomerIdentifier))
            {
                ErrorMessage = "Invalid Customer Identifier " + CustomerIdentifier;
                return false;
            }

            // Check the account name

            ThisColumn = CustomerListFormat.Column(XLAccountName)!;
            string TName = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            if (!ThisColumn.Valid(TName))
            {
                ErrorMessage = "Invalid account name " + TName;
                return false;
            }

            // Check the Business Name

            ThisColumn = CustomerListFormat.Column(XLBusinessName)!;
            string TBusinessName = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            if (!ThisColumn.Valid(TBusinessName))
            {
                ErrorMessage = "Invalid Business Name " + TBusinessName;
                return false;
            }

            // Check the Address

            ThisColumn = CustomerListFormat.Column(XLAddress)!;
            string TAddress = XRow.Cell(ThisColumn.ColumnNumber).GetString();

            if (!ThisColumn.Valid(TAddress))
            {
                ErrorMessage = "Invalid Address " + TAddress;
                return false;
            }


            ThisColumn = CustomerListFormat.Column(XLAddress2)!;
            string TAddress2 = XRow.Cell(ThisColumn.ColumnNumber).GetString();

            if (!ThisColumn.Valid(TAddress2))
            {
                ErrorMessage = "Invalid Address  " + TAddress2;
                return false;
            }

            // Check the City

            ThisColumn = CustomerListFormat.Column(XLCity)!;
            string TCity = XRow.Cell(ThisColumn.ColumnNumber).GetString();

            if (!ThisColumn.Valid(TCity))
            {
                ErrorMessage = "Invalid City  " + TCity;
                return false;
            }

            // Check the State

            ThisColumn = CustomerListFormat.Column(XLState)!;
            string TState = XRow.Cell(ThisColumn.ColumnNumber).GetString();

            if (!ThisColumn.Valid(TState))
            {
                ErrorMessage = "Invalid State " + TState;
                return false;
            }


            // Check the ZipCode

            ThisColumn = CustomerListFormat.Column(XLZipCode)!;
            string TZipCode = XRow.Cell(ThisColumn.ColumnNumber).GetString();

            if (!ThisColumn.Valid(TZipCode))
            {
                ErrorMessage = "Invalid ZipCode  " + TZipCode;
                return false;
            }

            // Check the country
            string TCountry = XRow.Cell(CustomerListFormat.Column(XLCountry)!.ColumnNumber).GetString();
            if (!ValidateCountry(TCountry, CustomerListFormat))
            {
                ErrorMessage = "Invalid Country  " + TCountry;
                return false;
            }

            // Check the ContactPerson

            ThisColumn = CustomerListFormat.Column(XLContactPerson)!;
            string TContactPerson = XRow.Cell(ThisColumn.ColumnNumber).GetString();

            if (!ThisColumn.Valid(TContactPerson))
            {
                ErrorMessage = "Invalid ContactPerson " + TContactPerson;
                return false;
            }


            // Check the Phone

            ThisColumn = CustomerListFormat.Column(XLPhone)!;
            string TPhone = XRow.Cell(ThisColumn.ColumnNumber).GetString();

            if (!ThisColumn.Valid(TPhone))
            {
                ErrorMessage = "Invalid Phone " + TPhone;
                return false;
            }


            // Check the EmailAddress

            ThisColumn = CustomerListFormat.Column(XLEmailAddress)!;
            string TEmailAddress = XRow.Cell(ThisColumn.ColumnNumber).GetString();

            if (!ThisColumn.Valid(TEmailAddress))
            {
                ErrorMessage = "Invalid Email Address " + TEmailAddress;
                return false;
            }


            // Check the Taxable

            ThisColumn = CustomerListFormat.Column(XLTaxable)!;
            string TTaxable = XRow.Cell(ThisColumn.ColumnNumber).GetString();

            if (!ThisColumn.Valid(TTaxable))
            {
                ErrorMessage = "Invalid Taxable " + TTaxable;
                return false;
            }

            // check the TaxID

            ThisColumn = CustomerListFormat.Column(XLTaxID)!;
            string TTaxID = XRow.Cell(ThisColumn.ColumnNumber).GetString();

            if (!ThisColumn.Valid(TTaxID))
            {
                ErrorMessage = "Invalid TaxID "  + TTaxID;
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
        internal static bool ValidateAccountName(string AccountName, SheetFormat CustomerListFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = CustomerListFormat.Column(XLAccountName)!;
            if (!ThisColumn.Valid(AccountName))
                return false;
            return true;
        }
        internal static bool ValidateBusinessName(string TBusinessName, SheetFormat CustomerListFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = CustomerListFormat.Column(XLBusinessName)!;
            if (!ThisColumn.Valid(TBusinessName))
                return false;
            return true;
        }
        internal static bool ValidateAddress(string TAddress, SheetFormat CustomerListFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = CustomerListFormat.Column(XLAddress)!;
            if (!ThisColumn.Valid(TAddress))
                return false;
            return true;
        }
        internal static bool ValidateAddress2(string TAddress2, SheetFormat CustomerListFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = CustomerListFormat.Column(XLAddress)!;
            if (!ThisColumn.Valid(TAddress2))
                return false;
            return true;
        }
        internal static bool ValidateCity(string TCity, SheetFormat CustomerListFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = CustomerListFormat.Column(XLCity)!;
            if (!ThisColumn.Valid(TCity))
                return false;
            return true;
        }
        internal static bool ValidateState(string TState, SheetFormat CustomerListFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = CustomerListFormat.Column(XLState)!;
            if (!ThisColumn.Valid(TState))
                    return false;
            return true;
        }
        internal static bool ValidateZipCode(string TZipCode, SheetFormat CustomerListFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = CustomerListFormat.Column(XLZipCode)!;
            if (!ThisColumn.Valid(TZipCode))
                return false;
            return true;
        }
        internal static bool ValidateCountry(string TCountry, SheetFormat CustomerListFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = CustomerListFormat.Column(XLCountry)!;
            if (!ThisColumn.Valid(TCountry))
                return false;
            if (!DataStore.Address.IsValidCountry(TCountry)) return false;
            return true;
        }
        internal static bool ValidateContactPerson(string TContactPerson, SheetFormat CustomerListFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = CustomerListFormat.Column(XLContactPerson)!;
            if (!ThisColumn.Valid(TContactPerson))
                return false;
            return true;
        }
        internal static bool ValidatePhone(string TPhone, SheetFormat CustomerListFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = CustomerListFormat.Column(XLPhone)!;
            if (!ThisColumn.Valid(TPhone))
                return false;
            return true;
        }
        internal static bool ValidateEmailAddress(string TEmailAddress, SheetFormat CustomerListFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = CustomerListFormat.Column(XLEmailAddress)!;
            if (!ThisColumn.Valid(TEmailAddress))
                return false;
            return true;
        }
        internal static bool ValidateTaxID(string TTaxID, SheetFormat CustomerListFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = CustomerListFormat.Column(XLTaxID)!;

            if (!ThisColumn.Valid(TTaxID))
                return false;
            return true;
        }








            internal void ParseExcelRow(IXLRow XRow, SheetFormat CustomerListFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = CustomerListFormat.Column(XLIsActive)!;
            IsActive = TrueFalseColumn.Parse(XRow.Cell(ThisColumn.ColumnNumber).GetString());
            ThisColumn = CustomerListFormat.Column(XLCustomerIdentifier)!;
            CustomerIdentifier = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            ThisColumn = CustomerListFormat.Column(XLAccountName)!;
            AccountName = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            ThisColumn = CustomerListFormat.Column(XLBusinessName)!;
            BusinessName = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            ThisColumn = CustomerListFormat.Column(XLAddress)!;
            Address = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            ThisColumn = CustomerListFormat.Column(XLAddress2)!;
            Address2 = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            ThisColumn = CustomerListFormat.Column(XLCity)!;
            City = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            ThisColumn = CustomerListFormat.Column(XLState)!;
            State = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            ThisColumn = CustomerListFormat.Column(XLZipCode)!;
            ZipCode = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            ThisColumn = CustomerListFormat.Column(XLCountry)!;
            Country = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            ThisColumn = CustomerListFormat.Column(XLContactPerson)!;
            ContactPerson = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            ThisColumn = CustomerListFormat.Column(XLPhone)!;
            Phone = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            ThisColumn = CustomerListFormat.Column(XLEmailAddress)!;
            EmailAddress = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            ThisColumn = CustomerListFormat.Column(XLTaxable)!;
            Taxable = TrueFalseColumn.Parse(XRow.Cell(ThisColumn.ColumnNumber).GetString());
            ThisColumn = CustomerListFormat.Column(XLTaxID)!;
            TaxID = XRow.Cell(ThisColumn.ColumnNumber).GetString();

        }

        internal void WriteExcelRow(IXLRow XRow, SheetFormat CustomerListFormat)
        {

            ColumnFormat? Col = CustomerListFormat.Column(XLIsActive)!;
            XRow.Cell(Col.ColumnNumber).Value = IsActive.ToString();
            Col = CustomerListFormat.Column(XLCustomerIdentifier)!;
            XRow.Cell(Col.ColumnNumber).Value = CustomerIdentifier.ToString();
            Col = CustomerListFormat.Column(XLAccountName)!;
            XRow.Cell(Col.ColumnNumber).Value = AccountName;
            Col = CustomerListFormat.Column(XLBusinessName)!;
            XRow.Cell(Col.ColumnNumber).Value = BusinessName;
            Col = CustomerListFormat.Column(XLAddress)!;
            XRow.Cell(Col.ColumnNumber).Value = Address;
            Col = CustomerListFormat.Column(XLAddress2)!;
            XRow.Cell(Col.ColumnNumber).Value = Address2;
            Col = CustomerListFormat.Column(XLCity)!;
            XRow.Cell(Col.ColumnNumber).Value = City;
            Col = CustomerListFormat.Column(XLState)!;
            XRow.Cell(Col.ColumnNumber).Value = State;
            Col = CustomerListFormat.Column(XLZipCode)!;
            XRow.Cell(Col.ColumnNumber).Value = ZipCode;
            Col = CustomerListFormat.Column(XLCountry)!;
            XRow.Cell(Col.ColumnNumber).Value = Country;
            Col = CustomerListFormat.Column(XLContactPerson)!;
            XRow.Cell(Col.ColumnNumber).Value = ContactPerson;
            Col = CustomerListFormat.Column(XLPhone)!;
            XRow.Cell(Col.ColumnNumber).Value = Phone;
            Col = CustomerListFormat.Column(XLEmailAddress)!;
            XRow.Cell(Col.ColumnNumber).Value = EmailAddress;
            Col = CustomerListFormat.Column(XLTaxable)!;
            XRow.Cell(Col.ColumnNumber).Value = Taxable.ToString();
            Col = CustomerListFormat.Column(XLTaxID)!;
            XRow.Cell(Col.ColumnNumber).Value = TaxID;
        }





        internal static void AddSheetColumns(SheetFormat CustomerListFormat)
        {
            CustomerListFormat.Add(new TrueFalseColumn(1, XLIsActive, 6, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsRequired));
            CustomerListFormat.Add(new TextColumn(2, XLCustomerIdentifier, 50, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsRequired));
            CustomerListFormat.Add(new NameColumn(3, XLAccountName, 60, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsRequired));
            CustomerListFormat.Add(new NameColumn(4, XLBusinessName, 255, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsRequired));
            CustomerListFormat.Add(new AddressColumn(5, XLAddress, 255, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsOptional));
            CustomerListFormat.Add(new AddressColumn(6, XLAddress2, 255, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsOptional));
            CustomerListFormat.Add(new CityColumn(7, XLCity, 50, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsOptional));
            CustomerListFormat.Add(new StateColumn(8, XLState, 50, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsOptional));
            CustomerListFormat.Add(new ZipCodeColumn(9, XLZipCode, 15, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsOptional));
            CustomerListFormat.Add(new NameColumn(10, XLCountry, 30, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsOptional));
            CustomerListFormat.Add(new NameColumn(11, XLContactPerson, 100, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsOptional));
            CustomerListFormat.Add(new PhoneColumn(12, XLPhone, 20, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsOptional));
            CustomerListFormat.Add(new EmailColumn(13, XLEmailAddress, 100, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsOptional));
            CustomerListFormat.Add(new TrueFalseColumn(14, XLTaxable, 6, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsRequired));
            CustomerListFormat.Add(new TextColumn(15, XLTaxID, 11, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsOptional));
        }

    }
}
