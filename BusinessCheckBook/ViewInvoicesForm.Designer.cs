namespace BusinessCheckBook
{
    partial class ViewInvoicesForm
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
            SelectCustomerComboBox = new ComboBox();
            InvoicesDataGridView = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)InvoicesDataGridView).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(33, 43);
            label1.Name = "label1";
            label1.Size = new Size(140, 25);
            label1.TabIndex = 0;
            label1.Text = "Select Customer";
            // 
            // SelectCustomerComboBox
            // 
            SelectCustomerComboBox.FormattingEnabled = true;
            SelectCustomerComboBox.Location = new Point(190, 40);
            SelectCustomerComboBox.Name = "SelectCustomerComboBox";
            SelectCustomerComboBox.Size = new Size(263, 33);
            SelectCustomerComboBox.TabIndex = 1;
            SelectCustomerComboBox.SelectedIndexChanged += SelectCustomerComboBox_SelectedIndexChanged;
            // 
            // InvoicesDataGridView
            // 
            InvoicesDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            InvoicesDataGridView.Location = new Point(66, 115);
            InvoicesDataGridView.Name = "InvoicesDataGridView";
            InvoicesDataGridView.RowHeadersWidth = 62;
            InvoicesDataGridView.RowTemplate.Height = 33;
            InvoicesDataGridView.Size = new Size(1039, 355);
            InvoicesDataGridView.TabIndex = 2;
            // 
            // ViewInvoicesForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1183, 510);
            Controls.Add(InvoicesDataGridView);
            Controls.Add(SelectCustomerComboBox);
            Controls.Add(label1);
            Name = "ViewInvoicesForm";
            Text = "View Invoices";
            ((System.ComponentModel.ISupportInitialize)InvoicesDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ComboBox SelectCustomerComboBox;
        private DataGridView InvoicesDataGridView;
    }
}