//**********************************************************************
//
//          Copyright © 2023 Prairie Trail Software, Inc.
//
//**********************************************************************

using BusinessCheckBook.Validation;
using ClosedXML.Excel;

namespace BusinessCheckBook.DataStore
{
    internal class PayTo
    {
        public bool IsActive = true;
        public string AccountName { get; set; } = string.Empty;
        public string BusinessName { get; set; } = string.Empty;
        public string PrintAs { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Address2 { get; set;} = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string ContactPerson { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public bool Taxable { get; set; } = false;
        public bool Send1099 { get; set; } = false;
        public string TaxID { get; set; } = string.Empty;


        // adding all this below allows one file to contain the names of all the fields and all the formats 

        // Excel column names
        // Column names and header values

        private const string XLIsActive = "IsActive";
        private const string XLAccountName = "AccountName";
        private const string XLBusinessName = "BusinessName";
        private const string XLPrintAs = "PrintAs";
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
        private const string XLSend1099 = "Send1099";
        private const string XLTaxID = "TaxID";
        // IIF column names - Vendors

        private const int IIFVNAME = 1;
        //private const int IIFVREFNUM = 2;
        //private const int IIFVTIMESTAMP = 3;
        //private const int IIFVPRINTAS = 4;
        private const int IIFVADDR1 = 5;
        private const int IIFVADDR2 = 6;
        private const int IIFVADDR3 = 7;
        private const int IIFVADDR4 = 8;
        //private const int IIFVADDR5 = 9;
        //private const int IIFVVTYPE = 10;
        //private const int IIFVCONT1 = 11;
        //private const int IIFVCONT2 = 12;
        private const int IIFVPHONE1 = 13;
        //private const int IIFVPHONE2 = 14;
        //private const int IIFVFAXNUM = 15;
        private const int IIFVEMAIL = 16;
        //private const int IIFVNOTE = 17;
        private const int IIFVTAXID = 18;
        //private const int IIFVLIMIT = 19;
        //private const int IIFVTERMS = 20;
        //private const int IIFVNOTEPAD = 21;
        //private const int IIFVSALUTATION = 22;
        private const int IIFVCOMPANYNAME = 23;
        private const int IIFVFIRSTNAME = 24;
        //private const int IIFVMIDINIT = 25;
        private const int IIFVLASTNAME = 26;
        //private const int IIFVCUSTFLD1 = 27;
        //private const int IIFVCUSTFLD2 = 28;
        //private const int IIFVCUSTFLD3 = 29;
        //private const int IIFVCUSTFLD4 = 30;
        //private const int IIFVCUSTFLD5 = 31;
        //private const int IIFVCUSTFLD6 = 32;
        //private const int IIFVCUSTFLD7 = 33;
        //private const int IIFVCUSTFLD8 = 34;
        //private const int IIFVCUSTFLD9 = 35;
        //private const int IIFVCUSTFLD10 = 36;
        //private const int IIFVCUSTFLD11 = 37;
        //private const int IIFVCUSTFLD12 = 38;
        //private const int IIFVCUSTFLD13 = 39;
        //private const int IIFVCUSTFLD14 = 40;
        //private const int IIFVCUSTFLD15 = 41;
        private const int IIFV1099 = 42;
        private const int IIFVHIDDEN = 43;
        //private const int IIFVDELCOUNT = 44;

        // IIF column names - Other Names

        private const int IIFONAME = 1;
        //private const int IIFOREFNUM = 2;
        //private const int IIFOTIMESTAMP = 3;
        private const int IIFOBADDR1 = 4;
        private const int IIFOBADDR2 = 5;
        private const int IIFOBADDR3 = 6;
        private const int IIFOBADDR4 = 7;
        //private const int IIFOBADDR5 = 8;
        private const int IIFOPHONE1 = 9;
        //private const int IIFOPHONE2 = 10;
        //private const int IIFOFAXNUM = 11;
        private const int IIFOEMAIL = 12;
        //private const int IIFONOTE = 13;
        private const int IIFOCONT1 = 14;
        //private const int IIFOCONT2 = 15;
        //private const int IIFONOTEPAD = 16;
        //private const int IIFOSALUTATION = 17;
        private const int IIFOCOMPANYNAME = 18;
        private const int IIFOFIRSTNAME = 19;
        //private const int IIFOMIDINIT = 20;
        private const int IIFOLASTNAME = 21;
        private const int IIFOHIDDEN = 22;
        //private const int IIFODELCOUNT = 23;


        internal static bool ValidateVendorIIFFIelds (string[] fields, SheetFormat PayToListFormat, out string ErrorMessage)
        {
            ErrorMessage = "";
            string TAccountName = fields[IIFVNAME];
            if (!ValidateAccountName(fields[IIFVNAME], PayToListFormat)) { ErrorMessage = "Invalid Account Name " + fields[IIFVNAME]; return false;  }
            string TBusinessName = fields[IIFVCOMPANYNAME];
            if (TBusinessName.Length == 0) TBusinessName = TAccountName;
            if (!ValidateBusinessName(TBusinessName, PayToListFormat)) { ErrorMessage = "Invalid Business Name " + TBusinessName; return false; }
            string TPrintAs = fields[IIFVADDR1];
            if (TPrintAs.Length == 0) TPrintAs = TBusinessName;
            if (!ValidatePrintAs(TPrintAs, PayToListFormat)) { ErrorMessage = "Invalid Print As " + TPrintAs; return false; }

            string[] combinedcitystate = DataStore.Address.ParseAddressCityStateZipCountry(fields[IIFVADDR2], fields[IIFVADDR3], fields[IIFVADDR4]);
            if (!ValidateAddress(combinedcitystate[0], PayToListFormat)) { ErrorMessage = "Invalid Address " + combinedcitystate[0]; return false; }
            if (!ValidateAddress2(combinedcitystate[1], PayToListFormat)) { ErrorMessage = "Invalid Address " + combinedcitystate[1]; return false; }
            if (!ValidateCity(combinedcitystate[2], PayToListFormat)) { ErrorMessage = "Invalid City " + combinedcitystate[2]; return false; }
            if (!ValidateState(combinedcitystate[3], PayToListFormat)) { ErrorMessage = "Invalid State " + combinedcitystate[3]; return false; }
            if (!ValidateZipCode(combinedcitystate[4], PayToListFormat)) { ErrorMessage = "Invalid Zip Code " + combinedcitystate[4]; return false; }
            if (!ValidateCountry(combinedcitystate[5], PayToListFormat)) { ErrorMessage = "Invalid Country " + combinedcitystate[5]; return false; }

            if (!ValidateContactPerson(fields[IIFVFIRSTNAME] + " " + fields[IIFVLASTNAME], PayToListFormat)) { ErrorMessage = "Invalid ContactPerson " + fields[IIFVFIRSTNAME] + " " + fields[IIFVLASTNAME]; return false; }
            if (!ValidatePhone(fields[IIFVPHONE1], PayToListFormat)) { ErrorMessage = "Invalid Phone " + fields[IIFVPHONE1]; return false; }
            if (!ValidateEmailAddress(fields[IIFVEMAIL], PayToListFormat)) { ErrorMessage = "Invalid Email Address " + fields[IIFVEMAIL]; return false; }
            if (!ValidateTaxID(fields[IIFVTAXID], PayToListFormat)) { ErrorMessage = "Invalid Tax ID " + fields[IIFVTAXID]; return false; }

            return true;
        }
        internal void ParseVendorIIFFields(string[] fields)
        {
            IsActive = true;
            if (fields[IIFVHIDDEN] == "Y") IsActive = false;
            AccountName = fields[IIFVNAME];
            BusinessName = fields[IIFVCOMPANYNAME];
            PrintAs = fields[IIFVADDR1];
            string[] combinedcitystate = DataStore.Address.ParseAddressCityStateZipCountry(fields[IIFVADDR2], fields[IIFVADDR3], fields[IIFVADDR4]);
            Address = combinedcitystate[0];
            Address2 = combinedcitystate[1];
            City = combinedcitystate[2];
            State = combinedcitystate[3];
            ZipCode = combinedcitystate[4];
            Country = combinedcitystate[5];
            ContactPerson = fields[IIFVFIRSTNAME] + " " + fields[IIFVLASTNAME];
            Phone = fields[IIFVPHONE1];
            EmailAddress = fields[IIFVEMAIL];
            Taxable = true;
            if (fields[IIFV1099] == "N")
                Taxable = false;
            Send1099 = false;
            TaxID = fields[IIFVTAXID];

            // clean up some fields
            if (BusinessName.Length == 0) BusinessName = AccountName;
            if (PrintAs.Length == 0) PrintAs = BusinessName;
        }

        internal static bool ValidateOtherNameIIFFIelds(string[] fields, SheetFormat PayToListFormat, out string ErrorMessage)
        {
            ErrorMessage = "";
            if (!ValidateAccountName(fields[IIFONAME], PayToListFormat)) { ErrorMessage = "Invalid Account Name " + fields[IIFVNAME]; return false; }
            string TBusinessName = fields[IIFOCOMPANYNAME];
            if (TBusinessName.Length == 0) TBusinessName = fields[IIFONAME];
            if (!ValidateBusinessName(TBusinessName, PayToListFormat)) { ErrorMessage = "Invalid Business Name " + fields[IIFOCOMPANYNAME]; return false; }
            string TPrintAs = fields[IIFOBADDR1];
            if (TPrintAs.Length == 0) TPrintAs = TBusinessName;
            if (!ValidatePrintAs(TPrintAs, PayToListFormat)) { ErrorMessage = "Invalid Print As " + TPrintAs; return false; }

            string[] combinedcitystate = DataStore.Address.ParseAddressCityStateZipCountry(fields[IIFVADDR2], fields[IIFVADDR3], fields[IIFVADDR4]);
            if (!ValidateAddress(combinedcitystate[0], PayToListFormat)) { ErrorMessage = "Invalid Address " + combinedcitystate[0]; return false; }
            if (!ValidateAddress2(combinedcitystate[1], PayToListFormat)) { ErrorMessage = "Invalid Address " + combinedcitystate[1]; return false; }
            if (!ValidateCity(combinedcitystate[2], PayToListFormat)) { ErrorMessage = "Invalid City " + combinedcitystate[2]; return false; }
            if (!ValidateState(combinedcitystate[3], PayToListFormat)) { ErrorMessage = "Invalid State " + combinedcitystate[3]; return false; }
            if (!ValidateZipCode(combinedcitystate[4], PayToListFormat)) { ErrorMessage = "Invalid Zip Code " + combinedcitystate[4]; return false; }
            if (!ValidateCountry(combinedcitystate[5], PayToListFormat)) { ErrorMessage = "Invalid Country " + combinedcitystate[5]; return false; }

            string TContactPerson = fields[IIFOFIRSTNAME] + " " + fields[IIFOLASTNAME];
            if (TContactPerson.Length == 0) TContactPerson = fields[IIFOCONT1];
            if (!ValidateContactPerson(TContactPerson, PayToListFormat)) { ErrorMessage = "Invalid Contact Person " + TContactPerson; return false; }
            if (!ValidatePhone(fields[IIFOPHONE1], PayToListFormat)) { ErrorMessage = "Invalid Phone " + fields[IIFOPHONE1]; return false; }
            if (!ValidateEmailAddress(fields[IIFOEMAIL], PayToListFormat)) { ErrorMessage = "Invalid Email Address " + fields[IIFOEMAIL]; return false; }

            return true;
        }

        internal void ParseOtherNameIIFFields(string[] fields)
        {
            IsActive = true;
            if (fields[IIFOHIDDEN] == "Y") IsActive = false;
            AccountName = fields[IIFONAME];
            BusinessName = fields[IIFOCOMPANYNAME];
            PrintAs = fields[IIFOBADDR1];
            string[] combinedcitystate = DataStore.Address.ParseAddressCityStateZipCountry(fields[IIFOBADDR2], fields[IIFOBADDR3], fields[IIFOBADDR4]);
            Address = combinedcitystate[0];
            Address2 = combinedcitystate[1];
            City = combinedcitystate[2];
            State = combinedcitystate[3];
            ZipCode = combinedcitystate[4];
            Country = combinedcitystate[5];
            ContactPerson = fields[IIFOFIRSTNAME] + " " + fields[IIFOLASTNAME];
            if (ContactPerson.Length == 0) ContactPerson = fields[IIFOCONT1];
            Phone = fields[IIFOPHONE1];
            EmailAddress = fields[IIFOEMAIL];
            Taxable = false;
            Send1099 = false;
            TaxID = "";

            // clean up some fields
            if (BusinessName.Length == 0) BusinessName = AccountName;
            if (PrintAs.Length == 0) PrintAs = BusinessName;
        }



        internal static bool ValidateAccountName(string AccountName, SheetFormat PayToListFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = PayToListFormat.Column(XLAccountName)!;
            if (!ThisColumn.Valid(AccountName))
                return false;
            return true;
        }
        internal static bool ValidateBusinessName(string TBusinessName, SheetFormat PayToListFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = PayToListFormat.Column(XLBusinessName)!;
            if (!ThisColumn.Valid(TBusinessName))
                return false;
            return true;
        }
        internal static bool ValidatePrintAs(string TPrintAs, SheetFormat PayToListFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = PayToListFormat.Column(XLPrintAs)!;
            if (!ThisColumn.Valid(TPrintAs))
                return false;
            return true;
        }
        internal static bool ValidateAddress(string TAddress, SheetFormat PayToListFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = PayToListFormat.Column(XLAddress)!;
            if (!ThisColumn.Valid(TAddress))
                return false;
            return true;
        }
        internal static bool ValidateAddress2(string TAddress2, SheetFormat PayToListFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = PayToListFormat.Column(XLAddress2)!;
            if (!ThisColumn.Valid(TAddress2))
                return false;
            return true;
        }
        internal static bool ValidateCity(string TCity, SheetFormat PayToListFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = PayToListFormat.Column(XLCity)!;
            if (!ThisColumn.Valid(TCity))
                return false;
            return true;
        }
        internal static bool ValidateState(string TState, SheetFormat PayToListFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = PayToListFormat.Column(XLState)!;
            if (!ThisColumn.Valid(TState))
                return false;
            return true;
        }
        internal static bool ValidateZipCode(string TZipCode, SheetFormat PayToListFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = PayToListFormat.Column(XLZipCode)!;
            if (!ThisColumn.Valid(TZipCode))
                return false;
            return true;
        }
        internal static bool ValidateCountry(string TCountry, SheetFormat PayToListFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = PayToListFormat.Column(XLCountry)!;
            if (!ThisColumn.Valid(TCountry))
                return false;
            if (!DataStore.Address.IsValidCountry(TCountry)) return false;
            return true;
        }
        internal static bool ValidateContactPerson(string TContactPerson, SheetFormat PayToListFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = PayToListFormat.Column(XLContactPerson)!;
            if (!ThisColumn.Valid(TContactPerson))
                return false;
            return true;
        }
        internal static bool ValidatePhone(string TPhone, SheetFormat PayToListFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = PayToListFormat.Column(XLPhone)!;
            if (!ThisColumn.Valid(TPhone))
                return false;
            return true;
        }
        internal static bool ValidateEmailAddress(string TEmailAddress, SheetFormat PayToListFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = PayToListFormat.Column(XLEmailAddress)!;
            if (!ThisColumn.Valid(TEmailAddress))
                return false;
            return true;
        }
        internal static bool ValidateTaxID(string TTaxID, SheetFormat PayToListFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = PayToListFormat.Column(XLTaxID)!;
            if (!ThisColumn.Valid(TTaxID))
                return false;
            return true;
        }






        internal static bool ValidateExcelRow(IXLRow XRow, SheetFormat PayToListFormat, out string ErrorMessage)
        {
            ColumnFormat ThisColumn;
            ErrorMessage = "";

            // check the IsActive flag - if no isactive, then skip this row

            ThisColumn = PayToListFormat.Column(XLIsActive)!;
            string TActive = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            if (TActive.Length == 0) return true;
            if (!ThisColumn.Valid(TActive))
            {
                ErrorMessage = "Invalid Pay To account active flag " + TActive;
                return false;
            }


            // Check the account name 

            ThisColumn = PayToListFormat.Column(XLAccountName)!;
            string TName = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            if (!ThisColumn.Valid(TName))
            {
                ErrorMessage = "Invalid Pay To account name " + TName;
                return false;
            }

            // Check the Business Name

            ThisColumn = PayToListFormat.Column(XLBusinessName)!;
            string TBusinessName = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            if (!ThisColumn.Valid(TBusinessName))
            {
                ErrorMessage = "Invalid Pay To Business Name " + TBusinessName;
                return false;
            }

            // check the Print As

            ThisColumn = PayToListFormat.Column(XLPrintAs)!;
            string TPrintAs = XRow.Cell(ThisColumn.ColumnNumber).GetString();

            if (!ThisColumn.Valid(TPrintAs))
            {
                ErrorMessage = "Invalid Pay To Print As " + TPrintAs;
                return false;
            }

            // Check the Address

            ThisColumn = PayToListFormat.Column(XLAddress)!;
            string TAddress = XRow.Cell(ThisColumn.ColumnNumber).GetString();

            if (!ThisColumn.Valid(TAddress))
            {
                ErrorMessage = "Invalid Pay To Address " + TAddress;
                return false;
            }
            ThisColumn = PayToListFormat.Column(XLAddress2)!;
            string TAddress2 = XRow.Cell(ThisColumn.ColumnNumber).GetString();

            if (!ThisColumn.Valid(TAddress2))
            {
                ErrorMessage = "Invalid Pay To Address2 " + TAddress2;
                return false;
            }
            // Check the City

            ThisColumn = PayToListFormat.Column(XLCity)!;
            string TCity = XRow.Cell(ThisColumn.ColumnNumber).GetString();

            if (!ThisColumn.Valid(TCity))
            {
                ErrorMessage = "Invalid Pay To City " + TCity;
                return false;
            }

            // Check the State

            ThisColumn = PayToListFormat.Column(XLState)!;
            string TState = XRow.Cell(ThisColumn.ColumnNumber).GetString();

            if (!ThisColumn.Valid(TState))
            {
                ErrorMessage = "Invalid Pay To State " + TState;
                return false;
            }


            // Check the ZipCode

            ThisColumn = PayToListFormat.Column(XLZipCode)!;
            string TZipCode = XRow.Cell(ThisColumn.ColumnNumber).GetString();

            if (!ThisColumn.Valid(TZipCode))
            {
                ErrorMessage = "Invalid Pay To ZipCode " + TZipCode;
                return false;
            }


            // Check the ContactPerson

            ThisColumn = PayToListFormat.Column(XLContactPerson)!;
            string TContactPerson = XRow.Cell(ThisColumn.ColumnNumber).GetString();

            if (!ThisColumn.Valid(TContactPerson))
            {
                ErrorMessage = "Invalid Pay To ContactPerson " + TContactPerson;
                return false;
            }


            // Check the Phone

            ThisColumn = PayToListFormat.Column(XLPhone)!;
            string TPhone = XRow.Cell(ThisColumn.ColumnNumber).GetString();

            if (!ThisColumn.Valid(TPhone))
            {
                ErrorMessage = "Invalid Pay To Phone " + TPhone;
                return false;
            }


            // Check the EmailAddress

            ThisColumn = PayToListFormat.Column(XLEmailAddress)!;
            string TEmailAddress = XRow.Cell(ThisColumn.ColumnNumber).GetString();

            if (!ThisColumn.Valid(TEmailAddress))
            {
                ErrorMessage = "Invalid Pay To EmailAddress " + TEmailAddress;
                return false;
            }


            // Check the Taxable

            ThisColumn = PayToListFormat.Column(XLTaxable)!;
            string TTaxable = XRow.Cell(ThisColumn.ColumnNumber).GetString();

            if (!ThisColumn.Valid(TTaxable))
            {
                ErrorMessage = "Invalid Taxable in Pay To " + TTaxable;
                return false;
            }

            // Check the Send1099

            ThisColumn = PayToListFormat.Column(XLSend1099)!;
            string TSend1099 = XRow.Cell(ThisColumn.ColumnNumber).GetString();

            if (!ThisColumn.Valid(TSend1099))
            {
                ErrorMessage = "Invalid Send1099 in Pay To " + TSend1099;
                return false;
            }

            // check the TaxID

            ThisColumn = PayToListFormat.Column(XLTaxID)!;
            string TTaxID = XRow.Cell(ThisColumn.ColumnNumber).GetString();

            if (!ThisColumn.Valid(TTaxID))
            {
                ErrorMessage = "Invalid Pay To TaxID " + TTaxID;
                return false;
            }

            return true;
        }
        internal void ParseExcelRow(IXLRow XRow, SheetFormat PayToListFormat)
        {
            ColumnFormat ThisColumn;

            ThisColumn = PayToListFormat.Column(XLIsActive)!;
            IsActive = TrueFalseColumn.Parse(XRow.Cell(ThisColumn.ColumnNumber).GetString());
            ThisColumn = PayToListFormat.Column(XLAccountName)!;
            AccountName = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            ThisColumn = PayToListFormat.Column(XLBusinessName)!;
            BusinessName = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            ThisColumn = PayToListFormat.Column(XLPrintAs)!;
            PrintAs = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            ThisColumn = PayToListFormat.Column(XLAddress)!;
            Address = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            ThisColumn = PayToListFormat.Column(XLAddress2)!;
            Address2 = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            ThisColumn = PayToListFormat.Column(XLCity)!;
            City = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            ThisColumn = PayToListFormat.Column(XLState)!;
            State = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            ThisColumn = PayToListFormat.Column(XLZipCode)!;
            ZipCode = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            ThisColumn = PayToListFormat.Column(XLCountry)!;
            Country = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            ThisColumn = PayToListFormat.Column(XLContactPerson)!;
            ContactPerson = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            ThisColumn = PayToListFormat.Column(XLPhone)!;
            Phone = XRow.Cell(ThisColumn.ColumnNumber).GetString(); 
            ThisColumn = PayToListFormat.Column(XLEmailAddress)!;
            EmailAddress = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            ThisColumn = PayToListFormat.Column(XLTaxable)!;
            Taxable = TrueFalseColumn.Parse(XRow.Cell(ThisColumn.ColumnNumber).GetString());
            ThisColumn = PayToListFormat.Column(XLSend1099)!;
            Send1099 = TrueFalseColumn.Parse(XRow.Cell(ThisColumn.ColumnNumber).GetString());
            ThisColumn = PayToListFormat.Column(XLTaxID)!;
            TaxID = XRow.Cell(ThisColumn.ColumnNumber).GetString();

        }

        internal void WriteExcelRow(IXLRow XRow, SheetFormat PayToListFormat)
        {

            ColumnFormat? Col = PayToListFormat.Column(XLIsActive)!;
            XRow.Cell(Col.ColumnNumber).Value = IsActive.ToString();
            Col = PayToListFormat.Column(XLPrintAs)!;
            XRow.Cell(Col.ColumnNumber).Value = PrintAs;
            Col = PayToListFormat.Column(XLAccountName)!;
            XRow.Cell(Col.ColumnNumber).Value = AccountName;
            Col = PayToListFormat.Column(XLBusinessName)!;
            XRow.Cell(Col.ColumnNumber).Value = BusinessName;
            Col = PayToListFormat.Column(XLAddress)!;
            XRow.Cell(Col.ColumnNumber).Value = Address;
            Col = PayToListFormat.Column(XLAddress2)!;
            XRow.Cell(Col.ColumnNumber).Value = Address2;
            Col = PayToListFormat.Column(XLCity)!;
            XRow.Cell(Col.ColumnNumber).Value = City;
            Col = PayToListFormat.Column(XLState)!;
            XRow.Cell(Col.ColumnNumber).Value = State;
            Col = PayToListFormat.Column(XLZipCode)!;
            XRow.Cell(Col.ColumnNumber).Value = ZipCode;
            Col = PayToListFormat.Column(XLCountry)!;
            XRow.Cell(Col.ColumnNumber).Value = Country;
            Col = PayToListFormat.Column(XLContactPerson)!;
            XRow.Cell(Col.ColumnNumber).Value = ContactPerson;
            Col = PayToListFormat.Column(XLPhone)!;
            XRow.Cell(Col.ColumnNumber).Value = Phone;
            Col = PayToListFormat.Column(XLEmailAddress)!;
            XRow.Cell(Col.ColumnNumber).Value = EmailAddress;
            Col = PayToListFormat.Column(XLTaxable)!;
            XRow.Cell(Col.ColumnNumber).Value = Taxable.ToString();
            Col = PayToListFormat.Column(XLSend1099)!;
            XRow.Cell(Col.ColumnNumber).Value = Send1099.ToString();
            Col = PayToListFormat.Column(XLTaxID)!;
            XRow.Cell(Col.ColumnNumber).Value = TaxID;
        }





        internal static void AddSheetColumns(SheetFormat CustomerListFormat)
        {
            CustomerListFormat.Add(new TrueFalseColumn(1, XLIsActive, 6, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsRequired));
            CustomerListFormat.Add(new TextColumn(2, XLAccountName, 60, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsRequired));
            CustomerListFormat.Add(new TextColumn(3, XLBusinessName, 255, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsOptional));
            CustomerListFormat.Add(new TextColumn(4, XLPrintAs, 50, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsRequired));
            CustomerListFormat.Add(new TextColumn(5, XLAddress, 255, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsOptional));
            CustomerListFormat.Add(new TextColumn(6, XLAddress2, 255, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsOptional));
            CustomerListFormat.Add(new TextColumn(7, XLCity, 50, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsOptional));
            CustomerListFormat.Add(new TextColumn(8, XLState, 50, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsOptional));
            CustomerListFormat.Add(new TextColumn(9, XLZipCode, 15, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsOptional));
            CustomerListFormat.Add(new TextColumn(10, XLCountry, 50, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsOptional));
            CustomerListFormat.Add(new TextColumn(11, XLContactPerson, 100, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsOptional));
            CustomerListFormat.Add(new PhoneColumn(25, XLPhone, 20, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsOptional));
            CustomerListFormat.Add(new EmailColumn(13, XLEmailAddress, 100, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsOptional));
            CustomerListFormat.Add(new TrueFalseColumn(14, XLTaxable, 6, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsRequired));
            CustomerListFormat.Add(new TrueFalseColumn(15, XLSend1099, 6, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsRequired));
            CustomerListFormat.Add(new TextColumn(16, XLTaxID, 11, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsOptional));
        }


    }
}
