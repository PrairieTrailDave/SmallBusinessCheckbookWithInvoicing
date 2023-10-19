namespace BusinessCheckBook
{
    partial class Federal1099Form
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
            ExportButton = new Button();
            ReportDataGridView = new DataGridView();
            RefreshButton = new Button();
            ReportYearTextBox = new TextBox();
            label1 = new Label();
            saveExportFileDialog = new SaveFileDialog();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)ReportDataGridView).BeginInit();
            SuspendLayout();
            // 
            // ExportButton
            // 
            ExportButton.Location = new Point(422, 18);
            ExportButton.Margin = new Padding(2, 2, 2, 2);
            ExportButton.Name = "ExportButton";
            ExportButton.Size = new Size(107, 19);
            ExportButton.TabIndex = 9;
            ExportButton.Text = "Export To CSV";
            ExportButton.UseVisualStyleBackColor = true;
            // 
            // ReportDataGridView
            // 
            ReportDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ReportDataGridView.Location = new Point(37, 58);
            ReportDataGridView.Margin = new Padding(2, 2, 2, 2);
            ReportDataGridView.Name = "ReportDataGridView";
            ReportDataGridView.RowHeadersWidth = 62;
            ReportDataGridView.RowTemplate.Height = 33;
            ReportDataGridView.Size = new Size(664, 199);
            ReportDataGridView.TabIndex = 8;
            // 
            // RefreshButton
            // 
            RefreshButton.Location = new Point(265, 18);
            RefreshButton.Margin = new Padding(2, 2, 2, 2);
            RefreshButton.Name = "RefreshButton";
            RefreshButton.Size = new Size(78, 19);
            RefreshButton.TabIndex = 7;
            RefreshButton.Text = "Refresh";
            RefreshButton.UseVisualStyleBackColor = true;
            // 
            // ReportYearTextBox
            // 
            ReportYearTextBox.Location = new Point(155, 18);
            ReportYearTextBox.Margin = new Padding(2, 2, 2, 2);
            ReportYearTextBox.Name = "ReportYearTextBox";
            ReportYearTextBox.Size = new Size(58, 23);
            ReportYearTextBox.TabIndex = 6;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(37, 20);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(108, 15);
            label1.TabIndex = 5;
            label1.Text = "Tax Report for Year:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(37, 1);
            label3.Name = "label3";
            label3.Size = new Size(218, 15);
            label3.TabIndex = 10;
            label3.Text = "Copyright 2023 Prarie Trail Software, Inc.";
            label3.Visible = false;
            // 
            // Federal1099Form
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(755, 270);
            Controls.Add(label3);
            Controls.Add(ExportButton);
            Controls.Add(ReportDataGridView);
            Controls.Add(RefreshButton);
            Controls.Add(ReportYearTextBox);
            Controls.Add(label1);
            Margin = new Padding(2, 2, 2, 2);
            Name = "Federal1099Form";
            Text = "Federal1099";
            ((System.ComponentModel.ISupportInitialize)ReportDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button ExportButton;
        private DataGridView ReportDataGridView;
        private Button RefreshButton;
        private TextBox ReportYearTextBox;
        private Label label1;
        private SaveFileDialog saveExportFileDialog;
        private Label label3;
    }
}