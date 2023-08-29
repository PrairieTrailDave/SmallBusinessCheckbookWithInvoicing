//**********************************************************************
//
//          Copyright © 2023 Prairie Trail Software, Inc.
//
//**********************************************************************

namespace BusinessCheckBook.DataStore
{
    internal class PrintLayout
    {
        internal string LayoutName { get; set; } = string.Empty;
        internal List<LayoutItem> LayoutItems { get; set; } = new();

        public PrintLayout() { }
    }
}
