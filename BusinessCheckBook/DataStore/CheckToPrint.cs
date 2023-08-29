//**********************************************************************
//
//          Copyright © 2023 Prairie Trail Software, Inc.
//
//**********************************************************************

namespace BusinessCheckBook.DataStore
{
    internal class CheckToPrint
    {
        internal string CheckNumber { get; set; } = string.Empty;
        internal string DateToPrint { get; set; } = string.Empty;
        internal string ToTheOrderOf { get; set; } = string.Empty;
        internal string AmountToPrint { get; set; } = string.Empty;
        internal string AmountWords { get; set; } = string.Empty;
        internal string BusinessName { get; set; } = string.Empty;
        internal string AddressLine1 { get; set; } = string.Empty;
        internal string AddressLine2 { get; set; } = string.Empty;
        internal string AddressLine3 { get; set; } = string.Empty;
        internal string AddressLine4 { get; set; } = string.Empty;
        internal string Memo { get; set; } = string.Empty;

        // breakdown
        internal List<LedgerEntryBreakdown> Breakdown = new();
    }
}