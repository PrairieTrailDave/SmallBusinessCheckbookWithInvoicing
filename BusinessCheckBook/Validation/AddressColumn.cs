using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessCheckBook.Validation
{
    public class AddressColumn : ColumnFormat
    {
        public AddressColumn(int columnNumber, string nName,  int length, bool RequiredCol, bool Required)
            // do not add semi colon to this list
            : base //("^\d{1,3}.?\d{0,3}\s[a-zA-Z]{2,30}\s[a-zA-Z]{2,15}", 80)
            ("^[a-zàâäæáçèêëéîïíôóœùûúýñüA-ZÀÂÄÁÆÇÈÊËÉÎÏÍÔÓŒÑÙÛÚÜ0-9_\\," + Regex.Escape("(") + Regex.Escape(")") + "\\#\\@\\-\\ \\.\\/\\'\\:\\&]+$")
        {
            SetParameters(columnNumber, nName, length, RequiredCol, Required);
            WhichFieldType = ColumnFieldType.Address;
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
            CurrentFieldType = "Address";

            CleanData(TestValue);
            if (!LengthTest(CleanedData)) return false;
            return base.Valid(CleanedData);
        }
    }


}
