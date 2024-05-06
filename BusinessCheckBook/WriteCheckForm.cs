//**********************************************************************
//
//          Copyright © 2023 Prairie Trail Software, Inc.
//
//**********************************************************************

using System.Drawing.Printing;
using System.Drawing.Text;
using BusinessCheckBook.DataStore;
using DocumentFormat.OpenXml.Vml;

namespace BusinessCheckBook
{
    public partial class WriteCheckForm : Form
    {
        // variables filled from the main window
        public MyCheckbook ActiveBook { get; set; } = new();


        // Internal Variables

        PayTo? Payee { get; set; } = new();

        decimal DetailSubTotal = 0.00M;
        decimal CurrentLedgerBalance = 0.00M;

        private const int WordFillInLength = 150;

        CheckToPrint CurrentCheckToPrint = new();
        List<CheckToPrint> CheckBatch = new();
        CheckPrintLayout CurrentPrintLayout = new();

        int CategoryRow;
        int CurrentCheckNumber;
        int BatchCheckNum;

        private readonly string[] NumberWord =
        {
            "Zero",
            "One",
            "Two",
            "Three",
            "Four",
            "Five",
            "Six",
            "Seven",
            "Eight",
            "Nine",
            "Ten",
            "Eleven",
            "Twelve",
            "Thirteen",
            "Fourteen",
            "Fifteen",
            "Sixteen",
            "Seventeen",
            "Eighteen",
            "Nineteen"
        };
        private readonly string[] TenWords =
        {
            "",
            "Ten",
            "Twenty",
            "Thirty",
            "Forty",
            "Fifty",
            "Sixty",
            "Seventy",
            "Eighty",
            "Ninety"
        };
        private readonly string Hundred = "Hundred";
        private readonly string Thousand = "Thousand";
        private readonly string Million = "Million";

        public WriteCheckForm()
        {
            InitializeComponent();
            CategoryRow = 0;
        }

        // called from main menu

        internal void SetUp(MyCheckbook activeBook)
        {
            ActiveBook = activeBook;
            DateTimePicker.Text = DateTime.Now.ToShortDateString();
            WriteCompanyInformation();
            ClearOtherLabelsOnCheck();
            CreateSubCategoryList();
            CategoryListBox.Items.Clear();
            foreach (string CategoryName in ActiveBook.CurrentAccounts.GetListOfAccounts())
            {
                CategoryListBox.Items.Add(CategoryName);
            }
            CurrentCheckNumber = ActiveBook.CurrentTransactionLedger.GetLastCheckNumber() + 1;
            CheckNumberLabel.Text = CurrentCheckNumber.ToString();
            CurrentLedgerBalance = ActiveBook.GetCurrentBalance();
            CurrentBalanceLabel.Text = "$" + CurrentLedgerBalance.ToString("0.00");
        }

        // Button Clicks

        private void PrintCheckButton_Click(object sender, EventArgs e)
        {
            // before allowing a check to be printed, make sure that there is a check to print

            if ((ToWhomTextBox.Text.Length > 0) && (AmountTextBox.Text.Length > 0))
            {
                // check if the breakdown total matches the check amount
                decimal breakdownTotal = Decimal.Parse(DetailTotalTextBox.Text ?? "0.00");
                decimal checkTotal = Decimal.Parse(AmountTextBox.Text ?? "0.00");
                if (checkTotal == 0.00M)
                {
                    if (MessageBox.Show("You are trying to print a Zero value check. Are you sure?", "No Value", MessageBoxButtons.YesNo) != DialogResult.Yes)
                        return;
                }
                else
                {
                    if (checkTotal != breakdownTotal)
                    {
                        if (MessageBox.Show("The breakdown does not match the check amount. Are you sure you want to print this check?", "Doesn't Match", MessageBoxButtons.YesNo) != DialogResult.Yes)
                            return;
                    }
                }
                // Allow the user to choose the page range he or she would
                // like to print.
                PrintCheckDialog.AllowSomePages = true;

                // Show the help button.
                PrintCheckDialog.ShowHelp = true;

                // Set the Document property to the PrintDocument for 
                // which the PrintPage Event has been handled. To display the
                // dialog, either this property or the PrinterSettings property 
                // must be set 
                PrintCheckDialog.PrinterSettings = new();
                //PrintCheckDialog.Document = docToPrint;

                DialogResult result = PrintCheckDialog.ShowDialog();

                // If the result is OK then print the document.
                if (result == DialogResult.OK)
                {

                    try
                    {
                        CurrentCheckToPrint = new();
                        FillInCurrentCheckToPrint(CurrentCheckToPrint);
                        BuildLaser1PTFormat();


                        PrintDocument pd = new();
                        pd.PrintPage += new PrintPageEventHandler(this.Pd_PrintPage);
                        pd.Print();

                        SaveCheckInLedger(CurrentCheckToPrint);

                        // start a new check

                        StartNewCheck();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter a check before attempting to print.");
            }
        }

        private void AddToBatchButton_Click(object sender, EventArgs e)
        {
            // before allowing a check to be saved in the batch, make sure that there is a check ready to go

            if ((ToWhomTextBox.Text.Length > 0) && (AmountTextBox.Text.Length > 0))
            {
                // check if the breakdown total matches the check amount
                decimal breakdownTotal = Decimal.Parse(DetailTotalTextBox.Text ?? "0.00");
                decimal checkTotal = Decimal.Parse(AmountTextBox.Text ?? "0.00");
                if (checkTotal == 0.00M)
                {
                    MessageBox.Show("You are trying to save a Zero value check.", "No Value");
                    return;
                }
                else
                {
                    if (checkTotal != breakdownTotal)
                    {
                        MessageBox.Show("The breakdown does not match the check amount. Can't save this check", "Doesn't Match");
                        return;
                    }
                }
                CheckToPrint BatchCheck = new();
                FillInCurrentCheckToPrint(BatchCheck);
                CheckBatch.Add(BatchCheck);

                // start a new check

                StartNewCheck();
            }
            else
                MessageBox.Show("Please fill in the check before saving to batch");
        }

        private void PrintBatchButton_Click(object sender, EventArgs e)
        {
            if (CheckBatch.Count > 0)
            {
                // Allow the user to choose the page range he or she would
                // like to print.
                PrintCheckDialog.AllowSomePages = true;

                // Show the help button.
                PrintCheckDialog.ShowHelp = true;

                // Set the Document property to the PrintDocument for 
                // which the PrintPage Event has been handled. To display the
                // dialog, either this property or the PrinterSettings property 
                // must be set 
                PrintCheckDialog.PrinterSettings = new();
                //PrintCheckDialog.Document = docToPrint;

                DialogResult result = PrintCheckDialog.ShowDialog();

                // If the result is OK then print the document.
                if (result == DialogResult.OK)
                {

                    try
                    {
                        BatchCheckNum = 0;
                        BuildLaser1PTFormat();


                        PrintDocument pd = new();
                        pd.PrintPage += new PrintPageEventHandler(this.Pd_PrintBatch);
                        pd.Print();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
                MessageBox.Show("There are no checks to print");
        }

        private void ClearCheckButton_Click(object sender, EventArgs e)
        {
            ClearCheck();
        }

        private void DeleteDetailLineButton_Click(object sender, EventArgs e)
        {
            void ClearThisLine(int line)
            {
                CheckBreakdownDataGridView.Rows[line].Cells[0].Value = "";
                CheckBreakdownDataGridView.Rows[line].Cells[1].Value = "0.00";
                CheckBreakdownDataGridView.Rows[line].Cells[2].Value = "";
            }


            int ThisRow = CheckBreakdownDataGridView.CurrentRow.Index;
            if (ThisRow > -1)
            {
                int NextRow = ThisRow + 1;
                if (NextRow < CheckBreakdownDataGridView.Rows.Count)
                {
                    if (CheckBreakdownDataGridView.Rows[NextRow].Cells[1].Value != null)
                    {
                        if ((decimal)CheckBreakdownDataGridView.Rows[NextRow].Cells[1].Value == 0.00M)
                            ClearThisLine(ThisRow);
                        else
                        {
                            while (NextRow < CheckBreakdownDataGridView.Rows.Count)
                            {
                                CheckBreakdownDataGridView.Rows[NextRow - 1].Cells[0].Value = CheckBreakdownDataGridView.Rows[NextRow].Cells[0].Value;
                                CheckBreakdownDataGridView.Rows[NextRow - 1].Cells[1].Value = CheckBreakdownDataGridView.Rows[NextRow].Cells[1].Value;
                                CheckBreakdownDataGridView.Rows[NextRow - 1].Cells[2].Value = CheckBreakdownDataGridView.Rows[NextRow].Cells[2].Value;
                                NextRow++;
                            }
                            ClearThisLine(NextRow - 1);
                        }
                    }
                    else
                        ClearThisLine(ThisRow);
                }
                else
                    ClearThisLine(ThisRow);
            }

        }
        private void DoneButton_Click(object sender, EventArgs e)
        {
            if (CheckBatch.Count > 0)
            {
                if (MessageBox.Show("You still have checks to print in the batch. You will lose them if you leave. Are you sure?", "Still to Print", MessageBoxButtons.YesNo)
                    == DialogResult.Yes)
                    Close();
                return;
            }
            Close();
        }

        // Event Handler
        // The PrintPage event is raised for each page to be printed.
        private void Pd_PrintPage(object sender, PrintPageEventArgs ev)
        {

            // Print the check
            FormatCheck(CurrentCheckToPrint, CurrentPrintLayout, ev);

            ev.HasMorePages = false;
        }

        // The PrintPage event is raised for each page to be printed.
        private void Pd_PrintBatch(object sender, PrintPageEventArgs ev)
        {

            CurrentCheckToPrint = CheckBatch[BatchCheckNum++];

            // Print each check
            FormatCheck(CurrentCheckToPrint, CurrentPrintLayout, ev);

            SaveCheckInLedger(CurrentCheckToPrint);

            // If more lines exist, print another page.
            if (BatchCheckNum < CheckBatch.Count)
                ev.HasMorePages = true;
            else
            {
                ev.HasMorePages = false;
                CheckBatch = new();
                BatchCheckNum = 0;
            }
        }



        // Key Presses, etc.

        private void ToWhomTextBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                if (MatchingListBox.Visible)
                    MatchingListBox_Click(MatchingListBox, e);
                else
                {
                    GetPayTo(ToWhomTextBox.Text);
                }
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
            //int ListRow = MatchingListBox.SelectedIndex;
            string MatchedName = (string)MatchingListBox.SelectedItem!;
            MatchingListBox.Visible = false;

            GetPayTo(MatchedName);

            // find the last transaction to this payee and autofill the form 
            FillInFromLastTransaction(MatchedName);

            AmountTextBox.Focus();

        }

        private void CheckBreakdownDataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            int Col = e.ColumnIndex;
            if (Col == 0)
            {
                // show only when a check is being written
                if (ToWhomTextBox.Text.Length > 0)
                {
                    int Row = e.RowIndex;
                    string? CellContents = CheckBreakdownDataGridView.Rows[Row].Cells[0].Value.ToString();
                    if (CellContents != null) { CategoryListBox.SelectedValue = CellContents; }
                    CategoryListBox.Visible = true;
                    CategoryListBox.Focus();
                    CategoryRow = Row;
                }
                else
                    CategoryListBox.Visible = false;
            }
            else
                CategoryListBox.Visible = false;
            if (Col == 1 || Col == 2)
                CheckBreakdownDataGridView.BeginEdit(true);
        }

        // cell leave happens before the new value is put into the cell.
        //private void CheckBreakdownDataGridView_CellLeave(object sender, DataGridViewCellEventArgs e)


        private void CheckBreakdownDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int Col = e.ColumnIndex;
            int WhichBreakdown = e.RowIndex;
            if (WhichBreakdown < 0) return;
            if (WhichBreakdown >= CurrentCheckToPrint.Breakdown.Count) return;
            if (Col == 1)
            {
                UpdateBreakdownTotal();
                decimal LineAmount;
                if (Decimal.TryParse((string?)CheckBreakdownDataGridView.Rows[WhichBreakdown].Cells[1].Value.ToString() ?? "0.00", out LineAmount))
                    CurrentCheckToPrint.Breakdown[WhichBreakdown].Amount = LineAmount;
                else
                    CurrentCheckToPrint.Breakdown[WhichBreakdown].Amount = 0.00M;

            }
            if (CheckBreakdownDataGridView.Rows[WhichBreakdown].Cells[2].Value != null)
                CurrentCheckToPrint.Breakdown[WhichBreakdown].Memo = CheckBreakdownDataGridView.Rows[WhichBreakdown].Cells[2].Value.ToString()!.Trim() ?? "";
            else
                CurrentCheckToPrint.Breakdown[WhichBreakdown].Memo = "";
        }

        private void CategoryListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Account = CategoryListBox.SelectedItem!.ToString()!.Trim();
            if (Account.Contains(':'))
            {
                Account = Account.Split(':')[1];
            }
            CheckBreakdownDataGridView.Rows[CategoryRow].Cells[0].Value = Account;
            CurrentCheckToPrint.Breakdown[CategoryRow].AccountName = Account;
            CheckBreakdownDataGridView.CurrentCell = CheckBreakdownDataGridView.Rows[CategoryRow].Cells[1];
            CategoryListBox.Visible = false;
            CheckBreakdownDataGridView.InvalidateRow(CategoryRow);
            CheckBreakdownDataGridView.Update();
        }






        // support routines


        private void SaveCheckInLedger(CheckToPrint TCheck)
        {
            string Category;
            if (TCheck.Breakdown.Count > 1)
            {
                Category = "Split";
            }
            else
                Category = TCheck.Breakdown[0]?.AccountName ?? "Unknown";

            LedgerEntry NEntry = ActiveBook.CurrentTransactionLedger.CreateLedgerEntry
            (
                DateTime.Parse(TCheck.DateToPrint),
                TCheck.CheckNumber,
                TCheck.ToTheOrderOf,
                decimal.Parse(CleanAmount(TCheck.AmountToPrint)),
                0.00M,
                0.00M,   // the balance gets adjusted during insertion
                decimal.Parse(CleanAmount(TCheck.AmountToPrint)),
                Category,
                TCheck.Breakdown
            );

            // make sure the breakdown is stored

            NEntry.SubAccounts = new();
            foreach (LedgerEntryBreakdown LE in TCheck.Breakdown)
            {
                if (LE.AccountName.Length == 0 && LE.Amount == 0.00M) break;
                NEntry.SubAccounts.Add(LE);
            }

            ActiveBook.CurrentTransactionLedger.InsertTransaction(NEntry);
        }
        private void StartNewCheck()
        {
            CurrentCheckNumber++;
            ClearCheck();
        }

        private void ClearCheck()
        {
            ClearOtherLabelsOnCheck();
            CheckNumberLabel.Text = CurrentCheckNumber.ToString();
            DateTimePicker.Text = DateTime.Now.ToShortDateString();
            WriteCompanyInformation();
            CreateSubCategoryList();
            CurrentLedgerBalance = ActiveBook.GetCurrentBalance();
            CurrentBalanceLabel.Text = "$" + CurrentLedgerBalance.ToString("0.00");
        }

        private void WriteCompanyInformation()
        {
            CompanyNameLabel.Text = ActiveBook.CompanyInformation.GetParameter(CompanyParameters.ParmCompanyName.Name);
            CompanyAddressLabel.Text = ActiveBook.CompanyInformation.GetParameter(CompanyParameters.ParmCompanyAddr.Name);
            string Adr2 = ActiveBook.CompanyInformation.GetParameter(CompanyParameters.ParmCompanyAdr2.Name);
            string CityState = ActiveBook.CompanyInformation.GetParameter(CompanyParameters.ParmCompanyCity.Name)
                + ", " + ActiveBook.CompanyInformation.GetParameter(CompanyParameters.ParmCompanyState.Name)
                + " " + ActiveBook.CompanyInformation.GetParameter(CompanyParameters.ParmCompanyZip.Name);

            if (Adr2.Length > 0)
            {
                CompanyAddress2Label.Text = ActiveBook.CompanyInformation.GetParameter(CompanyParameters.ParmCompanyAdr2.Name);
                CompanyAddress3Label.Text = CityState;
                CompanyAddress4Label.Text = ActiveBook.CompanyInformation.GetParameter(CompanyParameters.ParmCompanyPhone.Name);
            }
            else
            {
                CompanyAddress2Label.Text = CityState;
                CompanyAddress3Label.Text = ActiveBook.CompanyInformation.GetParameter(CompanyParameters.ParmCompanyPhone.Name);
                CompanyAddress4Label.Text = "";
            }
        }

        private void ClearOtherLabelsOnCheck()
        {
            CheckNumberLabel.Text = "";
            ToWhomTextBox.Text = "";
            AmountTextBox.Text = "";
            AmountWordsLabel.Text = "";
            ToWhomNameLabel.Text = "";
            ToWhomAddress1Label.Text = "";
            ToWhomAddress2Label.Text = "";
            ToWhomAddress3Label.Text = "";
            MemoTextBox.Text = "";
            DetailTotalTextBox.Text = "";
        }

        private void CreateSubCategoryList()
        {
            ClearBreakdown(CurrentCheckToPrint);
            CheckBreakdownDataGridView.DataSource = CurrentCheckToPrint.Breakdown;
            CheckBreakdownDataGridView.Columns[0].Width = 300;
            CheckBreakdownDataGridView.Columns[1].Width = 150;
            CheckBreakdownDataGridView.Columns[2].Width = 550;
        }



        private void UpdateBreakdownTotal()
        {
            decimal breakdownTotal = 0.00M;
            foreach (DataGridViewRow Row in CheckBreakdownDataGridView.Rows)
            {
                decimal itemCost = Decimal.Parse(Row.Cells[1].Value.ToString() ?? "0.00");
                breakdownTotal += itemCost;
            }
            DetailTotalTextBox.Text = breakdownTotal.ToString("0.00");
        }




        private static void FormatCheck(CheckToPrint TCheck, CheckPrintLayout PrintLayoutToUse, PrintPageEventArgs ev)
        {
            float yPos;
            float xPos;
            //float leftMargin = ev.MarginBounds.Left;
            //float topMargin = ev.MarginBounds.Top;
            //float Height;
            Font printFont;


            // check part

            // Don't write the check number

            // write the date
            if (PrintLayoutToUse.PrintDate.IfToPrintThisItem)
            {
                yPos = PrintLayoutToUse.PrintDate.YPos;
                xPos = PrintLayoutToUse.PrintDate.XPos;
                printFont = PrintLayoutToUse.PrintDate.ItemFont;
                //Height = printFont.GetHeight(ev.Graphics!);
                ev.Graphics!.DrawString(TCheck.DateToPrint, printFont, Brushes.Black, xPos, yPos, new StringFormat());
            }
            // write the ToTheOrderOf
            if (PrintLayoutToUse.PrintToTheOrderOf.IfToPrintThisItem)
            {
                yPos = PrintLayoutToUse.PrintToTheOrderOf.YPos;
                xPos = PrintLayoutToUse.PrintToTheOrderOf.XPos;
                printFont = PrintLayoutToUse.PrintToTheOrderOf.ItemFont;
                ev.Graphics!.DrawString(TCheck.ToTheOrderOf, printFont, Brushes.Black, xPos, yPos, new StringFormat());
            }
            // write the amount field
            if (PrintLayoutToUse.PrintAmount.IfToPrintThisItem)
            {
                yPos = PrintLayoutToUse.PrintAmount.YPos;
                xPos = PrintLayoutToUse.PrintAmount.XPos;
                printFont = PrintLayoutToUse.PrintAmount.ItemFont;
                ev.Graphics!.DrawString(TCheck.AmountToPrint, printFont, Brushes.Black, xPos, yPos, new StringFormat());
            }

            // write the wording line
            if (PrintLayoutToUse.PrintAmountWords.IfToPrintThisItem)
            {
                yPos = PrintLayoutToUse.PrintAmountWords.YPos;
                xPos = PrintLayoutToUse.PrintAmountWords.XPos;
                printFont = PrintLayoutToUse.PrintAmountWords.ItemFont;
                RectangleF WordsRect = new();
                WordsRect.X = xPos;
                WordsRect.Y = yPos;
                WordsRect.Height = PrintLayoutToUse.PrintAmountWords.Height;
                WordsRect.Width = PrintLayoutToUse.PrintAmountWords.Width;
                StringFormat st = new();
                st.Trimming = StringTrimming.Character;
                ev.Graphics!.DrawString(TCheck.AmountWords, printFont, Brushes.Black, WordsRect, st);
            }
            // write the to whom on the first address line
            if (PrintLayoutToUse.PrintToWhom.IfToPrintThisItem)
            {
                yPos = PrintLayoutToUse.PrintToWhom.YPos;
                xPos = PrintLayoutToUse.PrintToWhom.XPos;
                printFont = PrintLayoutToUse.PrintToWhom.ItemFont;
                ev.Graphics!.DrawString(TCheck.BusinessName, printFont, Brushes.Black, xPos, yPos, new StringFormat());
            }
            // write the remaining address lines
            if (PrintLayoutToUse.PrintAddress1.IfToPrintThisItem)
            {
                yPos = PrintLayoutToUse.PrintAddress1.YPos;
                xPos = PrintLayoutToUse.PrintAddress1.XPos;
                printFont = PrintLayoutToUse.PrintAddress1.ItemFont;
                ev.Graphics!.DrawString(TCheck.AddressLine1, printFont, Brushes.Black, xPos, yPos, new StringFormat());
            }
            if (PrintLayoutToUse.PrintAddress2.IfToPrintThisItem)
            {
                yPos = PrintLayoutToUse.PrintAddress2.YPos;
                xPos = PrintLayoutToUse.PrintAddress2.XPos;
                printFont = PrintLayoutToUse.PrintAddress2.ItemFont;
                ev.Graphics!.DrawString(TCheck.AddressLine2, printFont, Brushes.Black, xPos, yPos, new StringFormat());
            }
            if (PrintLayoutToUse.PrintAddress3.IfToPrintThisItem)
            {
                yPos = PrintLayoutToUse.PrintAddress3.YPos;
                xPos = PrintLayoutToUse.PrintAddress3.XPos;
                printFont = PrintLayoutToUse.PrintAddress3.ItemFont;
                ev.Graphics!.DrawString(TCheck.AddressLine3, printFont, Brushes.Black, xPos, yPos, new StringFormat());
            }
            if (PrintLayoutToUse.PrintAddress4.IfToPrintThisItem)
            {
                yPos = PrintLayoutToUse.PrintAddress4.YPos;
                xPos = PrintLayoutToUse.PrintAddress4.XPos;
                printFont = PrintLayoutToUse.PrintAddress4.ItemFont;
                ev.Graphics!.DrawString(TCheck.AddressLine4, printFont, Brushes.Black, xPos, yPos, new StringFormat());
            }
            if (PrintLayoutToUse.PrintMemo.IfToPrintThisItem)
            {
                yPos = PrintLayoutToUse.PrintMemo.YPos;
                xPos = PrintLayoutToUse.PrintMemo.XPos;
                printFont = PrintLayoutToUse.PrintMemo.ItemFont;
                ev.Graphics!.DrawString(TCheck.Memo, printFont, Brushes.Black, xPos, yPos, new StringFormat());
            }
            // second part

            if (PrintLayoutToUse.PrintBreakdown1.IfToPrintThisItem)
            {
                yPos = PrintLayoutToUse.PrintBreakdown1.YPos;
                //xPos = PrintLayoutToUse.PrintBreakdown1.XPos;
                printFont = PrintLayoutToUse.PrintBreakdown1.ItemFont;

                FormatBreakdown(yPos, TCheck, printFont, ev);
            }


            // third part

            if (PrintLayoutToUse.PrintBreakdown2.IfToPrintThisItem)
            {
                yPos = PrintLayoutToUse.PrintBreakdown2.YPos;
                //xPos = PrintLayoutToUse.PrintBreakdown2.XPos;
                printFont = PrintLayoutToUse.PrintBreakdown1.ItemFont;

                FormatBreakdown(yPos, TCheck, printFont, ev);
            }
        }
        private static void FormatBreakdown(float startingY, CheckToPrint TCheck, Font printFont, PrintPageEventArgs ev)
        {
            float yPos;
            float xPos;
            float margin = 5;

            yPos = startingY + printFont.SizeInPoints + margin;

            // write the company name and check number - already printed on the check
            // write to whom and the date
            string whatToPrint = TCheck.ToTheOrderOf.Trim();
            xPos = 60;
            ev.Graphics!.DrawString(whatToPrint, printFont, Brushes.Black, xPos, yPos, new StringFormat());

            xPos = 600;
            ev.Graphics!.DrawString(TCheck.DateToPrint, printFont, Brushes.Black, xPos, yPos, new StringFormat());

            decimal Total = 0.00M;
            // write the breakdown
            foreach (LedgerEntryBreakdown tCategory in TCheck.Breakdown)
            {
                yPos += printFont.SizeInPoints + margin;
                if (tCategory.AccountName.Length == 0) continue;
                whatToPrint = tCategory.AccountName.Trim();
                xPos = 50;
                ev.Graphics!.DrawString(whatToPrint, printFont, Brushes.Black, xPos, yPos, new StringFormat());

                xPos = 315;
                ev.Graphics!.DrawString(tCategory.Memo.Trim(), printFont, Brushes.Black, xPos, yPos, new StringFormat());

                xPos = 635;
                var format = new StringFormat() { Alignment = StringAlignment.Far };
                var rect = new RectangleF(xPos, yPos, 150, printFont.SizeInPoints + margin);
                ev.Graphics!.DrawString(tCategory.Amount.ToString(), printFont, Brushes.Black, rect, format);
                Total += tCategory.Amount;

            }

            // write the memo and the total

            yPos += printFont.SizeInPoints + margin;

            xPos = 100;
            ev.Graphics!.DrawString(TCheck.Memo.Trim(), printFont, Brushes.Black, xPos, yPos, new StringFormat());

            xPos = 635;
            var Totalformat = new StringFormat() { Alignment = StringAlignment.Far };
            var Trect = new RectangleF(xPos, yPos, 150, printFont.SizeInPoints + margin);
            ev.Graphics!.DrawString(Total.ToString(), printFont, Brushes.Black, Trect, Totalformat);
        }











        private void FillInCurrentCheckToPrint(CheckToPrint ToPrint)
        {
            ToPrint.CheckNumber = CheckNumberLabel.Text;
            ToPrint.DateToPrint = DateTimePicker.Value.ToShortDateString();
            ToPrint.ToTheOrderOf = ToWhomTextBox.Text;
            ToPrint.AmountToPrint = AmountTextBox.Text;
            while (ToPrint.AmountToPrint.Length < 8) ToPrint.AmountToPrint = "*" + ToPrint.AmountToPrint;
            ToPrint.AmountWords = AmountWordsLabel.Text;
            ToPrint.BusinessName = ToWhomNameLabel.Text;
            ToPrint.AddressLine1 = ToWhomAddress1Label.Text;
            ToPrint.AddressLine2 = ToWhomAddress2Label.Text;
            ToPrint.AddressLine3 = ToWhomAddress3Label.Text;
            ToPrint.Memo = MemoTextBox.Text;
            ClearBreakdown(ToPrint);
            PullInBreakdown(ToPrint);
        }
        private static string CleanAmount(string Amount)
        {
            string results = "";
            foreach (char c in Amount)
            {
                if (Char.IsDigit(c)) results += c;
                if (c == '-') results += c;
                if (c == '.') results += c;
            }
            return results;
        }

        private void FillInFromLastTransaction(string MatchedName)
        {
            LedgerEntry? LastTransaction = ActiveBook.CurrentTransactionLedger.FindLastTransactionFor(MatchedName);

            if (LastTransaction != null)
            {
                AmountTextBox.Text = LastTransaction.Debit.ToString();
                //CategoriesComboBox.Text = LastTransaction.Account;
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
                                Amount = LastTransaction.SubAccounts[subi].Amount
                            };
                            tSubAccounts.Add(tEntry);
                            subi++;
                            DetailSubTotal += tEntry.Amount;
                        }
                        while (subi++ < 40)
                        {
                            tSubAccounts.Add(new LedgerEntryBreakdown());
                        }
                        CheckBreakdownDataGridView.DataSource = tSubAccounts;
                        CheckBreakdownDataGridView.AutoResizeColumn(0);
                        CheckBreakdownDataGridView.Columns[1].Width = 350;
                        CheckBreakdownDataGridView.Visible = true;

                        DetailTotalLabel.Visible = true;
                        DetailTotalTextBox.Visible = true;
                        DetailTotalTextBox.ReadOnly = true;
                        DetailTotalTextBox.Text = DetailSubTotal.ToString();
                    }
                }
            }
        }

        private void AmountTextBox_Leave(object sender, EventArgs e)
        {
            if (AmountTextBox.Text.Length > 0)
            {
                string sAmount = AmountTextBox.Text;
                if (sAmount.Length > 1)
                    if (sAmount[0] == '$') sAmount = sAmount[1..];
                if (Decimal.TryParse(sAmount, out _))
                {
                    decimal Amount = Decimal.Parse(sAmount);
                    //TransactionDebit = Amount;
                    AmountTextBox.Text = Amount.ToString("0.00");
                    //CurrentBalanceTextBox.Text = (PriorBalance - Amount).ToString("C");

                    // write the wording
                    string AmountWording = BuildWordsFromNumber(Amount);
                    while (AmountWording.Length < WordFillInLength) { AmountWording += "*"; }
                    AmountWordsLabel.Text = AmountWording;
                }
            }
        }

        private void GetPayTo(string EnteredName)
        {
            Payee = ActiveBook.ToPayTo.GetMatchedPayTo(EnteredName);
            if (Payee == null)
            {
                if (MessageBox.Show("That payee is not in the list of entities to pay. Do you want to add that payee now?", "Not Found", MessageBoxButtons.YesNo)
                    == DialogResult.Yes)
                {
                    // show the screen to add this person now
                }
                else
                {
                    // put the focus back on that field
                    ToWhomTextBox.Focus();
                }
            }
            else
                WritePayToOnCheck();

        }
        private void WritePayToOnCheck()
        {
            if (Payee != null)
            {
                ToWhomTextBox.Text = Payee.PrintAs;
                ToWhomNameLabel.Text = Payee.BusinessName;

                // write mailing address on the check

                ToWhomAddress1Label.Text = Payee.Address;
                if (Payee.Address2.Length > 1)
                {
                    ToWhomAddress2Label.Text = Payee.Address2;
                    ToWhomAddress3Label.Text = Payee.City + ", " + Payee.State + " " + Payee.ZipCode;
                }
                else
                {
                    ToWhomAddress2Label.Text = Payee.City + ", " + Payee.State + " " + Payee.ZipCode;
                    ToWhomAddress3Label.Text = "";
                }
            }
        }

        private static void ClearBreakdown(CheckToPrint ToPrint)
        {
            ToPrint.Breakdown = new()
            {
                new LedgerEntryBreakdown(),
                new LedgerEntryBreakdown(),
                new LedgerEntryBreakdown(),
                new LedgerEntryBreakdown(),
                new LedgerEntryBreakdown(),
                new LedgerEntryBreakdown(),
                new LedgerEntryBreakdown(),
                new LedgerEntryBreakdown(),
                new LedgerEntryBreakdown(),
                new LedgerEntryBreakdown(),
                new LedgerEntryBreakdown(),
                new LedgerEntryBreakdown(),
                new LedgerEntryBreakdown(),
                new LedgerEntryBreakdown(),
                new LedgerEntryBreakdown(),
                new LedgerEntryBreakdown(),
                new LedgerEntryBreakdown(),
                new LedgerEntryBreakdown()
            };
        }

        private void PullInBreakdown(CheckToPrint ToPrint)
        {
            int RowNum = 0;
            foreach (LedgerEntryBreakdown TBreakdown in ToPrint.Breakdown)
            {
                TBreakdown.AccountName = CheckBreakdownDataGridView.Rows[RowNum].Cells[0].Value.ToString() ?? "";
                TBreakdown.Amount = Decimal.Parse(CheckBreakdownDataGridView.Rows[RowNum].Cells[1].Value.ToString() ?? "");
                if (CheckBreakdownDataGridView.Rows[RowNum].Cells[2].Value != null)
                    TBreakdown.Memo = CheckBreakdownDataGridView.Rows[RowNum].Cells[2].Value.ToString() ?? "";
                else
                    TBreakdown.Memo = "";
                RowNum++;
            }

        }



        private string BuildWordsFromNumber(decimal Number)
        {
            string results = "";

            int IntegerNumber = (int)Math.Truncate(Number);
            int[] Parts = SplitInteger(IntegerNumber);

            // start with hundred million
            if (Parts[0] > 0)
            {
                results += NumberWord[Parts[0]] + " " + Hundred;
            }

            // handle one and two digit millions

            if (Parts[1] > 0)
            {
                if (results.Length > 0) results += " ";
                results += BuildTens(Parts[1], Parts[2]) + " " + Million;
            }
            else
            {
                if (Parts[2] > 0)
                {
                    if (results.Length > 0) results += " ";
                    results += NumberWord[Parts[2]] + " " + Million;
                }
                else
                if (results.Length > 0) { results += " " + Million; }
            }

            // handle hundred thousands

            if (Parts[3] > 0)
            {
                if (results.Length > 0) results += " ";
                results += NumberWord[Parts[3]] + " " + Hundred;
                if (Parts[4] == 0 && Parts[5] == 0) results += " " + Thousand;
            }
            // handle two digit thousands
            if (Parts[4] > 0)
            {
                if (results.Length > 0) results += " ";
                results += BuildTens(Parts[4], Parts[5]) + " " + Thousand;
            }
            else
            {
                if (Parts[5] > 0)
                {
                    if (results.Length > 0) results += " ";
                    results += NumberWord[Parts[5]] + " " + Thousand;
                }
            }

            // handle hundreds
            if (Parts[6] > 0)
            {
                if (results.Length > 0) results += " ";
                results += NumberWord[Parts[6]] + " " + Hundred;
            }
            // handle two digits
            if (Parts[7] > 0)
            {
                if (results.Length > 0) results += " ";
                results += BuildTens(Parts[7], Parts[8]);
            }
            else
            {
                if (Parts[8] > 0)
                {
                    if (results.Length > 0) results += " ";
                    results += NumberWord[Parts[8]];
                }
            }


            // add in any part of dollar

            int decs = (int)((Number - Math.Truncate(Number)) * 100);
            if (decs > 0)
            {
                if (results.Length > 0) results += " and ";
                results += decs.ToString("00") + "/100";
            }
            else
                results += " and no/100";
            return results;
        }

        private string BuildTens(int Number, int Number2)
        {
            string results = "";
            if (Number == 0 && Number2 == 0) return "";
            if (Number > 0)
            {
                // deal with teens
                if (Number == 1)
                {
                    results += NumberWord[Number * 10 + Number2];
                }
                else
                    results += TenWords[Number] + " " + NumberWord[Number2];
            }
            else
                results += NumberWord[Number2];
            return results;
        }
        private static int[] SplitInteger(int Number)
        {
            // limited to hundred million

            int[] Parts = new int[9];

            Parts[0] = Number / 100000000;
            Number %= 100000000;
            Parts[1] = Number / 10000000;
            Number %= 10000000;
            Parts[2] = Number / 1000000;
            Number %= 1000000;
            Parts[3] = Number / 100000;
            Number %= 100000;
            Parts[4] = Number / 10000;
            Number %= 10000;
            Parts[5] = Number / 1000;
            Number %= 1000;
            Parts[6] = Number / 100;
            Number %= 100;
            Parts[7] = Number / 10;
            Parts[8] = Number % 10;
            return Parts;
        }


        private void BuildLaser1PTFormat()
        {
            CurrentPrintLayout = new();
            // check number
            CurrentPrintLayout.PrintCompanyName = new()
            { IfToPrintThisItem = false };
            CurrentPrintLayout.PrintCheckNumber = new()
            { IfToPrintThisItem = false };
            CurrentPrintLayout.PrintDate = new()
            {
                IfToPrintThisItem = true,
                ItemFont = new Font("Arial", 10),
                YPos = 60,
                XPos = 675,
                Width = 120,
                Height = 12
            };
            CurrentPrintLayout.PrintToTheOrderOf = new()
            {
                IfToPrintThisItem = true,
                ItemFont = new Font("Arial", 11),
                YPos = 110,
                XPos = 100,
                Width = 500,
                Height = 14
            };
            CurrentPrintLayout.PrintAmount = new()
            {
                IfToPrintThisItem = true,
                ItemFont = new Font("Arial", 10),
                YPos = 105,
                XPos = 675,
                Width = 130,
                Height = 12
            };
            CurrentPrintLayout.PrintAmountWords = new()
            {
                IfToPrintThisItem = true,
                ItemFont = new Font("Arial", 10),
                YPos = 147,
                XPos = 31,
                Width = 712,
                Height = 16
            };
            CurrentPrintLayout.PrintToWhom = new()
            {
                IfToPrintThisItem = true,
                ItemFont = new Font("Arial", 9),
                YPos = 177,
                XPos = 80,
                Width = 200,
                Height = 12
            };
            CurrentPrintLayout.PrintAddress1 = new()
            {
                IfToPrintThisItem = true,
                ItemFont = new Font("Arial", 9),
                YPos = 192,
                XPos = 80,
                Width = 200,
                Height = 12
            };
            CurrentPrintLayout.PrintAddress2 = new()
            {
                IfToPrintThisItem = true,
                ItemFont = new Font("Arial", 9),
                YPos = 207,
                XPos = 80,
                Width = 200,
                Height = 12
            };
            CurrentPrintLayout.PrintAddress3 = new()
            {
                IfToPrintThisItem = true,
                ItemFont = new Font("Arial", 9),
                YPos = 222,
                XPos = 80,
                Width = 200,
                Height = 12
            };
            CurrentPrintLayout.PrintMemo = new()
            {
                IfToPrintThisItem = true,
                ItemFont = new Font("Arial", 10),
                YPos = 250,
                XPos = 60,
                Width = 200,
                Height = 12
            };
            CurrentPrintLayout.PrintBreakdown1 = new()
            {
                IfToPrintThisItem = true,
                ItemFont = new Font("Arial", 10),
                YPos = 344,
                XPos = 31,
                Width = 800,
                Height = 12
            };
            CurrentPrintLayout.PrintBreakdown2 = new()
            {
                IfToPrintThisItem = true,
                ItemFont = new Font("Arial", 10),
                YPos = 692,
                XPos = 31,
                Width = 800,
                Height = 12
            };
        }


    }
}
