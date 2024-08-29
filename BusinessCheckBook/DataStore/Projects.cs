using BusinessCheckBook.Validation;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCheckBook.DataStore
{
    // This class is used to manage the projects that can be tracked with the daily activity.

    internal class Projects
    {
        // storage of the projects

        List<Project> CurrentProjects = new();

        // what the Excel sheet is supposed to look like
        internal SheetFormat ProjectFormat { get; set; } = new();

        private bool Changed;

        public Projects ()
        {
            CurrentProjects = [];
            SetSheetFormat();
            Changed = false; 
        }


        public bool IfChanged() { return Changed; }
        public void HasChanged() { Changed = true; }
        public void ClearChanged() { Changed = false; }


        public void AddProject(Project ToAdd)
        {
            CurrentProjects.Add(ToAdd);
            HasChanged();
        }

        public Project GetProject(int id) 
        { 
            if (id < 0 || id >= CurrentProjects.Count) return new Project();
            return CurrentProjects[id]; 
        }
        public Project GetProject(string ProjectID)
        {
            foreach (Project p in CurrentProjects)
            {
                if (p.ProjectID == ProjectID) return p;
            }
            return new Project();
        }

        public List<Project> GetProjects() { return CurrentProjects; }

        public List<Project> GetCustomerProjects(string CustomerID)
        {
            return CurrentProjects.Where(p => p.CustomerID == CustomerID && p.IsActive).ToList();
        }

        public List<string> GetProjectNames()
        {
            List <string> ProjectNames = new ();
            foreach (Project p in CurrentProjects)
            {
                if (p.IsActive)
                    ProjectNames.Add(p.ProjectID);
            }
            return ProjectNames;
        }



        #region Validation

        public bool Validate(XLWorkbook CheckBookXlsx, out string ErrorMessage)
        {
            ErrorMessage = "";
            IXLWorksheet ProjectsWorksheet;

            // a workbook might not support project list
            // a missing worksheet is not an error

            if (CheckBookXlsx.TryGetWorksheet(ProjectFormat.SheetName, out ProjectsWorksheet))
            {


                // the sheet exists. check all the headings and find which column is which

                try
                {
                    if (!Project.ValidateColumnHeaders(ProjectsWorksheet, ProjectFormat, out ErrorMessage))
                        return false;

                    // validate all the entries in the columns for validity
                    // at this point, we have all the columns that are required

                    int RowsUsedCount = ProjectsWorksheet.RowsUsed().Count();
                    for (int Row = 2; Row < RowsUsedCount; Row++)
                    {
                        IXLRow XRow = ProjectsWorksheet.Row(Row);
                        if (!Project.ValidateExcelRow(XRow, ProjectFormat, out ErrorMessage))
                        {
                            ErrorMessage += " in row " + Row.ToString();
                            return false;
                        }
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error in reading a cell " + ex.Message);
                    return false;
                }
            }
            return true;
        }


        internal bool ValidateProjectName(string projectName)
        {
            return Project.ValidateProjectName(projectName, ProjectFormat);
        }
        internal bool ValidateProjectDescription(string projectDescription)
        {
            return Project.ValidateProjectDescription(projectDescription, ProjectFormat);
        }
        internal bool ValidateCustomerID(string CustomerID)
        {
            return Project.ValidateCustomerID(CustomerID, ProjectFormat);
        }
        internal bool ValidateBillRate(string BillRate)
        {
            return Project.ValidateBillRate(BillRate, ProjectFormat);
        }



        // check the projects for consistency with the customer list

        internal bool CheckAgainstCustomerList(List<Customer> CustomerList, out string ErrorMessage)
        {
            ErrorMessage = string.Empty;
            foreach(Project PR in CurrentProjects)
            {
                bool Found = false;

                // only check active projects.
                // an inactive project may reference a customer who is not longer in existence

                if (!PR.IsActive) continue;  
                string WhichCustomer = PR.CustomerID;
                foreach(Customer TCustomer in CustomerList)
                {
                    // only check active customers

                    if (TCustomer.IsActive)
                    {
                        if (WhichCustomer == TCustomer.CustomerIdentifier)
                        {
                            Found = true; break;
                        }
                    }
                }
                if (!Found)
                {
                    ErrorMessage = "Customer " + WhichCustomer + " in project " + PR.ProjectID + "is not found or is inactive";
                    return false;
                }
            }
            return true;
        }

        #endregion Validation


        public bool ReadFromExcelFile(XLWorkbook CheckBookXlsx)
        {
            CurrentProjects = new();
            IXLWorksheet ProjectsWorksheet;

            // make this able to handle when the worksheet doesn't exist

            if (CheckBookXlsx.TryGetWorksheet(ProjectFormat.SheetName, out ProjectsWorksheet))
            {
                if (ProjectsWorksheet != null)
                {
                    int Row = 0;
                    foreach (var row in ProjectsWorksheet.RowsUsed())
                    {
                        // The header row was processed in the validation stage and we can use those values

                        Row++;
                        if (Row == 1) continue;

                        // Pull off the Project Values

                        Project NProject = new();
                        IXLRow xlRow = ProjectsWorksheet.Row(Row);

                        NProject.ParseExcelRow(xlRow, ProjectFormat);

                        CurrentProjects.Add(NProject);
                    }
                    ClearChanged();
                    return true;
                }
            }
            return true;
        }





        internal void WriteXLProjects(XLWorkbook CheckBookXlsx)
        {
            // projects is an optional sheet. Don't write if no projects defined.

            if (CurrentProjects.Count > 0)
            {
                // add the worksheet
                CheckBookXlsx.AddWorksheet(ProjectFormat.SheetName);
                IXLWorksheet ProjectsWorksheet = CheckBookXlsx.Worksheet(ProjectFormat.SheetName);

                // add any column formatting needed

                // first build the header
                Project.WriteXLHeader(ProjectsWorksheet, ProjectFormat);

                // then add all the rows

                int Row = 2;
                foreach (Project RProject in CurrentProjects)
                {
                    IXLRow xlRow = ProjectsWorksheet.Row(Row);
                    RProject.WriteExcelRow(xlRow, ProjectFormat);
                    Row++;
                }
            }
        }




        internal void SetSheetFormat()
        {
            ProjectFormat = new();
            ProjectFormat.SheetName = "Projects";
            Project.AddSheetColumns(ProjectFormat);
        }


    }
}
