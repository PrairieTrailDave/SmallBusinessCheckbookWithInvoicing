using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCheckBook.Validation
{
    public class NameColumn : ColumnFormat
    {
        // allow English, Spanish, and French characters in names
        public NameColumn(int columnNumber, string nName, int length, bool RequiredCol, bool Required)
            : base("^[a-zàâäæáçèêëéîïíôóœùûúýñüA-ZÀÂÄÁÆÇÈÊËÉÎÏÍÔÓŒÑÙÛÚÜ0-9_\\-\\ \\.\\,\\(\\)\\:\\/\\&\\']+$")
        {
            SetParameters(columnNumber, nName, length, RequiredCol, Required);
            WhichFieldType = ColumnFieldType.Name;
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
            CurrentFieldType = "Name";

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
            return base.Valid(CleanedData);
        }

    }
}
