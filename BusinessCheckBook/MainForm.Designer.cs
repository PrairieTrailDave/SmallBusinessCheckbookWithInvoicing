namespace BusinessCheckBook
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            FileNewMenuItem = new ToolStripMenuItem();
            FileOpenToolStripMenuItem = new ToolStripMenuItem();
            FileSaveToolStripMenuItem = new ToolStripMenuItem();
            ImportToolStripMenuItem = new ToolStripMenuItem();
            IIFFileImportToolStripMenuItem = new ToolStripMenuItem();
            JournalExcelFileImportToolStripMenuItem = new ToolStripMenuItem();
            ExportToolStripMenuItem = new ToolStripMenuItem();
            ExitToolStripMenuItem = new ToolStripMenuItem();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            chartOfAccountsToolStripMenuItem = new ToolStripMenuItem();
            customerListToolStripMenuItem = new ToolStripMenuItem();
            ListOfPayTosToolStripMenuItem = new ToolStripMenuItem();
            businessDataToolStripMenuItem = new ToolStripMenuItem();
            invoiceSettingsToolStripMenuItem1 = new ToolStripMenuItem();
            reconcileToolStripMenuItem = new ToolStripMenuItem();
            reportsToolStripMenuItem = new ToolStripMenuItem();
            compareIncomeToolStripMenuItem = new ToolStripMenuItem();
            federal1120ToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripMenuItem();
            openFileDialog = new OpenFileDialog();
            saveFileDialog = new SaveFileDialog();
            ViewLedgerButton = new Button();
            WriteChecksButton = new Button();
            CreateInvoiceButton = new Button();
            ReceivePaymentButton = new Button();
            ShowInvoicesButton = new Button();
            label3 = new Label();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, settingsToolStripMenuItem, reconcileToolStripMenuItem, reportsToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { FileNewMenuItem, FileOpenToolStripMenuItem, FileSaveToolStripMenuItem, ImportToolStripMenuItem, ExportToolStripMenuItem, ExitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // FileNewMenuItem
            // 
            FileNewMenuItem.Name = "FileNewMenuItem";
            FileNewMenuItem.Size = new Size(110, 22);
            FileNewMenuItem.Text = "New";
            FileNewMenuItem.Click += FileNewMenuItem_Click;
            // 
            // FileOpenToolStripMenuItem
            // 
            FileOpenToolStripMenuItem.Name = "FileOpenToolStripMenuItem";
            FileOpenToolStripMenuItem.Size = new Size(110, 22);
            FileOpenToolStripMenuItem.Text = "Open";
            FileOpenToolStripMenuItem.Click += FileOpenToolStripMenuItem_Click;
            // 
            // FileSaveToolStripMenuItem
            // 
            FileSaveToolStripMenuItem.Name = "FileSaveToolStripMenuItem";
            FileSaveToolStripMenuItem.Size = new Size(110, 22);
            FileSaveToolStripMenuItem.Text = "Save";
            FileSaveToolStripMenuItem.Click += FileSaveToolStripMenuItem_Click;
            // 
            // ImportToolStripMenuItem
            // 
            ImportToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { IIFFileImportToolStripMenuItem, JournalExcelFileImportToolStripMenuItem });
            ImportToolStripMenuItem.Name = "ImportToolStripMenuItem";
            ImportToolStripMenuItem.Size = new Size(110, 22);
            ImportToolStripMenuItem.Text = "Import";
            // 
            // IIFFileImportToolStripMenuItem
            // 
            IIFFileImportToolStripMenuItem.Name = "IIFFileImportToolStripMenuItem";
            IIFFileImportToolStripMenuItem.Size = new Size(163, 22);
            IIFFileImportToolStripMenuItem.Text = "IIF File";
            IIFFileImportToolStripMenuItem.Click += IIFFileImportToolStripMenuItem_Click;
            // 
            // JournalExcelFileImportToolStripMenuItem
            // 
            JournalExcelFileImportToolStripMenuItem.Name = "JournalExcelFileImportToolStripMenuItem";
            JournalExcelFileImportToolStripMenuItem.Size = new Size(163, 22);
            JournalExcelFileImportToolStripMenuItem.Text = "Journal Excel File";
            JournalExcelFileImportToolStripMenuItem.Click += JournalExcelFileImportToolStripMenuItem_Click;
            // 
            // ExportToolStripMenuItem
            // 
            ExportToolStripMenuItem.Name = "ExportToolStripMenuItem";
            ExportToolStripMenuItem.Size = new Size(110, 22);
            ExportToolStripMenuItem.Text = "Export";
            // 
            // ExitToolStripMenuItem
            // 
            ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            ExitToolStripMenuItem.Size = new Size(110, 22);
            ExitToolStripMenuItem.Text = "Exit";
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { chartOfAccountsToolStripMenuItem, customerListToolStripMenuItem, ListOfPayTosToolStripMenuItem, businessDataToolStripMenuItem, invoiceSettingsToolStripMenuItem1 });
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(61, 20);
            settingsToolStripMenuItem.Text = "Settings";
            // 
            // chartOfAccountsToolStripMenuItem
            // 
            chartOfAccountsToolStripMenuItem.Name = "chartOfAccountsToolStripMenuItem";
            chartOfAccountsToolStripMenuItem.Size = new Size(172, 22);
            chartOfAccountsToolStripMenuItem.Text = "Chart Of Accounts";
            chartOfAccountsToolStripMenuItem.Click += ChartOfAccountsToolStripMenuItem_Click;
            // 
            // customerListToolStripMenuItem
            // 
            customerListToolStripMenuItem.Name = "customerListToolStripMenuItem";
            customerListToolStripMenuItem.Size = new Size(172, 22);
            customerListToolStripMenuItem.Text = "Customer List";
            customerListToolStripMenuItem.Click += CustomerListToolStripMenuItem_Click;
            // 
            // ListOfPayTosToolStripMenuItem
            // 
            ListOfPayTosToolStripMenuItem.Name = "ListOfPayTosToolStripMenuItem";
            ListOfPayTosToolStripMenuItem.Size = new Size(172, 22);
            ListOfPayTosToolStripMenuItem.Text = "List of Pay To's";
            ListOfPayTosToolStripMenuItem.Click += ListOfPayTosToolStripMenuItem_Click;
            // 
            // businessDataToolStripMenuItem
            // 
            businessDataToolStripMenuItem.Name = "businessDataToolStripMenuItem";
            businessDataToolStripMenuItem.Size = new Size(172, 22);
            businessDataToolStripMenuItem.Text = "Business Data";
            businessDataToolStripMenuItem.Click += BusinessDataToolStripMenuItem_Click;
            // 
            // invoiceSettingsToolStripMenuItem1
            // 
            invoiceSettingsToolStripMenuItem1.Name = "invoiceSettingsToolStripMenuItem1";
            invoiceSettingsToolStripMenuItem1.Size = new Size(172, 22);
            invoiceSettingsToolStripMenuItem1.Text = "Invoice Settings";
            invoiceSettingsToolStripMenuItem1.Click += InvoiceSettingsToolStripMenuItem1_Click;
            // 
            // reconcileToolStripMenuItem
            // 
            reconcileToolStripMenuItem.Name = "reconcileToolStripMenuItem";
            reconcileToolStripMenuItem.Size = new Size(70, 20);
            reconcileToolStripMenuItem.Text = "Reconcile";
            reconcileToolStripMenuItem.Click += ReconcileToolStripMenuItem_Click;
            // 
            // reportsToolStripMenuItem
            // 
            reportsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { compareIncomeToolStripMenuItem, federal1120ToolStripMenuItem, toolStripMenuItem2 });
            reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
            reportsToolStripMenuItem.Size = new Size(59, 20);
            reportsToolStripMenuItem.Text = "Reports";
            // 
            // compareIncomeToolStripMenuItem
            // 
            compareIncomeToolStripMenuItem.Name = "compareIncomeToolStripMenuItem";
            compareIncomeToolStripMenuItem.Size = new Size(166, 22);
            compareIncomeToolStripMenuItem.Text = "Compare Income";
            compareIncomeToolStripMenuItem.Click += CompareIncomeToolStripMenuItem_Click;
            // 
            // federal1120ToolStripMenuItem
            // 
            federal1120ToolStripMenuItem.Name = "federal1120ToolStripMenuItem";
            federal1120ToolStripMenuItem.Size = new Size(166, 22);
            federal1120ToolStripMenuItem.Text = "Federal 1120";
            federal1120ToolStripMenuItem.Click += Federal1120ToolStripMenuItem_Click;
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(166, 22);
            toolStripMenuItem2.Text = "1099 / 1096";
            toolStripMenuItem2.Click += TaxReport1099_Click;
            // 
            // openFileDialog
            // 
            openFileDialog.DefaultExt = "xlsx";
            openFileDialog.FileName = "BusinessCheckbook";
            // 
            // saveFileDialog
            // 
            saveFileDialog.DefaultExt = "xlsx";
            saveFileDialog.FileName = "MyBusinessCheckbook";
            // 
            // ViewLedgerButton
            // 
            ViewLedgerButton.Enabled = false;
            ViewLedgerButton.Location = new Point(117, 83);
            ViewLedgerButton.Name = "ViewLedgerButton";
            ViewLedgerButton.Size = new Size(143, 23);
            ViewLedgerButton.TabIndex = 1;
            ViewLedgerButton.Text = "View Ledger";
            ViewLedgerButton.UseVisualStyleBackColor = true;
            ViewLedgerButton.Click += ViewLedgerButton_Click;
            // 
            // WriteChecksButton
            // 
            WriteChecksButton.Enabled = false;
            WriteChecksButton.Location = new Point(117, 139);
            WriteChecksButton.Name = "WriteChecksButton";
            WriteChecksButton.Size = new Size(143, 23);
            WriteChecksButton.TabIndex = 2;
            WriteChecksButton.Text = "Write Checks";
            WriteChecksButton.UseVisualStyleBackColor = true;
            WriteChecksButton.Click += WriteChecksButton_Click;
            // 
            // CreateInvoiceButton
            // 
            CreateInvoiceButton.Enabled = false;
            CreateInvoiceButton.Location = new Point(117, 199);
            CreateInvoiceButton.Name = "CreateInvoiceButton";
            CreateInvoiceButton.Size = new Size(143, 23);
            CreateInvoiceButton.TabIndex = 3;
            CreateInvoiceButton.Text = "Create Invoice";
            CreateInvoiceButton.UseVisualStyleBackColor = true;
            CreateInvoiceButton.Click += CreateInvoiceButton_Click;
            // 
            // ReceivePaymentButton
            // 
            ReceivePaymentButton.Enabled = false;
            ReceivePaymentButton.Location = new Point(117, 266);
            ReceivePaymentButton.Name = "ReceivePaymentButton";
            ReceivePaymentButton.Size = new Size(143, 23);
            ReceivePaymentButton.TabIndex = 4;
            ReceivePaymentButton.Text = "Receive Payment";
            ReceivePaymentButton.UseVisualStyleBackColor = true;
            ReceivePaymentButton.Click += ReceivePaymentButton_Click;
            // 
            // ShowInvoicesButton
            // 
            ShowInvoicesButton.Enabled = false;
            ShowInvoicesButton.Location = new Point(117, 325);
            ShowInvoicesButton.Margin = new Padding(2, 2, 2, 2);
            ShowInvoicesButton.Name = "ShowInvoicesButton";
            ShowInvoicesButton.Size = new Size(143, 20);
            ShowInvoicesButton.TabIndex = 5;
            ShowInvoicesButton.Text = "Show Invoices";
            ShowInvoicesButton.UseVisualStyleBackColor = true;
            ShowInvoicesButton.Click += ShowInvoicesButton_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 24);
            label3.Name = "label3";
            label3.Size = new Size(218, 15);
            label3.TabIndex = 7;
            label3.Text = "Copyright 2023 Prarie Trail Software, Inc.";
            label3.Visible = false;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label3);
            Controls.Add(ShowInvoicesButton);
            Controls.Add(ReceivePaymentButton);
            Controls.Add(CreateInvoiceButton);
            Controls.Add(WriteChecksButton);
            Controls.Add(ViewLedgerButton);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            Text = "Business Checking";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem FileOpenToolStripMenuItem;
        private ToolStripMenuItem FileSaveToolStripMenuItem;
        private ToolStripMenuItem ImportToolStripMenuItem;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripMenuItem chartOfAccountsToolStripMenuItem;
        private ToolStripMenuItem customerListToolStripMenuItem;
        private ToolStripMenuItem ListOfPayTosToolStripMenuItem;
        private OpenFileDialog openFileDialog;
        private SaveFileDialog saveFileDialog;
        private Button ViewLedgerButton;
        private ToolStripMenuItem FileNewMenuItem;
        private ToolStripMenuItem ExportToolStripMenuItem;
        private ToolStripMenuItem ExitToolStripMenuItem;
        private ToolStripMenuItem businessDataToolStripMenuItem;
        private ToolStripMenuItem invoiceSettingsToolStripMenuItem1;
        private Button WriteChecksButton;
        private Button CreateInvoiceButton;
        private Button ReceivePaymentButton;
        private ToolStripMenuItem IIFFileImportToolStripMenuItem;
        private ToolStripMenuItem JournalExcelFileImportToolStripMenuItem;
        private Button ShowInvoicesButton;
        private ToolStripMenuItem reconcileToolStripMenuItem;
        private ToolStripMenuItem reportsToolStripMenuItem;
        private ToolStripMenuItem compareIncomeToolStripMenuItem;
        private ToolStripMenuItem federal1120ToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem2;
        private Label label3;
    }
}