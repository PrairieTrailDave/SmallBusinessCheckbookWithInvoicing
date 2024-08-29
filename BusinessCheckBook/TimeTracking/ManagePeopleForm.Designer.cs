namespace BusinessCheckBook.TimeTracking
{
    partial class ManagePeopleForm
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
            PeopleListBox = new ListBox();
            AddButton = new Button();
            UpdateButton = new Button();
            DeleteButton = new Button();
            PersonTextBox = new TextBox();
            label1 = new Label();
            ClearButton = new Button();
            SuspendLayout();
            // 
            // PeopleListBox
            // 
            PeopleListBox.FormattingEnabled = true;
            PeopleListBox.ItemHeight = 25;
            PeopleListBox.Location = new Point(42, 59);
            PeopleListBox.Name = "PeopleListBox";
            PeopleListBox.Size = new Size(309, 329);
            PeopleListBox.TabIndex = 0;
            PeopleListBox.SelectedIndexChanged += PeopleListBox_SelectedIndexChanged;
            // 
            // AddButton
            // 
            AddButton.Location = new Point(393, 330);
            AddButton.Name = "AddButton";
            AddButton.Size = new Size(92, 34);
            AddButton.TabIndex = 1;
            AddButton.Text = "Add";
            AddButton.UseVisualStyleBackColor = true;
            AddButton.Click += AddButton_Click;
            // 
            // UpdateButton
            // 
            UpdateButton.Location = new Point(640, 330);
            UpdateButton.Name = "UpdateButton";
            UpdateButton.Size = new Size(112, 34);
            UpdateButton.TabIndex = 2;
            UpdateButton.Text = "Update";
            UpdateButton.UseVisualStyleBackColor = true;
            UpdateButton.Click += UpdateButton_Click;
            // 
            // DeleteButton
            // 
            DeleteButton.Location = new Point(777, 330);
            DeleteButton.Name = "DeleteButton";
            DeleteButton.Size = new Size(112, 34);
            DeleteButton.TabIndex = 3;
            DeleteButton.Text = "Delete";
            DeleteButton.UseVisualStyleBackColor = true;
            DeleteButton.Click += DeleteButton_Click;
            // 
            // PersonTextBox
            // 
            PersonTextBox.Location = new Point(393, 244);
            PersonTextBox.Name = "PersonTextBox";
            PersonTextBox.Size = new Size(505, 31);
            PersonTextBox.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(393, 197);
            label1.Name = "label1";
            label1.Size = new Size(160, 25);
            label1.TabIndex = 5;
            label1.Text = "Person for Projects";
            // 
            // ClearButton
            // 
            ClearButton.Location = new Point(508, 330);
            ClearButton.Name = "ClearButton";
            ClearButton.Size = new Size(82, 34);
            ClearButton.TabIndex = 6;
            ClearButton.Text = "Clear";
            ClearButton.UseVisualStyleBackColor = true;
            ClearButton.Click += ClearButton_Click;
            // 
            // ManagePeopleForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(910, 450);
            Controls.Add(ClearButton);
            Controls.Add(label1);
            Controls.Add(PersonTextBox);
            Controls.Add(DeleteButton);
            Controls.Add(UpdateButton);
            Controls.Add(AddButton);
            Controls.Add(PeopleListBox);
            Name = "ManagePeopleForm";
            Text = "People for projects";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox PeopleListBox;
        private Button AddButton;
        private Button UpdateButton;
        private Button DeleteButton;
        private TextBox PersonTextBox;
        private Label label1;
        private Button ClearButton;
    }
}