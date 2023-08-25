using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCheckBook.Validation
{
    internal class EmailColumn : ColumnFormat
    {
        public EmailColumn(int columnNumber, string nName, int length, bool RequiredCol, bool Required)
            : base("^[0-9A-Za-z\\-\\.\\(\\) ]+$")
        {
            SetParameters(columnNumber, nName, length, RequiredCol, Required);
            WhichFieldType = ColumnFieldType.Email;
        }
        public override void Initialize()
        {
            base.Initialize();
        }
        public override bool Valid(string TestValue)
        {
            CurrentFieldType = "Email";

            if (!LengthTest(TestValue)) return false;
            if (TestValue.Length == 0)
            {
                if (RequiredValue) return false;
                return true;  // we return true not because it is a valid email address, but that this is a valid value to read from files
            }
            return MailAddress.TryCreate(TestValue, out _);
        }
    }
}
