namespace BusinessCheckBook.Reports
{
    partial class CompareReportForm
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
            CurrentYearTextBox = new TextBox();
            label2 = new Label();
            PriorYearTextBox = new TextBox();
            ReportDataGridView = new DataGridView();
            RefreshButton = new Button();
            ((System.ComponentModel.ISupportInitialize)ReportDataGridView).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(50, 47);
            label1.Name = "label1";
            label1.Size = new Size(107, 25);
            label1.TabIndex = 0;
            label1.Text = "Current Year";
            // 
            // CurrentYearTextBox
            // 
            CurrentYearTextBox.Location = new Point(163, 44);
            CurrentYearTextBox.Name = "CurrentYearTextBox";
            CurrentYearTextBox.Size = new Size(110, 31);
            CurrentYearTextBox.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(320, 47);
            label2.Name = "label2";
            label2.Size = new Size(86, 25);
            label2.TabIndex = 2;
            label2.Text = "Prior Year";
            // 
            // PriorYearTextBox
            // 
            PriorYearTextBox.Location = new Point(412, 44);
            PriorYearTextBox.Name = "PriorYearTextBox";
            PriorYearTextBox.Size = new Size(150, 31);
            PriorYearTextBox.TabIndex = 3;
            // 
            // ReportDataGridView
            // 
            ReportDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ReportDataGridView.Location = new Point(74, 109);
            ReportDataGridView.Name = "ReportDataGridView";
            ReportDataGridView.RowHeadersWidth = 62;
            ReportDataGridView.RowTemplate.Height = 33;
            ReportDataGridView.Size = new Size(772, 601);
            ReportDataGridView.TabIndex = 4;
            // 
            // RefreshButton
            // 
            RefreshButton.Location = new Point(663, 42);
            RefreshButton.Name = "RefreshButton";
            RefreshButton.Size = new Size(112, 34);
            RefreshButton.TabIndex = 5;
            RefreshButton.Text = "Refresh";
            RefreshButton.UseVisualStyleBackColor = true;
            // 
            // CompareReportForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1163, 753);
            Controls.Add(RefreshButton);
            Controls.Add(ReportDataGridView);
            Controls.Add(PriorYearTextBox);
            Controls.Add(label2);
            Controls.Add(CurrentYearTextBox);
            Controls.Add(label1);
            Name = "CompareReportForm";
            Text = "CompareReportForm";
            ((System.ComponentModel.ISupportInitialize)ReportDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox CurrentYearTextBox;
        private Label label2;
        private TextBox PriorYearTextBox;
        private DataGridView ReportDataGridView;
        private Button RefreshButton;
    }
}