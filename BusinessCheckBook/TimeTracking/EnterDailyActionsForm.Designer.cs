namespace BusinessCheckBook.TimeTracking
{
    partial class EnterDailyActionsForm
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
            WhichDateTimePicker = new DateTimePicker();
            StartTimeComboBox = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            EndTimeComboBox = new ComboBox();
            label3 = new Label();
            IntervalComboBox = new ComboBox();
            TimeSheetDataGridView = new DataGridView();
            label4 = new Label();
            SubcontractorDescriptionTextBox = new TextBox();
            label5 = new Label();
            SubcontractorExpenseTextBox = new TextBox();
            label6 = new Label();
            label7 = new Label();
            BillableExpenseDescriptionTextBox = new TextBox();
            label8 = new Label();
            BillableExpenseTextBox = new TextBox();
            PersonEnteringDataForLabel = new Label();
            ClearButton = new Button();
            SaveButton = new Button();
            label9 = new Label();
            label10 = new Label();
            label11 = new Label();
            label12 = new Label();
            label13 = new Label();
            label14 = new Label();
            ((System.ComponentModel.ISupportInitialize)TimeSheetDataGridView).BeginInit();
            SuspendLayout();
            // 
            // WhichDateTimePicker
            // 
            WhichDateTimePicker.Location = new Point(50, 35);
            WhichDateTimePicker.Name = "WhichDateTimePicker";
            WhichDateTimePicker.Size = new Size(300, 31);
            WhichDateTimePicker.TabIndex = 0;
            // 
            // StartTimeComboBox
            // 
            StartTimeComboBox.FormattingEnabled = true;
            StartTimeComboBox.Location = new Point(554, 37);
            StartTimeComboBox.Name = "StartTimeComboBox";
            StartTimeComboBox.Size = new Size(108, 33);
            StartTimeComboBox.TabIndex = 1;
            StartTimeComboBox.SelectedIndexChanged += StartTimeComboBox_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(389, 40);
            label1.Name = "label1";
            label1.Size = new Size(159, 25);
            label1.TabIndex = 2;
            label1.Text = "Normal Start Time:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(684, 40);
            label2.Name = "label2";
            label2.Size = new Size(153, 25);
            label2.TabIndex = 3;
            label2.Text = "Normal End Time:";
            // 
            // EndTimeComboBox
            // 
            EndTimeComboBox.FormattingEnabled = true;
            EndTimeComboBox.Location = new Point(843, 37);
            EndTimeComboBox.Name = "EndTimeComboBox";
            EndTimeComboBox.Size = new Size(84, 33);
            EndTimeComboBox.TabIndex = 4;
            EndTimeComboBox.SelectedIndexChanged += EndTimeComboBox_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(943, 40);
            label3.Name = "label3";
            label3.Size = new Size(74, 25);
            label3.TabIndex = 5;
            label3.Text = "Interval:";
            // 
            // IntervalComboBox
            // 
            IntervalComboBox.FormattingEnabled = true;
            IntervalComboBox.Location = new Point(1023, 37);
            IntervalComboBox.Name = "IntervalComboBox";
            IntervalComboBox.Size = new Size(104, 33);
            IntervalComboBox.TabIndex = 6;
            IntervalComboBox.SelectedIndexChanged += IntervalComboBox_SelectedIndexChanged;
            // 
            // TimeSheetDataGridView
            // 
            TimeSheetDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            TimeSheetDataGridView.Location = new Point(50, 90);
            TimeSheetDataGridView.Name = "TimeSheetDataGridView";
            TimeSheetDataGridView.RowHeadersWidth = 62;
            TimeSheetDataGridView.Size = new Size(1104, 329);
            TimeSheetDataGridView.TabIndex = 7;
            TimeSheetDataGridView.CellEnter += TimeSheetDataGridView_CellEnter;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(50, 463);
            label4.Name = "label4";
            label4.Size = new Size(232, 25);
            label4.TabIndex = 8;
            label4.Text = "Add Subcontractor Expense";
            // 
            // SubcontractorDescriptionTextBox
            // 
            SubcontractorDescriptionTextBox.Location = new Point(304, 460);
            SubcontractorDescriptionTextBox.Name = "SubcontractorDescriptionTextBox";
            SubcontractorDescriptionTextBox.Size = new Size(335, 31);
            SubcontractorDescriptionTextBox.TabIndex = 9;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(370, 432);
            label5.Name = "label5";
            label5.Size = new Size(223, 25);
            label5.TabIndex = 10;
            label5.Text = "Subcontrator / Description";
            // 
            // SubcontractorExpenseTextBox
            // 
            SubcontractorExpenseTextBox.Location = new Point(787, 460);
            SubcontractorExpenseTextBox.Name = "SubcontractorExpenseTextBox";
            SubcontractorExpenseTextBox.Size = new Size(150, 31);
            SubcontractorExpenseTextBox.TabIndex = 11;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(684, 463);
            label6.Name = "label6";
            label6.Size = new Size(80, 25);
            label6.TabIndex = 12;
            label6.Text = "Expense:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(50, 513);
            label7.Name = "label7";
            label7.Size = new Size(285, 25);
            label7.TabIndex = 13;
            label7.Text = "Add billable expense   Description:";
            // 
            // BillableExpenseDescriptionTextBox
            // 
            BillableExpenseDescriptionTextBox.Location = new Point(350, 510);
            BillableExpenseDescriptionTextBox.Name = "BillableExpenseDescriptionTextBox";
            BillableExpenseDescriptionTextBox.Size = new Size(392, 31);
            BillableExpenseDescriptionTextBox.TabIndex = 14;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(761, 513);
            label8.Name = "label8";
            label8.Size = new Size(80, 25);
            label8.TabIndex = 15;
            label8.Text = "Expense:";
            // 
            // BillableExpenseTextBox
            // 
            BillableExpenseTextBox.Location = new Point(847, 510);
            BillableExpenseTextBox.Name = "BillableExpenseTextBox";
            BillableExpenseTextBox.Size = new Size(141, 31);
            BillableExpenseTextBox.TabIndex = 16;
            // 
            // PersonEnteringDataForLabel
            // 
            PersonEnteringDataForLabel.AutoSize = true;
            PersonEnteringDataForLabel.Location = new Point(50, 9);
            PersonEnteringDataForLabel.Name = "PersonEnteringDataForLabel";
            PersonEnteringDataForLabel.Size = new Size(119, 25);
            PersonEnteringDataForLabel.TabIndex = 17;
            PersonEnteringDataForLabel.Text = "Entering Data";
            // 
            // ClearButton
            // 
            ClearButton.Location = new Point(809, 580);
            ClearButton.Name = "ClearButton";
            ClearButton.Size = new Size(128, 35);
            ClearButton.TabIndex = 18;
            ClearButton.Text = "Clear";
            ClearButton.UseVisualStyleBackColor = true;
            ClearButton.Click += ClearButton_Click;
            // 
            // SaveButton
            // 
            SaveButton.Location = new Point(272, 580);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(197, 34);
            SaveButton.TabIndex = 19;
            SaveButton.Text = "Save TimeSheet";
            SaveButton.UseVisualStyleBackColor = true;
            SaveButton.Click += SaveButton_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(1196, 39);
            label9.Name = "label9";
            label9.Size = new Size(108, 25);
            label9.TabIndex = 20;
            label9.Text = "Instructions:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(1196, 90);
            label10.Name = "label10";
            label10.Size = new Size(428, 25);
            label10.TabIndex = 21;
            label10.Text = "At the start time, select a project and enter an action.";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(1196, 131);
            label11.Name = "label11";
            label11.Size = new Size(386, 25);
            label11.TabIndex = 22;
            label11.Text = "Keep selecting the project until either the action\r\n";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(1196, 172);
            label12.Name = "label12";
            label12.Size = new Size(339, 25);
            label12.TabIndex = 23;
            label12.Text = "should change or the end time is reached";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(1196, 213);
            label13.Name = "label13";
            label13.Size = new Size(322, 25);
            label13.TabIndex = 24;
            label13.Text = "A blank action is continue the previous.";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(1196, 250);
            label14.Name = "label14";
            label14.Size = new Size(423, 25);
            label14.TabIndex = 25;
            label14.Text = "The system will combine all hours till action changes";
            // 
            // EnterDailyActionsForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1631, 627);
            Controls.Add(label14);
            Controls.Add(label13);
            Controls.Add(label12);
            Controls.Add(label11);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(SaveButton);
            Controls.Add(ClearButton);
            Controls.Add(PersonEnteringDataForLabel);
            Controls.Add(BillableExpenseTextBox);
            Controls.Add(label8);
            Controls.Add(BillableExpenseDescriptionTextBox);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(SubcontractorExpenseTextBox);
            Controls.Add(label5);
            Controls.Add(SubcontractorDescriptionTextBox);
            Controls.Add(label4);
            Controls.Add(TimeSheetDataGridView);
            Controls.Add(IntervalComboBox);
            Controls.Add(label3);
            Controls.Add(EndTimeComboBox);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(StartTimeComboBox);
            Controls.Add(WhichDateTimePicker);
            Name = "EnterDailyActionsForm";
            Text = "Enter Daily Actions";
            ((System.ComponentModel.ISupportInitialize)TimeSheetDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DateTimePicker WhichDateTimePicker;
        private ComboBox StartTimeComboBox;
        private Label label1;
        private Label label2;
        private ComboBox EndTimeComboBox;
        private Label label3;
        private ComboBox IntervalComboBox;
        private DataGridView TimeSheetDataGridView;
        private Label label4;
        private TextBox SubcontractorDescriptionTextBox;
        private Label label5;
        private TextBox SubcontractorExpenseTextBox;
        private Label label6;
        private Label label7;
        private TextBox BillableExpenseDescriptionTextBox;
        private Label label8;
        private TextBox BillableExpenseTextBox;
        private Label PersonEnteringDataForLabel;
        private Button ClearButton;
        private Button SaveButton;
        private Label label9;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
    }
}