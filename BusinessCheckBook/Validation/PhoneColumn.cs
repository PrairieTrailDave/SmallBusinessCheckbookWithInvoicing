//**********************************************************************
//
//          Copyright © 2023 Prairie Trail Software, Inc.
//
//**********************************************************************


namespace BusinessCheckBook.Validation
{
    public class PhoneColumn : ColumnFormat
    {
        // This phone column assumes US, Canada, and Mexico phone numbers
        public PhoneColumn(int columnNumber, string nName,  int length, bool RequiredCol, bool Required)
            : base("^[0-9xX\\-\\.\\(\\)\\+ ]+$")
        {
            SetParameters(columnNumber, nName, length, RequiredCol, Required);
            WhichFieldType = ColumnFieldType.Phone;
        }
        public override void Initialize()
        {
            base.Initialize();
        }
        public override void CleanData(string TestValue)
        {
            CleanedData = TestValue;
            CleanedData = GetDigits(TestValue);
        }
        public override bool Valid(string TestValue)
        {
            CurrentFieldType = "Phone";
            CleanedData = TestValue;
            CleanedData = GetDigits(TestValue);
            if (!LengthTest(TestValue)) return false;
            return base.Valid(TestValue);
        }
        public override string Reformat(string TestValue)
        {
            string results = string.Empty;
            TestValue = GetDigits(TestValue);

            if (TestValue.Length > 0)
            {
                results += "(" + TestValue[0];
                if (TestValue.Length > 1) results += TestValue[1];
                if (TestValue.Length > 2) results += TestValue[2];
                if (TestValue.Length > 3) results += ") " +  TestValue[3];
                if (TestValue.Length > 4) results += TestValue[4];
                if (TestValue.Length > 5) results += TestValue[5];
                if (TestValue.Length > 6) results += "-" + TestValue[6];
                if (TestValue.Length > 7) results += TestValue[7];
                if (TestValue.Length > 8) results += TestValue[8];
                if (TestValue.Length > 9) results += TestValue[9];
                if (TestValue.Length > 10) results += "x" + TestValue[10];
                if (TestValue.Length > 11) results += TestValue[11];
                if (TestValue.Length > 12) results += TestValue[12];
                if (TestValue.Length > 13) results += TestValue[13];
            }


            return results;
        }
    }

}
