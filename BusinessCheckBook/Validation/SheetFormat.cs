using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCheckBook.Validation
{
    public class SheetFormat
    {
        public string SheetName { get; set; } = string.Empty;
        public List<ColumnFormat> ColumnFormats { get; set; } = new();

        public List<SheetFormat> SubFormats { get; set; } = new();


        public int Count()
        {
            return ColumnFormats.Count;
        }

        internal void Add(ColumnFormat c)
        {
            ColumnFormats.Add(c);
        }

        internal ColumnFormat? Column(string columnName)
        {
            foreach (ColumnFormat c in ColumnFormats)
            {
                if (c.Name == columnName) return c;
            }
            return null;
        }

        internal ColumnFormat Column(int formatItem)
        {
            return ColumnFormats[formatItem];
        }
    }
}
