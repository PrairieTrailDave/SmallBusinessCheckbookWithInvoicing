namespace BusinessCheckBook
{
    partial class InitialBalanceForm
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
            StartingBalanceTextBox = new TextBox();
            label2 = new Label();
            FirstCheckNumberTextBox = new TextBox();
            SaveButton = new Button();
            label3 = new Label();
            FirstInvoiceNumberTextBox = new TextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(61, 94);
            label1.Name = "label1";
            label1.Size = new Size(148, 15);
            label1.TabIndex = 7;
            label1.Text = "Checking Starting Balance:";
            // 
            // StartingBalanceTextBox
            // 
            StartingBalanceTextBox.Location = new Point(225, 91);
            StartingBalanceTextBox.Name = "StartingBalanceTextBox";
            StartingBalanceTextBox.Size = new Size(159, 23);
            StartingBalanceTextBox.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(94, 58);
            label2.Name = "label2";
            label2.Size = new Size(115, 15);
            label2.TabIndex = 6;
            label2.Text = "First Check Number:";
            // 
            // FirstCheckNumberTextBox
            // 
            FirstCheckNumberTextBox.Location = new Point(225, 50);
            FirstCheckNumberTextBox.Name = "FirstCheckNumberTextBox";
            FirstCheckNumberTextBox.Size = new Size(100, 23);
            FirstCheckNumberTextBox.TabIndex = 1;
            // 
            // SaveButton
            // 
            SaveButton.Location = new Point(134, 182);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(75, 23);
            SaveButton.TabIndex = 4;
            SaveButton.Text = "Save";
            SaveButton.UseVisualStyleBackColor = true;
            SaveButton.Click += SaveButton_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(89, 134);
            label3.Name = "label3";
            label3.Size = new Size(120, 15);
            label3.TabIndex = 8;
            label3.Text = "First Invoice Number:";
            // 
            // FirstInvoiceNumberTextBox
            // 
            FirstInvoiceNumberTextBox.Location = new Point(225, 131);
            FirstInvoiceNumberTextBox.Name = "FirstInvoiceNumberTextBox";
            FirstInvoiceNumberTextBox.Size = new Size(100, 23);
            FirstInvoiceNumberTextBox.TabIndex = 3;
            // 
            // InitialBalanceForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(465, 259);
            Controls.Add(FirstInvoiceNumberTextBox);
            Controls.Add(label3);
            Controls.Add(SaveButton);
            Controls.Add(FirstCheckNumberTextBox);
            Controls.Add(label2);
            Controls.Add(StartingBalanceTextBox);
            Controls.Add(label1);
            Name = "InitialBalanceForm";
            Text = "Account Set Up ";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox StartingBalanceTextBox;
        private Label label2;
        private TextBox FirstCheckNumberTextBox;
        private Button SaveButton;
        private Label label3;
        private TextBox FirstInvoiceNumberTextBox;
    }
}