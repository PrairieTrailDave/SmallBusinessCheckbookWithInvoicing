namespace BusinessCheckBook.Settings
{
    partial class ChartOfAccountForm
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
            EnableButton = new Button();
            DisableButton = new Button();
            AddButton = new Button();
            Fed1120MappingComboBox = new ComboBox();
            label6 = new Label();
            SubAccountComboBox = new ComboBox();
            label5 = new Label();
            AccountDescriptionTextBox = new TextBox();
            label4 = new Label();
            AccountTypeComboBox = new ComboBox();
            label3 = new Label();
            AccountNameTextBox = new TextBox();
            label2 = new Label();
            label1 = new Label();
            SaveChangesButton = new Button();
            ClearButton = new Button();
            CurrentAccountsDataGridView = new DataGridView();
            DoneButton = new Button();
            label7 = new Label();
            NewButton = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)CurrentAccountsDataGridView).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(EnableButton);
            panel1.Controls.Add(DisableButton);
            panel1.Controls.Add(AddButton);
            panel1.Controls.Add(Fed1120MappingComboBox);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(SubAccountComboBox);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(AccountDescriptionTextBox);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(AccountTypeComboBox);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(AccountNameTextBox);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(SaveChangesButton);
            panel1.Controls.Add(ClearButton);
            panel1.Location = new Point(759, 37);
            panel1.Name = "panel1";
            panel1.Size = new Size(582, 419);
            panel1.TabIndex = 0;
            // 
            // EnableButton
            // 
            EnableButton.Location = new Point(365, 264);
            EnableButton.Name = "EnableButton";
            EnableButton.Size = new Size(75, 23);
            EnableButton.TabIndex = 15;
            EnableButton.Text = "Enable";
            EnableButton.UseVisualStyleBackColor = true;
            EnableButton.Click += EnableButton_Click;
            // 
            // DisableButton
            // 
            DisableButton.Location = new Point(207, 264);
            DisableButton.Name = "DisableButton";
            DisableButton.Size = new Size(75, 23);
            DisableButton.TabIndex = 14;
            DisableButton.Text = "Disable";
            DisableButton.UseVisualStyleBackColor = true;
            DisableButton.Click += DisableButton_Click;
            // 
            // AddButton
            // 
            AddButton.Location = new Point(207, 360);
            AddButton.Name = "AddButton";
            AddButton.Size = new Size(75, 23);
            AddButton.TabIndex = 13;
            AddButton.Text = "Add";
            AddButton.UseVisualStyleBackColor = true;
            AddButton.Click += AddButton_Click;
            // 
            // Fed1120MappingComboBox
            // 
            Fed1120MappingComboBox.FormattingEnabled = true;
            Fed1120MappingComboBox.Location = new Point(197, 212);
            Fed1120MappingComboBox.Name = "Fed1120MappingComboBox";
            Fed1120MappingComboBox.Size = new Size(317, 23);
            Fed1120MappingComboBox.TabIndex = 12;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(22, 215);
            label6.Name = "label6";
            label6.Size = new Size(113, 15);
            label6.TabIndex = 11;
            label6.Text = "1120 Form Mapping";
            // 
            // SubAccountComboBox
            // 
            SubAccountComboBox.FormattingEnabled = true;
            SubAccountComboBox.Location = new Point(197, 171);
            SubAccountComboBox.Name = "SubAccountComboBox";
            SubAccountComboBox.Size = new Size(317, 23);
            SubAccountComboBox.TabIndex = 10;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(22, 174);
            label5.Name = "label5";
            label5.Size = new Size(91, 15);
            label5.TabIndex = 9;
            label5.Text = "Sub Account Of";
            // 
            // AccountDescriptionTextBox
            // 
            AccountDescriptionTextBox.Location = new Point(197, 123);
            AccountDescriptionTextBox.Name = "AccountDescriptionTextBox";
            AccountDescriptionTextBox.Size = new Size(361, 23);
            AccountDescriptionTextBox.TabIndex = 8;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(22, 126);
            label4.Name = "label4";
            label4.Size = new Size(67, 15);
            label4.TabIndex = 7;
            label4.Text = "Description";
            // 
            // AccountTypeComboBox
            // 
            AccountTypeComboBox.FormattingEnabled = true;
            AccountTypeComboBox.Items.AddRange(new object[] { "Income", "Expense", "SubAccount" });
            AccountTypeComboBox.Location = new Point(197, 86);
            AccountTypeComboBox.Name = "AccountTypeComboBox";
            AccountTypeComboBox.Size = new Size(175, 23);
            AccountTypeComboBox.TabIndex = 6;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(22, 89);
            label3.Name = "label3";
            label3.Size = new Size(79, 15);
            label3.TabIndex = 5;
            label3.Text = "Account Type";
            // 
            // AccountNameTextBox
            // 
            AccountNameTextBox.Location = new Point(197, 51);
            AccountNameTextBox.Name = "AccountNameTextBox";
            AccountNameTextBox.Size = new Size(317, 23);
            AccountNameTextBox.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(22, 54);
            label2.Name = "label2";
            label2.Size = new Size(87, 15);
            label2.TabIndex = 3;
            label2.Text = "Account Name";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(22, 17);
            label1.Name = "label1";
            label1.Size = new Size(90, 15);
            label1.TabIndex = 2;
            label1.Text = "Account Details";
            // 
            // SaveChangesButton
            // 
            SaveChangesButton.Location = new Point(393, 360);
            SaveChangesButton.Name = "SaveChangesButton";
            SaveChangesButton.Size = new Size(134, 23);
            SaveChangesButton.TabIndex = 1;
            SaveChangesButton.Text = "Save Changes";
            SaveChangesButton.UseVisualStyleBackColor = true;
            SaveChangesButton.Click += SaveChangesButton_Click;
            // 
            // ClearButton
            // 
            ClearButton.Location = new Point(21, 360);
            ClearButton.Name = "ClearButton";
            ClearButton.Size = new Size(96, 23);
            ClearButton.TabIndex = 0;
            ClearButton.Text = "Clear";
            ClearButton.UseVisualStyleBackColor = true;
            ClearButton.Click += ClearButton_Click;
            // 
            // CurrentAccountsDataGridView
            // 
            CurrentAccountsDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            CurrentAccountsDataGridView.Location = new Point(36, 37);
            CurrentAccountsDataGridView.Name = "CurrentAccountsDataGridView";
            CurrentAccountsDataGridView.RowTemplate.Height = 25;
            CurrentAccountsDataGridView.Size = new Size(597, 487);
            CurrentAccountsDataGridView.TabIndex = 1;
            CurrentAccountsDataGridView.CellContentClick += CurrentAccountsDataGridView_CellContentClick;
            // 
            // DoneButton
            // 
            DoneButton.Location = new Point(773, 485);
            DoneButton.Name = "DoneButton";
            DoneButton.Size = new Size(75, 23);
            DoneButton.TabIndex = 2;
            DoneButton.Text = "Done";
            DoneButton.UseVisualStyleBackColor = true;
            DoneButton.Click += DoneButton_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(36, 9);
            label7.Name = "label7";
            label7.Size = new Size(218, 15);
            label7.TabIndex = 7;
            label7.Text = "Copyright 2023 Prarie Trail Software, Inc.";
            label7.Visible = false;
            // 
            // NewButton
            // 
            NewButton.Location = new Point(656, 37);
            NewButton.Name = "NewButton";
            NewButton.Size = new Size(75, 23);
            NewButton.TabIndex = 8;
            NewButton.Text = "New";
            NewButton.UseVisualStyleBackColor = true;
            NewButton.Click += NewButton_Click;
            // 
            // ChartOfAccountForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1417, 552);
            Controls.Add(NewButton);
            Controls.Add(label7);
            Controls.Add(DoneButton);
            Controls.Add(CurrentAccountsDataGridView);
            Controls.Add(panel1);
            Name = "ChartOfAccountForm";
            Text = "ChartOfAccount";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)CurrentAccountsDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private DataGridView CurrentAccountsDataGridView;
        private ComboBox Fed1120MappingComboBox;
        private Label label6;
        private ComboBox SubAccountComboBox;
        private Label label5;
        private TextBox AccountDescriptionTextBox;
        private Label label4;
        private ComboBox AccountTypeComboBox;
        private Label label3;
        private TextBox AccountNameTextBox;
        private Label label2;
        private Label label1;
        private Button SaveChangesButton;
        private Button ClearButton;
        private Button DoneButton;
        private Button AddButton;
        private Button EnableButton;
        private Button DisableButton;
        private Label label7;
        private Button NewButton;
    }
}