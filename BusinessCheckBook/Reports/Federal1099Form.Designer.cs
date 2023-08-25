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
            ((System.ComponentModel.ISupportInitialize)ReportDataGridView).BeginInit();
            SuspendLayout();
            // 
            // ExportButton
            // 
            ExportButton.Location = new Point(603, 30);
            ExportButton.Name = "ExportButton";
            ExportButton.Size = new Size(153, 32);
            ExportButton.TabIndex = 9;
            ExportButton.Text = "Export To CSV";
            ExportButton.UseVisualStyleBackColor = true;
            // 
            // ReportDataGridView
            // 
            ReportDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ReportDataGridView.Location = new Point(53, 97);
            ReportDataGridView.Name = "ReportDataGridView";
            ReportDataGridView.RowHeadersWidth = 62;
            ReportDataGridView.RowTemplate.Height = 33;
            ReportDataGridView.Size = new Size(949, 331);
            ReportDataGridView.TabIndex = 8;
            // 
            // RefreshButton
            // 
            RefreshButton.Location = new Point(378, 30);
            RefreshButton.Name = "RefreshButton";
            RefreshButton.Size = new Size(112, 32);
            RefreshButton.TabIndex = 7;
            RefreshButton.Text = "Refresh";
            RefreshButton.UseVisualStyleBackColor = true;
            // 
            // ReportYearTextBox
            // 
            ReportYearTextBox.Location = new Point(222, 30);
            ReportYearTextBox.Name = "ReportYearTextBox";
            ReportYearTextBox.Size = new Size(81, 31);
            ReportYearTextBox.TabIndex = 6;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(53, 33);
            label1.Name = "label1";
            label1.Size = new Size(163, 25);
            label1.TabIndex = 5;
            label1.Text = "Tax Report for Year:";
            // 
            // Federal1099
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1079, 450);
            Controls.Add(ExportButton);
            Controls.Add(ReportDataGridView);
            Controls.Add(RefreshButton);
            Controls.Add(ReportYearTextBox);
            Controls.Add(label1);
            Name = "Federal1099";
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
    }
}