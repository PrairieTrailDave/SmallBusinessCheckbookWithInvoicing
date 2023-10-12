namespace BusinessCheckBook.Reports
{
    partial class Federal1120Form
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
            ReportYearTextBox = new TextBox();
            RefreshButton = new Button();
            ReportDataGridView = new DataGridView();
            ExportButton = new Button();
            saveExportFileDialog = new SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)ReportDataGridView).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(29, 16);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(108, 15);
            label1.TabIndex = 0;
            label1.Text = "Tax Report for Year:";
            // 
            // ReportYearTextBox
            // 
            ReportYearTextBox.Location = new Point(147, 14);
            ReportYearTextBox.Margin = new Padding(2, 2, 2, 2);
            ReportYearTextBox.Name = "ReportYearTextBox";
            ReportYearTextBox.Size = new Size(58, 23);
            ReportYearTextBox.TabIndex = 1;
            // 
            // RefreshButton
            // 
            RefreshButton.Location = new Point(256, 13);
            RefreshButton.Margin = new Padding(2, 2, 2, 2);
            RefreshButton.Name = "RefreshButton";
            RefreshButton.Size = new Size(78, 24);
            RefreshButton.TabIndex = 2;
            RefreshButton.Text = "Refresh";
            RefreshButton.UseVisualStyleBackColor = true;
            RefreshButton.Click += RefreshButton_Click;
            // 
            // ReportDataGridView
            // 
            ReportDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ReportDataGridView.Location = new Point(29, 47);
            ReportDataGridView.Margin = new Padding(2, 2, 2, 2);
            ReportDataGridView.Name = "ReportDataGridView";
            ReportDataGridView.RowHeadersWidth = 62;
            ReportDataGridView.RowTemplate.Height = 33;
            ReportDataGridView.Size = new Size(664, 385);
            ReportDataGridView.TabIndex = 3;
            // 
            // ExportButton
            // 
            ExportButton.Location = new Point(414, 13);
            ExportButton.Margin = new Padding(2, 2, 2, 2);
            ExportButton.Name = "ExportButton";
            ExportButton.Size = new Size(107, 24);
            ExportButton.TabIndex = 4;
            ExportButton.Text = "Export To CSV";
            ExportButton.UseVisualStyleBackColor = true;
            ExportButton.Click += ExportButton_Click;
            // 
            // Federal1120Form
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(802, 482);
            Controls.Add(ExportButton);
            Controls.Add(ReportDataGridView);
            Controls.Add(RefreshButton);
            Controls.Add(ReportYearTextBox);
            Controls.Add(label1);
            Margin = new Padding(2, 2, 2, 2);
            Name = "Federal1120Form";
            Text = "Federal1120Form";
            ((System.ComponentModel.ISupportInitialize)ReportDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox ReportYearTextBox;
        private Button RefreshButton;
        private DataGridView ReportDataGridView;
        private Button ExportButton;
        private SaveFileDialog saveExportFileDialog;
    }
}