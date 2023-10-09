//**********************************************************************
//
//          Copyright © 2023 Prairie Trail Software, Inc.
//
//**********************************************************************

//using ClosedXML.Excel;


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

        //internal void AddColumnFormatting(IXLWorksheet CurrentWorksheet)
        //{
        //    foreach (ColumnFormat cf in ColumnFormats)
        //    {
        //        if (cf.CurrentFieldType == ColumnFormat.ColumnFieldType.Date.ToString())
        //        {
        //            CurrentWorksheet.Columns(cf.ColumnNumber).Style.DateFormat.Format = "mm/dd/yyyy"; ;
        //        }
        //    }
        //}
    }
}
