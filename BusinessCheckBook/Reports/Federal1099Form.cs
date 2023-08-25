using BusinessCheckBook.DataStore;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusinessCheckBook
{
    public partial class Federal1099Form : Form
    {
        public class ReportLine
        {
            public string WhatWhen { get; set; } = string.Empty;
            public string TransactionID { get; set; } = string.Empty;
            public string ToWhom { get; set; } = string.Empty;
            public string Amount { get; set; } = string.Empty;
        }


        // local variables

        MyCheckbook ActiveBook = new();
        List<ReportLine> Form1099Report = new();
        //DateTime StartDate;  at present always assume last year

        public Federal1099Form()
        {
            InitializeComponent();
        }
        public void Setup(MyCheckbook activeBook)
        {
            ActiveBook = activeBook;
            int PriorYear = DateTime.Now.AddYears(-1).Year;
            ReportYearTextBox.Text = PriorYear.ToString();

            try
            {
                ShowReport(PriorYear);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred " + ex.Message);
            }
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            int PriorYear;
            if (Int32.TryParse(ReportYearTextBox.Text, out PriorYear))
            {
                try
                {
                    ShowReport(PriorYear);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Invalid prior year ");
            }

        }


        private void ExportButton_Click(object sender, EventArgs e)
        {
            saveExportFileDialog.Filter = "Report CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            if (saveExportFileDialog.ShowDialog() == DialogResult.OK)
            {
                SaveReportFile(saveExportFileDialog.FileName);
            }

        }



        private void ShowReport(int PriorYear)
        {
            TransactionLedger TLedger = ActiveBook.CurrentTransactionLedger;

            List<LedgerEntry> PriorYearTransactions = TLedger.GetLedgerForAYear(PriorYear);


            ReportLine RL = new();

            Form1099Report = new();

            // go through all the payees 
            foreach (PayTo TPayee in ActiveBook.ToPayTo.GetCurrentList())
            {
                // this payee is a 1099 reporting payee

                if (TPayee.Send1099)
                {
                    RL = new ReportLine();
                    decimal FieldTotal = 0.00M;
                    foreach (LedgerEntry LE in PriorYearTransactions)
                    {
                        if ((LE.ToWhom == TPayee.BusinessName)
                            || (LE.ToWhom == TPayee.AccountName))
                        {
                            RL = new ReportLine()
                            {
                                WhatWhen = LE.When.ToShortDateString(),
                                TransactionID = LE.CheckNumber,
                                ToWhom = LE.ToWhom
                            };
                            if (LE.Debit > 0.00M)
                            {
                                RL.Amount = LE.Debit.ToString("0.00");
                                FieldTotal += LE.Debit;
                            }
                            else
                            {
                                RL.Amount = LE.Credit.ToString("0.00");
                                FieldTotal += LE.Credit;
                            }
                            Form1099Report.Add(RL);
                        }
                    }
                    // add subtotal line
                    if (FieldTotal > 0)
                    {
                        RL = new ReportLine()
                        {
                            WhatWhen = TPayee.BusinessName,
                            TransactionID = TPayee.TaxID,
                            Amount = FieldTotal.ToString("0.00")
                        };
                        Form1099Report.Add(RL);
                    }

                }


            }
            ReportDataGridView.DataSource = Form1099Report;
            ReportDataGridView.AutoResizeColumn(2);
        }
        private void SaveReportFile(string FileName)
        {
            using (StreamWriter csvWriter = new StreamWriter(FileName, false))
            {
                using (var csvFile = new CsvWriter(csvWriter, CultureInfo.InvariantCulture))
                {
                    csvFile.WriteRecords(Form1099Report);
                }
            }
        }

    }
}
