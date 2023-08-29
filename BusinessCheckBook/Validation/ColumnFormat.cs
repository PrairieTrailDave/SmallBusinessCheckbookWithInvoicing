//**********************************************************************
//
//          Copyright © 2023 Prairie Trail Software, Inc.
//
//**********************************************************************
using System.Text.RegularExpressions;

namespace BusinessCheckBook.Validation
{

    public class ColumnFormat
    {
        public const bool FieldIsRequired = true;
        public const bool FieldIsOptional = false;
        public const bool ColumnIsRequired = true;
        public const bool ColumnIsOptional = false;

        public enum ColumnFieldType
        {
            Name,
            Address,
            City,
            State,
            Zip,
            Phone,
            Email,
            Date,
            Number,
            Decimal,
            String,
            TrueFalse,
            ListOfValues
        }


        //public enum Formats
        //{
        //    Name,
        //    Address,
        //    City,
        //    State,
        //    Zip,
        //    Phone,
        //    Date,
        //    Number,
        //    Decimal,
        //    String,
        //    TrueFalse
        //}



        public string Name { get; set; } = string.Empty;
        public int ColumnNumber { get; set; }
        public ColumnFieldType WhichFieldType { get; set; }  // used in deciding how to handle this field
        public string CurrentFieldType { get; set; } = string.Empty; // used for error reporting
        public string ErrorCode { get; set; } = string.Empty;
        public string Warning { get; set; } = string.Empty;
        public string RegExpData { get; set; } = string.Empty;
        public int MaxFieldLength { get; set; } = 255;
        public string CleanedData { get; set; } = string.Empty;
        public bool RequiredValue { get; set; } = false;
        public bool RequiredColumn { get; set; } = false;

        private Regex regex = new Regex("");

        public ColumnFormat() { }

        public ColumnFormat(string NewExpression)
        {
            Name = "";
            RegExpData = NewExpression;
            regex = new Regex(RegExpData);
        }
        public ColumnFormat(int columnNumber, string nName, int length, bool RequiredCol, bool tRequired)
        {
            ColumnNumber = columnNumber;
            Name = nName;
            MaxFieldLength = length;
            RequiredValue = tRequired;
            RequiredColumn = RequiredCol;
            regex = new Regex(RegExpData);
        }

        protected void SetParameters(int columnNumber, string nName, int length, bool RequiredCol, bool tRequired)
        {
            ColumnNumber = columnNumber;
            Name = nName;
            MaxFieldLength = length;
            RequiredValue = tRequired;
            RequiredColumn = RequiredCol;
        }
        public virtual void Initialize()
        {
            Warning = "";
            ErrorCode = "";
            CleanedData = "";
            CurrentFieldType = "";
        }

        public virtual void CleanData(string TestValue)
        {
            CleanedData = TestValue;
        }
        public virtual decimal GetValue (string? TestValue) { return 0.00M; }
        public virtual bool Valid(string TestValue)
        {
            Warning = "";
            if (RequiredValue)
            {
                ErrorCode = "Missing value";
                if (TestValue == null) return false;
                if (TestValue.Length == 0)
                    return false;
            }

            // don't try to match empty strings
            if (TestValue.Length == 0)
                return true;

            // do the character validation
            ErrorCode = "Invalid Character in " + CurrentFieldType;
            Match rMatch = regex.Match(TestValue);
            return rMatch.Success;
        }



        public bool LengthTest(string TestValue)
        {
            if (TestValue == null) return true;
            if (TestValue.Length == 0) return true;

            if (MaxFieldLength > 0)
            {
                if (TestValue.Length > MaxFieldLength)
                {
                    ErrorCode = "**Length Error ";
                    return false;
                }
            }
            return true;
        }

        // remove any quotes around the string
        // and any embedded single quotes
        public static string CleanStringQuotes(string Nm)
        {
            string Results = "";
            if (Nm == null) return Results;
            if (Nm.Length == 0) return Results;

            foreach (char ch in Nm)
            {
                if (ch != '"' && ch.ToString() != "'")
                    Results = Results + ch;
            }
            return Results;
        }
        // routine to pull the digits out of an entered string

        public static string GetDigits(string Stuff)
        {
            string Contents = "";
            if (Stuff == null) return "";
            foreach (char ch in Stuff)
            {
                if (char.IsDigit(ch))
                    Contents = Contents + ch;
            }
            return Contents;
        }

        public virtual string Reformat(string TestValue)
        {
            CleanData(TestValue);
            if (TestValue.Length > 0 && CleanedData.Length == 0)
                throw new Exception("No results from valid");
            return CleanedData;
        }

    }

























 

    //public class ExcelFieldFactory
    //{
    //    public static ExcelField MakeExcelField(string nName, string tColumnID, string FormatType, int length, bool RequiredCol, bool RequiredValue)
    //    {
    //        string tFieldFormat = FormatType.ToUpper();

    //        if (tFieldFormat == ExcelField.Formats.Name.ToString().ToUpper())
    //        {
    //            NameField nfd = new NameField(nName, tColumnID, length, RequiredCol, RequiredValue);
    //            return nfd;
    //        }
    //        else if (tFieldFormat == ExcelField.Formats.Address.ToString().ToUpper())
    //        {
    //            AddressField afd = new AddressField(nName, tColumnID, length, RequiredCol, RequiredValue);
    //            return afd;
    //        }
    //        else if (tFieldFormat == ExcelField.Formats.City.ToString().ToUpper())
    //        {
    //            CityField cfd = new CityField(nName, tColumnID, length, RequiredCol, RequiredValue);
    //            return cfd;
    //        }
    //        else if (tFieldFormat == ExcelField.Formats.State.ToString().ToUpper())
    //        {
    //            StateField sfd = new StateField(nName, tColumnID, length, RequiredCol, RequiredValue);
    //            return sfd;
    //        }
    //        else if (tFieldFormat == ExcelField.Formats.Phone.ToString().ToUpper())
    //        {
    //            PhoneField pfd = new PhoneField(nName, tColumnID, length, RequiredCol, RequiredValue);
    //            return pfd;
    //        }
    //        else if (tFieldFormat == ExcelField.Formats.Date.ToString().ToUpper())
    //        {
    //            DateField dfd = new DateField(nName, tColumnID, length, RequiredCol, RequiredValue);
    //            return dfd;
    //        }
    //        else if (tFieldFormat == ExcelField.Formats.Number.ToString().ToUpper())
    //        {
    //            NumberField nfd = new NumberField(nName, tColumnID, length, RequiredCol, RequiredValue);
    //            return nfd;
    //        }
    //        else if (tFieldFormat == ExcelField.Formats.Decimal.ToString().ToUpper())
    //        {
    //            DecimalField dfd = new DecimalField(nName, tColumnID, length, RequiredCol, RequiredValue);
    //            return dfd;
    //        }
    //        else if (tFieldFormat == ExcelField.Formats.String.ToString().ToUpper())
    //        {
    //            StringField nfd = new StringField(nName, tColumnID, length, RequiredCol, RequiredValue);
    //            return nfd;
    //        }
    //        else if (tFieldFormat == ExcelField.Formats.TrueFalse.ToString().ToUpper())
    //        {
    //            TrueFalseField nfd = new TrueFalseField(nName, tColumnID, length, RequiredCol, RequiredValue);
    //            return nfd;
    //        }
    //        else if (tFieldFormat == ExcelField.Formats.Zip.ToString().ToUpper())
    //        {
    //            ZipField zfd = new ZipField(nName, tColumnID, length, RequiredCol, RequiredValue);
    //            return zfd;
    //        }
    //        else
    //            throw new Exception("Invalid format type:" + tFieldFormat);
    //        //return null;
    //    }


    //}
}

