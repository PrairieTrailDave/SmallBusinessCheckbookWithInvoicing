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
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)ReportDataGridView).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(35, 28);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(72, 15);
            label1.TabIndex = 0;
            label1.Text = "Current Year";
            // 
            // CurrentYearTextBox
            // 
            CurrentYearTextBox.Location = new Point(114, 26);
            CurrentYearTextBox.Margin = new Padding(2, 2, 2, 2);
            CurrentYearTextBox.Name = "CurrentYearTextBox";
            CurrentYearTextBox.Size = new Size(78, 23);
            CurrentYearTextBox.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(224, 28);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(57, 15);
            label2.TabIndex = 2;
            label2.Text = "Prior Year";
            // 
            // PriorYearTextBox
            // 
            PriorYearTextBox.Location = new Point(288, 26);
            PriorYearTextBox.Margin = new Padding(2, 2, 2, 2);
            PriorYearTextBox.Name = "PriorYearTextBox";
            PriorYearTextBox.Size = new Size(106, 23);
            PriorYearTextBox.TabIndex = 3;
            // 
            // ReportDataGridView
            // 
            ReportDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ReportDataGridView.Location = new Point(52, 65);
            ReportDataGridView.Margin = new Padding(2, 2, 2, 2);
            ReportDataGridView.Name = "ReportDataGridView";
            ReportDataGridView.RowHeadersWidth = 62;
            ReportDataGridView.RowTemplate.Height = 33;
            ReportDataGridView.Size = new Size(540, 361);
            ReportDataGridView.TabIndex = 4;
            // 
            // RefreshButton
            // 
            RefreshButton.Location = new Point(464, 25);
            RefreshButton.Margin = new Padding(2, 2, 2, 2);
            RefreshButton.Name = "RefreshButton";
            RefreshButton.Size = new Size(78, 20);
            RefreshButton.TabIndex = 5;
            RefreshButton.Text = "Refresh";
            RefreshButton.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(35, 9);
            label3.Name = "label3";
            label3.Size = new Size(218, 15);
            label3.TabIndex = 6;
            label3.Text = "Copyright 2023 Prarie Trail Software, Inc.";
            label3.Visible = false;
            // 
            // CompareReportForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(814, 452);
            Controls.Add(label3);
            Controls.Add(RefreshButton);
            Controls.Add(ReportDataGridView);
            Controls.Add(PriorYearTextBox);
            Controls.Add(label2);
            Controls.Add(CurrentYearTextBox);
            Controls.Add(label1);
            Margin = new Padding(2, 2, 2, 2);
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
        private Label label3;
    }
}