namespace BusinessCheckBook
{
    partial class ReconcileForm
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
            ChangeTransactionValueButton = new Button();
            AddFeesAndInterestButton = new Button();
            ReconciliationDateTimePicker = new DateTimePicker();
            label13 = new Label();
            AddMissingTransactionButton = new Button();
            ClearedBalanceTextBox = new TextBox();
            label5 = new Label();
            InterestEarnedTextBox = new TextBox();
            label4 = new Label();
            BankFeesTextBox = new TextBox();
            label3 = new Label();
            EndingBalanceTextBox = new TextBox();
            label2 = new Label();
            LastReconciledBalanceTextBox = new TextBox();
            label1 = new Label();
            CancelFormButton = new Button();
            DoneButton = new Button();
            label6 = new Label();
            ChecksDataGridView = new DataGridView();
            DepositsDataGridView = new DataGridView();
            label7 = new Label();
            ((System.ComponentModel.ISupportInitialize)ChecksDataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DepositsDataGridView).BeginInit();
            SuspendLayout();
            // 
            // ChangeTransactionValueButton
            // 
            ChangeTransactionValueButton.Location = new Point(60, 404);
            ChangeTransactionValueButton.Name = "ChangeTransactionValueButton";
            ChangeTransactionValueButton.Size = new Size(343, 45);
            ChangeTransactionValueButton.TabIndex = 35;
            ChangeTransactionValueButton.Text = "Change the value of a transaction";
            ChangeTransactionValueButton.UseVisualStyleBackColor = true;
            ChangeTransactionValueButton.Click += ChangeTransactionValueButton_Click;
            // 
            // AddFeesAndInterestButton
            // 
            AddFeesAndInterestButton.Location = new Point(59, 296);
            AddFeesAndInterestButton.Name = "AddFeesAndInterestButton";
            AddFeesAndInterestButton.Size = new Size(343, 50);
            AddFeesAndInterestButton.TabIndex = 34;
            AddFeesAndInterestButton.Text = "Add fees and interest to Ledger";
            AddFeesAndInterestButton.UseVisualStyleBackColor = true;
            AddFeesAndInterestButton.Click += AddFeesAndInterestButton_Click;
            // 
            // ReconciliationDateTimePicker
            // 
            ReconciliationDateTimePicker.CustomFormat = "MM/dd/yyyy";
            ReconciliationDateTimePicker.Location = new Point(252, 56);
            ReconciliationDateTimePicker.Name = "ReconciliationDateTimePicker";
            ReconciliationDateTimePicker.Size = new Size(150, 31);
            ReconciliationDateTimePicker.TabIndex = 33;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(59, 56);
            label13.Name = "label13";
            label13.Size = new Size(162, 25);
            label13.TabIndex = 32;
            label13.Text = "Reconciliation Date";
            // 
            // AddMissingTransactionButton
            // 
            AddMissingTransactionButton.Location = new Point(60, 352);
            AddMissingTransactionButton.Name = "AddMissingTransactionButton";
            AddMissingTransactionButton.Size = new Size(343, 45);
            AddMissingTransactionButton.TabIndex = 31;
            AddMissingTransactionButton.Text = "Add A Missing Transaction";
            AddMissingTransactionButton.UseVisualStyleBackColor = true;
            AddMissingTransactionButton.Click += AddMissingTransactionButton_Click;
            // 
            // ClearedBalanceTextBox
            // 
            ClearedBalanceTextBox.Location = new Point(252, 244);
            ClearedBalanceTextBox.Name = "ClearedBalanceTextBox";
            ClearedBalanceTextBox.ReadOnly = true;
            ClearedBalanceTextBox.Size = new Size(150, 31);
            ClearedBalanceTextBox.TabIndex = 30;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(59, 244);
            label5.Name = "label5";
            label5.Size = new Size(135, 25);
            label5.TabIndex = 29;
            label5.Text = "Cleared Balance";
            // 
            // InterestEarnedTextBox
            // 
            InterestEarnedTextBox.Location = new Point(252, 199);
            InterestEarnedTextBox.Name = "InterestEarnedTextBox";
            InterestEarnedTextBox.Size = new Size(150, 31);
            InterestEarnedTextBox.TabIndex = 28;
            InterestEarnedTextBox.KeyPress += InterestEarnedTextBox_KeyPress;
            InterestEarnedTextBox.Leave += InterestEarnedTextBox_Leave;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(59, 202);
            label4.Name = "label4";
            label4.Size = new Size(130, 25);
            label4.TabIndex = 27;
            label4.Text = "Interest Earned";
            // 
            // BankFeesTextBox
            // 
            BankFeesTextBox.Location = new Point(252, 161);
            BankFeesTextBox.Name = "BankFeesTextBox";
            BankFeesTextBox.Size = new Size(150, 31);
            BankFeesTextBox.TabIndex = 26;
            BankFeesTextBox.KeyPress += BankFeesTextBox_KeyPress;
            BankFeesTextBox.Leave += BankFeesTextBox_Leave;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(59, 164);
            label3.Name = "label3";
            label3.Size = new Size(90, 25);
            label3.TabIndex = 25;
            label3.Text = "Bank Fees";
            // 
            // EndingBalanceTextBox
            // 
            EndingBalanceTextBox.Location = new Point(252, 124);
            EndingBalanceTextBox.Name = "EndingBalanceTextBox";
            EndingBalanceTextBox.Size = new Size(150, 31);
            EndingBalanceTextBox.TabIndex = 24;
            EndingBalanceTextBox.KeyPress += EndingBalanceTextBox_KeyPress;
            EndingBalanceTextBox.Leave += EndingBalanceTextBox_Leave;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(59, 127);
            label2.Name = "label2";
            label2.Size = new Size(131, 25);
            label2.TabIndex = 23;
            label2.Text = "Ending Balance";
            // 
            // LastReconciledBalanceTextBox
            // 
            LastReconciledBalanceTextBox.Location = new Point(252, 87);
            LastReconciledBalanceTextBox.Name = "LastReconciledBalanceTextBox";
            LastReconciledBalanceTextBox.ReadOnly = true;
            LastReconciledBalanceTextBox.Size = new Size(150, 31);
            LastReconciledBalanceTextBox.TabIndex = 22;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(59, 87);
            label1.Name = "label1";
            label1.Size = new Size(196, 25);
            label1.TabIndex = 21;
            label1.Text = "Last Reconciled Balance";
            // 
            // CancelFormButton
            // 
            CancelFormButton.Location = new Point(724, 508);
            CancelFormButton.Name = "CancelFormButton";
            CancelFormButton.Size = new Size(111, 33);
            CancelFormButton.TabIndex = 37;
            CancelFormButton.Text = "Cancel";
            CancelFormButton.UseVisualStyleBackColor = true;
            CancelFormButton.Click += CancelFormButton_Click;
            // 
            // DoneButton
            // 
            DoneButton.Location = new Point(504, 506);
            DoneButton.Name = "DoneButton";
            DoneButton.Size = new Size(111, 33);
            DoneButton.TabIndex = 36;
            DoneButton.Text = "Done";
            DoneButton.UseVisualStyleBackColor = true;
            DoneButton.Click += DoneButton_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(599, 24);
            label6.Name = "label6";
            label6.Size = new Size(212, 25);
            label6.TabIndex = 39;
            label6.Text = "Outstanding Transactions";
            // 
            // ChecksDataGridView
            // 
            ChecksDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ChecksDataGridView.Location = new Point(427, 55);
            ChecksDataGridView.Name = "ChecksDataGridView";
            ChecksDataGridView.RowHeadersWidth = 62;
            ChecksDataGridView.RowTemplate.Height = 33;
            ChecksDataGridView.Size = new Size(796, 440);
            ChecksDataGridView.TabIndex = 38;
            ChecksDataGridView.CellClick += ChecksDataGridView_CellClick;
            // 
            // DepositsDataGridView
            // 
            DepositsDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DepositsDataGridView.Location = new Point(1255, 56);
            DepositsDataGridView.Name = "DepositsDataGridView";
            DepositsDataGridView.RowHeadersWidth = 62;
            DepositsDataGridView.RowTemplate.Height = 33;
            DepositsDataGridView.Size = new Size(637, 440);
            DepositsDataGridView.TabIndex = 40;
            DepositsDataGridView.CellClick += DepositsDataGridView_CellClick;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(1399, 28);
            label7.Name = "label7";
            label7.Size = new Size(82, 25);
            label7.TabIndex = 41;
            label7.Text = "Deposits";
            // 
            // ReconcileForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1921, 551);
            Controls.Add(label7);
            Controls.Add(DepositsDataGridView);
            Controls.Add(label6);
            Controls.Add(ChecksDataGridView);
            Controls.Add(CancelFormButton);
            Controls.Add(DoneButton);
            Controls.Add(ChangeTransactionValueButton);
            Controls.Add(AddFeesAndInterestButton);
            Controls.Add(ReconciliationDateTimePicker);
            Controls.Add(label13);
            Controls.Add(AddMissingTransactionButton);
            Controls.Add(ClearedBalanceTextBox);
            Controls.Add(label5);
            Controls.Add(InterestEarnedTextBox);
            Controls.Add(label4);
            Controls.Add(BankFeesTextBox);
            Controls.Add(label3);
            Controls.Add(EndingBalanceTextBox);
            Controls.Add(label2);
            Controls.Add(LastReconciledBalanceTextBox);
            Controls.Add(label1);
            Name = "ReconcileForm";
            Text = "Reconcile Checks with Bank Statement";
            Shown += ReconcileForm_Shown;
            ((System.ComponentModel.ISupportInitialize)ChecksDataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)DepositsDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button ChangeTransactionValueButton;
        private Button AddFeesAndInterestButton;
        private DateTimePicker ReconciliationDateTimePicker;
        private Label label13;
        private Button AddMissingTransactionButton;
        private TextBox ClearedBalanceTextBox;
        private Label label5;
        private TextBox InterestEarnedTextBox;
        private Label label4;
        private TextBox BankFeesTextBox;
        private Label label3;
        private TextBox EndingBalanceTextBox;
        private Label label2;
        private TextBox LastReconciledBalanceTextBox;
        private Label label1;
        private Button CancelFormButton;
        private Button DoneButton;
        private Label label6;
        private DataGridView ChecksDataGridView;
        private DataGridView DepositsDataGridView;
        private Label label7;
    }
}