//**********************************************************************
//
//          Copyright © 2024 Prairie Trail Software, Inc.
//
//**********************************************************************

using BusinessCheckBook.Validation;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCheckBook.DataStore
{
    internal class YearlyBusinessActivity
    {
        public List<MonthlyBusinessActivity> MonthlyBusinessActivities { get; set; } = [];

        // which row is used for each month

        const int JanRow = 2;
        const int FebRow = 3;
        const int MarRow = 4;
        const int FirstQuarterRow = 5;
        const int AprRow = 7;
        const int MayRow = 8;
        const int JunRow = 9;
        const int SecondQuarterRow = 10;
        const int HalfYearRow = 11;
        const int JulRow = 13;
        const int AugRow = 14;
        const int SepRow = 15;
        const int ThirdQuarterRow = 16;
        const int NineMonthTotalRow = 17;
        const int OctRow = 19;
        const int NovRow = 20;
        const int DecRow = 21;
        const int FourthQuarterRow = 22;
        const int DoubleUnderlineRow = 23;
        const int YearlyTotalRow = 24;




        public YearlyBusinessActivity() { }




        internal static bool ValidateColumnHeaders(int LastColumn, IXLWorksheet BusinessHistoryWorksheet, SheetFormat YearlyHistoryFormat, out string ErrorMessage, out int BusinessActivityColumnCount)
        {
            return MonthlyBusinessActivity.ValidateMonthlyActivityHeader(BusinessHistoryWorksheet, LastColumn, YearlyHistoryFormat, out ErrorMessage, out BusinessActivityColumnCount);
        }

        internal static bool ValidateExcelColumns(ref int NextColumn, IXLWorksheet BusinessHistoryWorksheet, SheetFormat SubColumnsFormat, out string ErrorMessage)
        {
            if (!ValidateExcelRowColumns(JanRow, NextColumn, BusinessHistoryWorksheet, SubColumnsFormat, out ErrorMessage)) return false;
            if (!ValidateExcelRowColumns(FebRow, NextColumn, BusinessHistoryWorksheet, SubColumnsFormat, out ErrorMessage)) return false;
            if (!ValidateExcelRowColumns(MarRow, NextColumn, BusinessHistoryWorksheet, SubColumnsFormat, out ErrorMessage)) return false;
            if (!ValidateExcelRowColumns(AprRow, NextColumn, BusinessHistoryWorksheet, SubColumnsFormat, out ErrorMessage)) return false;
            if (!ValidateExcelRowColumns(MayRow, NextColumn, BusinessHistoryWorksheet, SubColumnsFormat, out ErrorMessage)) return false;
            if (!ValidateExcelRowColumns(JunRow, NextColumn, BusinessHistoryWorksheet, SubColumnsFormat, out ErrorMessage)) return false;
            if (!ValidateExcelRowColumns(JulRow, NextColumn, BusinessHistoryWorksheet, SubColumnsFormat, out ErrorMessage)) return false;
            if (!ValidateExcelRowColumns(AugRow, NextColumn, BusinessHistoryWorksheet, SubColumnsFormat, out ErrorMessage)) return false;
            if (!ValidateExcelRowColumns(SepRow, NextColumn, BusinessHistoryWorksheet, SubColumnsFormat, out ErrorMessage)) return false;
            if (!ValidateExcelRowColumns(OctRow, NextColumn, BusinessHistoryWorksheet, SubColumnsFormat, out ErrorMessage)) return false;
            if (!ValidateExcelRowColumns(NovRow, NextColumn, BusinessHistoryWorksheet, SubColumnsFormat, out ErrorMessage)) return false;
            if (!ValidateExcelRowColumns(DecRow, NextColumn, BusinessHistoryWorksheet, SubColumnsFormat, out ErrorMessage)) return false;

            NextColumn += MonthlyBusinessActivity.ColumnsUsed();
            return true;
        }

        private static bool ValidateExcelRowColumns(int Month, int NextColumn, IXLWorksheet BusinessHistoryWorksheet, SheetFormat SubColumnsFormat, out string ErrorMessage)
        {
            IXLRow MonthlyRow = BusinessHistoryWorksheet.Row(Month);

            return MonthlyBusinessActivity.ValidateExcelColumns(MonthlyRow, SubColumnsFormat, out ErrorMessage);

        }


        public int ReadMonthlyActivities (IXLWorksheet BusinessHistoryWorksheet, SheetFormat BusinessHistoryFormat)
        {
            MonthlyBusinessActivities.Clear();

            int ColumnsParsed = ParseSingleMonth(JanRow, BusinessHistoryWorksheet, BusinessHistoryFormat);
            ParseSingleMonth(FebRow, BusinessHistoryWorksheet, BusinessHistoryFormat);
            ParseSingleMonth(MarRow, BusinessHistoryWorksheet, BusinessHistoryFormat);
            ParseSingleMonth(AprRow, BusinessHistoryWorksheet, BusinessHistoryFormat);
            ParseSingleMonth(MayRow, BusinessHistoryWorksheet, BusinessHistoryFormat);
            ParseSingleMonth(JunRow, BusinessHistoryWorksheet, BusinessHistoryFormat);
            ParseSingleMonth(JulRow, BusinessHistoryWorksheet, BusinessHistoryFormat);
            ParseSingleMonth(AugRow, BusinessHistoryWorksheet, BusinessHistoryFormat);
            ParseSingleMonth(SepRow, BusinessHistoryWorksheet, BusinessHistoryFormat);
            ParseSingleMonth(OctRow, BusinessHistoryWorksheet, BusinessHistoryFormat);
            ParseSingleMonth(NovRow, BusinessHistoryWorksheet, BusinessHistoryFormat);
            ParseSingleMonth(DecRow, BusinessHistoryWorksheet, BusinessHistoryFormat);

            return ColumnsParsed;
        }
        int ParseSingleMonth(int Month, IXLWorksheet BusinessHistoryWorksheet, SheetFormat BusinessHistoryFormat)
        {
            IXLRow MonthlyRow = BusinessHistoryWorksheet.Row(Month);
            MonthlyBusinessActivity MBA = new();
            int ColumnsParsed = MBA.ParseExcelColumns(MonthlyRow, BusinessHistoryFormat);
            MonthlyBusinessActivities.Add(MBA);
            return ColumnsParsed;
        }



        public bool WriteYearlyActivity(IXLWorksheet BusinessHistoryWorksheet, SheetFormat YearlyFormat)
        {
            MonthlyBusinessActivity ThreeMonthTotal = new();
            MonthlyBusinessActivity SixMonthTotal = new();
            MonthlyBusinessActivity NineMonthTotal = new();
            MonthlyBusinessActivity YearlyTotal = new();

            MonthlyBusinessActivity.WriteHeader(BusinessHistoryWorksheet, YearlyFormat);
            IXLRow MonthlyRow = BusinessHistoryWorksheet.Row(JanRow);
            MonthlyBusinessActivity MBA = MonthlyBusinessActivities[0];
            MBA.WriteExcelRow(MonthlyRow, YearlyFormat);
            ThreeMonthTotal.AddInvoicesAndCash(MBA);
            SixMonthTotal.AddInvoicesAndCash(MBA);
            NineMonthTotal.AddInvoicesAndCash(MBA);
            YearlyTotal.AddInvoicesAndCash(MBA);

            MonthlyRow = BusinessHistoryWorksheet.Row(FebRow);
            MBA = MonthlyBusinessActivities[1];
            MBA.WriteExcelRow(MonthlyRow, YearlyFormat);
            ThreeMonthTotal.AddInvoicesAndCash(MBA);
            SixMonthTotal.AddInvoicesAndCash(MBA);
            NineMonthTotal.AddInvoicesAndCash(MBA);
            YearlyTotal.AddInvoicesAndCash(MBA);

            MonthlyRow = BusinessHistoryWorksheet.Row(MarRow);
            MBA = MonthlyBusinessActivities[2];
            MBA.WriteExcelRow(MonthlyRow, YearlyFormat);
            MBA.AddUnderline(MonthlyRow, YearlyFormat);
            ThreeMonthTotal.AddInvoicesAndCash(MBA);
            SixMonthTotal.AddInvoicesAndCash(MBA);
            NineMonthTotal.AddInvoicesAndCash(MBA);
            YearlyTotal.AddInvoicesAndCash(MBA);

            MonthlyRow = BusinessHistoryWorksheet.Row(FirstQuarterRow);
            ThreeMonthTotal.WriteExcelRow(MonthlyRow, YearlyFormat);

            ThreeMonthTotal = new();

            MonthlyRow = BusinessHistoryWorksheet.Row(AprRow);
            MBA = MonthlyBusinessActivities[3];
            MBA.WriteExcelRow(MonthlyRow, YearlyFormat);
            ThreeMonthTotal.AddInvoicesAndCash(MBA);
            SixMonthTotal.AddInvoicesAndCash(MBA);
            NineMonthTotal.AddInvoicesAndCash(MBA);
            YearlyTotal.AddInvoicesAndCash(MBA);

            MonthlyRow = BusinessHistoryWorksheet.Row(MayRow);
            MBA = MonthlyBusinessActivities[4];
            MBA.WriteExcelRow(MonthlyRow, YearlyFormat);
            ThreeMonthTotal.AddInvoicesAndCash(MBA);
            SixMonthTotal.AddInvoicesAndCash(MBA);
            NineMonthTotal.AddInvoicesAndCash(MBA);
            YearlyTotal.AddInvoicesAndCash(MBA);

            MonthlyRow = BusinessHistoryWorksheet.Row(JunRow);
            MBA = MonthlyBusinessActivities[5];
            MBA.WriteExcelRow(MonthlyRow, YearlyFormat);
            MBA.AddUnderline(MonthlyRow, YearlyFormat);
            ThreeMonthTotal.AddInvoicesAndCash(MBA);
            SixMonthTotal.AddInvoicesAndCash(MBA);
            NineMonthTotal.AddInvoicesAndCash(MBA);
            YearlyTotal.AddInvoicesAndCash(MBA);

            MonthlyRow = BusinessHistoryWorksheet.Row(SecondQuarterRow);
            ThreeMonthTotal.WriteExcelRow(MonthlyRow, YearlyFormat);
            MonthlyRow = BusinessHistoryWorksheet.Row(HalfYearRow);
            SixMonthTotal.WriteExcelRow(MonthlyRow, YearlyFormat);

            ThreeMonthTotal = new();

            MonthlyRow = BusinessHistoryWorksheet.Row(JulRow);
            MBA = MonthlyBusinessActivities[6];
            MBA.WriteExcelRow(MonthlyRow, YearlyFormat);
            ThreeMonthTotal.AddInvoicesAndCash(MBA);
            NineMonthTotal.AddInvoicesAndCash(MBA);
            YearlyTotal.AddInvoicesAndCash(MBA);

            MonthlyRow = BusinessHistoryWorksheet.Row(AugRow);
            MBA = MonthlyBusinessActivities[7];
            MBA.WriteExcelRow(MonthlyRow, YearlyFormat);
            ThreeMonthTotal.AddInvoicesAndCash(MBA);
            NineMonthTotal.AddInvoicesAndCash(MBA);
            YearlyTotal.AddInvoicesAndCash(MBA);

            MonthlyRow = BusinessHistoryWorksheet.Row(SepRow);
            MBA = MonthlyBusinessActivities[8];
            MBA.WriteExcelRow(MonthlyRow, YearlyFormat);
            MBA.AddUnderline(MonthlyRow, YearlyFormat);
            ThreeMonthTotal.AddInvoicesAndCash(MBA);
            NineMonthTotal.AddInvoicesAndCash(MBA);
            YearlyTotal.AddInvoicesAndCash(MBA);

            MonthlyRow = BusinessHistoryWorksheet.Row(ThirdQuarterRow);
            ThreeMonthTotal.WriteExcelRow(MonthlyRow, YearlyFormat);
            MonthlyRow = BusinessHistoryWorksheet.Row(NineMonthTotalRow);
            NineMonthTotal.WriteExcelRow(MonthlyRow, YearlyFormat);

            ThreeMonthTotal = new();

            MonthlyRow = BusinessHistoryWorksheet.Row(OctRow);
            MBA = MonthlyBusinessActivities[9];
            MBA.WriteExcelRow(MonthlyRow, YearlyFormat);
            ThreeMonthTotal.AddInvoicesAndCash(MBA);
            YearlyTotal.AddInvoicesAndCash(MBA);

            MonthlyRow = BusinessHistoryWorksheet.Row(NovRow);
            MBA = MonthlyBusinessActivities[10];
            MBA.WriteExcelRow(MonthlyRow, YearlyFormat);
            ThreeMonthTotal.AddInvoicesAndCash(MBA);
            YearlyTotal.AddInvoicesAndCash(MBA);

            MonthlyRow = BusinessHistoryWorksheet.Row(DecRow);
            MBA = MonthlyBusinessActivities[11];
            MBA.WriteExcelRow(MonthlyRow, YearlyFormat);
            MBA.AddUnderline(MonthlyRow, YearlyFormat);
            ThreeMonthTotal.AddInvoicesAndCash(MBA);
            YearlyTotal.AddInvoicesAndCash(MBA);

            MonthlyRow = BusinessHistoryWorksheet.Row(FourthQuarterRow);
            ThreeMonthTotal.WriteExcelRow(MonthlyRow, YearlyFormat);
            MonthlyRow = BusinessHistoryWorksheet.Row(DoubleUnderlineRow);
            YearlyTotal.AddDoubleUnderLine(MonthlyRow, YearlyFormat);
            MonthlyRow = BusinessHistoryWorksheet.Row(YearlyTotalRow);
            YearlyTotal.WriteExcelRow(MonthlyRow, YearlyFormat);




            return true;
        }


        internal static void AddSheetColumns(SheetFormat BusinessHistoryFormat)
        {

            // add a breakdown so that we can do some validation

            BusinessHistoryFormat.SubFormats = [new SheetFormat()];
            MonthlyBusinessActivity.AddSheetColumns(BusinessHistoryFormat.SubFormats[0], 0);
        }
        internal static void AddAnotherSheetColumn(SheetFormat BusinessHistoryFormat)
        {
            // find the column at which to add the sheet columns

            int newcolumn = (from Acolumnformat in
                                (from Asubformat in BusinessHistoryFormat.SubFormats
                                 select Asubformat.ColumnFormats.Last())
                             select Acolumnformat.ColumnNumber).Max();
                              ;

            SheetFormat NewSubFormat = new();
            MonthlyBusinessActivity.AddSheetColumns(NewSubFormat, newcolumn);
            BusinessHistoryFormat.SubFormats.Add(NewSubFormat);
        }
    }
}
