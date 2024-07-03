namespace BusinessCheckBook
{
    partial class WriteCheckForm
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
            panel1 = new Panel();
            DateTimePicker = new DateTimePicker();
            CheckNumberLabel = new Label();
            CompanyAddress4Label = new Label();
            MatchingListBox = new ListBox();
            DollarLabel = new Label();
            OrderOfLabel = new Label();
            PayToTheOrderOfLabel = new Label();
            AmountWordsLabel = new Label();
            MemoTextBox = new TextBox();
            MemoNameLabel = new Label();
            ToWhomAddress3Label = new Label();
            ToWhomAddress2Label = new Label();
            ToWhomAddress1Label = new Label();
            ToWhomNameLabel = new Label();
            AmountTextBox = new TextBox();
            ToWhomTextBox = new TextBox();
            CompanyAddress3Label = new Label();
            CompanyAddress2Label = new Label();
            CompanyAddressLabel = new Label();
            CompanyNameLabel = new Label();
            panel2 = new Panel();
            CategoryListBox = new ListBox();
            DetailTotalTextBox = new TextBox();
            DetailTotalLabel = new Label();
            CheckBreakdownDataGridView = new DataGridView();
            PrintCheckButton = new Button();
            PrintCheckDialog = new PrintDialog();
            AddToBatchButton = new Button();
            PrintBatchButton = new Button();
            DoneButton = new Button();
            label3 = new Label();
            DeleteDetailLineButton = new Button();
            ClearCheckButton = new Button();
            CurrentBalanceTextLabel = new Label();
            CurrentBalanceLabel = new Label();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)CheckBreakdownDataGridView).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(DateTimePicker);
            panel1.Controls.Add(CheckNumberLabel);
            panel1.Controls.Add(CompanyAddress4Label);
            panel1.Controls.Add(MatchingListBox);
            panel1.Controls.Add(DollarLabel);
            panel1.Controls.Add(OrderOfLabel);
            panel1.Controls.Add(PayToTheOrderOfLabel);
            panel1.Controls.Add(AmountWordsLabel);
            panel1.Controls.Add(MemoTextBox);
            panel1.Controls.Add(MemoNameLabel);
            panel1.Controls.Add(ToWhomAddress3Label);
            panel1.Controls.Add(ToWhomAddress2Label);
            panel1.Controls.Add(ToWhomAddress1Label);
            panel1.Controls.Add(ToWhomNameLabel);
            panel1.Controls.Add(AmountTextBox);
            panel1.Controls.Add(ToWhomTextBox);
            panel1.Controls.Add(CompanyAddress3Label);
            panel1.Controls.Add(CompanyAddress2Label);
            panel1.Controls.Add(CompanyAddressLabel);
            panel1.Controls.Add(CompanyNameLabel);
            panel1.Location = new Point(56, 92);
            panel1.Margin = new Padding(4, 5, 4, 5);
            panel1.Name = "panel1";
            panel1.Size = new Size(1625, 535);
            panel1.TabIndex = 0;
            // 
            // DateTimePicker
            // 
            DateTimePicker.Format = DateTimePickerFormat.Short;
            DateTimePicker.Location = new Point(1277, 92);
            DateTimePicker.Margin = new Padding(4, 5, 4, 5);
            DateTimePicker.Name = "DateTimePicker";
            DateTimePicker.Size = new Size(284, 31);
            DateTimePicker.TabIndex = 1;
            // 
            // CheckNumberLabel
            // 
            CheckNumberLabel.AutoSize = true;
            CheckNumberLabel.Location = new Point(1380, 27);
            CheckNumberLabel.Margin = new Padding(4, 0, 4, 0);
            CheckNumberLabel.Name = "CheckNumberLabel";
            CheckNumberLabel.Size = new Size(70, 25);
            CheckNumberLabel.TabIndex = 95;
            CheckNumberLabel.Text = "Check#";
            // 
            // CompanyAddress4Label
            // 
            CompanyAddress4Label.AutoSize = true;
            CompanyAddress4Label.Location = new Point(66, 127);
            CompanyAddress4Label.Margin = new Padding(4, 0, 4, 0);
            CompanyAddress4Label.Name = "CompanyAddress4Label";
            CompanyAddress4Label.Size = new Size(59, 25);
            CompanyAddress4Label.TabIndex = 94;
            CompanyAddress4Label.Text = "label1";
            // 
            // MatchingListBox
            // 
            MatchingListBox.FormattingEnabled = true;
            MatchingListBox.ItemHeight = 25;
            MatchingListBox.Location = new Point(254, 240);
            MatchingListBox.Name = "MatchingListBox";
            MatchingListBox.Size = new Size(593, 129);
            MatchingListBox.TabIndex = 18;
            MatchingListBox.Visible = false;
            MatchingListBox.Click += MatchingListBox_Click;
            // 
            // DollarLabel
            // 
            DollarLabel.AutoSize = true;
            DollarLabel.Location = new Point(1477, 265);
            DollarLabel.Margin = new Padding(4, 0, 4, 0);
            DollarLabel.Name = "DollarLabel";
            DollarLabel.Size = new Size(89, 25);
            DollarLabel.TabIndex = 16;
            DollarLabel.Text = "DOLLARS";
            // 
            // OrderOfLabel
            // 
            OrderOfLabel.AutoSize = true;
            OrderOfLabel.Location = new Point(23, 207);
            OrderOfLabel.Margin = new Padding(4, 0, 4, 0);
            OrderOfLabel.Name = "OrderOfLabel";
            OrderOfLabel.Size = new Size(98, 25);
            OrderOfLabel.TabIndex = 15;
            OrderOfLabel.Text = "ORDER OF";
            // 
            // PayToTheOrderOfLabel
            // 
            PayToTheOrderOfLabel.AutoSize = true;
            PayToTheOrderOfLabel.Location = new Point(23, 175);
            PayToTheOrderOfLabel.Margin = new Padding(4, 0, 4, 0);
            PayToTheOrderOfLabel.Name = "PayToTheOrderOfLabel";
            PayToTheOrderOfLabel.Size = new Size(105, 25);
            PayToTheOrderOfLabel.TabIndex = 14;
            PayToTheOrderOfLabel.Text = "PAY TO THE";
            // 
            // AmountWordsLabel
            // 
            AmountWordsLabel.AutoSize = true;
            AmountWordsLabel.Font = new Font("Segoe UI", 10F);
            AmountWordsLabel.Location = new Point(84, 265);
            AmountWordsLabel.Margin = new Padding(4, 0, 4, 0);
            AmountWordsLabel.Name = "AmountWordsLabel";
            AmountWordsLabel.Size = new Size(65, 28);
            AmountWordsLabel.TabIndex = 13;
            AmountWordsLabel.Text = "label1";
            // 
            // MemoTextBox
            // 
            MemoTextBox.Location = new Point(111, 460);
            MemoTextBox.Margin = new Padding(4, 5, 4, 5);
            MemoTextBox.Name = "MemoTextBox";
            MemoTextBox.Size = new Size(433, 31);
            MemoTextBox.TabIndex = 4;
            // 
            // MemoNameLabel
            // 
            MemoNameLabel.AutoSize = true;
            MemoNameLabel.Location = new Point(39, 465);
            MemoNameLabel.Margin = new Padding(4, 0, 4, 0);
            MemoNameLabel.Name = "MemoNameLabel";
            MemoNameLabel.Size = new Size(68, 25);
            MemoNameLabel.TabIndex = 11;
            MemoNameLabel.Text = "Memo:";
            // 
            // ToWhomAddress3Label
            // 
            ToWhomAddress3Label.AutoSize = true;
            ToWhomAddress3Label.Location = new Point(111, 403);
            ToWhomAddress3Label.Margin = new Padding(4, 0, 4, 0);
            ToWhomAddress3Label.Name = "ToWhomAddress3Label";
            ToWhomAddress3Label.Size = new Size(59, 25);
            ToWhomAddress3Label.TabIndex = 99;
            ToWhomAddress3Label.Text = "label1";
            // 
            // ToWhomAddress2Label
            // 
            ToWhomAddress2Label.AutoSize = true;
            ToWhomAddress2Label.Location = new Point(111, 378);
            ToWhomAddress2Label.Margin = new Padding(4, 0, 4, 0);
            ToWhomAddress2Label.Name = "ToWhomAddress2Label";
            ToWhomAddress2Label.Size = new Size(59, 25);
            ToWhomAddress2Label.TabIndex = 98;
            ToWhomAddress2Label.Text = "label1";
            // 
            // ToWhomAddress1Label
            // 
            ToWhomAddress1Label.AutoSize = true;
            ToWhomAddress1Label.Location = new Point(111, 353);
            ToWhomAddress1Label.Margin = new Padding(4, 0, 4, 0);
            ToWhomAddress1Label.Name = "ToWhomAddress1Label";
            ToWhomAddress1Label.Size = new Size(59, 25);
            ToWhomAddress1Label.TabIndex = 97;
            ToWhomAddress1Label.Text = "label1";
            // 
            // ToWhomNameLabel
            // 
            ToWhomNameLabel.AutoSize = true;
            ToWhomNameLabel.Location = new Point(111, 328);
            ToWhomNameLabel.Margin = new Padding(4, 0, 4, 0);
            ToWhomNameLabel.Name = "ToWhomNameLabel";
            ToWhomNameLabel.Size = new Size(59, 25);
            ToWhomNameLabel.TabIndex = 96;
            ToWhomNameLabel.Text = "label1";
            // 
            // AmountTextBox
            // 
            AmountTextBox.Location = new Point(1394, 193);
            AmountTextBox.Margin = new Padding(4, 5, 4, 5);
            AmountTextBox.Name = "AmountTextBox";
            AmountTextBox.Size = new Size(167, 31);
            AmountTextBox.TabIndex = 3;
            AmountTextBox.Leave += AmountTextBox_Leave;
            // 
            // ToWhomTextBox
            // 
            ToWhomTextBox.Location = new Point(129, 193);
            ToWhomTextBox.Margin = new Padding(4, 5, 4, 5);
            ToWhomTextBox.Name = "ToWhomTextBox";
            ToWhomTextBox.Size = new Size(1125, 31);
            ToWhomTextBox.TabIndex = 2;
            ToWhomTextBox.KeyPress += ToWhomTextBox_KeyPress;
            ToWhomTextBox.PreviewKeyDown += ToWhomTextBox_PreviewKeyDown;
            // 
            // CompanyAddress3Label
            // 
            CompanyAddress3Label.AutoSize = true;
            CompanyAddress3Label.Location = new Point(66, 102);
            CompanyAddress3Label.Margin = new Padding(4, 0, 4, 0);
            CompanyAddress3Label.Name = "CompanyAddress3Label";
            CompanyAddress3Label.Size = new Size(59, 25);
            CompanyAddress3Label.TabIndex = 93;
            CompanyAddress3Label.Text = "label1";
            // 
            // CompanyAddress2Label
            // 
            CompanyAddress2Label.AutoSize = true;
            CompanyAddress2Label.Location = new Point(66, 77);
            CompanyAddress2Label.Margin = new Padding(4, 0, 4, 0);
            CompanyAddress2Label.Name = "CompanyAddress2Label";
            CompanyAddress2Label.Size = new Size(59, 25);
            CompanyAddress2Label.TabIndex = 92;
            CompanyAddress2Label.Text = "label1";
            // 
            // CompanyAddressLabel
            // 
            CompanyAddressLabel.AutoSize = true;
            CompanyAddressLabel.Location = new Point(66, 52);
            CompanyAddressLabel.Margin = new Padding(4, 0, 4, 0);
            CompanyAddressLabel.Name = "CompanyAddressLabel";
            CompanyAddressLabel.Size = new Size(59, 25);
            CompanyAddressLabel.TabIndex = 91;
            CompanyAddressLabel.Text = "label1";
            // 
            // CompanyNameLabel
            // 
            CompanyNameLabel.AutoSize = true;
            CompanyNameLabel.Location = new Point(66, 27);
            CompanyNameLabel.Margin = new Padding(4, 0, 4, 0);
            CompanyNameLabel.Name = "CompanyNameLabel";
            CompanyNameLabel.Size = new Size(59, 25);
            CompanyNameLabel.TabIndex = 90;
            CompanyNameLabel.Text = "label1";
            // 
            // panel2
            // 
            panel2.Controls.Add(CategoryListBox);
            panel2.Controls.Add(DetailTotalTextBox);
            panel2.Controls.Add(DetailTotalLabel);
            panel2.Controls.Add(CheckBreakdownDataGridView);
            panel2.Location = new Point(56, 653);
            panel2.Margin = new Padding(4, 5, 4, 5);
            panel2.Name = "panel2";
            panel2.Size = new Size(1626, 310);
            panel2.TabIndex = 1;
            // 
            // CategoryListBox
            // 
            CategoryListBox.FormattingEnabled = true;
            CategoryListBox.ItemHeight = 25;
            CategoryListBox.Location = new Point(4, 107);
            CategoryListBox.Margin = new Padding(4, 5, 4, 5);
            CategoryListBox.Name = "CategoryListBox";
            CategoryListBox.Size = new Size(415, 179);
            CategoryListBox.TabIndex = 3;
            CategoryListBox.Visible = false;
            CategoryListBox.SelectedIndexChanged += CategoryListBox_SelectedIndexChanged;
            // 
            // DetailTotalTextBox
            // 
            DetailTotalTextBox.Location = new Point(133, 10);
            DetailTotalTextBox.Margin = new Padding(4, 5, 4, 5);
            DetailTotalTextBox.Name = "DetailTotalTextBox";
            DetailTotalTextBox.Size = new Size(154, 31);
            DetailTotalTextBox.TabIndex = 89;
            // 
            // DetailTotalLabel
            // 
            DetailTotalLabel.AutoSize = true;
            DetailTotalLabel.Location = new Point(66, 15);
            DetailTotalLabel.Margin = new Padding(4, 0, 4, 0);
            DetailTotalLabel.Name = "DetailTotalLabel";
            DetailTotalLabel.Size = new Size(64, 25);
            DetailTotalLabel.TabIndex = 1;
            DetailTotalLabel.Text = "Total $";
            // 
            // CheckBreakdownDataGridView
            // 
            CheckBreakdownDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            CheckBreakdownDataGridView.Location = new Point(66, 60);
            CheckBreakdownDataGridView.Margin = new Padding(4, 5, 4, 5);
            CheckBreakdownDataGridView.Name = "CheckBreakdownDataGridView";
            CheckBreakdownDataGridView.RowHeadersWidth = 62;
            CheckBreakdownDataGridView.RowTemplate.Height = 25;
            CheckBreakdownDataGridView.Size = new Size(1556, 203);
            CheckBreakdownDataGridView.TabIndex = 0;
            CheckBreakdownDataGridView.CellClick += CheckBreakdownDataGridView_CellClick;
            CheckBreakdownDataGridView.CellEnter += CheckBreakdownDataGridView_CellEnter;
            CheckBreakdownDataGridView.CellValueChanged += CheckBreakdownDataGridView_CellValueChanged;
            // 
            // PrintCheckButton
            // 
            PrintCheckButton.Location = new Point(114, 20);
            PrintCheckButton.Margin = new Padding(4, 5, 4, 5);
            PrintCheckButton.Name = "PrintCheckButton";
            PrintCheckButton.Size = new Size(150, 38);
            PrintCheckButton.TabIndex = 20;
            PrintCheckButton.Text = "Print Check";
            PrintCheckButton.UseVisualStyleBackColor = true;
            PrintCheckButton.Click += PrintCheckButton_Click;
            // 
            // PrintCheckDialog
            // 
            PrintCheckDialog.UseEXDialog = true;
            // 
            // AddToBatchButton
            // 
            AddToBatchButton.Location = new Point(426, 20);
            AddToBatchButton.Margin = new Padding(4, 5, 4, 5);
            AddToBatchButton.Name = "AddToBatchButton";
            AddToBatchButton.Size = new Size(139, 38);
            AddToBatchButton.TabIndex = 21;
            AddToBatchButton.Text = "Add To Batch";
            AddToBatchButton.UseVisualStyleBackColor = true;
            AddToBatchButton.Click += AddToBatchButton_Click;
            // 
            // PrintBatchButton
            // 
            PrintBatchButton.Location = new Point(749, 20);
            PrintBatchButton.Margin = new Padding(4, 5, 4, 5);
            PrintBatchButton.Name = "PrintBatchButton";
            PrintBatchButton.Size = new Size(107, 38);
            PrintBatchButton.TabIndex = 22;
            PrintBatchButton.Text = "Print Batch";
            PrintBatchButton.UseVisualStyleBackColor = true;
            PrintBatchButton.Click += PrintBatchButton_Click;
            // 
            // DoneButton
            // 
            DoneButton.Location = new Point(1421, 20);
            DoneButton.Margin = new Padding(4, 5, 4, 5);
            DoneButton.Name = "DoneButton";
            DoneButton.Size = new Size(107, 38);
            DoneButton.TabIndex = 50;
            DoneButton.Text = "Done";
            DoneButton.UseVisualStyleBackColor = true;
            DoneButton.Click += DoneButton_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 62);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(331, 25);
            label3.TabIndex = 51;
            label3.Text = "Copyright 2023 Prarie Trail Software, Inc.";
            label3.Visible = false;
            // 
            // DeleteDetailLineButton
            // 
            DeleteDetailLineButton.Location = new Point(1707, 843);
            DeleteDetailLineButton.Margin = new Padding(4, 5, 4, 5);
            DeleteDetailLineButton.Name = "DeleteDetailLineButton";
            DeleteDetailLineButton.Size = new Size(107, 38);
            DeleteDetailLineButton.TabIndex = 52;
            DeleteDetailLineButton.Text = "Delete Line";
            DeleteDetailLineButton.UseVisualStyleBackColor = true;
            DeleteDetailLineButton.Click += DeleteDetailLineButton_Click;
            // 
            // ClearCheckButton
            // 
            ClearCheckButton.Location = new Point(1707, 967);
            ClearCheckButton.Margin = new Padding(4, 5, 4, 5);
            ClearCheckButton.Name = "ClearCheckButton";
            ClearCheckButton.Size = new Size(107, 38);
            ClearCheckButton.TabIndex = 53;
            ClearCheckButton.Text = "Clear";
            ClearCheckButton.UseVisualStyleBackColor = true;
            ClearCheckButton.Click += ClearCheckButton_Click;
            // 
            // CurrentBalanceTextLabel
            // 
            CurrentBalanceTextLabel.AutoSize = true;
            CurrentBalanceTextLabel.Location = new Point(1707, 232);
            CurrentBalanceTextLabel.Margin = new Padding(4, 0, 4, 0);
            CurrentBalanceTextLabel.Name = "CurrentBalanceTextLabel";
            CurrentBalanceTextLabel.Size = new Size(134, 25);
            CurrentBalanceTextLabel.TabIndex = 54;
            CurrentBalanceTextLabel.Text = "Current Balance";
            // 
            // CurrentBalanceLabel
            // 
            CurrentBalanceLabel.AutoSize = true;
            CurrentBalanceLabel.BackColor = SystemColors.HighlightText;
            CurrentBalanceLabel.Location = new Point(1720, 268);
            CurrentBalanceLabel.Margin = new Padding(4, 0, 4, 0);
            CurrentBalanceLabel.Name = "CurrentBalanceLabel";
            CurrentBalanceLabel.Size = new Size(59, 25);
            CurrentBalanceLabel.TabIndex = 55;
            CurrentBalanceLabel.Text = "label1";
            // 
            // WriteCheckForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1874, 1025);
            Controls.Add(CurrentBalanceLabel);
            Controls.Add(CurrentBalanceTextLabel);
            Controls.Add(ClearCheckButton);
            Controls.Add(DeleteDetailLineButton);
            Controls.Add(label3);
            Controls.Add(DoneButton);
            Controls.Add(PrintBatchButton);
            Controls.Add(AddToBatchButton);
            Controls.Add(PrintCheckButton);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Margin = new Padding(4, 5, 4, 5);
            Name = "WriteCheckForm";
            Text = "WriteCheckForm";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)CheckBreakdownDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Label CompanyAddress3Label;
        private Label CompanyAddress2Label;
        private Label CompanyAddressLabel;
        private Label CompanyNameLabel;
        private Label AmountWordsLabel;
        private TextBox MemoTextBox;
        private Label MemoNameLabel;
        private Label ToWhomAddress3Label;
        private Label ToWhomAddress2Label;
        private Label ToWhomAddress1Label;
        private Label ToWhomNameLabel;
        private TextBox AmountTextBox;
        private TextBox ToWhomTextBox;
        private Label DollarLabel;
        private Label OrderOfLabel;
        private Label PayToTheOrderOfLabel;
        private Panel panel2;
        private DataGridView CheckBreakdownDataGridView;
        private ListBox MatchingListBox;
        private Label CompanyAddress4Label;
        private Label DetailTotalLabel;
        private TextBox DetailTotalTextBox;
        private Button PrintCheckButton;
        private Label CheckNumberLabel;
        private PrintDialog PrintCheckDialog;
        private ListBox CategoryListBox;
        private Button AddToBatchButton;
        private Button PrintBatchButton;
        private Button DoneButton;
        private DateTimePicker DateTimePicker;
        private Label label3;
        private Button DeleteDetailLineButton;
        private Button ClearCheckButton;
        private Label CurrentBalanceTextLabel;
        private Label CurrentBalanceLabel;
    }
}