//**********************************************************************
//
//          Copyright © 2023 Prairie Trail Software, Inc.
//
//**********************************************************************

using BusinessCheckBook.Validation;
using ClosedXML.Excel;

namespace BusinessCheckBook.DataStore
{
    internal class LayoutItem
    {
        internal string ItemID { get; set; } = string.Empty;
        internal int YPos { get; set; }
        internal int XPos { get; set; }
        internal int Width { get; set; }   
        internal int Height { get; set; }
        internal string Font { get; set; } = string.Empty;

        // Excel column names

        private const string XLItemID = "ItemID";
        private const string XLYPos = "YPos";
        private const string XLXPos = "XPos";
        private const string XLWidth = "Width";
        private const string XLHeight = "Height";
        private const string XLFont = "Font";

        internal LayoutItem() { }
        internal void ParseExcelColumns(IXLRow XRow, SheetFormat LayoutFormat)
        {
            ColumnFormat? Col;
            Col = LayoutFormat.Column(XLItemID)!;
            ItemID = XRow.Cell(Col.ColumnNumber).GetString();
            Col = LayoutFormat.Column(XLYPos)!;
            YPos = Int32.Parse(XRow.Cell(Col.ColumnNumber).GetString());
            Col = LayoutFormat.Column(XLXPos)!;
            XPos = Int32.Parse(XRow.Cell(Col.ColumnNumber).GetString());
            Col = LayoutFormat.Column(XLWidth)!;
            Width = Int32.Parse(XRow.Cell(Col.ColumnNumber).GetString());
            Col = LayoutFormat.Column(XLHeight)!;
            Height = Int32.Parse(XRow.Cell(Col.ColumnNumber).GetString());
            Col = LayoutFormat.Column(XLFont)!;
            Font = XRow.Cell(Col.ColumnNumber).GetString();
        }
        internal void WriteExcelColumns(IXLRow XRow, SheetFormat LayoutFormat)
        {
            ColumnFormat? Col = LayoutFormat.Column(XLItemID)!;
            XRow.Cell(Col.ColumnNumber).Value = ItemID;
            Col = LayoutFormat.Column(XLYPos)!;
            XRow.Cell(Col.ColumnNumber).Value = YPos.ToString();
            Col = LayoutFormat.Column(XLXPos)!;
            XRow.Cell(Col.ColumnNumber).Value = XPos.ToString();
            Col = LayoutFormat.Column(XLWidth)!;
            XRow.Cell(Col.ColumnNumber).Value = Width.ToString();
            Col = LayoutFormat.Column(XLHeight)!;
            XRow.Cell(Col.ColumnNumber).Value = Height.ToString();
            Col = LayoutFormat.Column(XLFont)!;
            XRow.Cell(Col.ColumnNumber).Value = Font;
        }
        internal static void AddSheetColumns(SheetFormat LayoutFormat)
        {
            LayoutFormat.Add(new NameColumn(1, XLItemID, 50, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsRequired));
            LayoutFormat.Add(new NumberColumn(2, XLYPos, 5, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsRequired));
            LayoutFormat.Add(new NumberColumn(3, XLXPos, 5, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsRequired));
            LayoutFormat.Add(new NumberColumn(4, XLWidth, 5, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsOptional));
            LayoutFormat.Add(new NumberColumn(5, XLHeight, 5, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsOptional));
            LayoutFormat.Add(new TextColumn(6, XLFont, 50, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsOptional));

        }


    }
}
