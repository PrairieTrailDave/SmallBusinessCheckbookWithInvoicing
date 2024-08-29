//**********************************************************************
//
//          Copyright © 2023 Prairie Trail Software, Inc.
//
//**********************************************************************


namespace BusinessCheckBook.Validation
{
    public class TimeColumn : ColumnFormat
    {
        public TimeColumn(int columnNumber, string nName, int length, bool RequiredCol, bool Required) : 
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

            // drop any leading date

            int pos = 0;
            while ("0123456789/".IndexOf(TestValue[pos]) > -1)
            {
                pos++;
                if (pos >= TestValue.Length)
                    break;
            }
            if (pos > 0)
                TestValue = TestValue.Substring(pos);
            CleanedData = TestValue.Trim();
        }

        public override bool Valid(string TestValue)
        {
            CurrentFieldType = "Date";
            CleanedData = "";
            TestValue = TestValue.Trim();


            if (TestValue.Length == 0)
            {
                if (RequiredValue)
                    return false;
                return true;
            }
            // drop any leading date

            int pos = 0;
            while ("0123456789/".IndexOf(TestValue[pos]) > -1)
            {
                pos++;
                if (pos >= TestValue.Length)
                    break;
            }
            if (pos > 0)
                TestValue = TestValue.Substring(pos);

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

            // TryParse is supposed to be able to handle simple time value (3:46 PM)

            DateTime TestDate;
            bool Res = DateTime.TryParse(ToTest, out TestDate);
            if (Res)
            {
                CleanedData = TestDate.ToShortTimeString();
            }
            return Res;
        }
    }

}
