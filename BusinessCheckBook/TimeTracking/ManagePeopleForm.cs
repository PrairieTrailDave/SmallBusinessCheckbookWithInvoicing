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
    public partial class ManagePeopleForm : Form
    {
        // variables filled from the main window
        public MyCheckbook ActiveBook { get; set; } = new();


        Person CurrentPerson = new();

        public ManagePeopleForm()
        {
            InitializeComponent();
        }
        public ManagePeopleForm(MyCheckbook activeBook)
        {
            InitializeComponent();
            ActiveBook = activeBook;
            ClearDisplay();
        }

        // button clicks

        private void AddButton_Click(object sender, EventArgs e)
        {
            string NameToAdd = PersonTextBox.Text;
            if (ActiveBook.CurrentPersons.ValidateName(NameToAdd))
            {
                CurrentPerson = new()
                {
                    IsActive = true,
                    Name = NameToAdd
                };
                ActiveBook.CurrentPersons.AddPerson(CurrentPerson);
                ClearDisplay();
                DisplayAllPersons();
            }
            else
            {
                MessageBox.Show("That name does not validate. Please change it.");
                PersonTextBox.Focus();
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            ClearDisplay();
        }
        private void UpdateButton_Click(object sender, EventArgs e)
        {
            string UpdatedName = PersonTextBox.Text;
            if (ActiveBook.CurrentPersons.ValidateName(UpdatedName))
            {
                if (CurrentPerson != null)
                {
                    CurrentPerson.Name = UpdatedName;
                    DisplayAllPersons();
                }
            }
            else
            {
                MessageBox.Show("That name does not validate. Please change it.");
                PersonTextBox.Focus();
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (CurrentPerson != null)
                ActiveBook.CurrentPersons.DeletePerson(CurrentPerson);
            ClearDisplay();
            DisplayAllPersons();
        }


        // other UI events

        private void PeopleListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // get the name

            string selectedPerson = PeopleListBox.Text;

            // find this person in the list
            
            Person? FoundPerson = ActiveBook.CurrentPersons.GetPerson(selectedPerson);
            if (FoundPerson != null)
            {
                // display the name

                PersonTextBox.Text = FoundPerson.Name;
                AddButton.Enabled = false;
                UpdateButton.Enabled = true;
                DeleteButton.Enabled = true;
                CurrentPerson = FoundPerson;
            }
            else
                MessageBox.Show("Can't find that person");
        }


        // support routines

        private void ShowThisPerson(Person person)
        {
            PersonTextBox.Text = person.Name;
            CurrentPerson = person;
        }

        private void ClearDisplay()
        {
            PersonTextBox.Text = string.Empty;
            AddButton.Enabled = true;
            UpdateButton.Enabled = false;
            DeleteButton.Enabled = false; 
        }

        private void DisplayAllPersons()
        {
            PeopleListBox.Items.Clear();
            foreach(Person PR in ActiveBook.CurrentPersons.GetPeople())
            {
                PeopleListBox.Items.Add(PR.Name);
            }
        }
    }
}
