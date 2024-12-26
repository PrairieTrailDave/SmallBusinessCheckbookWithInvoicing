//**********************************************************************
//
//          Copyright © 2023 Prairie Trail Software, Inc.
//
//**********************************************************************

using System.Data;
using BusinessCheckBook.DataStore;

namespace BusinessCheckBook
{
    public partial class AddTransactionForm : Form
    {
        // variables filled from the main window
        public MyCheckbook ActiveBook { get; set; } = new();
        public decimal PriorBalance { get; set; }
        public int LastCheckNumber { get; set; }

        // variable sent back to the main window
        public LedgerEntry TEntry { get; set; } = new();
        public bool NewEntry { get; set; }

        // Internal Variables

        decimal TransactionDebit = 0.00M;
        decimal TransactionCredit = 0.00M;
        decimal DetailSubTotal = 0.00M;

        int CurrentDetailRow;
        readonly int scrollLen = 4;

        public AddTransactionForm()
        {
            InitializeComponent();
            TransactionDateTimePicker.Format = DateTimePickerFormat.Custom;
        }
        internal void SetUp(MyCheckbook activeBook, decimal currentBalance, int lastCheckNumber)
        {
            ActiveBook = activeBook;
            PriorBalance = currentBalance;
            LastCheckNumber = lastCheckNumber;
        }


        private void AddTransactionForm_Activated(object sender, EventArgs e)
        {


        }


        private void AddTransactionForm_Shown(object sender, EventArgs e)
        {
            // clear out what we are to return
            TEntry = new LedgerEntry();
            NewEntry = false;
            TransactionCredit = 0.00M;
            TransactionDebit = 0.00M;

            // load the forms 

            PriorBalance = ActiveBook.CurrentTransactionLedger.GetCurrentBalance();
            LastCheckNumber = ActiveBook.CurrentTransactionLedger.GetLastCheckNumber();
            TransactionDateTimePicker.Value = DateTime.Now;
            PriorBalanceTextBox.Text = PriorBalance.ToString("C");
            List<string> Categories = ActiveBook.CurrentAccounts.GetListOfAccounts();
            CategoriesComboBox.DataSource = Categories;
            CategoryListBox.Items.Clear();
            foreach (string cat in Categories)
                CategoryListBox.Items.Add(cat);

            CurrentDetailRow = -1;
        }

        private void CheckNumberTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '+')
            {
                CheckNumberTextBox.Text = (LastCheckNumber + 1).ToString();
                e.Handled = true;
            }
        }


        private void ToWhomTextBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                if (MatchingListBox.Visible)
                    MatchingListBox_Click(MatchingListBox, e);
            }
        }

        private void ToWhomTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            List<string> PossibleNames = ActiveBook.ToPayTo.GetMatchingPayToNames(ToWhomTextBox.Text + e.KeyChar);
            if (PossibleNames.Count > 0)
            {
                MatchingListBox.DataSource = PossibleNames;
                MatchingListBox.Visible = true;
            }
            else
                MatchingListBox.Visible = false;
        }

        private void MatchingListBox_Click(object sender, EventArgs e)
        {
            int ListRow = MatchingListBox.SelectedIndex;
            string MatchedName = (string)MatchingListBox.SelectedItem!;
            ToWhomTextBox.Text = MatchedName;
            MatchingListBox.Visible = false;

            // find the last transaction to this payee and autofill the form 

            LedgerEntry? LastTransaction = (from ch in ActiveBook.CurrentTransactionLedger.GetCurrentList()
                                            where ch.ToWhom == MatchedName
                                            //   && ch.When.AddYears(1) > DateTime.Now
                                            orderby ch.When descending
                                            select ch).FirstOrDefault();
            if (LastTransaction != null)
            {
                CheckAmountTextBox.Text = LastTransaction.Debit.ToString();
                DepositTextBox.Text = LastTransaction.Credit.ToString();
                CategoriesComboBox.Text = LastTransaction.Account;
                if (LastTransaction.SubAccounts != null)
                {
                    if (LastTransaction.SubAccounts.Count > 0)
                    {
                        int subi = 0;
                        DetailSubTotal = 0.00M;
                        List<LedgerEntryBreakdown> tSubAccounts = new();

                        while (subi < LastTransaction.SubAccounts.Count)
                        {
                            LedgerEntryBreakdown tEntry = new()
                            {
                                AccountName = LastTransaction.SubAccounts[subi].AccountName,
                                Memo = LastTransaction.SubAccounts[subi].Memo,
                                Amount = 0.00M - LastTransaction.SubAccounts[subi].Amount
                            };
                            tSubAccounts.Add(tEntry);
                            subi++;
                            DetailSubTotal = DetailSubTotal + tEntry.Amount;
                        }
                        while (subi++ < 40)
                        {
                            tSubAccounts.Add(new LedgerEntryBreakdown());
                        }
                        DetailDataGridView.DataSource = tSubAccounts;
                        DetailDataGridView.AutoResizeColumn(0);
                        DetailDataGridView.Columns["Memo"].Width = 350;
                        DetailDataGridView.Visible = true;

                        DetailTotalLabel.Visible = true;
                        DetailTotalTextBox.Visible = true;
                        DetailTotalTextBox.ReadOnly = true;
                        DetailTotalTextBox.Text = DetailSubTotal.ToString();
                    }
                }
            }
            CheckAmountTextBox.Focus();
        }

        private void CheckAmountTextBox_Leave(object sender, EventArgs e)
        {
            if (CheckAmountTextBox.Text.Length > 0)
            {
                string sAmount = CheckAmountTextBox.Text;
                if (sAmount.Length > 1)
                    if (sAmount[0] == '$') sAmount = sAmount[1..];
                if (Decimal.TryParse(sAmount, out _))
                {
                    decimal Amount = Decimal.Parse(sAmount);
                    TransactionDebit = Amount;
                    CheckAmountTextBox.Text = Amount.ToString("0.00");
                    CurrentBalanceTextBox.Text = (PriorBalance - Amount).ToString("C");
                    if (Amount > 0.00M)
                    {
                        DepositTextBox.Text = "";
                    }
                }
            }
        }

        private void DepositTextBox_Leave(object sender, EventArgs e)
        {
            string sDeposit = DepositTextBox.Text;
            if (sDeposit.Length > 1)
                if (sDeposit[0] == '$') sDeposit = sDeposit[1..];
            if (Decimal.TryParse(sDeposit, out _))
            {
                decimal Amount = Decimal.Parse(sDeposit);
                TransactionCredit = Amount;
                DepositTextBox.Text = Amount.ToString("0.00");
                CurrentBalanceTextBox.Text = (PriorBalance + Amount).ToString("C");
            }
        }

        private void CheckAmountTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Char.IsDigit(e.KeyChar)) ||
                (e.KeyChar == '.') ||
                (e.KeyChar == (char)Keys.Back))
            {
                if (DepositTextBox.Text.Length > 0)
                    DepositTextBox.Text = "";
                return;
            }
            e.Handled = true;
        }

        private void DepositTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Char.IsDigit(e.KeyChar)) ||
                (e.KeyChar == '.') ||
                (e.KeyChar == (char)Keys.Back))
            {
                if (CheckAmountTextBox.Text.Length > 0)
                    CheckAmountTextBox.Text = "";
                return;
            }
            e.Handled = true;
        }

        // methods for dealing with a split transaction - show the detail grid

        private void SplitCategoryButton_Click(object sender, EventArgs e)
        {
            CategoriesComboBox.Text = "Split";
            List<LedgerEntryBreakdown> TBreakdown = new();
            for (int i = 0; i < 40; i++)
                TBreakdown.Add(new LedgerEntryBreakdown());
            DetailDataGridView.DataSource = TBreakdown;
            DetailDataGridView.AutoResizeColumn(0);
            DetailDataGridView.Columns["Memo"].Width = 350;
            DetailDataGridView.EditMode = DataGridViewEditMode.EditOnEnter;
            DetailDataGridView.Visible = true;
            ShowCurrentSubTotal();
        }

        private void DetailDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            CurrentDetailRow = e.RowIndex;

            // then show the input panel

            if ((String.IsNullOrEmpty(DetailDataGridView.Rows[CurrentDetailRow].Cells["AccountName"].Value.ToString())) &&
                (String.IsNullOrEmpty(DetailDataGridView.Rows[CurrentDetailRow].Cells["Memo"].Value.ToString())) &&
                ((decimal)DetailDataGridView.Rows[CurrentDetailRow].Cells["Amount"].Value == 0.00M))
                ClearItemPanel();
            else
            {
                CategoryListBox.SelectedItem = DetailDataGridView.Rows[CurrentDetailRow].Cells["AccountName"].Value.ToString();
                ItemNotesTextBox.Text = DetailDataGridView.Rows[CurrentDetailRow].Cells["Memo"].Value.ToString();
                ItemAmountTextBox.Text = DetailDataGridView.Rows[CurrentDetailRow].Cells["Amount"].Value.ToString();
            }


            // figure out where to show the detail panel

            int detailTop;
            if (DetailDataGridView.FirstDisplayedScrollingRowIndex > scrollLen)
                detailTop = CurrentDetailRow + 2 - DetailDataGridView.FirstDisplayedScrollingRowIndex;
            else
                detailTop = CurrentDetailRow + 2;

            DetailInputPanel.Location = new Point(DetailDataGridView.Location.X,
                DetailDataGridView.Top + (detailTop) * DetailDataGridView.Rows[CurrentDetailRow].Height);
            DetailInputPanel.Visible = true;
            DetailInputPanel.Controls["CategoryListBox"]!.Focus();
        }


        // methods for dealing with the specific item subpanel

        private void ItemClearButton_Click(object sender, EventArgs e)
        {
            ClearItemPanel();
        }
        private void ItemCancelButton_Click(object sender, EventArgs e)
        {
            DetailInputPanel.Visible = false;
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            // delete this item 

            DataGridViewRow tRow = DetailDataGridView.Rows[CurrentDetailRow];
            tRow.Cells["AccountName"].Value = "";
            tRow.Cells["Memo"].Value = "";
            tRow.Cells["Amount"].Value = 0.00M;


            // move all the subsequent items up
            int LastRow = DetailDataGridView.Rows.Count - 1;
            if (LastRow > 1)
            {
                int MoveRow = CurrentDetailRow + 1;
                while (MoveRow < LastRow)
                {
                    DataGridViewRow MRow = DetailDataGridView.Rows[MoveRow];
                    tRow = DetailDataGridView.Rows[MoveRow - 1];
                    tRow.Cells["AccountName"].Value = MRow.Cells["AccountName"].Value;
                    tRow.Cells["Memo"].Value = MRow.Cells["Memo"].Value;
                    tRow.Cells["Amount"].Value = MRow.Cells["Amount"].Value;

                    MoveRow++;
                }

            }
            // clear last row
            tRow = DetailDataGridView.Rows[LastRow];
            tRow.Cells["AccountName"].Value = "";
            tRow.Cells["Memo"].Value = "";
            tRow.Cells["Amount"].Value = 0.00M;

            ShowCurrentSubTotal();

            // act as if we have clicked on the first cell of the new row
            var arg = new DataGridViewCellEventArgs(0, CurrentDetailRow);
            DetailDataGridView_CellClick(DetailDataGridView, arg);

        }


        private void ClearItemPanel()
        {
            CategoryListBox.SelectedItem = ActiveBook.CurrentAccounts.GetFirstExpenseAccount();
            ItemNotesTextBox.Text = "";
            ItemAmountTextBox.Text = "";
        }

        private void ItemAmountTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // only allow amounts to be entered
            if (!((e.KeyChar == '.') ||
                  (e.KeyChar == '-') ||
                  (Char.IsDigit(e.KeyChar)) ||
                  (e.KeyChar == ((char)Keys.Back))))
                e.Handled = true;
        }

        private void ItemAmountTextBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                // move the values into the datagrid view

                DataGridViewRow tRow = DetailDataGridView.Rows[CurrentDetailRow];
                tRow.Cells["AccountName"].Value = CategoryListBox.SelectedItem;
                tRow.Cells["Memo"].Value = ItemNotesTextBox.Text;
                tRow.Cells["Amount"].Value = 0.00M;
                if (Decimal.TryParse(ItemAmountTextBox.Text, out _))
                    tRow.Cells["Amount"].Value = Decimal.Parse(ItemAmountTextBox.Text);


                ShowCurrentSubTotal();

                // and hide the input panel

                DetailInputPanel.Visible = false;

                // move to the next row

                CurrentDetailRow++;
                if (CurrentDetailRow > scrollLen)
                    DetailDataGridView.FirstDisplayedScrollingRowIndex++;

                // if need to add rows to the grid

                if (CurrentDetailRow >= DetailDataGridView.Rows.Count)
                {

                }

                // act as if we have clicked on the first cell of the new row
                var arg = new DataGridViewCellEventArgs(0, CurrentDetailRow);
                DetailDataGridView_CellClick(DetailDataGridView, arg);

            }
        }

        private void ShowCurrentSubTotal()
        {
            DetailSubTotal = 0.00M;
            foreach (DataGridViewRow tRow in DetailDataGridView.Rows)
            {
                string amt = tRow.Cells["Amount"].Value.ToString()!;
                decimal subDecimal;
                if (Decimal.TryParse(amt, out subDecimal))
                    DetailSubTotal = DetailSubTotal + subDecimal;
            }
            DetailTotalTextBox.Text = DetailSubTotal.ToString();
            DetailTotalLabel.Visible = true;
            DetailTotalTextBox.Visible = true;
            DetailTotalTextBox.ReadOnly = true;
        }


        private void DoneButton_Click(object sender, EventArgs e)
        {
            decimal CheckAmount;
            decimal ResultingBalance = 0.00M;

            // someone can press the done key without entering a value 
            // because we used the value from the prior transaction

            if ((TransactionCredit == 0.00M) &&
                 TransactionDebit == 0.00M)
            {
                if (DepositTextBox.Text.Length > 0)
                {
                    if (Char.IsDigit(DepositTextBox.Text[0]))
                        TransactionCredit = decimal.Parse(DepositTextBox.Text);
                    else
                        TransactionCredit = decimal.Parse(DepositTextBox.Text[1..]);
                    ResultingBalance = PriorBalance + TransactionCredit;
                    CurrentBalanceTextBox.Text = ResultingBalance.ToString("C");
                }
                if (CheckAmountTextBox.Text.Length > 0)
                {
                    if (Char.IsDigit(CheckAmountTextBox.Text[0]))
                        TransactionDebit = decimal.Parse(CheckAmountTextBox.Text);
                    else
                        TransactionDebit = decimal.Parse(CheckAmountTextBox.Text[1..]);
                    ResultingBalance = PriorBalance + TransactionCredit - TransactionDebit;
                    CurrentBalanceTextBox.Text = ResultingBalance.ToString("C");
                }
            }
            else
            {
                ResultingBalance = PriorBalance + TransactionCredit - TransactionDebit;
                CurrentBalanceTextBox.Text = ResultingBalance.ToString("C");
            }

            if ((TransactionCredit > 0.00M) && (TransactionDebit > 0.00M))
            {
                MessageBox.Show("Transaction has both credit and debit, please choose which");
                return;
            }
            if (TransactionDebit == 0.00M)
                CheckAmount = TransactionCredit;
            else
                CheckAmount = 0.00M - TransactionDebit;

            List<LedgerEntryBreakdown>? TBreakdown = null;
            if (DetailDataGridView.Rows.Count > 0)
            {
                TBreakdown = new List<LedgerEntryBreakdown>();

                // build the sub accounts

                foreach (DataGridViewRow tRow in DetailDataGridView.Rows)
                {
                    if (tRow.Cells["AccountName"].Value != null)
                        if (tRow.Cells["AccountName"].Value.ToString()!.Length > 0)
                        {
                            LedgerEntryBreakdown tEntry = new();
                            tEntry.AccountName = tRow.Cells["AccountName"].Value.ToString()!;
                            tEntry.Memo = tRow.Cells["Memo"].Value.ToString()!;
                            string amt = tRow.Cells["Amount"].Value.ToString()!;
                            decimal EAmt;
                            if (Decimal.TryParse(amt, out EAmt))
                                tEntry.Amount = 0.00M - EAmt;
                            TBreakdown.Add(tEntry);
                        }
                }
            }

            TEntry = ActiveBook.CurrentTransactionLedger.CreateLedgerEntry
            (
                TransactionDateTimePicker.Value.Date,
                CheckNumberTextBox.Text,
                ToWhomTextBox.Text,
                TransactionDebit,
                TransactionCredit,
                ResultingBalance,
                CheckAmount,
                (string)CategoriesComboBox.SelectedItem!,
                TBreakdown
            );

            // say that a new entry is available 

            NewEntry = true;
            Close();
        }


    }
}
