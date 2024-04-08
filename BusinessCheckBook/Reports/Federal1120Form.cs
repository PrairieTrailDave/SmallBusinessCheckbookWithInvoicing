//**********************************************************************
//
//          Copyright © 2023 Prairie Trail Software, Inc.
//
//**********************************************************************

using BusinessCheckBook.DataStore;
using CsvHelper;
using System.Data;
using System.Globalization;

namespace BusinessCheckBook.Reports
{
    public partial class Federal1120Form : Form
    {
        public class ReportLine
        {
            public string WhatWhen { get; set; } = string.Empty;
            public string TransactionID { get; set; } = string.Empty;
            public string ToWhom { get; set; } = string.Empty;
            public string Account { get; set;} = string.Empty;
            public string Amount { get; set; } = string.Empty;
        }


        // local variables

        MyCheckbook ActiveBook = new();
        List<ReportLine> TaxForm1120Report = new();
        //DateTime StartDate;  at present always assume last year

        public Federal1120Form()
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

            TaxForm1120Report = new();

            // get all the accounts

            List<Account> AccountList = ActiveBook.CurrentAccounts.GetCurrentList();

            foreach (string FormField in Fed1120.F1120Fields)
            {

                // go through all the transactions 
                // and find those amounts that are for this field

                RL = new ReportLine()
                {
                    WhatWhen = FormField
                };

                if ((FormField == Fed1120.OfficerCompensation)
                    || (FormField == Fed1120.SalariesAndWages))
                {
                    RL.ToWhom = "Pull from payroll program";
                    TaxForm1120Report.Add(RL);
                }
                else
                {
                    decimal FieldTotal = 0.00M;
                    TaxForm1120Report.Add(RL);
                    foreach (LedgerEntry LE in PriorYearTransactions)
                    {
                        string EntryAccount = LE.GetPrimaryAccount();
                        Account? TAccount;
                        if (EntryAccount == "Split")
                            TAccount = new Account()
                            {
                                Fed1120Mapping = ""
                            };
                        else
                        {
                            TAccount = (from tcc in AccountList
                                        where tcc.Name == EntryAccount
                                        select tcc).FirstOrDefault();
                        }
                        if (TAccount != null)
                        {
                            if (TAccount.Fed1120Mapping == FormField)
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
                                TaxForm1120Report.Add(RL);
                            }
                            else
                            {
                                if (LE.SubAccounts.Count > 0)
                                {
                                    foreach (var SubAcc in LE.SubAccounts)
                                    {
                                        EntryAccount = SubAcc.AccountName;

                                        TAccount = (from tcc in AccountList
                                                    where tcc.Name == EntryAccount
                                                    select tcc).First();
                                        if (TAccount.Fed1120Mapping == FormField)
                                        {
                                            RL = new ReportLine()
                                            {
                                                WhatWhen = LE.When.ToShortDateString(),
                                                TransactionID = LE.CheckNumber,
                                                ToWhom = LE.ToWhom,
                                                Account = (SubAcc.AccountName.Length > 0 ? (SubAcc.Memo.Length > 0 ? SubAcc.AccountName + ":" + SubAcc.Memo : SubAcc.AccountName) : LE.ToWhom),
                                                Amount = SubAcc.Amount.ToString("0.00")
                                            };
                                            TaxForm1120Report.Add(RL);
                                            FieldTotal += SubAcc.Amount;
                                        }
                                    }

                                }
                            }
                        }
                    }
                    // add subtotal line
                    if (FieldTotal > 0)
                    {
                        RL = new ReportLine()
                        {
                            WhatWhen = FormField,
                            Amount = FieldTotal.ToString("0.00")
                        };
                        TaxForm1120Report.Add(RL);
                    }
                }
            }

            // want a section of items not found above.
            RL = new ReportLine()
            {
                WhatWhen = "Items not properly categorized"
            };
            TaxForm1120Report.Add(RL);

            foreach (LedgerEntry LE in PriorYearTransactions)
            {
                string EntryAccount = LE.GetPrimaryAccount();
                Account? TAccount;
                if (EntryAccount == "Split")
                    TAccount = new Account()
                    {
                        Fed1120Mapping = ""
                    };
                else
                {
                    TAccount = (from tcc in AccountList
                                where tcc.Name == EntryAccount
                                select tcc).FirstOrDefault();
                }
                if (TAccount == null)
                {
                    RL = new ReportLine()
                    {
                        WhatWhen = LE.When.ToShortDateString(),
                        TransactionID = LE.CheckNumber,
                        ToWhom = LE.ToWhom
                    };
                    TaxForm1120Report.Add(RL);
                }
            }

            ReportDataGridView.DataSource = TaxForm1120Report;
            ReportDataGridView.AutoResizeColumn(2);
        }
        private void SaveReportFile(string FileName)
        {
            using (StreamWriter csvWriter = new StreamWriter(FileName, false))
            {
                using (var csvFile = new CsvWriter(csvWriter, CultureInfo.InvariantCulture))
                {
                    csvFile.WriteRecords(TaxForm1120Report);
                }
            }
        }

    }
}
