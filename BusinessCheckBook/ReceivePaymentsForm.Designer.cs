namespace BusinessCheckBook
{
    partial class ReceivePaymentsForm
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
            CustomerComboBox = new ComboBox();
            label1 = new Label();
            PostingDateTimePicker = new DateTimePicker();
            label2 = new Label();
            label3 = new Label();
            CurrnetBalanceTextBox = new TextBox();
            label4 = new Label();
            AmountPaidTextBox = new TextBox();
            SaveButton = new Button();
            DoneButton = new Button();
            PartialPaymentButton = new Button();
            OutstandingInvoicesDataGridView = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)OutstandingInvoicesDataGridView).BeginInit();
            SuspendLayout();
            // 
            // CustomerComboBox
            // 
            CustomerComboBox.FormattingEnabled = true;
            CustomerComboBox.Location = new Point(166, 72);
            CustomerComboBox.Name = "CustomerComboBox";
            CustomerComboBox.Size = new Size(152, 23);
            CustomerComboBox.TabIndex = 0;
            CustomerComboBox.SelectedIndexChanged += CustomerComboBox_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(45, 75);
            label1.Name = "label1";
            label1.Size = new Size(77, 15);
            label1.TabIndex = 1;
            label1.Text = "From Whom:";
            // 
            // PostingDateTimePicker
            // 
            PostingDateTimePicker.Format = DateTimePickerFormat.Short;
            PostingDateTimePicker.Location = new Point(460, 72);
            PostingDateTimePicker.Name = "PostingDateTimePicker";
            PostingDateTimePicker.Size = new Size(200, 23);
            PostingDateTimePicker.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(359, 76);
            label2.Name = "label2";
            label2.Size = new Size(77, 15);
            label2.TabIndex = 3;
            label2.Text = "Posting Date:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(369, 323);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(91, 15);
            label3.TabIndex = 4;
            label3.Text = "Current Balance";
            // 
            // CurrnetBalanceTextBox
            // 
            CurrnetBalanceTextBox.Location = new Point(490, 322);
            CurrnetBalanceTextBox.Margin = new Padding(2);
            CurrnetBalanceTextBox.Name = "CurrnetBalanceTextBox";
            CurrnetBalanceTextBox.Size = new Size(122, 23);
            CurrnetBalanceTextBox.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(45, 118);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(77, 15);
            label4.TabIndex = 6;
            label4.Text = "Amount Paid";
            // 
            // AmountPaidTextBox
            // 
            AmountPaidTextBox.Location = new Point(166, 118);
            AmountPaidTextBox.Margin = new Padding(2);
            AmountPaidTextBox.Name = "AmountPaidTextBox";
            AmountPaidTextBox.Size = new Size(122, 23);
            AmountPaidTextBox.TabIndex = 7;
            AmountPaidTextBox.KeyPress += AmountPaidTextBox_KeyPress;
            // 
            // SaveButton
            // 
            SaveButton.Location = new Point(310, 118);
            SaveButton.Margin = new Padding(2);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(100, 23);
            SaveButton.TabIndex = 9;
            SaveButton.Text = "Post Payment";
            SaveButton.UseVisualStyleBackColor = true;
            SaveButton.Click += PostPaymentButton_Click;
            // 
            // DoneButton
            // 
            DoneButton.Location = new Point(666, 395);
            DoneButton.Margin = new Padding(2);
            DoneButton.Name = "DoneButton";
            DoneButton.Size = new Size(78, 20);
            DoneButton.TabIndex = 10;
            DoneButton.Text = "Done";
            DoneButton.UseVisualStyleBackColor = true;
            DoneButton.Click += DoneButton_Click;
            // 
            // PartialPaymentButton
            // 
            PartialPaymentButton.Location = new Point(449, 118);
            PartialPaymentButton.Margin = new Padding(2);
            PartialPaymentButton.Name = "PartialPaymentButton";
            PartialPaymentButton.Size = new Size(168, 23);
            PartialPaymentButton.TabIndex = 11;
            PartialPaymentButton.Text = "Accept Partial Payment";
            PartialPaymentButton.UseVisualStyleBackColor = true;
            PartialPaymentButton.Click += PartialPaymentButton_Click;
            // 
            // OutstandingInvoicesDataGridView
            // 
            OutstandingInvoicesDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            OutstandingInvoicesDataGridView.Location = new Point(140, 158);
            OutstandingInvoicesDataGridView.Name = "OutstandingInvoicesDataGridView";
            OutstandingInvoicesDataGridView.RowTemplate.Height = 25;
            OutstandingInvoicesDataGridView.Size = new Size(557, 150);
            OutstandingInvoicesDataGridView.TabIndex = 12;
            // 
            // ReceivePaymentsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(806, 436);
            Controls.Add(OutstandingInvoicesDataGridView);
            Controls.Add(PartialPaymentButton);
            Controls.Add(DoneButton);
            Controls.Add(SaveButton);
            Controls.Add(AmountPaidTextBox);
            Controls.Add(label4);
            Controls.Add(CurrnetBalanceTextBox);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(PostingDateTimePicker);
            Controls.Add(label1);
            Controls.Add(CustomerComboBox);
            Name = "ReceivePaymentsForm";
            Text = "Receive Payment";
            ((System.ComponentModel.ISupportInitialize)OutstandingInvoicesDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox CustomerComboBox;
        private Label label1;
        private DateTimePicker PostingDateTimePicker;
        private Label label2;
        private Label label3;
        private TextBox CurrnetBalanceTextBox;
        private Label label4;
        private TextBox AmountPaidTextBox;
        private Button SaveButton;
        private Button DoneButton;
        private Button PartialPaymentButton;
        private DataGridView OutstandingInvoicesDataGridView;
    }
}