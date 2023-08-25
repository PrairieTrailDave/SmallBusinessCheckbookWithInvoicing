using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCheckBook.DataStore
{
    internal class InvoicePrintLayout
    {
        // logo as an image
        public PrintItemLayout LogoFileName { get; set; } = new();

        // Company Information texts
        public List<PrintItemLayout> CompanyInformationItems { get; set; } = new();

        // boxes to draw around text
        public List<Rectangle> BoxRects { get; set; } = new();

        // standard text on invoice
        public List<PrintItemLayout> StandardTexts { get; set; } = new();

        // Invoice specific text placements
        public List<PrintItemLayout> TextToPrintLayouts { get; set; } = new();

    }
}
