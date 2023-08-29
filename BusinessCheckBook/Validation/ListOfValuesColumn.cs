//**********************************************************************
//
//          Copyright © 2023 Prairie Trail Software, Inc.
//
//**********************************************************************


namespace BusinessCheckBook.Validation
{
    internal class ListOfValuesColumn : ColumnFormat
    {
        List<string> ValuesToAccept = new();

        public ListOfValuesColumn(int columnNumber, string nName, List<string> values, int length, bool RequiredCol, bool Required) 
            : base(columnNumber, nName, length, RequiredCol, Required)
        {
            ValuesToAccept = values;
            WhichFieldType = ColumnFieldType.ListOfValues;
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
            CurrentFieldType = "List of Values";

            // the next test fails on empty fields
            // so, check for an empty field
            if (TestValue.Trim().Length < 1)
            {
                CleanedData = "";
                if (RequiredValue) return false;
                return true;
            }
            // Names can have quotes around them, clean those off
            if (TestValue[0] == '"')
            {
                CleanedData = CleanStringQuotes(TestValue);
            }
            else
                CleanedData = TestValue;

            if (!LengthTest(CleanedData)) return false;

            // passed the sanity checks, now check if in list

            foreach (string ListValue in ValuesToAccept)
            {
                if (ListValue.ToLower() == TestValue.Trim().ToLower()) 
                    return true; 
            }


            return false;
        }


    }
}
