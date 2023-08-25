using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessCheckBook.DataStore;

namespace BusinessCheckBook.Validation
{

    public class StateColumn : ColumnFormat
    {
 
        public StateColumn(int columnNumber, string nName, int length, bool RequiredCol, bool Required)
                : base("^[a-zA-Z]+$")
        {
            SetParameters(columnNumber, nName, length, RequiredCol, Required);
            WhichFieldType = ColumnFieldType.State;
        }
        public override void Initialize()
        {
            base.Initialize();
        }
        public override void CleanData(string TestValue)
        {
            CleanedData = CleanStringQuotes(TestValue).Trim();
        }
        public override bool Valid(string TestValue)
        {
            CurrentFieldType = "State";

            CleanData(TestValue);
            if (!LengthTest(CleanedData)) return false;
            if (!base.Valid(CleanedData)) return false;
            if (CleanedData.Length > 0)
                return Address.StateListAbreviations.Contains(CleanedData.ToUpper());
            return true;
        }
    }

}
