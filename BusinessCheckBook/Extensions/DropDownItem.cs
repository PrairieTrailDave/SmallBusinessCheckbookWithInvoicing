//**********************************************************************
//
//          Copyright © 2023 Prairie Trail Software, Inc.
//
//**********************************************************************


namespace BusinessCheckBook.Extensions
{
    public class DropDownItem
    {
        public string Text {  get; set; }
        public string Value { get; set; }
        public override string ToString() {  return  Text; }
        public DropDownItem(string text, string value) {  Text = text; Value = value; }
    }
}
