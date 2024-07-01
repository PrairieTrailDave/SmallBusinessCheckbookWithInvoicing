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
            CurrentBalanceTextBox = new TextBox();
            label4 = new Label();
            AmountPaidTextBox = new TextBox();
            SaveButton = new Button();
            DoneButton = new Button();
            PartialPaymentButton = new Button();
            OutstandingInvoicesDataGridView = new DataGridView();
            label5 = new Label();
            ((System.ComponentModel.ISupportInitialize)OutstandingInvoicesDataGridView).BeginInit();
            SuspendLayout();
            // 
            // CustomerComboBox
            // 
            CustomerComboBox.FormattingEnabled = true;
            CustomerComboBox.Location = new Point(237, 120);
            CustomerComboBox.Margin = new Padding(4, 5, 4, 5);
            CustomerComboBox.Name = "CustomerComboBox";
            CustomerComboBox.Size = new Size(215, 33);
            CustomerComboBox.TabIndex = 0;
            CustomerComboBox.SelectedIndexChanged += CustomerComboBox_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(64, 125);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(117, 25);
            label1.TabIndex = 1;
            label1.Text = "From Whom:";
            // 
            // PostingDateTimePicker
            // 
            PostingDateTimePicker.Format = DateTimePickerFormat.Short;
            PostingDateTimePicker.Location = new Point(657, 120);
            PostingDateTimePicker.Margin = new Padding(4, 5, 4, 5);
            PostingDateTimePicker.Name = "PostingDateTimePicker";
            PostingDateTimePicker.Size = new Size(284, 31);
            PostingDateTimePicker.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(513, 127);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(117, 25);
            label2.TabIndex = 3;
            label2.Text = "Posting Date:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(527, 538);
            label3.Name = "label3";
            label3.Size = new Size(134, 25);
            label3.TabIndex = 4;
            label3.Text = "Current Balance";
            // 
            // CurrentBalanceTextBox
            // 
            CurrentBalanceTextBox.Location = new Point(700, 537);
            CurrentBalanceTextBox.Name = "CurrentBalanceTextBox";
            CurrentBalanceTextBox.Size = new Size(173, 31);
            CurrentBalanceTextBox.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(64, 197);
            label4.Name = "label4";
            label4.Size = new Size(115, 25);
            label4.TabIndex = 6;
            label4.Text = "Amount Paid";
            // 
            // AmountPaidTextBox
            // 
            AmountPaidTextBox.Location = new Point(237, 197);
            AmountPaidTextBox.Name = "AmountPaidTextBox";
            AmountPaidTextBox.Size = new Size(173, 31);
            AmountPaidTextBox.TabIndex = 7;
            AmountPaidTextBox.KeyPress += AmountPaidTextBox_KeyPress;
            // 
            // SaveButton
            // 
            SaveButton.Location = new Point(443, 197);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(143, 38);
            SaveButton.TabIndex = 9;
            SaveButton.Text = "Post Payment";
            SaveButton.UseVisualStyleBackColor = true;
            SaveButton.Click += PostPaymentButton_Click;
            // 
            // DoneButton
            // 
            DoneButton.Location = new Point(951, 658);
            DoneButton.Name = "DoneButton";
            DoneButton.Size = new Size(111, 33);
            DoneButton.TabIndex = 10;
            DoneButton.Text = "Done";
            DoneButton.UseVisualStyleBackColor = true;
            DoneButton.Click += DoneButton_Click;
            // 
            // PartialPaymentButton
            // 
            PartialPaymentButton.Location = new Point(641, 197);
            PartialPaymentButton.Name = "PartialPaymentButton";
            PartialPaymentButton.Size = new Size(240, 38);
            PartialPaymentButton.TabIndex = 11;
            PartialPaymentButton.Text = "Accept Partial Payment";
            PartialPaymentButton.UseVisualStyleBackColor = true;
            PartialPaymentButton.Click += PartialPaymentButton_Click;
            // 
            // OutstandingInvoicesDataGridView
            // 
            OutstandingInvoicesDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            OutstandingInvoicesDataGridView.Location = new Point(200, 263);
            OutstandingInvoicesDataGridView.Margin = new Padding(4, 5, 4, 5);
            OutstandingInvoicesDataGridView.Name = "OutstandingInvoicesDataGridView";
            OutstandingInvoicesDataGridView.RowHeadersWidth = 62;
            OutstandingInvoicesDataGridView.RowTemplate.Height = 25;
            OutstandingInvoicesDataGridView.Size = new Size(796, 250);
            OutstandingInvoicesDataGridView.TabIndex = 12;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(17, 15);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(331, 25);
            label5.TabIndex = 13;
            label5.Text = "Copyright 2023 Prarie Trail Software, Inc.";
            label5.Visible = false;
            // 
            // ReceivePaymentsForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1151, 727);
            Controls.Add(label5);
            Controls.Add(OutstandingInvoicesDataGridView);
            Controls.Add(PartialPaymentButton);
            Controls.Add(DoneButton);
            Controls.Add(SaveButton);
            Controls.Add(AmountPaidTextBox);
            Controls.Add(label4);
            Controls.Add(CurrentBalanceTextBox);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(PostingDateTimePicker);
            Controls.Add(label1);
            Controls.Add(CustomerComboBox);
            Margin = new Padding(4, 5, 4, 5);
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
        private TextBox CurrentBalanceTextBox;
        private Label label4;
        private TextBox AmountPaidTextBox;
        private Button SaveButton;
        private Button DoneButton;
        private Button PartialPaymentButton;
        private DataGridView OutstandingInvoicesDataGridView;
        private Label label5;
    }
}