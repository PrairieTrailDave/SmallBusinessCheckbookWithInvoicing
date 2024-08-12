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
            label1.Location = new Point(67, 72);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(93, 25);
            label1.TabIndex = 0;
            label1.Text = "To Whom:";
            // 
            // CustomerComboBox
            // 
            CustomerComboBox.FormattingEnabled = true;
            CustomerComboBox.Location = new Point(201, 67);
            CustomerComboBox.Margin = new Padding(4, 5, 4, 5);
            CustomerComboBox.Name = "CustomerComboBox";
            CustomerComboBox.Size = new Size(314, 33);
            CustomerComboBox.TabIndex = 1;
            CustomerComboBox.SelectedIndexChanged += CustomerComboBox_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(794, 112);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(114, 25);
            label2.TabIndex = 2;
            label2.Text = "Invoice Date:";
            // 
            // InvoiceDateTimePicker
            // 
            InvoiceDateTimePicker.Format = DateTimePickerFormat.Short;
            InvoiceDateTimePicker.Location = new Point(931, 102);
            InvoiceDateTimePicker.Margin = new Padding(4, 5, 4, 5);
            InvoiceDateTimePicker.Name = "InvoiceDateTimePicker";
            InvoiceDateTimePicker.Size = new Size(284, 31);
            InvoiceDateTimePicker.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(823, 172);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(84, 25);
            label3.TabIndex = 6;
            label3.Text = "Invoice #";
            // 
            // InvoiceNumberTextBox
            // 
            InvoiceNumberTextBox.Location = new Point(931, 167);
            InvoiceNumberTextBox.Margin = new Padding(4, 5, 4, 5);
            InvoiceNumberTextBox.Name = "InvoiceNumberTextBox";
            InvoiceNumberTextBox.Size = new Size(193, 31);
            InvoiceNumberTextBox.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(67, 147);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(61, 25);
            label4.TabIndex = 8;
            label4.Text = "Bill To:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(819, 238);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(90, 25);
            label5.TabIndex = 10;
            label5.Text = "Due Date:";
            // 
            // DueDateDateTimePicker
            // 
            DueDateDateTimePicker.Format = DateTimePickerFormat.Short;
            DueDateDateTimePicker.Location = new Point(931, 228);
            DueDateDateTimePicker.Margin = new Padding(4, 5, 4, 5);
            DueDateDateTimePicker.Name = "DueDateDateTimePicker";
            DueDateDateTimePicker.Size = new Size(284, 31);
            DueDateDateTimePicker.TabIndex = 11;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(1139, 772);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(53, 25);
            label6.TabIndex = 12;
            label6.Text = "Total:";
            // 
            // TotalTextBox
            // 
            TotalTextBox.Location = new Point(1224, 767);
            TotalTextBox.Margin = new Padding(4, 5, 4, 5);
            TotalTextBox.Name = "TotalTextBox";
            TotalTextBox.Size = new Size(141, 31);
            TotalTextBox.TabIndex = 13;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(1081, 842);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(112, 25);
            label7.TabIndex = 14;
            label7.Text = "Balance Due:";
            // 
            // BalanceDueTextBox
            // 
            BalanceDueTextBox.Location = new Point(1224, 837);
            BalanceDueTextBox.Margin = new Padding(4, 5, 4, 5);
            BalanceDueTextBox.Name = "BalanceDueTextBox";
            BalanceDueTextBox.Size = new Size(141, 31);
            BalanceDueTextBox.TabIndex = 15;
            // 
            // PrintButton
            // 
            PrintButton.Location = new Point(686, 20);
            PrintButton.Margin = new Padding(4, 5, 4, 5);
            PrintButton.Name = "PrintButton";
            PrintButton.Size = new Size(107, 38);
            PrintButton.TabIndex = 16;
            PrintButton.Text = "Print";
            PrintButton.UseVisualStyleBackColor = true;
            PrintButton.Click += PrintButton_Click;
            // 
            // SaveToBatchButton
            // 
            SaveToBatchButton.Location = new Point(890, 20);
            SaveToBatchButton.Margin = new Padding(4, 5, 4, 5);
            SaveToBatchButton.Name = "SaveToBatchButton";
            SaveToBatchButton.Size = new Size(141, 38);
            SaveToBatchButton.TabIndex = 17;
            SaveToBatchButton.Text = "Save To Batch";
            SaveToBatchButton.UseVisualStyleBackColor = true;
            SaveToBatchButton.Click += SaveToBatchButton_Click;
            // 
            // PrintBatchButton
            // 
            PrintBatchButton.Location = new Point(1096, 20);
            PrintBatchButton.Margin = new Padding(4, 5, 4, 5);
            PrintBatchButton.Name = "PrintBatchButton";
            PrintBatchButton.Size = new Size(121, 38);
            PrintBatchButton.TabIndex = 18;
            PrintBatchButton.Text = "Print Batch";
            PrintBatchButton.UseVisualStyleBackColor = true;
            PrintBatchButton.Click += PrintBatchButton_Click;
            // 
            // ClearButton
            // 
            ClearButton.Location = new Point(1309, 910);
            ClearButton.Margin = new Padding(4, 5, 4, 5);
            ClearButton.Name = "ClearButton";
            ClearButton.Size = new Size(107, 38);
            ClearButton.TabIndex = 19;
            ClearButton.Text = "Clear";
            ClearButton.UseVisualStyleBackColor = true;
            ClearButton.Click += ClearButton_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(1150, 722);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(40, 25);
            label8.TabIndex = 20;
            label8.Text = "Tax:";
            // 
            // TaxTextBox
            // 
            TaxTextBox.Location = new Point(1224, 717);
            TaxTextBox.Margin = new Padding(4, 5, 4, 5);
            TaxTextBox.Name = "TaxTextBox";
            TaxTextBox.Size = new Size(141, 31);
            TaxTextBox.TabIndex = 21;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(121, 700);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(187, 25);
            label9.TabIndex = 22;
            label9.Text = "Message To Customer";
            // 
            // CustomerMemoTextBox
            // 
            CustomerMemoTextBox.Location = new Point(121, 730);
            CustomerMemoTextBox.Margin = new Padding(4, 5, 4, 5);
            CustomerMemoTextBox.Multiline = true;
            CustomerMemoTextBox.Name = "CustomerMemoTextBox";
            CustomerMemoTextBox.Size = new Size(394, 157);
            CustomerMemoTextBox.TabIndex = 23;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(OpenBalanceTextBox);
            panel1.Controls.Add(label10);
            panel1.Location = new Point(1469, 163);
            panel1.Margin = new Padding(4, 5, 4, 5);
            panel1.Name = "panel1";
            panel1.Size = new Size(303, 169);
            panel1.TabIndex = 24;
            // 
            // OpenBalanceTextBox
            // 
            OpenBalanceTextBox.Location = new Point(116, 85);
            OpenBalanceTextBox.Margin = new Padding(4, 5, 4, 5);
            OpenBalanceTextBox.Name = "OpenBalanceTextBox";
            OpenBalanceTextBox.Size = new Size(141, 31);
            OpenBalanceTextBox.TabIndex = 1;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(46, 42);
            label10.Margin = new Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new Size(124, 25);
            label10.TabIndex = 0;
            label10.Text = "Open Balance:";
            // 
            // panel2
            // 
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(label11);
            panel2.Controls.Add(RecentTransactionsTextBox);
            panel2.Location = new Point(1469, 343);
            panel2.Margin = new Padding(4, 5, 4, 5);
            panel2.Name = "panel2";
            panel2.Size = new Size(303, 442);
            panel2.TabIndex = 25;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(26, 18);
            label11.Margin = new Padding(4, 0, 4, 0);
            label11.Name = "label11";
            label11.Size = new Size(165, 25);
            label11.TabIndex = 1;
            label11.Text = "Recent Transactions";
            // 
            // RecentTransactionsTextBox
            // 
            RecentTransactionsTextBox.Location = new Point(43, 67);
            RecentTransactionsTextBox.Margin = new Padding(4, 5, 4, 5);
            RecentTransactionsTextBox.Multiline = true;
            RecentTransactionsTextBox.Name = "RecentTransactionsTextBox";
            RecentTransactionsTextBox.Size = new Size(233, 336);
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
            panel3.Location = new Point(201, 115);
            panel3.Margin = new Padding(4, 5, 4, 5);
            panel3.Name = "panel3";
            panel3.Size = new Size(315, 190);
            panel3.TabIndex = 26;
            // 
            // BillToAddress5TextBox
            // 
            BillToAddress5TextBox.Location = new Point(-1, 145);
            BillToAddress5TextBox.Margin = new Padding(4, 5, 4, 5);
            BillToAddress5TextBox.Name = "BillToAddress5TextBox";
            BillToAddress5TextBox.ReadOnly = true;
            BillToAddress5TextBox.Size = new Size(314, 31);
            BillToAddress5TextBox.TabIndex = 4;
            // 
            // BillToAddress4TextBox
            // 
            BillToAddress4TextBox.Location = new Point(-1, 108);
            BillToAddress4TextBox.Margin = new Padding(4, 5, 4, 5);
            BillToAddress4TextBox.Name = "BillToAddress4TextBox";
            BillToAddress4TextBox.ReadOnly = true;
            BillToAddress4TextBox.Size = new Size(314, 31);
            BillToAddress4TextBox.TabIndex = 3;
            // 
            // BillToAddress3TextBox
            // 
            BillToAddress3TextBox.Location = new Point(-1, 70);
            BillToAddress3TextBox.Margin = new Padding(4, 5, 4, 5);
            BillToAddress3TextBox.Name = "BillToAddress3TextBox";
            BillToAddress3TextBox.ReadOnly = true;
            BillToAddress3TextBox.Size = new Size(314, 31);
            BillToAddress3TextBox.TabIndex = 2;
            // 
            // BillToAddress2TextBox
            // 
            BillToAddress2TextBox.Location = new Point(-1, 30);
            BillToAddress2TextBox.Margin = new Padding(4, 5, 4, 5);
            BillToAddress2TextBox.Name = "BillToAddress2TextBox";
            BillToAddress2TextBox.ReadOnly = true;
            BillToAddress2TextBox.Size = new Size(314, 31);
            BillToAddress2TextBox.TabIndex = 1;
            // 
            // BillToAddress1TextBox
            // 
            BillToAddress1TextBox.Location = new Point(-1, -2);
            BillToAddress1TextBox.Margin = new Padding(4, 5, 4, 5);
            BillToAddress1TextBox.Name = "BillToAddress1TextBox";
            BillToAddress1TextBox.ReadOnly = true;
            BillToAddress1TextBox.Size = new Size(314, 31);
            BillToAddress1TextBox.TabIndex = 0;
            // 
            // InvoiceDetailDataGridView
            // 
            InvoiceDetailDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            InvoiceDetailDataGridView.EditMode = DataGridViewEditMode.EditOnEnter;
            InvoiceDetailDataGridView.Location = new Point(121, 317);
            InvoiceDetailDataGridView.Margin = new Padding(4, 5, 4, 5);
            InvoiceDetailDataGridView.Name = "InvoiceDetailDataGridView";
            InvoiceDetailDataGridView.RowHeadersWidth = 62;
            InvoiceDetailDataGridView.RowTemplate.Height = 25;
            InvoiceDetailDataGridView.Size = new Size(1261, 377);
            InvoiceDetailDataGridView.TabIndex = 29;
            InvoiceDetailDataGridView.CellValueChanged += InvoiceDetailDataGridView_CellValueChanged;
            // 
            // SaveToHistoryButton
            // 
            SaveToHistoryButton.Location = new Point(1330, 20);
            SaveToHistoryButton.Margin = new Padding(4, 5, 4, 5);
            SaveToHistoryButton.Name = "SaveToHistoryButton";
            SaveToHistoryButton.Size = new Size(187, 38);
            SaveToHistoryButton.TabIndex = 28;
            SaveToHistoryButton.Text = "Save To History";
            SaveToHistoryButton.UseVisualStyleBackColor = true;
            SaveToHistoryButton.Click += SaveToHistoryButton_Click;
            // 
            // PrintInvoiceDialog
            // 
            PrintInvoiceDialog.UseEXDialog = true;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(34, 15);
            label12.Margin = new Padding(4, 0, 4, 0);
            label12.Name = "label12";
            label12.Size = new Size(331, 25);
            label12.TabIndex = 30;
            label12.Text = "Copyright 2023 Prarie Trail Software, Inc.";
            label12.Visible = false;
            // 
            // CreateInvoiceForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1834, 982);
            Controls.Add(label12);
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
            Margin = new Padding(4, 5, 4, 5);
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
        private Label label12;
    }
}