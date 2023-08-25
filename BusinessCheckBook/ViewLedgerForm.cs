using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessCheckBook.DataStore;

namespace BusinessCheckBook
{
    public partial class ViewLedgerForm : Form
    {
        // variables filled from the main window
        public MyCheckbook ActiveBook { get; set; } = new();

        public ViewLedgerForm()
        {
            InitializeComponent();

        }
        internal void SetUp(MyCheckbook activeBook)
        {
            ActiveBook = activeBook;
            DisplayCurrentLedger();
        }


        // button clicks

        private void AddTransactionButton_Click(object sender, EventArgs e)
        {
            AddTransactionForm ATF = new();
            decimal currentBalance = ActiveBook.CurrentTransactionLedger.GetCurrentBalance();
            int lastCheckNumber = ActiveBook.CurrentTransactionLedger.GetLastCheckNumber();
            ATF.SetUp(ActiveBook, currentBalance, lastCheckNumber);
            ATF.ShowDialog();
            if (ATF.NewEntry)
            {
                if (ATF.TEntry.When < ActiveBook.CurrentTransactionLedger.CurrentLedger[ActiveBook.CurrentTransactionLedger.CurrentLedger.Count - 1].When)
                {
                    ActiveBook.CurrentTransactionLedger.InsertTransaction(ATF.TEntry);
                }
                else
                    ActiveBook.CurrentTransactionLedger.CurrentLedger.Add(ATF.TEntry);

                DisplayCurrentLedger();
            }
        }
        private void EditTransactionButton_Click(object sender, EventArgs e)
        {
            int TransactionRowNumber = LedgerDataGridView.CurrentCell.RowIndex;
            AddTransactionForm ATF = new();
            LedgerEntry WhichEntryToEdit = ActiveBook.CurrentTransactionLedger.GetLedgerEntry(TransactionRowNumber);

            MessageBox.Show("This function is not working yet");

            decimal currentBalance = WhichEntryToEdit.Balance;
            int lastCheckNumber;
            Int32.TryParse(WhichEntryToEdit.CheckNumber, out lastCheckNumber);
            ATF.SetUp(ActiveBook, currentBalance, lastCheckNumber);
            ATF.TEntry = WhichEntryToEdit;
            ATF.ShowDialog();
            if (ATF.NewEntry)
            {
                // update current ledger entry

                DisplayCurrentLedger();
            }
        }

        private void DoneButton_Click(object sender, EventArgs e)
        {
            Close();
        }


        // support routines

        private void DisplayCurrentLedger ()
        {
            LedgerDataGridView.DataSource = null;
            LedgerDataGridView.DataSource = ActiveBook.CurrentTransactionLedger.GetCurrentList();
            LedgerDataGridView.Columns[0].Visible = false;
            LedgerDataGridView.AutoResizeColumns();
            if (LedgerDataGridView.RowCount > 0)
                LedgerDataGridView.FirstDisplayedScrollingRowIndex = LedgerDataGridView.RowCount - 1;
        }


    }
}
