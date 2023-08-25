namespace BusinessCheckBook
{
    partial class ChangeAccountForm
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
            AccountNotFoundTextBox = new TextBox();
            AccountsListBox = new ListBox();
            DoneButton = new Button();
            label2 = new Label();
            ChangeToTextBox = new TextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(38, 29);
            label1.Name = "label1";
            label1.Size = new Size(172, 25);
            label1.TabIndex = 0;
            label1.Text = "Account Not Found:";
            // 
            // AccountNotFoundTextBox
            // 
            AccountNotFoundTextBox.Location = new Point(216, 26);
            AccountNotFoundTextBox.Name = "AccountNotFoundTextBox";
            AccountNotFoundTextBox.ReadOnly = true;
            AccountNotFoundTextBox.Size = new Size(150, 31);
            AccountNotFoundTextBox.TabIndex = 1;
            // 
            // AccountsListBox
            // 
            AccountsListBox.FormattingEnabled = true;
            AccountsListBox.ItemHeight = 25;
            AccountsListBox.Location = new Point(70, 95);
            AccountsListBox.Name = "AccountsListBox";
            AccountsListBox.Size = new Size(599, 304);
            AccountsListBox.TabIndex = 2;
            AccountsListBox.SelectedIndexChanged += AccountsListBox_SelectedIndexChanged;
            // 
            // DoneButton
            // 
            DoneButton.Location = new Point(758, 355);
            DoneButton.Name = "DoneButton";
            DoneButton.Size = new Size(112, 34);
            DoneButton.TabIndex = 3;
            DoneButton.Text = "Done";
            DoneButton.UseVisualStyleBackColor = true;
            DoneButton.Click += DoneButton_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(394, 29);
            label2.Name = "label2";
            label2.Size = new Size(99, 25);
            label2.TabIndex = 4;
            label2.Text = "Change To:";
            // 
            // ChangeToTextBox
            // 
            ChangeToTextBox.Location = new Point(505, 26);
            ChangeToTextBox.Name = "ChangeToTextBox";
            ChangeToTextBox.Size = new Size(365, 31);
            ChangeToTextBox.TabIndex = 5;
            // 
            // ChangeAccountForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(964, 450);
            Controls.Add(ChangeToTextBox);
            Controls.Add(label2);
            Controls.Add(DoneButton);
            Controls.Add(AccountsListBox);
            Controls.Add(AccountNotFoundTextBox);
            Controls.Add(label1);
            Name = "ChangeAccountForm";
            Text = "Change Account";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox AccountNotFoundTextBox;
        private ListBox AccountsListBox;
        private Button DoneButton;
        private Label label2;
        private TextBox ChangeToTextBox;
    }
}