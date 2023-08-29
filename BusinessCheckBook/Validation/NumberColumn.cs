//**********************************************************************
//
//          Copyright © 2023 Prairie Trail Software, Inc.
//
//**********************************************************************


namespace BusinessCheckBook.Validation
{
    public class NumberColumn : ColumnFormat
    {
        public NumberColumn(int columnNumber, string nName, int length, bool RequiredCol, bool Required)
            : base("^[0-9]+$")
        {
            SetParameters(columnNumber, nName, length, RequiredCol, Required);
            WhichFieldType = ColumnFieldType.Number;
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
            CurrentFieldType = "Number Field";

            CleanedData = CleanStringQuotes(TestValue);
            if (!LengthTest(CleanedData)) return false;
            if (!base.Valid(CleanedData)) return false;
            if (CleanedData.Length == 0) return true;  // rely on base.valid to do required check

            return int.TryParse(CleanedData, out _);
        }
    }
}
