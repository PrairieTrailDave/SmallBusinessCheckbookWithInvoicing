//**********************************************************************
//
//          Copyright © 2023 Prairie Trail Software, Inc.
//
//**********************************************************************


namespace BusinessCheckBook.Validation
{
    public class DecimalColumn : ColumnFormat
    {
        public DecimalColumn(int columnNumber, string nName, int length, bool RequiredCol, bool Required)
            : base("^[0-9.\\-\\ E]+$")
        {
            SetParameters(columnNumber, nName, length, RequiredCol, Required);
            WhichFieldType = ColumnFieldType.Decimal;
        }
        public override void Initialize()
        {
            base.Initialize();
        }


        // this version is not handling "6.0E-5" formats
        public override void CleanData(string DFld)
        {
            string Results = "";
            int i = 0;

            CleanedData = "";
            if (DFld == null) return;

            // clean off leading blanks and any quotes
            while (i < DFld.Length)
            {
                if (DFld[i] != ' '
                    && DFld[i] != '\"')
                    break;
                i++;
            }

            // go through and take the digits and periods, skip commas

            while (i < DFld.Length)
            {
                if (DFld[i] == ',')
                {
                    i++;
                    continue;
                }
                if (DFld[i] == '-' || DFld[i] == '.' || char.IsDigit(DFld[i]))
                    Results = Results + DFld[i];
                else
                    if (DFld[i] == 'E')
                {
                    CleanedData = HandleEFormat(DFld);
                    return;
                }
                else
                {
                    CleanedData = Results;
                    return;
                }
                i++;

            }
            CleanedData = Results;
            return;
        }


        private static string HandleEFormat(string Test)
        {
            string results = "";

            // copy up to the E

            foreach (char ch in Test)
            {
                if (ch == 'E') break;
                results = results + ch;
            }

            // insure that there is a decimal point in the results

            if (!results.Contains('.'))
            {
                results = results + '.';  // if not in the results, then only one digit
            }

            // get the exponent

            string Exp = Test.Substring(Test.IndexOf('E') + 1);
            int Ex;
            if (int.TryParse(Exp, out Ex))
            {
                // insure at least one decimal place for the below code to work
                results = results + '0';

                // then move the decimal point over that many times
                while (Ex != 0)
                {
                    results = "0." + results[0] + results.Substring(2);
                    Ex++;
                }
            }
            return results;
        }
        public override bool Valid(string TestValue)
        {
            CurrentFieldType = "Decimal Field";
            if (TestValue.Length == 0)
            {
                if (RequiredValue) return false;
                return true;
            }
            // check for illegal characters in field
            if (!base.Valid(TestValue)) return false;

            // clean up to be just decimal characters
            CleanData(TestValue);

            if (!LengthTest(CleanedData)) return false;
            if (!base.Valid(CleanedData)) return false;
            if (CleanedData.Length == 0) return true;  // rely on base.Valid for required test

            return decimal.TryParse(CleanedData, out _);
        }
        public override decimal GetValue(string? TestValue)
        {
 
            if (TestValue == null) return 0.00M;
            if (TestValue.Length == 0) return 0.00M;
            return decimal.Parse(TestValue);
        }
    }

}
