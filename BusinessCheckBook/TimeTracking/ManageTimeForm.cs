using BusinessCheckBook.DataStore;
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
    public partial class ManageTimeForm : Form
    {
        // variables filled from the main window
        private MyCheckbook ActiveBook { get; set; } = new();

        public ManageTimeForm()
        {
            InitializeComponent();
        }
        public ManageTimeForm(MyCheckbook workingData)
        {
            InitializeComponent();
            ActiveBook = workingData;
            LoadListOfPeople();
        }


        // Button Clicks

        private void EnterTimeButton_Click(object sender, EventArgs e)
        {
            string PersonsToEnterTimeFor = PersonsListBox.Text;
            DateTime WhichDay = WhichDateTimePicker.Value;
            EnterDailyActionsForm EDAF = new EnterDailyActionsForm(ActiveBook, PersonsToEnterTimeFor, WhichDay);
            EDAF.ShowDialog();
        }



        // Other UI Events

        private void PersonsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            PersonSelectedLabel.Text = "Selected " + PersonsListBox.Text;
        }



        // Support Routines

        private void LoadListOfPeople()
        {
            PersonsListBox.Items.Clear();
            List<Person> people = ActiveBook.CurrentPersons.GetPeople();
            foreach (Person person in people)
            {
                PersonsListBox.Items.Add(person.Name);
            }
        }

    }
}
