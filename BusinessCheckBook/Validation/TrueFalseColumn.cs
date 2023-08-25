using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCheckBook.Validation
{
    public class TrueFalseColumn : ColumnFormat
    {
        public TrueFalseColumn(int columnNumber, string nName,  int length, bool RequiredCol, bool Required)
            : base("^[a-zA-Z]+$")
        {
            SetParameters(columnNumber, nName, length, RequiredCol, Required);
            WhichFieldType = ColumnFieldType.TrueFalse;
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
            CurrentFieldType = "TrueFalse";

            // the next test fails on empty fields
            // so, check for an empty field
            if (TestValue.Trim().Length < 1)
            {
                CleanedData = "";
                if (RequiredValue) return false;
                return true;
            }

            CleanedData = TestValue.ToUpper().Trim();
            if (TestValue.ToUpper().Trim() == "TRUE" ||
                TestValue.ToUpper().Trim() == "FALSE")
                return true;
            return false;
        }

        public static bool Parse (string value)
        {
            if (value.Trim().ToUpper() == "TRUE") return true;
            return false;

        }

    }

}
