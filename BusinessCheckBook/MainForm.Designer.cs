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
            InvoiceSettingsToolStripMenuItem = new ToolStripMenuItem();
            reconcileToolStripMenuItem = new ToolStripMenuItem();
            reportsToolStripMenuItem = new ToolStripMenuItem();
            compareIncomeToolStripMenuItem = new ToolStripMenuItem();
            federal1120ToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripMenuItem();
            timeTrackingToolStripMenuItem = new ToolStripMenuItem();
            managePeopleToolStripMenuItem = new ToolStripMenuItem();
            manageProjectsToolStripMenuItem = new ToolStripMenuItem();
            enterTimeSheetToolStripMenuItem = new ToolStripMenuItem();
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
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, settingsToolStripMenuItem, reconcileToolStripMenuItem, reportsToolStripMenuItem, timeTrackingToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(9, 3, 0, 3);
            menuStrip1.Size = new Size(1143, 35);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { FileNewMenuItem, FileOpenToolStripMenuItem, FileSaveToolStripMenuItem, ImportToolStripMenuItem, ExportToolStripMenuItem, ExitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(54, 29);
            fileToolStripMenuItem.Text = "File";
            // 
            // FileNewMenuItem
            // 
            FileNewMenuItem.Name = "FileNewMenuItem";
            FileNewMenuItem.Size = new Size(169, 34);
            FileNewMenuItem.Text = "New";
            FileNewMenuItem.Click += FileNewMenuItem_Click;
            // 
            // FileOpenToolStripMenuItem
            // 
            FileOpenToolStripMenuItem.Name = "FileOpenToolStripMenuItem";
            FileOpenToolStripMenuItem.Size = new Size(169, 34);
            FileOpenToolStripMenuItem.Text = "Open";
            FileOpenToolStripMenuItem.Click += FileOpenToolStripMenuItem_Click;
            // 
            // FileSaveToolStripMenuItem
            // 
            FileSaveToolStripMenuItem.Name = "FileSaveToolStripMenuItem";
            FileSaveToolStripMenuItem.Size = new Size(169, 34);
            FileSaveToolStripMenuItem.Text = "Save";
            FileSaveToolStripMenuItem.Click += FileSaveToolStripMenuItem_Click;
            // 
            // ImportToolStripMenuItem
            // 
            ImportToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { IIFFileImportToolStripMenuItem, JournalExcelFileImportToolStripMenuItem });
            ImportToolStripMenuItem.Name = "ImportToolStripMenuItem";
            ImportToolStripMenuItem.Size = new Size(169, 34);
            ImportToolStripMenuItem.Text = "Import";
            // 
            // IIFFileImportToolStripMenuItem
            // 
            IIFFileImportToolStripMenuItem.Name = "IIFFileImportToolStripMenuItem";
            IIFFileImportToolStripMenuItem.Size = new Size(244, 34);
            IIFFileImportToolStripMenuItem.Text = "IIF File";
            IIFFileImportToolStripMenuItem.Click += IIFFileImportToolStripMenuItem_Click;
            // 
            // JournalExcelFileImportToolStripMenuItem
            // 
            JournalExcelFileImportToolStripMenuItem.Name = "JournalExcelFileImportToolStripMenuItem";
            JournalExcelFileImportToolStripMenuItem.Size = new Size(244, 34);
            JournalExcelFileImportToolStripMenuItem.Text = "Journal Excel File";
            JournalExcelFileImportToolStripMenuItem.Click += JournalExcelFileImportToolStripMenuItem_Click;
            // 
            // ExportToolStripMenuItem
            // 
            ExportToolStripMenuItem.Name = "ExportToolStripMenuItem";
            ExportToolStripMenuItem.Size = new Size(169, 34);
            ExportToolStripMenuItem.Text = "Export";
            // 
            // ExitToolStripMenuItem
            // 
            ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            ExitToolStripMenuItem.Size = new Size(169, 34);
            ExitToolStripMenuItem.Text = "Exit";
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { chartOfAccountsToolStripMenuItem, customerListToolStripMenuItem, ListOfPayTosToolStripMenuItem, businessDataToolStripMenuItem, InvoiceSettingsToolStripMenuItem });
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(92, 29);
            settingsToolStripMenuItem.Text = "Settings";
            // 
            // chartOfAccountsToolStripMenuItem
            // 
            chartOfAccountsToolStripMenuItem.Name = "chartOfAccountsToolStripMenuItem";
            chartOfAccountsToolStripMenuItem.Size = new Size(270, 34);
            chartOfAccountsToolStripMenuItem.Text = "Chart Of Accounts";
            chartOfAccountsToolStripMenuItem.Click += ChartOfAccountsToolStripMenuItem_Click;
            // 
            // customerListToolStripMenuItem
            // 
            customerListToolStripMenuItem.Name = "customerListToolStripMenuItem";
            customerListToolStripMenuItem.Size = new Size(270, 34);
            customerListToolStripMenuItem.Text = "Customer List";
            customerListToolStripMenuItem.Click += CustomerListToolStripMenuItem_Click;
            // 
            // ListOfPayTosToolStripMenuItem
            // 
            ListOfPayTosToolStripMenuItem.Name = "ListOfPayTosToolStripMenuItem";
            ListOfPayTosToolStripMenuItem.Size = new Size(270, 34);
            ListOfPayTosToolStripMenuItem.Text = "List of Pay To's";
            ListOfPayTosToolStripMenuItem.Click += ListOfPayTosToolStripMenuItem_Click;
            // 
            // businessDataToolStripMenuItem
            // 
            businessDataToolStripMenuItem.Name = "businessDataToolStripMenuItem";
            businessDataToolStripMenuItem.Size = new Size(270, 34);
            businessDataToolStripMenuItem.Text = "Business Data";
            businessDataToolStripMenuItem.Click += BusinessDataToolStripMenuItem_Click;
            // 
            // InvoiceSettingsToolStripMenuItem
            // 
            InvoiceSettingsToolStripMenuItem.Name = "InvoiceSettingsToolStripMenuItem";
            InvoiceSettingsToolStripMenuItem.Size = new Size(270, 34);
            InvoiceSettingsToolStripMenuItem.Text = "Invoice Settings";
            InvoiceSettingsToolStripMenuItem.Click += InvoiceSettingsToolStripMenuItem1_Click;
            // 
            // reconcileToolStripMenuItem
            // 
            reconcileToolStripMenuItem.Name = "reconcileToolStripMenuItem";
            reconcileToolStripMenuItem.Size = new Size(101, 29);
            reconcileToolStripMenuItem.Text = "Reconcile";
            reconcileToolStripMenuItem.Click += ReconcileToolStripMenuItem_Click;
            // 
            // reportsToolStripMenuItem
            // 
            reportsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { compareIncomeToolStripMenuItem, federal1120ToolStripMenuItem, toolStripMenuItem2 });
            reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
            reportsToolStripMenuItem.Size = new Size(89, 29);
            reportsToolStripMenuItem.Text = "Reports";
            // 
            // compareIncomeToolStripMenuItem
            // 
            compareIncomeToolStripMenuItem.Name = "compareIncomeToolStripMenuItem";
            compareIncomeToolStripMenuItem.Size = new Size(251, 34);
            compareIncomeToolStripMenuItem.Text = "Compare Income";
            compareIncomeToolStripMenuItem.Click += CompareIncomeToolStripMenuItem_Click;
            // 
            // federal1120ToolStripMenuItem
            // 
            federal1120ToolStripMenuItem.Name = "federal1120ToolStripMenuItem";
            federal1120ToolStripMenuItem.Size = new Size(251, 34);
            federal1120ToolStripMenuItem.Text = "Federal 1120";
            federal1120ToolStripMenuItem.Click += Federal1120ToolStripMenuItem_Click;
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(251, 34);
            toolStripMenuItem2.Text = "1099 / 1096";
            toolStripMenuItem2.Click += TaxReport1099_Click;
            // 
            // timeTrackingToolStripMenuItem
            // 
            timeTrackingToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { managePeopleToolStripMenuItem, manageProjectsToolStripMenuItem, enterTimeSheetToolStripMenuItem });
            timeTrackingToolStripMenuItem.Name = "timeTrackingToolStripMenuItem";
            timeTrackingToolStripMenuItem.Size = new Size(130, 29);
            timeTrackingToolStripMenuItem.Text = "TimeTracking";
            // 
            // managePeopleToolStripMenuItem
            // 
            managePeopleToolStripMenuItem.Name = "managePeopleToolStripMenuItem";
            managePeopleToolStripMenuItem.Size = new Size(270, 34);
            managePeopleToolStripMenuItem.Text = "Manage People";
            managePeopleToolStripMenuItem.Click += ManagePeopleToolStripMenuItem_Click;
            // 
            // manageProjectsToolStripMenuItem
            // 
            manageProjectsToolStripMenuItem.Name = "manageProjectsToolStripMenuItem";
            manageProjectsToolStripMenuItem.Size = new Size(270, 34);
            manageProjectsToolStripMenuItem.Text = "Manage Projects";
            manageProjectsToolStripMenuItem.Click += ManageProjectsToolStripMenuItem_Click;
            // 
            // enterTimeSheetToolStripMenuItem
            // 
            enterTimeSheetToolStripMenuItem.Name = "enterTimeSheetToolStripMenuItem";
            enterTimeSheetToolStripMenuItem.Size = new Size(270, 34);
            enterTimeSheetToolStripMenuItem.Text = "Enter Time Sheet";
            enterTimeSheetToolStripMenuItem.Click += EnterTimeSheetToolStripMenuItem_Click;
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
            ViewLedgerButton.Location = new Point(167, 138);
            ViewLedgerButton.Margin = new Padding(4, 5, 4, 5);
            ViewLedgerButton.Name = "ViewLedgerButton";
            ViewLedgerButton.Size = new Size(204, 38);
            ViewLedgerButton.TabIndex = 1;
            ViewLedgerButton.Text = "View Ledger";
            ViewLedgerButton.UseVisualStyleBackColor = true;
            ViewLedgerButton.Click += ViewLedgerButton_Click;
            // 
            // WriteChecksButton
            // 
            WriteChecksButton.Enabled = false;
            WriteChecksButton.Location = new Point(167, 232);
            WriteChecksButton.Margin = new Padding(4, 5, 4, 5);
            WriteChecksButton.Name = "WriteChecksButton";
            WriteChecksButton.Size = new Size(204, 38);
            WriteChecksButton.TabIndex = 2;
            WriteChecksButton.Text = "Write Checks";
            WriteChecksButton.UseVisualStyleBackColor = true;
            WriteChecksButton.Click += WriteChecksButton_Click;
            // 
            // CreateInvoiceButton
            // 
            CreateInvoiceButton.Enabled = false;
            CreateInvoiceButton.Location = new Point(167, 332);
            CreateInvoiceButton.Margin = new Padding(4, 5, 4, 5);
            CreateInvoiceButton.Name = "CreateInvoiceButton";
            CreateInvoiceButton.Size = new Size(204, 38);
            CreateInvoiceButton.TabIndex = 3;
            CreateInvoiceButton.Text = "Create Invoice";
            CreateInvoiceButton.UseVisualStyleBackColor = true;
            CreateInvoiceButton.Click += CreateInvoiceButton_Click;
            // 
            // ReceivePaymentButton
            // 
            ReceivePaymentButton.Enabled = false;
            ReceivePaymentButton.Location = new Point(167, 443);
            ReceivePaymentButton.Margin = new Padding(4, 5, 4, 5);
            ReceivePaymentButton.Name = "ReceivePaymentButton";
            ReceivePaymentButton.Size = new Size(204, 38);
            ReceivePaymentButton.TabIndex = 4;
            ReceivePaymentButton.Text = "Receive Payment";
            ReceivePaymentButton.UseVisualStyleBackColor = true;
            ReceivePaymentButton.Click += ReceivePaymentButton_Click;
            // 
            // ShowInvoicesButton
            // 
            ShowInvoicesButton.Enabled = false;
            ShowInvoicesButton.Location = new Point(167, 542);
            ShowInvoicesButton.Name = "ShowInvoicesButton";
            ShowInvoicesButton.Size = new Size(204, 33);
            ShowInvoicesButton.TabIndex = 5;
            ShowInvoicesButton.Text = "Show Invoices";
            ShowInvoicesButton.UseVisualStyleBackColor = true;
            ShowInvoicesButton.Click += ShowInvoicesButton_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(17, 40);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(331, 25);
            label3.TabIndex = 7;
            label3.Text = "Copyright 2024 Prarie Trail Software, Inc.";
            label3.Visible = false;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1143, 750);
            Controls.Add(label3);
            Controls.Add(ShowInvoicesButton);
            Controls.Add(ReceivePaymentButton);
            Controls.Add(CreateInvoiceButton);
            Controls.Add(WriteChecksButton);
            Controls.Add(ViewLedgerButton);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(4, 5, 4, 5);
            Name = "MainForm";
            Text = "Business Checking";
            FormClosing += MainForm_FormClosing;
            Shown += MainForm_Shown;
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
        private ToolStripMenuItem InvoiceSettingsToolStripMenuItem;
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
        private ToolStripMenuItem timeTrackingToolStripMenuItem;
        private ToolStripMenuItem managePeopleToolStripMenuItem;
        private ToolStripMenuItem manageProjectsToolStripMenuItem;
        private ToolStripMenuItem enterTimeSheetToolStripMenuItem;
    }
}