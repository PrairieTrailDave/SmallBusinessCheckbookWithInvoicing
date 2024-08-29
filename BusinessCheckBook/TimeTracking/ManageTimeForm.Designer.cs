namespace BusinessCheckBook.TimeTracking
{
    partial class ManageTimeForm
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
            PersonsListBox = new ListBox();
            WhichDateTimePicker = new DateTimePicker();
            label2 = new Label();
            EnterTimeButton = new Button();
            PersonSelectedLabel = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(56, 42);
            label1.Name = "label1";
            label1.Size = new Size(271, 25);
            label1.TabIndex = 0;
            label1.Text = "Select a Person to Enter Time For";
            // 
            // PersonsListBox
            // 
            PersonsListBox.FormattingEnabled = true;
            PersonsListBox.ItemHeight = 25;
            PersonsListBox.Location = new Point(56, 95);
            PersonsListBox.Name = "PersonsListBox";
            PersonsListBox.Size = new Size(271, 304);
            PersonsListBox.TabIndex = 1;
            PersonsListBox.SelectedIndexChanged += PersonsListBox_SelectedIndexChanged;
            // 
            // WhichDateTimePicker
            // 
            WhichDateTimePicker.Location = new Point(388, 95);
            WhichDateTimePicker.Name = "WhichDateTimePicker";
            WhichDateTimePicker.Size = new Size(300, 31);
            WhichDateTimePicker.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(480, 42);
            label2.Name = "label2";
            label2.Size = new Size(100, 25);
            label2.TabIndex = 3;
            label2.Text = "Select Date";
            // 
            // EnterTimeButton
            // 
            EnterTimeButton.Location = new Point(495, 273);
            EnterTimeButton.Name = "EnterTimeButton";
            EnterTimeButton.Size = new Size(112, 34);
            EnterTimeButton.TabIndex = 4;
            EnterTimeButton.Text = "Enter Time";
            EnterTimeButton.UseVisualStyleBackColor = true;
            EnterTimeButton.Click += EnterTimeButton_Click;
            // 
            // PersonSelectedLabel
            // 
            PersonSelectedLabel.AutoSize = true;
            PersonSelectedLabel.Location = new Point(440, 225);
            PersonSelectedLabel.Name = "PersonSelectedLabel";
            PersonSelectedLabel.Size = new Size(0, 25);
            PersonSelectedLabel.TabIndex = 5;
            // 
            // ManageTimeForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(PersonSelectedLabel);
            Controls.Add(EnterTimeButton);
            Controls.Add(label2);
            Controls.Add(WhichDateTimePicker);
            Controls.Add(PersonsListBox);
            Controls.Add(label1);
            Name = "ManageTimeForm";
            Text = "Manage Time On Projects Form";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ListBox PersonsListBox;
        private DateTimePicker WhichDateTimePicker;
        private Label label2;
        private Button EnterTimeButton;
        private Label PersonSelectedLabel;
    }
}