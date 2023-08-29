//**********************************************************************
//
//          Copyright © 2023 Prairie Trail Software, Inc.
//
//**********************************************************************


namespace BusinessCheckBook.Validation
{

    public class CityColumn : ColumnFormat
    {
        public CityColumn(int columnNumber, string nName,  int length, bool RequiredCol, bool Required)
            : base("^[a-zàâäæáçèêëéîïíôóœùûúýñüA-ZÀÂÄÁÆÇÈÊËÉÎÏÍÔÓŒÑÙÛÚÜ \\-\\.\\']+$")
        {
            SetParameters(columnNumber, nName, length, RequiredCol, Required);
            WhichFieldType = ColumnFieldType.City;
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
            CurrentFieldType = "City";

            CleanData(TestValue);
            if (!LengthTest(CleanedData)) return false;
            return base.Valid(TestValue);
        }
    }


}
