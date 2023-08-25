namespace BusinessCheckBook
{
    partial class ViewLedgerForm
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
            DoneButton = new Button();
            AddTransactionButton = new Button();
            EditTransactionButton = new Button();
            LedgerDataGridView = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)LedgerDataGridView).BeginInit();
            SuspendLayout();
            // 
            // DoneButton
            // 
            DoneButton.Location = new Point(60, 468);
            DoneButton.Name = "DoneButton";
            DoneButton.Size = new Size(75, 23);
            DoneButton.TabIndex = 0;
            DoneButton.Text = "Done";
            DoneButton.UseVisualStyleBackColor = true;
            DoneButton.Click += DoneButton_Click;
            // 
            // AddTransactionButton
            // 
            AddTransactionButton.Location = new Point(315, 468);
            AddTransactionButton.Name = "AddTransactionButton";
            AddTransactionButton.Size = new Size(122, 23);
            AddTransactionButton.TabIndex = 1;
            AddTransactionButton.Text = "Add Transaction";
            AddTransactionButton.UseVisualStyleBackColor = true;
            AddTransactionButton.Click += AddTransactionButton_Click;
            // 
            // EditTransactionButton
            // 
            EditTransactionButton.Location = new Point(533, 468);
            EditTransactionButton.Name = "EditTransactionButton";
            EditTransactionButton.Size = new Size(174, 23);
            EditTransactionButton.TabIndex = 2;
            EditTransactionButton.Text = "Edit Transaction";
            EditTransactionButton.UseVisualStyleBackColor = true;
            EditTransactionButton.Click += EditTransactionButton_Click;
            // 
            // LedgerDataGridView
            // 
            LedgerDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            LedgerDataGridView.Location = new Point(49, 39);
            LedgerDataGridView.Name = "LedgerDataGridView";
            LedgerDataGridView.RowTemplate.Height = 25;
            LedgerDataGridView.Size = new Size(741, 403);
            LedgerDataGridView.TabIndex = 3;
            // 
            // ViewLedgerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(874, 569);
            Controls.Add(LedgerDataGridView);
            Controls.Add(EditTransactionButton);
            Controls.Add(AddTransactionButton);
            Controls.Add(DoneButton);
            Name = "ViewLedgerForm";
            Text = "View Ledger";
            ((System.ComponentModel.ISupportInitialize)LedgerDataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button DoneButton;
        private Button AddTransactionButton;
        private Button EditTransactionButton;
        private DataGridView LedgerDataGridView;
    }
}