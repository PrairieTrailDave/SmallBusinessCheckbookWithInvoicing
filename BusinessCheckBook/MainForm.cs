//**********************************************************************
//
//          Copyright � 2023 Prairie Trail Software, Inc.
//
//**********************************************************************

using BusinessCheckBook.DataStore;
using BusinessCheckBook.Reports;
using BusinessCheckBook.Settings;
using ClosedXML.Excel;
using System.Windows.Forms;
using BusinessCheckBook.TimeTracking;

namespace BusinessCheckBook
{
    public partial class MainForm : Form
    {
        public MyCheckbook ActiveBook;

        string CurrentActiveFile = string.Empty;
        string InputFolder = string.Empty;

        public MainForm()
        {
            InitializeComponent();
            ActiveBook = new MyCheckbook();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            OpenTheDataFile();
        }


        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ActiveBook.IfChanged())
            {
                if (MessageBox.Show("The file has changed. Do you really want to close without saving?", "Dirty File", MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    e.Cancel = true;
                }
            }
        }

        // Menu Item Clicks

        // File Menu
        private void FileNewMenuItem_Click(object sender, EventArgs e)
        {

            // ask where the checkbook will be created
            saveFileDialog.Filter = "CheckBook files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                CurrentActiveFile = saveFileDialog.FileName;
                InputFolder = Path.GetDirectoryName(saveFileDialog.FileName) ?? "";
                ActiveBook = new MyCheckbook();

                InitialBalanceForm IBF = new();
                IBF.ShowDialog();
                try
                {
                    ActiveBook.CreateNewCheckBook(IBF.InitialBalance, IBF.FirstCheckNumber, IBF.FirstInvoiceNumber);
                    // open the business data entry screen
                    BusinessDataToolStripMenuItem_Click(sender, e);

                    EnableButtons();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    ShowWarningIncomplete();
                }
            }
        }

        private void FileOpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenTheDataFile();
        }


        private void FileSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog.OverwritePrompt = false;
            saveFileDialog.Filter = "CheckBook files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            saveFileDialog.FileName = CurrentActiveFile;
            saveFileDialog.InitialDirectory = InputFolder;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.UseWaitCursor = true;
                    Application.DoEvents();
                    WriteDataFile(saveFileDialog.FileName);

                    this.Cursor = Cursors.Default;
                    this.UseWaitCursor = false;
                    MessageBox.Show("File saved");
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("An error occured " + Ex.Message);
                }
            }
        }

        private void IIFFileImportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.FileName = "*.IIF";
            openFileDialog.Filter = "Quickbook IIF Files (*.IIF)|*.IIF|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {

                    if (ReadIIFFile(openFileDialog.FileName))
                    {
                        MessageBox.Show("File imported");
                        EnableButtons();
                    }
                    else
                        ShowWarningIncomplete();

                    this.Cursor = Cursors.Default;
                    this.UseWaitCursor = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occured " + ex.Message);
                }
            }

        }


        private async void JournalExcelFileImportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.FileName = "*.xlsx";
            openFileDialog.Filter = "Quickbook Journal Excel Export Files (*.xlsx)|*.xlsx|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (await ReadJournalFile(openFileDialog.FileName))
                    {
                        MessageBox.Show("File imported");
                        EnableButtons();
                    }
                    else
                        ShowWarningIncomplete();

                    this.Cursor = Cursors.Default;
                    this.UseWaitCursor = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occured " + ex.Message);
                }
            }


        }



        // Settings Menu


        private void ChartOfAccountsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChartOfAccountForm CHF = new();
            CHF.Setup(ActiveBook);
            CHF.ShowDialog();
        }

        private void CustomerListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomerListForm CLF = new();
            CLF.SetUp(ActiveBook);
            CLF.ShowDialog();
        }

        private void ListOfPayTosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PayToListForm PTLF = new();
            PTLF.SetUp(ActiveBook);
            PTLF.ShowDialog();
        }


        private void BusinessDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CompanyInformationForm CIF = new();
            CIF.SetUp(ActiveBook);
            CIF.ShowDialog();
        }


        private void InvoiceSettingsToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        // Time Tracking Menu Items

        private void ManagePeopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManagePeopleForm MPF = new(ActiveBook);
            MPF.ShowDialog();
        }

        private void ManageProjectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageProjectForm MPF = new(ActiveBook);
            MPF.ShowDialog();
        }

        private void EnterTimeSheetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageTimeForm MTF = new(ActiveBook);
            MTF.ShowDialog();
        }



        // Reconcile

        private void ReconcileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReconcileForm RF = new();
            RF.Setup(ActiveBook);
            RF.ShowDialog();
        }


        // Reports

        private void CompareIncomeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CompareReportForm CRF = new();
            CRF.Setup(ActiveBook);
            CRF.ShowDialog();
        }

        private void Federal1120ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Federal1120Form RF = new();
            RF.Setup(ActiveBook);
            RF.ShowDialog();
        }

        private void TaxReport1099_Click(object sender, EventArgs e)
        {
            Federal1099Form RF = new();
            RF.Setup(ActiveBook);
            RF.ShowDialog();
        }


        // Button Routines

        private void ViewLedgerButton_Click(object sender, EventArgs e)
        {
            ViewLedgerForm VLF = new();
            VLF.SetUp(ActiveBook);
            VLF.ShowDialog();
        }

        private void WriteChecksButton_Click(object sender, EventArgs e)
        {
            WriteCheckForm WCF = new();
            WCF.SetUp(ActiveBook);
            WCF.ShowDialog();
        }

        private void CreateInvoiceButton_Click(object sender, EventArgs e)
        {
            CreateInvoiceForm CIF = new();
            CIF.SetUp(ActiveBook);
            CIF.ShowDialog();
        }

        private void ReceivePaymentButton_Click(object sender, EventArgs e)
        {
            ReceivePaymentsForm RPF = new();
            RPF.SetUp(ActiveBook);
            RPF.ShowDialog();
        }

        private void ShowInvoicesButton_Click(object sender, EventArgs e)
        {
            ViewInvoicesForm VIF = new();
            VIF.SetUp(ActiveBook);
            VIF.ShowDialog();
        }



        // Support Routines


        private async void OpenTheDataFile()
        {
            openFileDialog.FileName = "*.xlsx";
            openFileDialog.Filter = "CheckBook files (*.xlsx)|*.xlsx|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.UseWaitCursor = true;
                    Application.DoEvents();
                    CurrentActiveFile = openFileDialog.SafeFileName;
                    InputFolder = Path.GetDirectoryName(openFileDialog.FileName) ?? "";
                    if (await ReadDataFile(openFileDialog.FileName))
                    {
                        MessageBox.Show("File read"); ;
                        EnableButtons();
                    }
                    this.Cursor = Cursors.Default;
                    this.UseWaitCursor = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occured " + ex.Message);
                    ShowWarningIncomplete();
                }
            }
        }

        private void ShowWarningIncomplete()
        {
            MessageBox.Show("The operation was incomplete. The file in memory is likely to be corrupted. Please restart.");
        }
        private async Task<bool> ReadDataFile(string FileName)
        {
            try
            {
                XLWorkbook CurrentWorkbook;
                ActiveBook = new MyCheckbook();
                string ErrorMessage = "";
                bool FileRead = false;
                await Task.Run(() =>
                {
                    CurrentWorkbook = new XLWorkbook(FileName);


                    if (ActiveBook.FileIsValid(CurrentWorkbook, out ErrorMessage))
                    {
                        if (ActiveBook.ReadExcelFile(CurrentWorkbook, out ErrorMessage))
                            FileRead = true;
                        else
                            MessageBox.Show(ErrorMessage);
                    }
                    else
                    {
                        MessageBox.Show(ErrorMessage);
                    }
                }
                );

                return FileRead;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error in processing file" + Ex.Message);
            }
            return false;
        }

        private bool ReadIIFFile(string FileName)
        {
            return ActiveBook.ReadIIFFile(FileName);
        }


        private async Task<bool> ReadJournalFile(string FileName)
        {
            bool result = false;
            await Task.Run(() =>
            {
                result = ActiveBook.ReadJournalFile(FileName);
            });

            return result;
        }


        private void WriteDataFile(string FileName)
        {

            XLWorkbook CurrentWorkbook = new();
            ActiveBook.WriteCheckBook(CurrentWorkbook);

            CurrentWorkbook.SaveAs(FileName);
            ActiveBook.ClearChanged();
        }


        private void EnableButtons()
        {
            ViewLedgerButton.Enabled = true;
            WriteChecksButton.Enabled = true;
            CreateInvoiceButton.Enabled = true;
            ReceivePaymentButton.Enabled = true;
            ShowInvoicesButton.Enabled = true;

        }


    }
}