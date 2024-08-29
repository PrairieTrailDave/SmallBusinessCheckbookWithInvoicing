using BusinessCheckBook.DataStore;
using BusinessCheckBook.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusinessCheckBook.TimeTracking
{
    public partial class ManageProjectForm : Form
    {
        // variables filled from the main window
        public MyCheckbook ActiveBook { get; set; } = new();


        // variables used internally

        private Project TProject = new();
        private bool ShowingAllProjects = false;

        public ManageProjectForm()
        {
            InitializeComponent();
        }
        public ManageProjectForm(MyCheckbook activeBook)
        {
            InitializeComponent();
            ActiveBook = activeBook;
            ShowExistingProjects();
            LoadCustomerList();
            LoadChartOfAccounts();
            AddProjectButton.Enabled = true;
            UpdateButton.Enabled = false;
            ShowingAllProjects = false;
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            ClearProject();
            AddProjectButton.Enabled = true;
            UpdateButton.Enabled = false;
        }

        private void AddProjectButton_Click(object sender, EventArgs e)
        {
            // if the input is valid

            if (ValidateInputs())
            {
                // save the new project
                var WhichCustomer = (DropDownItem)WhichCustomerComboBox.SelectedItem!;

                Project newProject = new()
                {
                    ProjectID = ProjectNameTextBox.Text,
                    Description = ProjectDescriptionTextBox.Text,
                    CustomerID = WhichCustomer.Value,
                    AllocatedAccount = ChartOfAccountComboBox.Text,
                    BillRate = Decimal.Parse(BillRateTextBox.Text),
                    IsActive = true
                };
                ActiveBook.CurrentProjects.AddProject(newProject);
                ShowExistingProjects();
                ClearProject();
                AddProjectButton.Enabled = true;
                UpdateButton.Enabled = false;
            }
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            // if the input is valid

            if (ValidateInputs())
            {
                // save the changed project
                TProject.ProjectID = ProjectNameTextBox.Text;
                TProject.Description = ProjectDescriptionTextBox.Text;
                TProject.CustomerID = WhichCustomerComboBox.Text;
                TProject.BillRate = Decimal.Parse(BillRateTextBox.Text);
                TProject.AllocatedAccount = ChartOfAccountComboBox.Text;

                ShowExistingProjects();
                ClearProject();
                AddProjectButton.Enabled = true;
                UpdateButton.Enabled = false;
            }
        }

        private void DeactivateButton_Click(object sender, EventArgs e)
        {
            if (TProject.IsActive)
                TProject.IsActive = false;
            else
                TProject.IsActive = true;
            ShowThisProject(TProject);
            ShowExistingProjects();
        }

        private void DoneButton_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void MyCancelButton_Click(object sender, EventArgs e)
        {
            // if a new project
            if (TProject.ProjectID.Length == 0)
                ClearProject();

            // if an existing project
            // redisplay initial values
            else
                ShowThisProject(TProject);
        }
        private void ShowAllProjectsButton_Click(object sender, EventArgs e)
        {
            if (ShowingAllProjects)
            {
                ShowingAllProjects = false;
                ShowAllProjectsButton.Text = "Show All Projects";
                ShowExistingProjects();
            }
            else
            {
                ShowingAllProjects = true;
                ShowAllProjectsButton.Text = "Show Active Projects";
                ShowExistingProjects();
            }
        }


        // Other UI methods

        private void ProjectsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ProjectID = ProjectsListBox.Text;
            Project AProject = ActiveBook.CurrentProjects.GetProject(ProjectID);
            if (AProject.ProjectID.Length > 0)
            {
                ShowThisProject(AProject);
                AddProjectButton.Enabled = false;
                UpdateButton.Enabled = true;
                TProject = AProject;
            }
            else MessageBox.Show("Couldn't find that project");
        }

        private void BillRateTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // only allow numbers and decimal point

            if (! (e.KeyChar == '.' || Char.IsDigit(e.KeyChar)))
                e.Handled = true;
        }



        // Support routines

        private void ShowExistingProjects()
        {
            ProjectsListBox.Items.Clear();
            foreach (Project PR in ActiveBook.CurrentProjects.GetProjects())
            {
                if (ShowingAllProjects)
                    ProjectsListBox.Items.Add(PR.ProjectID);
                else
                {
                    if (PR.IsActive)
                        ProjectsListBox.Items.Add(PR.ProjectID);
                }
            }
        }

        private void LoadCustomerList()
        {
            WhichCustomerComboBox.Items.Clear();
            foreach (Customer cu in ActiveBook.Customers.GetCurrentList())
            {
                if (cu.IsActive)
                {
                    WhichCustomerComboBox.Items.Add(new DropDownItem(cu.AccountName, cu.CustomerIdentifier));
                }
            }
        }
        private void LoadChartOfAccounts()
        {
            ChartOfAccountComboBox.Items.Clear();
            foreach (string Acc in ActiveBook.CurrentAccounts.GetListOfIncomeAccounts())
            {
                ChartOfAccountComboBox.Items.Add(Acc);
            }
        }

        private bool ValidateInputs()
        {
            Projects ProjectList = ActiveBook.CurrentProjects;
            if (!ProjectList.ValidateProjectName(ProjectNameTextBox.Text))
            {
                MessageBox.Show("Invalid Project Name");
                ProjectNameTextBox.Focus();
                return false;
            }

            if (!ProjectList.ValidateProjectDescription(ProjectDescriptionTextBox.Text))
            {
                MessageBox.Show("Invalid Project Description");
                ProjectDescriptionTextBox.Focus();
                return false;
            }

            // since it is a combo box, the values should be ok
            //WhichCustomerComboBox.Text = string.Empty;

            // likewise the chart of accounts should be ok


            if (!ProjectList.ValidateBillRate(BillRateTextBox.Text))
            {
                MessageBox.Show("Invalid Bill Rate");
                BillRateTextBox.Focus();
                return false;
            }

            return true;
        }

        private void ClearProject()
        {
            ProjectNameTextBox.Text = string.Empty;
            ProjectDescriptionTextBox.Text = string.Empty;
            WhichCustomerComboBox.Text = string.Empty;
            BillRateTextBox.Text = string.Empty;
            IsActiveCheckBox.Checked = false;
            ChartOfAccountComboBox.Text = string.Empty;
        }
        private void ShowThisProject(Project project)
        {
            ProjectNameTextBox.Text = project.ProjectID;
            ProjectDescriptionTextBox.Text = project.Description;
            WhichCustomerComboBox.Text = project.CustomerID;
            ChartOfAccountComboBox.Text = project.AllocatedAccount;
            BillRateTextBox.Text = project.BillRate.ToString();
            IsActiveCheckBox.Checked = project.IsActive;
            if (project.IsActive)
                DeactivateButton.Text = "Make Inactive";
            else
                DeactivateButton.Text = "Make Active";
        }

    }
}
