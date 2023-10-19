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
            label3 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(27, 17);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(115, 15);
            label1.TabIndex = 0;
            label1.Text = "Account Not Found:";
            // 
            // AccountNotFoundTextBox
            // 
            AccountNotFoundTextBox.Location = new Point(151, 16);
            AccountNotFoundTextBox.Margin = new Padding(2, 2, 2, 2);
            AccountNotFoundTextBox.Name = "AccountNotFoundTextBox";
            AccountNotFoundTextBox.ReadOnly = true;
            AccountNotFoundTextBox.Size = new Size(106, 23);
            AccountNotFoundTextBox.TabIndex = 1;
            // 
            // AccountsListBox
            // 
            AccountsListBox.FormattingEnabled = true;
            AccountsListBox.ItemHeight = 15;
            AccountsListBox.Location = new Point(49, 57);
            AccountsListBox.Margin = new Padding(2, 2, 2, 2);
            AccountsListBox.Name = "AccountsListBox";
            AccountsListBox.Size = new Size(420, 184);
            AccountsListBox.TabIndex = 2;
            AccountsListBox.SelectedIndexChanged += AccountsListBox_SelectedIndexChanged;
            // 
            // DoneButton
            // 
            DoneButton.Location = new Point(531, 213);
            DoneButton.Margin = new Padding(2, 2, 2, 2);
            DoneButton.Name = "DoneButton";
            DoneButton.Size = new Size(78, 20);
            DoneButton.TabIndex = 3;
            DoneButton.Text = "Done";
            DoneButton.UseVisualStyleBackColor = true;
            DoneButton.Click += DoneButton_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(276, 17);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(66, 15);
            label2.TabIndex = 4;
            label2.Text = "Change To:";
            // 
            // ChangeToTextBox
            // 
            ChangeToTextBox.Location = new Point(354, 16);
            ChangeToTextBox.Margin = new Padding(2, 2, 2, 2);
            ChangeToTextBox.Name = "ChangeToTextBox";
            ChangeToTextBox.Size = new Size(257, 23);
            ChangeToTextBox.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(27, -1);
            label3.Name = "label3";
            label3.Size = new Size(218, 15);
            label3.TabIndex = 7;
            label3.Text = "Copyright 2023 Prarie Trail Software, Inc.";
            label3.Visible = false;
            // 
            // ChangeAccountForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(675, 270);
            Controls.Add(label3);
            Controls.Add(ChangeToTextBox);
            Controls.Add(label2);
            Controls.Add(DoneButton);
            Controls.Add(AccountsListBox);
            Controls.Add(AccountNotFoundTextBox);
            Controls.Add(label1);
            Margin = new Padding(2, 2, 2, 2);
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
        private Label label3;
    }
}