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
            label1.Location = new Point(41, 27);
            label1.Name = "label1";
            label1.Size = new Size(163, 25);
            label1.TabIndex = 0;
            label1.Text = "Tax Report for Year:";
            // 
            // ReportYearTextBox
            // 
            ReportYearTextBox.Location = new Point(210, 24);
            ReportYearTextBox.Name = "ReportYearTextBox";
            ReportYearTextBox.Size = new Size(81, 31);
            ReportYearTextBox.TabIndex = 1;
            // 
            // RefreshButton
            // 
            RefreshButton.Location = new Point(366, 22);
            RefreshButton.Name = "RefreshButton";
            RefreshButton.Size = new Size(112, 34);
            RefreshButton.TabIndex = 2;
            RefreshButton.Text = "Refresh";
            RefreshButton.UseVisualStyleBackColor = true;
            RefreshButton.Click += RefreshButton_Click;
            // 
            // ReportDataGridView
            // 
            ReportDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ReportDataGridView.Location = new Point(41, 78);
            ReportDataGridView.Name = "ReportDataGridView";
            ReportDataGridView.RowHeadersWidth = 62;
            ReportDataGridView.RowTemplate.Height = 33;
            ReportDataGridView.Size = new Size(949, 642);
            ReportDataGridView.TabIndex = 3;
            // 
            // ExportButton
            // 
            ExportButton.Location = new Point(591, 22);
            ExportButton.Name = "ExportButton";
            ExportButton.Size = new Size(153, 34);
            ExportButton.TabIndex = 4;
            ExportButton.Text = "Export To CSV";
            ExportButton.UseVisualStyleBackColor = true;
            ExportButton.Click += ExportButton_Click;
            // 
            // Federal1120Form
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1146, 803);
            Controls.Add(ExportButton);
            Controls.Add(ReportDataGridView);
            Controls.Add(RefreshButton);
            Controls.Add(ReportYearTextBox);
            Controls.Add(label1);
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