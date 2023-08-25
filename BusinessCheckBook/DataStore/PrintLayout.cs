using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCheckBook.DataStore
{
    internal class PrintLayout
    {
        internal string LayoutName { get; set; } = string.Empty;
        internal List<LayoutItem> LayoutItems { get; set; } = new();

        public PrintLayout() { }
    }
}
