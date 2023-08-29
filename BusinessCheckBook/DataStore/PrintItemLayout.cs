//**********************************************************************
//
//          Copyright © 2023 Prairie Trail Software, Inc.
//
//**********************************************************************

namespace BusinessCheckBook.DataStore
{
    internal class PrintItemLayout
    {
        internal enum Justification  { LeftJustify, RightJustify, Center };
        internal bool IfToPrintThisItem { get; set; } = false;
        internal string VariableNameToPrint { get; set; } = string.Empty;
        internal Font ItemFont { get; set; } = new Font("Arial", 10);
        internal int YPos { get; set; } = 0;
        internal int XPos { get; set; } = 0;
        internal int Width { get; set; } = 0;
        internal int Height { get; set; } = 0;
        internal Justification HowToJustify = Justification.LeftJustify;

    }
}
