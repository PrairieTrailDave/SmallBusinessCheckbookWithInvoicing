using BusinessCheckBook.DataStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;

namespace BusinessCheckBook
{
    // this logs to a csv file 
    public class ActivityLogger
    {
        internal string LogFileName = string.Empty;
        const string DefaultFileName = "NewCheckbookLog.csv";

        enum RecordTypes { Account, Customer, Payee, Invoice, Check, Deposit }
        enum TypesOfAction { Add, Modify, Delete }
        internal class LogRecord
        {
            internal string When = String.Empty;
            internal string RecordType = String.Empty;
            internal string TypeOfAction = String.Empty;
            internal string ActionText = String.Empty;
            internal string Amount = String.Empty;
        }

        // this is a class that is subordinate to the checkbook
        // if the logFile is not defined (checkbook new),
        // save to a default location
        public ActivityLogger(string logFile) {
            if (logFile.Length > 0)
            {
                LogFileName = logFile;
            }
            else { LogFileName = DefaultFileName; }
        }
        public ActivityLogger CurrentLog { get { return this;  } private set { } }

        // to make sure that the file is defined, 
        // ask for the output file when a new data file is selected


        void Log (LogRecord record)
        {
            StreamWriter logWriter = new StreamWriter(LogFileName);
            logWriter.WriteLine(record.When +
                "," + record.RecordType + "," + record.TypeOfAction + "," + 
                record.ActionText + "," + record.Amount);
            logWriter.Close();

        }

        public void LogObject(string Action, string RecordType, object TObject)
        {
            StreamWriter logWriter = new StreamWriter(LogFileName);
            CsvHelper.Configuration.CsvConfiguration config = new(System.Globalization.CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
                MissingFieldFound = null,
                TrimOptions = CsvHelper.Configuration.TrimOptions.Trim,
                HeaderValidated = null
            };

            var csvWriter = new CsvWriter(logWriter, config);
            logWriter.Write(DateTime.Now.ToShortDateString() +
                "," + RecordType + "," + Action + ",");
            csvWriter.WriteRecord(TObject);
            csvWriter.Dispose();
            logWriter.Close();
        }

    }
}
