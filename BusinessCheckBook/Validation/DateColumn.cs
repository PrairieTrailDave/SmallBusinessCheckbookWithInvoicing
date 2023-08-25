using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCheckBook.Validation
{
    public class DateColumn : ColumnFormat
    {
        public DateColumn(int columnNumber, string nName, int length, bool RequiredCol, bool Required) : 
            base("^[0-9\\/\\-\\.]+$") 
        { 
            SetParameters(columnNumber, nName, length, RequiredCol, Required); 
            WhichFieldType = ColumnFieldType.Date; 
        }
        public override void Initialize()
        {
            base.Initialize();
        }
        public override void CleanData(string TestValue)
        {
            CleanedData = "";
            if (TestValue.Length == 0)
                return;

            // drop any trailing time

            int pos = 0;
            while ("0123456789/".IndexOf(TestValue[pos]) > -1)
            {
                pos++;
                if (pos >= TestValue.Length)
                    break;
            }
            if (pos > 0)
                TestValue = TestValue.Substring(0, pos);
            CleanedData = TestValue.Trim();
        }

        public override bool Valid(string TestValue)
        {
            CurrentFieldType = "Date";
            CleanedData = "";
            TestValue = TestValue.Trim();

            // check for all zeros as a date
            // treat it as if empty field
            if (TestValue == "00000000"
             || TestValue == "00/00/0000"
             || TestValue == "0/00/0000"
             || TestValue == "0/0/0000"
             || TestValue == "0/00/00"
             || TestValue == "0/0/00"
             || TestValue == "00-00-0000")
            {
                if (!base.Valid("")) return false;
                return true;
            }

            if (TestValue.Length == 0)
            {
                if (RequiredValue)
                    return false;
                return true;
            }
            // drop any trailing time

            int pos = 0;
            while ("0123456789/".IndexOf(TestValue[pos]) > -1)
            {
                pos++;
                if (pos >= TestValue.Length)
                    break;
            }
            if (pos > 0)
                TestValue = TestValue.Substring(0, pos);

            if (!LengthTest(TestValue)) return false;
            if (!base.Valid(TestValue)) return false;

            // see if the base valid test replaced with default value

            string ToTest = TestValue;
            if (TestValue.Length == 0)
            {
                if (CleanedData.Length > 0)
                    ToTest = CleanedData;
            }

            // if not required and no default value
            // then if not here, that is not a problem
            if (ToTest.Length == 0) return true;

            // verify that the date is not too early

            DateTime TestDate;
            bool Res = DateTime.TryParse(ToTest, out TestDate);
            if (Res)
            {
                CleanedData = TestDate.ToShortDateString();
                if (TestDate.Year < 1900)
                {
                    ErrorCode = "Date too early";
                    return false;
                }
            }
            return Res;
        }
    }

}
