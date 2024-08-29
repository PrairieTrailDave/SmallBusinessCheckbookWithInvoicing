namespace BusinessCheckBook.TimeTracking
{
    partial class ManageProjectForm
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
            ProjectsListBox = new ListBox();
            DoneButton = new Button();
            panel1 = new Panel();
            DeactivateButton = new Button();
            IsActiveCheckBox = new CheckBox();
            ClearButton = new Button();
            AddProjectButton = new Button();
            MyCancelButton = new Button();
            UpdateButton = new Button();
            BillRateTextBox = new TextBox();
            label5 = new Label();
            ProjectDescriptionTextBox = new TextBox();
            label3 = new Label();
            ProjectNameTextBox = new TextBox();
            label2 = new Label();
            label6 = new Label();
            ShowAllProjectsButton = new Button();
            WhichCustomerComboBox = new ComboBox();
            label4 = new Label();
            label7 = new Label();
            ChartOfAccountComboBox = new ComboBox();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(55, 30);
            label1.Name = "label1";
            label1.Size = new Size(207, 25);
            label1.TabIndex = 0;
            label1.Text = "Pick a Project to Manage";
            // 
            // ProjectsListBox
            // 
            ProjectsListBox.FormattingEnabled = true;
            ProjectsListBox.ItemHeight = 25;
            ProjectsListBox.Location = new Point(55, 83);
            ProjectsListBox.Name = "ProjectsListBox";
            ProjectsListBox.Size = new Size(428, 404);
            ProjectsListBox.TabIndex = 1;
            ProjectsListBox.SelectedIndexChanged += ProjectsListBox_SelectedIndexChanged;
            // 
            // DoneButton
            // 
            DoneButton.Location = new Point(55, 493);
            DoneButton.Name = "DoneButton";
            DoneButton.Size = new Size(112, 34);
            DoneButton.TabIndex = 2;
            DoneButton.Text = "Done";
            DoneButton.UseVisualStyleBackColor = true;
            DoneButton.Click += DoneButton_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(ChartOfAccountComboBox);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(WhichCustomerComboBox);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(DeactivateButton);
            panel1.Controls.Add(IsActiveCheckBox);
            panel1.Controls.Add(ClearButton);
            panel1.Controls.Add(AddProjectButton);
            panel1.Controls.Add(MyCancelButton);
            panel1.Controls.Add(UpdateButton);
            panel1.Controls.Add(BillRateTextBox);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(ProjectDescriptionTextBox);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(ProjectNameTextBox);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label6);
            panel1.Location = new Point(547, 77);
            panel1.Name = "panel1";
            panel1.Size = new Size(738, 450);
            panel1.TabIndex = 4;
            // 
            // DeactivateButton
            // 
            DeactivateButton.Location = new Point(378, 351);
            DeactivateButton.Name = "DeactivateButton";
            DeactivateButton.Size = new Size(156, 34);
            DeactivateButton.TabIndex = 25;
            DeactivateButton.Text = "Make Inactive";
            DeactivateButton.UseVisualStyleBackColor = true;
            DeactivateButton.Click += DeactivateButton_Click;
            // 
            // IsActiveCheckBox
            // 
            IsActiveCheckBox.AutoSize = true;
            IsActiveCheckBox.Location = new Point(238, 355);
            IsActiveCheckBox.Name = "IsActiveCheckBox";
            IsActiveCheckBox.Size = new Size(104, 29);
            IsActiveCheckBox.TabIndex = 24;
            IsActiveCheckBox.Text = "Is Active";
            IsActiveCheckBox.UseVisualStyleBackColor = true;
            // 
            // ClearButton
            // 
            ClearButton.Location = new Point(215, 406);
            ClearButton.Name = "ClearButton";
            ClearButton.Size = new Size(112, 34);
            ClearButton.TabIndex = 23;
            ClearButton.Text = "Clear";
            ClearButton.UseVisualStyleBackColor = true;
            ClearButton.Click += ClearButton_Click;
            // 
            // AddProjectButton
            // 
            AddProjectButton.Location = new Point(30, 406);
            AddProjectButton.Name = "AddProjectButton";
            AddProjectButton.Size = new Size(151, 34);
            AddProjectButton.TabIndex = 22;
            AddProjectButton.Text = "Add Project";
            AddProjectButton.UseVisualStyleBackColor = true;
            AddProjectButton.Click += AddProjectButton_Click;
            // 
            // MyCancelButton
            // 
            MyCancelButton.Location = new Point(544, 406);
            MyCancelButton.Name = "MyCancelButton";
            MyCancelButton.Size = new Size(112, 34);
            MyCancelButton.TabIndex = 21;
            MyCancelButton.Text = "Cancel";
            MyCancelButton.UseVisualStyleBackColor = true;
            MyCancelButton.Click += MyCancelButton_Click;
            // 
            // UpdateButton
            // 
            UpdateButton.Location = new Point(363, 406);
            UpdateButton.Name = "UpdateButton";
            UpdateButton.Size = new Size(112, 34);
            UpdateButton.TabIndex = 20;
            UpdateButton.Text = "Update";
            UpdateButton.UseVisualStyleBackColor = true;
            UpdateButton.Click += UpdateButton_Click;
            // 
            // BillRateTextBox
            // 
            BillRateTextBox.Location = new Point(238, 294);
            BillRateTextBox.Name = "BillRateTextBox";
            BillRateTextBox.Size = new Size(118, 31);
            BillRateTextBox.TabIndex = 36;
            BillRateTextBox.KeyPress += BillRateTextBox_KeyPress;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(30, 297);
            label5.Name = "label5";
            label5.Size = new Size(78, 25);
            label5.TabIndex = 18;
            label5.Text = "Bill Rate:";
            // 
            // ProjectDescriptionTextBox
            // 
            ProjectDescriptionTextBox.Location = new Point(238, 165);
            ProjectDescriptionTextBox.Name = "ProjectDescriptionTextBox";
            ProjectDescriptionTextBox.Size = new Size(489, 31);
            ProjectDescriptionTextBox.TabIndex = 32;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(30, 168);
            label3.Name = "label3";
            label3.Size = new Size(165, 25);
            label3.TabIndex = 14;
            label3.Text = "Project Description:";
            // 
            // ProjectNameTextBox
            // 
            ProjectNameTextBox.Location = new Point(238, 114);
            ProjectNameTextBox.Name = "ProjectNameTextBox";
            ProjectNameTextBox.Size = new Size(448, 31);
            ProjectNameTextBox.TabIndex = 30;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(30, 117);
            label2.Name = "label2";
            label2.Size = new Size(122, 25);
            label2.TabIndex = 12;
            label2.Text = "Project Name:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(30, 33);
            label6.Name = "label6";
            label6.Size = new Size(151, 25);
            label6.TabIndex = 11;
            label6.Text = "Project Properties";
            // 
            // ShowAllProjectsButton
            // 
            ShowAllProjectsButton.Location = new Point(268, 493);
            ShowAllProjectsButton.Name = "ShowAllProjectsButton";
            ShowAllProjectsButton.Size = new Size(215, 34);
            ShowAllProjectsButton.TabIndex = 5;
            ShowAllProjectsButton.Text = "Show All Projects";
            ShowAllProjectsButton.UseVisualStyleBackColor = true;
            ShowAllProjectsButton.Click += ShowAllProjectsButton_Click;
            // 
            // WhichCustomerComboBox
            // 
            WhichCustomerComboBox.FormattingEnabled = true;
            WhichCustomerComboBox.Location = new Point(238, 65);
            WhichCustomerComboBox.Name = "WhichCustomerComboBox";
            WhichCustomerComboBox.Size = new Size(467, 33);
            WhichCustomerComboBox.TabIndex = 27;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(30, 68);
            label4.Name = "label4";
            label4.Size = new Size(178, 25);
            label4.TabIndex = 26;
            label4.Text = "For Which Customer:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(30, 214);
            label7.Name = "label7";
            label7.Size = new Size(196, 25);
            label7.TabIndex = 28;
            label7.Text = "Select Account to Allot:";
            // 
            // ChartOfAccountComboBox
            // 
            ChartOfAccountComboBox.FormattingEnabled = true;
            ChartOfAccountComboBox.Location = new Point(238, 211);
            ChartOfAccountComboBox.Name = "ChartOfAccountComboBox";
            ChartOfAccountComboBox.Size = new Size(296, 33);
            ChartOfAccountComboBox.TabIndex = 34;
            // 
            // ManageProjectForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1297, 625);
            Controls.Add(ShowAllProjectsButton);
            Controls.Add(panel1);
            Controls.Add(DoneButton);
            Controls.Add(ProjectsListBox);
            Controls.Add(label1);
            Name = "ManageProjectForm";
            Text = "Manage Projects";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ListBox ProjectsListBox;
        private Button DoneButton;
        private Panel panel1;
        private Button MyCancelButton;
        private Button UpdateButton;
        private TextBox BillRateTextBox;
        private Label label5;
        private TextBox ProjectDescriptionTextBox;
        private Label label3;
        private TextBox ProjectNameTextBox;
        private Label label2;
        private Label label6;
        private Button ClearButton;
        private Button AddProjectButton;
        private CheckBox IsActiveCheckBox;
        private Button DeactivateButton;
        private Button ShowAllProjectsButton;
        private ComboBox ChartOfAccountComboBox;
        private Label label7;
        private ComboBox WhichCustomerComboBox;
        private Label label4;
    }
}