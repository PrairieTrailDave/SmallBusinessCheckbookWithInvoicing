using BusinessCheckBook.DataStore;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Spreadsheet;
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
    public partial class EnterDailyActionsForm : Form
    {

        // variables filled from the main window
        public MyCheckbook ActiveBook { get; set; } = new();
        public string TimeSheetPerson {  get; set; } = string.Empty;

        // default settings for the grid
        int Interval = 60;
        int StartTime = 8;
        int EndTime = 17;

        // values for the grid display
        const string HourString = "1 Hour";
        const string HalfHourString = "Half Hour";
        const string FifteenMinuteString = "15 Minutes";

        private string[] Hours =
        {
            "1 AM", "2 AM", "3 AM", "4 AM", "5 AM", "6 AM", "7 AM", "8 AM", "9 AM", "10 AM", "11 AM", "12 Noon",
            "1 PM", "2 PM", "3 PM", "4 PM", "5 PM", "6 PM", "7 PM", "8 PM", "9 PM", "10 PM", "11 PM", "12 Midnight"
        };

        private string[] HalfHours =
        {
            "1 AM", "1:30 AM", "2 AM", "2:30 AM", "3 AM", "3:30 AM", "4 AM", "4:30 AM", "5 AM", "5:30 AM", "6 AM", "6:30 AM",
            "7 AM", "7:30 AM", "8 AM", "8:30 AM", "9 AM", "9:30 AM", "10 AM", "10:30 AM", "11 AM", "11:30 AM", "12 Noon",
            "12:30 PM",
            "1 PM", "1:30 PM", "2 PM", "2:30 PM", "3 PM", "3:30 PM", "4 PM", "4:30 PM", "5 PM", "5:30 PM", "6 PM", "6:30 PM",
            "7 PM", "7:30 PM", "8 PM", "8:30 PM", "9 PM", "9:30 PM", "10 PM", "10:30 PM", "11 PM", "11:30 PM", "12 Midnight",
            "12:30 AM"
        };

        private string[] QuarterHours =
        {
            "1 AM", "1:15 AM", "1:30 AM", "1:45 AM", "2 AM", "2:15 AM", "2:30 AM", "2:45 AM",
            "3 AM", "3:15 AM", "3:30 AM", "3:45 AM", "4 AM", "4:15 AM", "4:30 AM", "4:45 AM",
            "5 AM", "5:15 AM", "5:30 AM", "5:45 AM", "6 AM", "6:15 AM", "6:30 AM", "6:45 AM",
            "7 AM", "7:15 AM", "7:30 AM", "7:45 AM", "8 AM", "8:15 AM", "8:30 AM", "8:45 AM",
            "9 AM", "9:15 AM", "9:30 AM", "9:45 AM", "10 AM", "10:15 AM", "10:30 AM", "10:45 AM",
            "11 AM", "11:15 AM", "11:30 AM", "11:45 AM",
            "12 Noon", "12:15 PM", "12:30 PM", "12:45 PM",
            "1 PM", "1:15 PM", "1:30 PM", "1:45 PM", "2 PM", "2:15 PM", "2:30 PM", "2:45 PM",
            "3 PM", "3:15 PM", "3:30 PM", "3:45 PM", "4 PM", "4:15 PM", "4:30 PM", "4:45 PM",
            "5 PM", "5:15 PM", "5:30 PM", "5:45 PM", "6 PM", "6:15 PM", "6:30 PM", "6:45 PM",
            "7 PM", "7:15 PM", "7:30 PM", "7:45 PM", "8 PM", "8:15 PM", "8:30 PM", "8:45 PM",
            "9 PM", "9:15 PM", "9:30 PM", "9:45 PM", "10 PM", "10:15 PM", "10:30 PM", "10:45 PM",
            "11 PM", "11:15 PM", "11:30 PM", "11:45 PM",
            "12 Midnight", "12:15 AM", "12:30 AM", "12:45 AM"
        };




        // a sub class for display and time entry
        public class DisplayTimeEntry
        {
            public string StartTime { get; set; }
            public string Project { get; set; }
            public string Action { get; set; }
            public DisplayTimeEntry(string startTime)
            {
                StartTime = startTime;
                Project = "";
                Action = "";
            }
        }
        List<DisplayTimeEntry> DisplayTimeEntries = new List<DisplayTimeEntry>();






        // Constructors

        public EnterDailyActionsForm()
        {
            InitializeComponent();
            SetIntervals();
            SetStartTimeItems();
            SetEndTimeItems();
            ShowGrid();
        }

        public EnterDailyActionsForm(MyCheckbook activeBook, string PersonsName, DateTime ForWhichDay)
        {
            InitializeComponent();
            ActiveBook = activeBook;
            PersonEnteringDataForLabel.Text = PersonsName;
            TimeSheetPerson = PersonsName;
            WhichDateTimePicker.Value = ForWhichDay;
            SetIntervals();
            SetStartTimeItems();
            SetEndTimeItems();
            ShowGrid();
        }


        // Button Clicks

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (ValidatedEntry())
            {
                SaveAllTimeEntries();
                SaveSubContractorEntry();
                SaveBillableExpenseEntry();
                MessageBox.Show("Time Sheet Saved");
                ClearScreen();
            }

        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            ClearScreen();
        }








        // event handlers
        private void StartTimeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowGrid();
        }

        private void EndTimeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowGrid();
        }

        private void IntervalComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DetermineInterval();
            ShowGrid();
        }




        // Support Routines

        private void ClearScreen()
        {
            ShowGrid();
            SubcontractorDescriptionTextBox.Text = string.Empty;
            SubcontractorExpenseTextBox.Text = string.Empty;
            BillableExpenseDescriptionTextBox.Text = string.Empty;
            BillableExpenseTextBox.Text = string.Empty;
        }


        private void SetIntervals()
        {
            IntervalComboBox.Items.Clear();
            IntervalComboBox.Items.Add(HourString);
            IntervalComboBox.Items.Add(HalfHourString);
            IntervalComboBox.Items.Add(FifteenMinuteString);
            IntervalComboBox.SelectedIndex = 0;
        }
        private void DetermineInterval()
        {
            string IntervalString = IntervalComboBox.Text;
            if (IntervalString.Length > 0)
            {
                switch (IntervalString)
                {
                    case HourString: Interval = 60; break;
                    case HalfHourString: Interval = 30; break;
                    case FifteenMinuteString: Interval = 15; break;
                    default: Interval = 60; break;
                }
            }
            else
            {
                Interval = 60;
                IntervalComboBox.SelectedIndex = 0;
            }
        }

        // only allow hours for start and stop of grid
        private void SetStartTimeItems()
        {
            StartTimeComboBox.Items.Clear();
            foreach (string hr in Hours)
            {
                StartTimeComboBox.Items.Add(hr);
            }

            StartTimeComboBox.SelectedIndex = 7;
        }
        private void SetStartTime()
        {
            if (StartTimeComboBox.SelectedIndex == -1)
            {
                StartTime = 1;
            }
            else
            {
                StartTime = StartTimeComboBox.SelectedIndex + 1;
            }
        }
        private void SetEndTimeItems()
        {
            EndTimeComboBox.Items.Clear();
            foreach (string hr in Hours)
            {
                EndTimeComboBox.Items.Add(hr);
            }

            EndTimeComboBox.SelectedIndex = 17;
        }

        private void SetEndTime()
        {
            if (EndTimeComboBox.SelectedIndex == -1)
                EndTime = 1;
            else EndTime = EndTimeComboBox.SelectedIndex + 1;
        }
        private void ShowGrid()
        {
            SetStartTime();
            SetEndTime();
            BuildBlankEntries();

            List<string> Projects = ActiveBook.CurrentProjects.GetProjectNames();

            TimeSheetDataGridView.DataSource = DisplayTimeEntries;
            TimeSheetDataGridView.Columns.Remove("Project");
            DataGridViewComboBoxColumn ProjectCol = new DataGridViewComboBoxColumn();
            ProjectCol.DataSource = Projects;
            ProjectCol.HeaderText = "Project";
            ProjectCol.Name = "Project";
            ProjectCol.DataPropertyName = "Project";
            TimeSheetDataGridView.Columns.Insert(1, ProjectCol);

            TimeSheetDataGridView.Columns["StartTime"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            TimeSheetDataGridView.Columns["Project"].Width = 300;
            TimeSheetDataGridView.Columns["Action"].Width = 550;

        }
        private void BuildBlankEntries()
        {
            DisplayTimeEntries = new List<DisplayTimeEntry>();

            // figure out the start time

            switch (Interval)
            {
                case 60:
                    int WhichHourPosition = StartTime - 1;
                    while (true)
                    {
                        DisplayTimeEntries.Add(new DisplayTimeEntry(Hours[WhichHourPosition]));
                        WhichHourPosition++;
                        if (WhichHourPosition > EndTime - 1) break;
                    }
                    break;
                case 30:
                    int WhichHalfHourPosition = (StartTime - 1) * 2;
                    while (true)
                    {
                        DisplayTimeEntries.Add(new DisplayTimeEntry(HalfHours[WhichHalfHourPosition]));
                        WhichHalfHourPosition++;
                        if (WhichHalfHourPosition > (EndTime - 1) * 2 - 1) break;
                    }
                    break;
                case 15:
                    int WhichQuarterHourPosition = (StartTime - 1) * 4;
                    while (true)
                    {
                        DisplayTimeEntries.Add(new DisplayTimeEntry(QuarterHours[WhichQuarterHourPosition]));
                        WhichQuarterHourPosition++;
                        if (WhichQuarterHourPosition > (EndTime - 1) * 4 - 1) break;
                    }
                    break;
            }
        }

        private bool ValidatedEntry()
        {

            return true;
        }

        private void SaveAllTimeEntries()
        {
            // actually, we want to walk through the rows
            // first to find any start time as many rows may be blank
            // then to walk through all consecutive rows that have the same project id
            // till finding either a different project id or a blank row
            int i = 0;
            while (i < TimeSheetDataGridView.Rows.Count)
            {
                DataGridViewRow row = TimeSheetDataGridView.Rows[i];

                if (!String.IsNullOrEmpty((string)row.Cells["Project"].Value))
                {
                    // start the time entry

                    DateTime ComputedStartTime = GetStartingTime((string)row.Cells["StartTime"].Value);
                    string ProjectID = (string)row.Cells["Project"].Value;
                    string Action = (string)row.Cells["Action"].Value;
                    string LastAction = Action;

                    // compute the end time by either no project in the row
                    // or the project changes

                    DateTime ComputedEndTime = ComputedStartTime.AddMinutes(Interval);

                    while (true)
                    {
                        i++;
                        if (i >= TimeSheetDataGridView.Rows.Count) break;

                        row = TimeSheetDataGridView.Rows[i];
                        if (ProjectID != (string)row.Cells["Project"].Value) break;

                        ComputedEndTime = GetStartingTime((string)row.Cells["StartTime"].Value).AddMinutes(Interval);
                        string ThisRowAction = (string)row.Cells["Action"].Value;
                        if (LastAction != ThisRowAction)
                        {
                            Action += " " + ThisRowAction;
                            LastAction = ThisRowAction;
                        }
                    }

                    TimeSheetEntry TSE = new()
                    {
                        HasBeenInvoiced = false,
                        WhichType = TimeSheetEntry.TimeEntryType.Time,
                        StartTime = ComputedStartTime,
                        EndTime = ComputedEndTime,
                        ProjectID = ProjectID,
                        Person = TimeSheetPerson,
                        Action = Action
                    };
                    ActiveBook.CurrentTimeSheets.Add(TSE);
                }
                i++;
            }
        }

        private DateTime GetStartingTime(string StartingPoint)
        {
            DateTime StartingTime = DateTime.MinValue;
            DateTime WhichDate = WhichDateTimePicker.Value;

            if (StartingPoint == "12 Noon")
            {
                StartingTime = new DateTime(WhichDate.Year, WhichDate.Month, WhichDate.Day, 12, 0, 0);
            }
            else
            if (StartingPoint == "12 Midnight")
            {
                StartingTime = new DateTime(WhichDate.Year, WhichDate.Month, WhichDate.Day, 24, 0, 0);
            }
            else
            {
                // parsing assumes today when there isn't a date specified

                DateTime ParsedTime = DateTime.Parse(StartingPoint);
                
                // but we want to put it on the date selected in the date picker

                StartingTime = new DateTime(WhichDate.Year, WhichDate.Month, WhichDate.Day,
                    ParsedTime.Hour, ParsedTime.Minute, 0);
            }
            return StartingTime;
        }
        private void SaveSubContractorEntry()
        {
            if (SubcontractorDescriptionTextBox.Text.Length > 0)
            {
                TimeSheetEntry TSE = new()
                {
                    HasBeenInvoiced = false,
                    WhichType = TimeSheetEntry.TimeEntryType.SubContractor,
                    SubcontractorName = SubcontractorDescriptionTextBox.Text,
                    SubcontractorAmount = Decimal.Parse(SubcontractorExpenseTextBox.Text)
                };
                ActiveBook.CurrentTimeSheets.Add(TSE);
            }
        }
        private void SaveBillableExpenseEntry()
        {
            if (BillableExpenseDescriptionTextBox.Text.Length > 0)
            {
                TimeSheetEntry TSE = new()
                {
                    HasBeenInvoiced = false,
                    WhichType = TimeSheetEntry.TimeEntryType.OtherExpense,
                    OtherExpenseDescription = BillableExpenseDescriptionTextBox.Text,
                    OtherExpenseAmount = Decimal.Parse(BillableExpenseTextBox.Text)
                };

                // save it
                ActiveBook.CurrentTimeSheets.Add(TSE);
            }
        }

        private void TimeSheetDataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            int column = e.ColumnIndex;

            if (row > 0 && column == 1)
            {
                // see if to copy the row above this one

                if (TimeSheetDataGridView.Rows[row-1].Cells[column].Value?.ToString()?.Length > 0)
                {
                    TimeSheetDataGridView.Rows[row].Cells[column].Value = TimeSheetDataGridView.Rows[row - 1].Cells[column].Value;
                    TimeSheetDataGridView.Rows[row].Cells[column+1].Value = TimeSheetDataGridView.Rows[row - 1].Cells[column+1].Value;

                }
            }
        }
    }
}
