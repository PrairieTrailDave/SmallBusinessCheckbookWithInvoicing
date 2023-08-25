using BusinessCheckBook.DataStore;
using CsvHelper;
using DocumentFormat.OpenXml.Vml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusinessCheckBook.Reports
{
    public partial class CompareReportForm : Form
    {
        public class ReportLine
        {
            public string Month { get; set; } = string.Empty;
            public string ThisYearsInvoice { get; set; } = string.Empty;
            public string ThisYearsCashIn { get; set; } = string.Empty;
            public string LastYearsInvoice { get; set; } = string.Empty;
            public string LastYearsCashIn { get; set; } = string.Empty;
        }


        // local variables

        MyCheckbook ActiveBook = new();
        List<ReportLine> ComparisonReport = new();
        //DateTime StartDate;
        //DateTime EndDate;
        public CompareReportForm()
        {
            InitializeComponent();
        }

        public void Setup(MyCheckbook activeBook)
        {
            ActiveBook = activeBook;
            int CurrentYear = DateTime.Now.Year;
            int PriorYear = DateTime.Now.AddYears(-1).Year;
            CurrentYearTextBox.Text = CurrentYear.ToString();
            PriorYearTextBox.Text = PriorYear.ToString();
            ShowReport(CurrentYear, PriorYear);
        }


        private void ShowReport(int CurrentYear, int PriorYear)
        {
            Invoices TInvoices = ActiveBook.CurrentInvoices;
            TransactionLedger TLedger = ActiveBook.CurrentTransactionLedger;

            List<Invoice> CurrentYearInvoices = TInvoices.GetInvoicesForAYear(CurrentYear);
            List<Invoice> PriorYearInvoices = TInvoices.GetInvoicesForAYear(PriorYear);
            List<LedgerEntry> CurrentYearTransactions = TLedger.GetLedgerForAYear(CurrentYear);
            List<LedgerEntry> PriorYearTransactions = TLedger.GetLedgerForAYear(PriorYear);


            ReportLine getMonthValues (int month)
            {
                ReportLine RL = new();
                //Jan
                RL.Month = DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(month);
                RL.ThisYearsInvoice = (from iv in CurrentYearInvoices
                                       where DateTime.Parse(iv.BillingDate).Month == month
                                       select iv.Total).Sum().ToString();
                RL.ThisYearsCashIn = (from CS in CurrentYearTransactions
                                      where CS.When.Month == month
                                      select CS.Credit).Sum().ToString();
                RL.LastYearsInvoice = (from iv in PriorYearInvoices
                                       where DateTime.Parse(iv.BillingDate).Month == month
                                       select iv.Total).Sum().ToString();
                RL.LastYearsCashIn = (from CS in PriorYearTransactions
                                      where CS.When.Month == month
                                      select CS.Credit).Sum().ToString();
                return RL;
            }


            ComparisonReport = new();
            ReportLine RL = getMonthValues(1);
            ComparisonReport.Add(RL);
            RL = getMonthValues(2);
            ComparisonReport.Add(RL);
            RL = getMonthValues(3);
            ComparisonReport.Add(RL);
            RL = getMonthValues(4);
            ComparisonReport.Add(RL);
            RL = getMonthValues(5);
            ComparisonReport.Add(RL);
            RL = getMonthValues(6);
            ComparisonReport.Add(RL);
            RL = getMonthValues(7);
            ComparisonReport.Add(RL);
            RL = getMonthValues(8);
            ComparisonReport.Add(RL);
            RL = getMonthValues(9);
            ComparisonReport.Add(RL);
            RL = getMonthValues(10);
            ComparisonReport.Add(RL);
            RL = getMonthValues(11);
            ComparisonReport.Add(RL);
            RL = getMonthValues(12);
            ComparisonReport.Add(RL);

            ReportDataGridView.DataSource = ComparisonReport;
            ReportDataGridView.AutoResizeColumns();
        }
        private void SaveReportFile(string FileName)
        {
            using (StreamWriter csvWriter = new StreamWriter(FileName, false))
            {
                using (var csvFile = new CsvWriter(csvWriter, CultureInfo.InvariantCulture))
                {
                    csvFile.WriteRecords(ComparisonReport);
                }
            }
        }
    }
}
