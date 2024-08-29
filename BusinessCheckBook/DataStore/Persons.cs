using BusinessCheckBook.Validation;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCheckBook.DataStore
{
    // This class is for managing the people that can be tracked with the daily and hourly activity
    // This feeds into the invoicing
    internal class Persons
    {
        // storage of the Persons

        List<Person> CurrentPersons = new();

        // what the Excel sheet is supposed to look like
        internal SheetFormat PersonFormat { get; set; } = new();

        private bool Changed;

        public Persons()
        {
            CurrentPersons = [];
            SetSheetFormat();
            Changed = false;
        }


        public bool IfChanged() { return Changed; }
        public void HasChanged() { Changed = true; }
        public void ClearChanged() { Changed = false; }


        // routines used in management screen

        public void AddPerson(Person person) { CurrentPersons.Add(person); HasChanged(); }

        public Person? GetPerson(string Name)
        {
            foreach (Person pers in CurrentPersons)
            {
                if (String.Compare(pers.Name, Name, StringComparison.OrdinalIgnoreCase) == 0)
                    { return pers; }
            }
            return null;
        }

        public List<Person> GetPeople ()
        {  return CurrentPersons; }


        public void DeletePerson(Person PersonToDelete)
        {  CurrentPersons.Remove(PersonToDelete); HasChanged(); }




        #region Validation

        public bool Validate(XLWorkbook CheckBookXlsx, out string ErrorMessage)
        {
            ErrorMessage = "";
            IXLWorksheet PersonsWorksheet;

            if (CheckBookXlsx.TryGetWorksheet(PersonFormat.SheetName, out PersonsWorksheet))
            {

                // the sheet exists. check all the headings and find which column is which

                try
                {
                    if (!Person.ValidateColumnHeaders(PersonsWorksheet, PersonFormat, out ErrorMessage))
                        return false;

                    // validate all the entries in the columns for validity
                    // at this point, we have all the columns that are required

                    int RowsUsedCount = PersonsWorksheet.RowsUsed().Count();
                    for (int Row = 2; Row < RowsUsedCount; Row++)
                    {
                        IXLRow XRow = PersonsWorksheet.Row(Row);
                        if (!Person.ValidateExcelRow(XRow, PersonFormat, out ErrorMessage))
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
            // if the sheet does not exist, that is not an error
            return true;
        }



        public bool ValidateName(string proposedName)
        {
            return Person.ValidateName(proposedName, PersonFormat);
        }
        #endregion Validation


        public bool ReadFromExcelFile(XLWorkbook CheckBookXlsx)
        {
            CurrentPersons = new();
            IXLWorksheet PersonsWorksheet;

            if (CheckBookXlsx.TryGetWorksheet(PersonFormat.SheetName, out PersonsWorksheet))
            { 
                int Row = 0;
                foreach (var row in PersonsWorksheet.RowsUsed())
                {
                    // The header row was processed in the validation stage and we can use those values

                    Row++;
                    if (Row == 1) continue;

                    // Pull off the Person Values

                    Person NPerson = new();
                    IXLRow xlRow = PersonsWorksheet.Row(Row);

                    NPerson.ParseExcelRow(xlRow, PersonFormat);

                    CurrentPersons.Add(NPerson);
                }
                ClearChanged();
                return true;
            }
            return false;
        }





        internal void WriteXLPersons(XLWorkbook CheckBookXlsx)
        {
            // only write the persons worksheet if any people are defined

            if (CurrentPersons.Count > 0)
            {
                // add the invoices worksheet
                CheckBookXlsx.AddWorksheet(PersonFormat.SheetName);
                IXLWorksheet PersonsWorksheet = CheckBookXlsx.Worksheet(PersonFormat.SheetName);

                // add any column formatting needed

                // first build the header
                Person.WriteXLHeader(PersonsWorksheet, PersonFormat);

                // then add all the rows

                int Row = 2;
                foreach (Person RPerson in CurrentPersons)
                {
                    IXLRow xlRow = PersonsWorksheet.Row(Row);
                    RPerson.WriteExcelRow(xlRow, PersonFormat);
                    Row++;
                }
            }
        }




        internal void SetSheetFormat()
        {
            PersonFormat = new();
            PersonFormat.SheetName = "Persons";
            Person.AddSheetColumns(PersonFormat);
        }


    }
}
