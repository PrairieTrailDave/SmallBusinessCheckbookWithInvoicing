
namespace BusinessCheckBook
{
    partial class AddTransactionForm
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
            CheckNumberTextBox = new TextBox();
            label2 = new Label();
            ToWhomTextBox = new TextBox();
            label3 = new Label();
            CheckAmountTextBox = new TextBox();
            label4 = new Label();
            DepositTextBox = new TextBox();
            label5 = new Label();
            CurrentBalanceTextBox = new TextBox();
            label6 = new Label();
            PriorBalanceTextBox = new TextBox();
            label7 = new Label();
            CategoriesComboBox = new ComboBox();
            SplitCategoryButton = new Button();
            MatchingListBox = new ListBox();
            DetailDataGridView = new DataGridView();
            DoneButton = new Button();
            DetailInputPanel = new Panel();
            DeleteButton = new Button();
            ItemCancelButton = new Button();
            ItemClearButton = new Button();
            ItemAmountTextBox = new TextBox();
            ItemNotesTextBox = new TextBox();
            CategoryListBox = new ListBox();
            TransactionDateTimePicker = new DateTimePicker();
            DetailTotalLabel = new Label();
            DetailTotalTextBox = new TextBox();
            ((System.ComponentModel.ISupportInitialize)DetailDataGridView).BeginInit();
            DetailInputPanel.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(53, 43);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(31, 15);
            label1.TabIndex = 1;
            label1.Text = "Date";
            // 
            // CheckNumberTextBox
            // 
            CheckNumberTextBox.Location = new Point(162, 60);
            CheckNumberTextBox.Margin = new Padding(2);
            CheckNumberTextBox.Name = "CheckNumberTextBox";
            CheckNumberTextBox.Size = new Size(59, 23);
            CheckNumberTextBox.TabIndex = 2;
            CheckNumberTextBox.KeyPress += CheckNumberTextBox_KeyPress;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(162, 43);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(50, 15);
            label2.TabIndex = 3;
            label2.Text = "Check #";
            // 
            // ToWhomTextBox
            // 
            ToWhomTextBox.Location = new Point(225, 60);
            ToWhomTextBox.Margin = new Padding(2);
            ToWhomTextBox.Name = "ToWhomTextBox";
            ToWhomTextBox.Size = new Size(261, 23);
            ToWhomTextBox.TabIndex = 4;
            ToWhomTextBox.KeyPress += ToWhomTextBox_KeyPress;
            ToWhomTextBox.PreviewKeyDown += ToWhomTextBox_PreviewKeyDown;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(232, 43);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(58, 15);
            label3.TabIndex = 5;
            label3.Text = "To Whom";
            // 
            // CheckAmountTextBox
            // 
            CheckAmountTextBox.Location = new Point(489, 60);
            CheckAmountTextBox.Margin = new Padding(2);
            CheckAmountTextBox.Name = "CheckAmountTextBox";
            CheckAmountTextBox.Size = new Size(106, 23);
            CheckAmountTextBox.TabIndex = 6;
            CheckAmountTextBox.KeyPress += CheckAmountTextBox_KeyPress;
            CheckAmountTextBox.Leave += CheckAmountTextBox_Leave;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(489, 43);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(66, 15);
            label4.TabIndex = 7;
            label4.Text = "Check Amt";
            // 
            // DepositTextBox
            // 
            DepositTextBox.Location = new Point(598, 60);
            DepositTextBox.Margin = new Padding(2);
            DepositTextBox.Name = "DepositTextBox";
            DepositTextBox.Size = new Size(97, 23);
            DepositTextBox.TabIndex = 8;
            DepositTextBox.KeyPress += DepositTextBox_KeyPress;
            DepositTextBox.Leave += DepositTextBox_Leave;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(598, 43);
            label5.Margin = new Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new Size(73, 15);
            label5.TabIndex = 9;
            label5.Text = "Deposit Amt";
            // 
            // CurrentBalanceTextBox
            // 
            CurrentBalanceTextBox.Location = new Point(699, 60);
            CurrentBalanceTextBox.Margin = new Padding(2);
            CurrentBalanceTextBox.Name = "CurrentBalanceTextBox";
            CurrentBalanceTextBox.Size = new Size(106, 23);
            CurrentBalanceTextBox.TabIndex = 10;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(699, 19);
            label6.Margin = new Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Size = new Size(111, 15);
            label6.TabIndex = 11;
            label6.Text = "Checkbook Balance";
            // 
            // PriorBalanceTextBox
            // 
            PriorBalanceTextBox.Location = new Point(699, 41);
            PriorBalanceTextBox.Margin = new Padding(2);
            PriorBalanceTextBox.Name = "PriorBalanceTextBox";
            PriorBalanceTextBox.Size = new Size(106, 23);
            PriorBalanceTextBox.TabIndex = 12;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(162, 84);
            label7.Margin = new Padding(2, 0, 2, 0);
            label7.Name = "label7";
            label7.Size = new Size(58, 15);
            label7.TabIndex = 14;
            label7.Text = "Category:";
            // 
            // CategoriesComboBox
            // 
            CategoriesComboBox.FormattingEnabled = true;
            CategoriesComboBox.Location = new Point(225, 82);
            CategoriesComboBox.Margin = new Padding(2);
            CategoriesComboBox.Name = "CategoriesComboBox";
            CategoriesComboBox.Size = new Size(261, 23);
            CategoriesComboBox.TabIndex = 15;
            // 
            // SplitCategoryButton
            // 
            SplitCategoryButton.Location = new Point(53, 84);
            SplitCategoryButton.Margin = new Padding(2);
            SplitCategoryButton.Name = "SplitCategoryButton";
            SplitCategoryButton.Size = new Size(105, 20);
            SplitCategoryButton.TabIndex = 16;
            SplitCategoryButton.Text = "Split Category";
            SplitCategoryButton.UseVisualStyleBackColor = true;
            SplitCategoryButton.Click += SplitCategoryButton_Click;
            // 
            // MatchingListBox
            // 
            MatchingListBox.FormattingEnabled = true;
            MatchingListBox.ItemHeight = 15;
            MatchingListBox.Location = new Point(232, 82);
            MatchingListBox.Margin = new Padding(2);
            MatchingListBox.Name = "MatchingListBox";
            MatchingListBox.Size = new Size(261, 79);
            MatchingListBox.TabIndex = 17;
            MatchingListBox.Visible = false;
            MatchingListBox.Click += MatchingListBox_Click;
            // 
            // DetailDataGridView
            // 
            DetailDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DetailDataGridView.Location = new Point(53, 108);
            DetailDataGridView.Margin = new Padding(2);
            DetailDataGridView.Name = "DetailDataGridView";
            DetailDataGridView.RowHeadersWidth = 62;
            DetailDataGridView.RowTemplate.Height = 33;
            DetailDataGridView.Size = new Size(681, 361);
            DetailDataGridView.TabIndex = 18;
            DetailDataGridView.Visible = false;
            DetailDataGridView.CellClick += DetailDataGridView_CellClick;
            // 
            // DoneButton
            // 
            DoneButton.Location = new Point(686, 81);
            DoneButton.Margin = new Padding(2);
            DoneButton.Name = "DoneButton";
            DoneButton.Size = new Size(78, 20);
            DoneButton.TabIndex = 19;
            DoneButton.Text = "Done";
            DoneButton.UseVisualStyleBackColor = true;
            DoneButton.Click += DoneButton_Click;
            // 
            // DetailInputPanel
            // 
            DetailInputPanel.Controls.Add(DeleteButton);
            DetailInputPanel.Controls.Add(ItemCancelButton);
            DetailInputPanel.Controls.Add(ItemClearButton);
            DetailInputPanel.Controls.Add(ItemAmountTextBox);
            DetailInputPanel.Controls.Add(ItemNotesTextBox);
            DetailInputPanel.Controls.Add(CategoryListBox);
            DetailInputPanel.Location = new Point(53, 202);
            DetailInputPanel.Margin = new Padding(2);
            DetailInputPanel.Name = "DetailInputPanel";
            DetailInputPanel.Size = new Size(556, 186);
            DetailInputPanel.TabIndex = 20;
            DetailInputPanel.Visible = false;
            // 
            // DeleteButton
            // 
            DeleteButton.Location = new Point(457, 156);
            DeleteButton.Name = "DeleteButton";
            DeleteButton.Size = new Size(75, 23);
            DeleteButton.TabIndex = 5;
            DeleteButton.Text = "Delete";
            DeleteButton.UseVisualStyleBackColor = true;
            DeleteButton.Click += DeleteButton_Click;
            // 
            // ItemCancelButton
            // 
            ItemCancelButton.Location = new Point(339, 97);
            ItemCancelButton.Margin = new Padding(2);
            ItemCancelButton.Name = "ItemCancelButton";
            ItemCancelButton.Size = new Size(78, 20);
            ItemCancelButton.TabIndex = 4;
            ItemCancelButton.Text = "Cancel";
            ItemCancelButton.UseVisualStyleBackColor = true;
            ItemCancelButton.Click += ItemCancelButton_Click;
            // 
            // ItemClearButton
            // 
            ItemClearButton.Location = new Point(249, 97);
            ItemClearButton.Margin = new Padding(2);
            ItemClearButton.Name = "ItemClearButton";
            ItemClearButton.Size = new Size(78, 20);
            ItemClearButton.TabIndex = 3;
            ItemClearButton.Text = "Clear";
            ItemClearButton.UseVisualStyleBackColor = true;
            ItemClearButton.Click += ItemClearButton_Click;
            // 
            // ItemAmountTextBox
            // 
            ItemAmountTextBox.Location = new Point(180, 10);
            ItemAmountTextBox.Margin = new Padding(2);
            ItemAmountTextBox.Name = "ItemAmountTextBox";
            ItemAmountTextBox.Size = new Size(136, 23);
            ItemAmountTextBox.TabIndex = 2;
            ItemAmountTextBox.KeyPress += ItemAmountTextBox_KeyPress;
            ItemAmountTextBox.PreviewKeyDown += ItemAmountTextBox_PreviewKeyDown;
            // 
            // ItemNotesTextBox
            // 
            ItemNotesTextBox.Location = new Point(331, 10);
            ItemNotesTextBox.Margin = new Padding(2);
            ItemNotesTextBox.Name = "ItemNotesTextBox";
            ItemNotesTextBox.Size = new Size(223, 23);
            ItemNotesTextBox.TabIndex = 1;
            // 
            // CategoryListBox
            // 
            CategoryListBox.FormattingEnabled = true;
            CategoryListBox.ItemHeight = 15;
            CategoryListBox.Location = new Point(2, 10);
            CategoryListBox.Margin = new Padding(2);
            CategoryListBox.Name = "CategoryListBox";
            CategoryListBox.Size = new Size(174, 169);
            CategoryListBox.TabIndex = 0;
            // 
            // TransactionDateTimePicker
            // 
            TransactionDateTimePicker.CustomFormat = "MM/dd/yyyy";
            TransactionDateTimePicker.Location = new Point(53, 59);
            TransactionDateTimePicker.Margin = new Padding(2);
            TransactionDateTimePicker.Name = "TransactionDateTimePicker";
            TransactionDateTimePicker.Size = new Size(106, 23);
            TransactionDateTimePicker.TabIndex = 21;
            // 
            // DetailTotalLabel
            // 
            DetailTotalLabel.AutoSize = true;
            DetailTotalLabel.Location = new Point(603, 484);
            DetailTotalLabel.Name = "DetailTotalLabel";
            DetailTotalLabel.Size = new Size(68, 15);
            DetailTotalLabel.TabIndex = 22;
            DetailTotalLabel.Text = "Detail Total:";
            DetailTotalLabel.Visible = false;
            // 
            // DetailTotalTextBox
            // 
            DetailTotalTextBox.Location = new Point(634, 502);
            DetailTotalTextBox.Name = "DetailTotalTextBox";
            DetailTotalTextBox.Size = new Size(100, 23);
            DetailTotalTextBox.TabIndex = 23;
            DetailTotalTextBox.Visible = false;
            // 
            // AddTransactionForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(905, 687);
            Controls.Add(DetailTotalTextBox);
            Controls.Add(DetailTotalLabel);
            Controls.Add(TransactionDateTimePicker);
            Controls.Add(DetailInputPanel);
            Controls.Add(DoneButton);
            Controls.Add(DetailDataGridView);
            Controls.Add(MatchingListBox);
            Controls.Add(SplitCategoryButton);
            Controls.Add(CategoriesComboBox);
            Controls.Add(label7);
            Controls.Add(PriorBalanceTextBox);
            Controls.Add(label6);
            Controls.Add(CurrentBalanceTextBox);
            Controls.Add(label5);
            Controls.Add(DepositTextBox);
            Controls.Add(label4);
            Controls.Add(CheckAmountTextBox);
            Controls.Add(label3);
            Controls.Add(ToWhomTextBox);
            Controls.Add(label2);
            Controls.Add(CheckNumberTextBox);
            Controls.Add(label1);
            Margin = new Padding(2);
            Name = "AddTransactionForm";
            Text = "Add / Edit Transaction";
            Activated += AddTransactionForm_Activated;
            Shown += AddTransactionForm_Shown;
            ((System.ComponentModel.ISupportInitialize)DetailDataGridView).EndInit();
            DetailInputPanel.ResumeLayout(false);
            DetailInputPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private TextBox CheckNumberTextBox;
        private Label label2;
        private TextBox ToWhomTextBox;
        private Label label3;
        private TextBox CheckAmountTextBox;
        private Label label4;
        private TextBox DepositTextBox;
        private Label label5;
        private TextBox CurrentBalanceTextBox;
        private Label label6;
        private TextBox PriorBalanceTextBox;
        private Label label7;
        private ComboBox CategoriesComboBox;
        private Button SplitCategoryButton;
        private ListBox MatchingListBox;
        private DataGridView DetailDataGridView;
        private Button DoneButton;
        private Panel DetailInputPanel;
        private TextBox ItemAmountTextBox;
        private TextBox ItemNotesTextBox;
        private ListBox CategoryListBox;
        private Button ItemClearButton;
        private Button ItemCancelButton;
        private DateTimePicker TransactionDateTimePicker;
        private Label DetailTotalLabel;
        private TextBox DetailTotalTextBox;
        private Button DeleteButton;
    }
}