namespace BusinessCheckBook
{
    partial class CreateInvoiceForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            CustomerComboBox = new ComboBox();
            label2 = new Label();
            InvoiceDateTimePicker = new DateTimePicker();
            label3 = new Label();
            InvoiceNumberTextBox = new TextBox();
            label4 = new Label();
            label5 = new Label();
            DueDateDateTimePicker = new DateTimePicker();
            label6 = new Label();
            TotalTextBox = new TextBox();
            label7 = new Label();
            BalanceDueTextBox = new TextBox();
            PrintButton = new Button();
            SaveToBatchButton = new Button();
            PrintBatchButton = new Button();
            ClearButton = new Button();
            label8 = new Label();
            TaxTextBox = new TextBox();
            label9 = new Label();
            CustomerMemoTextBox = new TextBox();
            panel1 = new Panel();
            OpenBalanceTextBox = new TextBox();
            label10 = new Label();
            panel2 = new Panel();
            label11 = new Label();
            RecentTransactionsTextBox = new TextBox();
            panel3 = new Panel();
            BillToAddress5TextBox = new TextBox();
            BillToAddress4TextBox = new TextBox();
            BillToAddress3TextBox = new TextBox();
            BillToAddress2TextBox = new TextBox();
            BillToAddress1TextBox = new TextBox();
            InvoiceDetailDataGridView = new DataGridView();
            SaveToHistoryButton = new Button();
            PrintInvoiceDialog = new PrintDialog();
            AccountListBox = new ListBox();
            label12 = new Label();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)InvoiceDetailDataGridView).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(47, 43);
            label1.Name = "label1";
            label1.Size = new Size(61, 15);
            label1.TabIndex = 0;
            label1.Text = "To Whom:";
            // 
            // CustomerComboBox
            // 
            CustomerComboBox.FormattingEnabled = true;
            CustomerComboBox.Location = new Point(141, 40);
            CustomerComboBox.Name = "CustomerComboBox";
            CustomerComboBox.Size = new Size(221, 23);
            CustomerComboBox.TabIndex = 1;
            CustomerComboBox.SelectedIndexChanged += CustomerComboBox_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(556, 67);
            label2.Name = "label2";
            label2.Size = new Size(75, 15);
            label2.TabIndex = 2;
            label2.Text = "Invoice Date:";
            // 
            // InvoiceDateTimePicker
            // 
            InvoiceDateTimePicker.Format = DateTimePickerFormat.Short;
            InvoiceDateTimePicker.Location = new Point(652, 61);
            InvoiceDateTimePicker.Name = "InvoiceDateTimePicker";
            InvoiceDateTimePicker.Size = new Size(200, 23);
            InvoiceDateTimePicker.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(576, 103);
            label3.Name = "label3";
            label3.Size = new Size(55, 15);
            label3.TabIndex = 6;
            label3.Text = "Invoice #";
            // 
            // InvoiceNumberTextBox
            // 
            InvoiceNumberTextBox.Location = new Point(652, 100);
            InvoiceNumberTextBox.Name = "InvoiceNumberTextBox";
            InvoiceNumberTextBox.Size = new Size(136, 23);
            InvoiceNumberTextBox.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(47, 88);
            label4.Name = "label4";
            label4.Size = new Size(41, 15);
            label4.TabIndex = 8;
            label4.Text = "Bill To:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(573, 143);
            label5.Name = "label5";
            label5.Size = new Size(58, 15);
            label5.TabIndex = 10;
            label5.Text = "Due Date:";
            // 
            // DueDateDateTimePicker
            // 
            DueDateDateTimePicker.Format = DateTimePickerFormat.Short;
            DueDateDateTimePicker.Location = new Point(652, 137);
            DueDateDateTimePicker.Name = "DueDateDateTimePicker";
            DueDateDateTimePicker.Size = new Size(200, 23);
            DueDateDateTimePicker.TabIndex = 11;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(797, 463);
            label6.Name = "label6";
            label6.Size = new Size(35, 15);
            label6.TabIndex = 12;
            label6.Text = "Total:";
            // 
            // TotalTextBox
            // 
            TotalTextBox.Location = new Point(857, 460);
            TotalTextBox.Name = "TotalTextBox";
            TotalTextBox.Size = new Size(100, 23);
            TotalTextBox.TabIndex = 13;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(757, 505);
            label7.Name = "label7";
            label7.Size = new Size(75, 15);
            label7.TabIndex = 14;
            label7.Text = "Balance Due:";
            // 
            // BalanceDueTextBox
            // 
            BalanceDueTextBox.Location = new Point(857, 502);
            BalanceDueTextBox.Name = "BalanceDueTextBox";
            BalanceDueTextBox.Size = new Size(100, 23);
            BalanceDueTextBox.TabIndex = 15;
            // 
            // PrintButton
            // 
            PrintButton.Location = new Point(480, 12);
            PrintButton.Name = "PrintButton";
            PrintButton.Size = new Size(75, 23);
            PrintButton.TabIndex = 16;
            PrintButton.Text = "Print";
            PrintButton.UseVisualStyleBackColor = true;
            PrintButton.Click += PrintButton_Click;
            // 
            // SaveToBatchButton
            // 
            SaveToBatchButton.Location = new Point(623, 12);
            SaveToBatchButton.Name = "SaveToBatchButton";
            SaveToBatchButton.Size = new Size(99, 23);
            SaveToBatchButton.TabIndex = 17;
            SaveToBatchButton.Text = "Save To Batch";
            SaveToBatchButton.UseVisualStyleBackColor = true;
            SaveToBatchButton.Click += SaveToBatchButton_Click;
            // 
            // PrintBatchButton
            // 
            PrintBatchButton.Location = new Point(767, 12);
            PrintBatchButton.Name = "PrintBatchButton";
            PrintBatchButton.Size = new Size(85, 23);
            PrintBatchButton.TabIndex = 18;
            PrintBatchButton.Text = "Print Batch";
            PrintBatchButton.UseVisualStyleBackColor = true;
            PrintBatchButton.Click += PrintBatchButton_Click;
            // 
            // ClearButton
            // 
            ClearButton.Location = new Point(916, 546);
            ClearButton.Name = "ClearButton";
            ClearButton.Size = new Size(75, 23);
            ClearButton.TabIndex = 19;
            ClearButton.Text = "Clear";
            ClearButton.UseVisualStyleBackColor = true;
            ClearButton.Click += ClearButton_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(805, 433);
            label8.Name = "label8";
            label8.Size = new Size(27, 15);
            label8.TabIndex = 20;
            label8.Text = "Tax:";
            // 
            // TaxTextBox
            // 
            TaxTextBox.Location = new Point(857, 430);
            TaxTextBox.Name = "TaxTextBox";
            TaxTextBox.Size = new Size(100, 23);
            TaxTextBox.TabIndex = 21;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(85, 420);
            label9.Name = "label9";
            label9.Size = new Size(123, 15);
            label9.TabIndex = 22;
            label9.Text = "Message To Customer";
            // 
            // CustomerMemoTextBox
            // 
            CustomerMemoTextBox.Location = new Point(85, 438);
            CustomerMemoTextBox.Multiline = true;
            CustomerMemoTextBox.Name = "CustomerMemoTextBox";
            CustomerMemoTextBox.Size = new Size(277, 96);
            CustomerMemoTextBox.TabIndex = 23;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(OpenBalanceTextBox);
            panel1.Controls.Add(label10);
            panel1.Location = new Point(1028, 98);
            panel1.Name = "panel1";
            panel1.Size = new Size(213, 102);
            panel1.TabIndex = 24;
            // 
            // OpenBalanceTextBox
            // 
            OpenBalanceTextBox.Location = new Point(81, 51);
            OpenBalanceTextBox.Name = "OpenBalanceTextBox";
            OpenBalanceTextBox.Size = new Size(100, 23);
            OpenBalanceTextBox.TabIndex = 1;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(32, 25);
            label10.Name = "label10";
            label10.Size = new Size(83, 15);
            label10.TabIndex = 0;
            label10.Text = "Open Balance:";
            // 
            // panel2
            // 
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(label11);
            panel2.Controls.Add(RecentTransactionsTextBox);
            panel2.Location = new Point(1028, 206);
            panel2.Name = "panel2";
            panel2.Size = new Size(213, 266);
            panel2.TabIndex = 25;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(18, 11);
            label11.Name = "label11";
            label11.Size = new Size(111, 15);
            label11.TabIndex = 1;
            label11.Text = "Recent Transactions";
            // 
            // RecentTransactionsTextBox
            // 
            RecentTransactionsTextBox.Location = new Point(30, 40);
            RecentTransactionsTextBox.Multiline = true;
            RecentTransactionsTextBox.Name = "RecentTransactionsTextBox";
            RecentTransactionsTextBox.Size = new Size(164, 203);
            RecentTransactionsTextBox.TabIndex = 0;
            // 
            // panel3
            // 
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel3.Controls.Add(BillToAddress5TextBox);
            panel3.Controls.Add(BillToAddress4TextBox);
            panel3.Controls.Add(BillToAddress3TextBox);
            panel3.Controls.Add(BillToAddress2TextBox);
            panel3.Controls.Add(BillToAddress1TextBox);
            panel3.Location = new Point(141, 69);
            panel3.Name = "panel3";
            panel3.Size = new Size(221, 115);
            panel3.TabIndex = 26;
            // 
            // BillToAddress5TextBox
            // 
            BillToAddress5TextBox.Location = new Point(-1, 87);
            BillToAddress5TextBox.Name = "BillToAddress5TextBox";
            BillToAddress5TextBox.ReadOnly = true;
            BillToAddress5TextBox.Size = new Size(221, 23);
            BillToAddress5TextBox.TabIndex = 4;
            // 
            // BillToAddress4TextBox
            // 
            BillToAddress4TextBox.Location = new Point(-1, 65);
            BillToAddress4TextBox.Name = "BillToAddress4TextBox";
            BillToAddress4TextBox.ReadOnly = true;
            BillToAddress4TextBox.Size = new Size(221, 23);
            BillToAddress4TextBox.TabIndex = 3;
            // 
            // BillToAddress3TextBox
            // 
            BillToAddress3TextBox.Location = new Point(-1, 42);
            BillToAddress3TextBox.Name = "BillToAddress3TextBox";
            BillToAddress3TextBox.ReadOnly = true;
            BillToAddress3TextBox.Size = new Size(221, 23);
            BillToAddress3TextBox.TabIndex = 2;
            // 
            // BillToAddress2TextBox
            // 
            BillToAddress2TextBox.Location = new Point(-1, 18);
            BillToAddress2TextBox.Name = "BillToAddress2TextBox";
            BillToAddress2TextBox.ReadOnly = true;
            BillToAddress2TextBox.Size = new Size(221, 23);
            BillToAddress2TextBox.TabIndex = 1;
            // 
            // BillToAddress1TextBox
            // 
            BillToAddress1TextBox.Location = new Point(-1, -1);
            BillToAddress1TextBox.Name = "BillToAddress1TextBox";
            BillToAddress1TextBox.ReadOnly = true;
            BillToAddress1TextBox.Size = new Size(221, 23);
            BillToAddress1TextBox.TabIndex = 0;
            // 
            // InvoiceDetailDataGridView
            // 
            InvoiceDetailDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            InvoiceDetailDataGridView.EditMode = DataGridViewEditMode.EditOnEnter;
            InvoiceDetailDataGridView.Location = new Point(85, 190);
            InvoiceDetailDataGridView.Name = "InvoiceDetailDataGridView";
            InvoiceDetailDataGridView.RowHeadersWidth = 62;
            InvoiceDetailDataGridView.RowTemplate.Height = 25;
            InvoiceDetailDataGridView.Size = new Size(883, 226);
            InvoiceDetailDataGridView.TabIndex = 29;
            InvoiceDetailDataGridView.CellEnter += InvoiceDetailDataGridView_CellEnter;
            InvoiceDetailDataGridView.CellValueChanged += InvoiceDetailDataGridView_CellValueChanged;
            // 
            // SaveToHistoryButton
            // 
            SaveToHistoryButton.Location = new Point(931, 12);
            SaveToHistoryButton.Name = "SaveToHistoryButton";
            SaveToHistoryButton.Size = new Size(131, 23);
            SaveToHistoryButton.TabIndex = 28;
            SaveToHistoryButton.Text = "Save To History";
            SaveToHistoryButton.UseVisualStyleBackColor = true;
            SaveToHistoryButton.Click += SaveToHistoryButton_Click;
            // 
            // PrintInvoiceDialog
            // 
            PrintInvoiceDialog.UseEXDialog = true;
            // 
            // AccountListBox
            // 
            AccountListBox.FormattingEnabled = true;
            AccountListBox.ItemHeight = 15;
            AccountListBox.Location = new Point(113, 206);
            AccountListBox.Margin = new Padding(2, 2, 2, 2);
            AccountListBox.Name = "AccountListBox";
            AccountListBox.Size = new Size(159, 199);
            AccountListBox.TabIndex = 27;
            AccountListBox.Visible = false;
            AccountListBox.SelectedIndexChanged += AccountListBox_SelectedIndexChanged;
            AccountListBox.Leave += AccountListBox_Leave;
            AccountListBox.PreviewKeyDown += AccountListBox_PreviewKeyDown;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(24, 9);
            label12.Name = "label12";
            label12.Size = new Size(218, 15);
            label12.TabIndex = 30;
            label12.Text = "Copyright 2023 Prarie Trail Software, Inc.";
            label12.Visible = false;
            // 
            // CreateInvoiceForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1284, 589);
            Controls.Add(label12);
            Controls.Add(AccountListBox);
            Controls.Add(SaveToHistoryButton);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(CustomerMemoTextBox);
            Controls.Add(label9);
            Controls.Add(TaxTextBox);
            Controls.Add(label8);
            Controls.Add(ClearButton);
            Controls.Add(PrintBatchButton);
            Controls.Add(SaveToBatchButton);
            Controls.Add(PrintButton);
            Controls.Add(BalanceDueTextBox);
            Controls.Add(label7);
            Controls.Add(TotalTextBox);
            Controls.Add(label6);
            Controls.Add(DueDateDateTimePicker);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(InvoiceNumberTextBox);
            Controls.Add(label3);
            Controls.Add(InvoiceDateTimePicker);
            Controls.Add(label2);
            Controls.Add(CustomerComboBox);
            Controls.Add(label1);
            Controls.Add(InvoiceDetailDataGridView);
            Name = "CreateInvoiceForm";
            Text = "Create Invoice";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)InvoiceDetailDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ComboBox CustomerComboBox;
        private Label label2;
        private DateTimePicker InvoiceDateTimePicker;
        private Label label3;
        private TextBox InvoiceNumberTextBox;
        private Label label4;
        private Label label5;
        private DateTimePicker DueDateDateTimePicker;
        private Label label6;
        private TextBox TotalTextBox;
        private Label label7;
        private TextBox BalanceDueTextBox;
        private Button PrintButton;
        private Button SaveToBatchButton;
        private Button PrintBatchButton;
        private Button ClearButton;
        private Label label8;
        private TextBox TaxTextBox;
        private Label label9;
        private TextBox CustomerMemoTextBox;
        private Panel panel1;
        private TextBox OpenBalanceTextBox;
        private Label label10;
        private Panel panel2;
        private Label label11;
        private TextBox RecentTransactionsTextBox;
        private Panel panel3;
        private TextBox BillToAddress2TextBox;
        private TextBox BillToAddress1TextBox;
        private TextBox BillToAddress4TextBox;
        private TextBox BillToAddress3TextBox;
        private TextBox BillToAddress5TextBox;
        private DataGridView InvoiceDetailDataGridView;
        private Button SaveToHistoryButton;
        private PrintDialog PrintInvoiceDialog;
        private ListBox AccountListBox;
        private Label label12;
    }
}