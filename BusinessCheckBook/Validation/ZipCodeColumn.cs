using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessCheckBook.Validation
{
    public class ZipCodeColumn : ColumnFormat
    {
        //private static readonly string USValid = "^[0-9\\-]+$";
        private static readonly string CAValid = "^[A-Za-z][0-9][A-Za-z] [0-9][A-Za-z][0-9]$";
        //private static readonly string MXValid = "^[0-9\\-]+$";

        public ZipCodeColumn(int columnNumber, string nName, int length, bool RequiredCol, bool Required)
            : base("^[0-9\\-]+$")
        {
            SetParameters(columnNumber, nName, length, RequiredCol, Required);
            WhichFieldType = ColumnFieldType.Zip;
        }
        public override void Initialize()
        {
            base.Initialize();
        }
        public override void CleanData(string TestValue)
        {
            CleanedData = CleanStringQuotes(TestValue);
        }
        public override bool Valid(string TestValue)
        {
            CurrentFieldType = "Zip";

            CleanData(TestValue);
            if (!LengthTest(TestValue)) return false;
            if (Regex.IsMatch(TestValue, CAValid)) return true;
            return base.Valid(TestValue);
        }
        public override string Reformat(string TestValue)
        {
            TestValue = CleanStringQuotes(TestValue).Trim();
            return TestValue.Length switch
            {
                2 => "000" + TestValue,
                3 => "00" + TestValue,
                4 => "0" + TestValue,
                5 => TestValue,
                _ => TestValue,
            };
        }
    }

}
