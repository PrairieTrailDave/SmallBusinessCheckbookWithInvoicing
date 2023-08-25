using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCheckBook.DataStore
{
    internal class CheckPrintLayout
    {
        internal PrintItemLayout PrintCompanyName { get; set; } = new();
        internal PrintItemLayout PrintCheckNumber { get; set; } = new();
        internal PrintItemLayout PrintDate { get; set; } = new();
        internal PrintItemLayout PrintToTheOrderOf { get; set; } = new();
        internal PrintItemLayout PrintAmount { get; set; } = new();
        internal PrintItemLayout PrintAmountWords { get; set; } = new();
        internal PrintItemLayout PrintToWhom { get; set; } = new();
        internal PrintItemLayout PrintAddress1 { get; set; } = new();
        internal PrintItemLayout PrintAddress2 { get; set; } = new();
        internal PrintItemLayout PrintAddress3 { get; set; } = new();
        internal PrintItemLayout PrintAddress4 { get; set; } = new();
        internal PrintItemLayout PrintMemo { get; set; } = new();
        internal PrintItemLayout PrintBreakdown1 { get; set; } = new();
        internal PrintItemLayout PrintBreakdown2 { get; set; } = new();
    }
}
