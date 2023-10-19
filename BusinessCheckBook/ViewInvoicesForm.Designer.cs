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
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)InvoicesDataGridView).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(23, 26);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(93, 15);
            label1.TabIndex = 0;
            label1.Text = "Select Customer";
            // 
            // SelectCustomerComboBox
            // 
            SelectCustomerComboBox.FormattingEnabled = true;
            SelectCustomerComboBox.Location = new Point(133, 24);
            SelectCustomerComboBox.Margin = new Padding(2);
            SelectCustomerComboBox.Name = "SelectCustomerComboBox";
            SelectCustomerComboBox.Size = new Size(185, 23);
            SelectCustomerComboBox.TabIndex = 1;
            SelectCustomerComboBox.SelectedIndexChanged += SelectCustomerComboBox_SelectedIndexChanged;
            // 
            // InvoicesDataGridView
            // 
            InvoicesDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            InvoicesDataGridView.Location = new Point(46, 69);
            InvoicesDataGridView.Margin = new Padding(2);
            InvoicesDataGridView.Name = "InvoicesDataGridView";
            InvoicesDataGridView.RowHeadersWidth = 62;
            InvoicesDataGridView.RowTemplate.Height = 33;
            InvoicesDataGridView.Size = new Size(727, 213);
            InvoicesDataGridView.TabIndex = 2;
            InvoicesDataGridView.CellContentClick += InvoicesDataGridView_CellContentClick;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, -3);
            label3.Name = "label3";
            label3.Size = new Size(218, 15);
            label3.TabIndex = 7;
            label3.Text = "Copyright 2023 Prarie Trail Software, Inc.";
            label3.Visible = false;
            // 
            // ViewInvoicesForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(828, 306);
            Controls.Add(label3);
            Controls.Add(InvoicesDataGridView);
            Controls.Add(SelectCustomerComboBox);
            Controls.Add(label1);
            Margin = new Padding(2);
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
        private Label label3;
    }
}