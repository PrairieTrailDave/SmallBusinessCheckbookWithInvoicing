//**********************************************************************
//
//          Copyright © 2023 Prairie Trail Software, Inc.
//
//**********************************************************************

using BusinessCheckBook.Validation;
using ClosedXML.Excel;

namespace BusinessCheckBook.DataStore
{
    internal class CompanyParameters
    {
        Dictionary<string, string> ParameterList { get; set; } = new();

        // what the Excel sheet is supposed to look like
        private SheetFormat ParameterFormat { get; set; } = new();

        // Excel column names
        // Column names and header values

        private const string XLName = "Name";
        private const string XLValue = "Value";

        // Parameter Names

        internal const string ParmCompanyName = "CompanyName";
        internal const string ParmCompanyAddr = "Address";
        internal const string ParmCompanyAdr2 = "Address2";
        internal const string ParmCompanyCity = "City";
        internal const string ParmCompanyState = "State";
        internal const string ParmCompanyZip = "ZipCode";
        internal const string ParmCompanyPhone = "Phone";
        internal const string ParmCompanyEIN = "EIN";
        internal const string CheckFormat = "CheckFormat";
        internal const string FirstInvoiceNumber = "FirstInvoice";


        internal CompanyParameters() 
        {
            SetSheetFormat();
        }


        internal void Clear()
        {
            ParameterList.Clear();
            ParameterList.Add(ParmCompanyName, "");
            ParameterList.Add(ParmCompanyAddr, "");
            ParameterList.Add(ParmCompanyAdr2, "");
            ParameterList.Add(ParmCompanyCity, "");
            ParameterList.Add(ParmCompanyState, "");
            ParameterList.Add(ParmCompanyZip, "");
            ParameterList.Add(ParmCompanyEIN, "");
            ParameterList.Add(ParmCompanyPhone, "");
            ParameterList.Add(CheckFormat, "Laser 1PT");
            ParameterList.Add(FirstInvoiceNumber, "2678");
        }

        internal string GetAddressLine(int LineNumber)
        {
            switch (LineNumber)
            {
                case 0: return ParameterList[ParmCompanyName];
                case 1: return ParameterList[ParmCompanyAddr];
                case 2:
                    if (ParameterList.TryGetValue(ParmCompanyAdr2, out string? results)) return results;
                    else
                        return ParameterList[ParmCompanyCity] + ", "
                            + ParameterList[ParmCompanyState] + " "
                            + ParameterList[ParmCompanyZip];
                case 3:
                    if (ParameterList.TryGetValue(ParmCompanyAdr2, out _))
                        return ParameterList[ParmCompanyCity] + ", "
                        + ParameterList[ParmCompanyState] + " "
                        + ParameterList[ParmCompanyZip];
                    else
                        return "";
            
            }
            return "";
        }
        internal string GetParameter(string parameterName)
        {
            if (ParameterList.TryGetValue(parameterName, out string? results)) return results;
            return string.Empty;
        }
        internal void PutParameter(string parameterName, string newValue)
        {
            ParameterList[parameterName] = newValue;
        }



        internal bool Validate(XLWorkbook CheckBookXlsx, out string ErrorMessage)
        {
            IXLWorksheet ParameterWorksheet;
            Dictionary<string, string> VParameterList = new();
            ErrorMessage = "";

            try
            {
                ParameterWorksheet = CheckBookXlsx.Worksheet(ParameterFormat.SheetName);
            }
            catch (Exception ex)
            {
                ErrorMessage = "Missing the Parameters Worksheet " + ex.Message;
                return false;
            }

            // the sheet exists. check all the headings and find which column is which

            try
            {
                for (int PayToNum = 0; PayToNum < ParameterFormat.Count(); PayToNum++)
                {
                    ColumnFormat col = ParameterFormat.Column(PayToNum);
                    if (col == null) continue;
                    bool headerNotFound = true;
                    for (int ColNum = 1; ColNum < ParameterWorksheet.ColumnCount(); ColNum++)
                    {
                        string HeaderValue = ParameterWorksheet.Cell(1, ColNum).GetString();
                        if (col.Name == HeaderValue)
                        {
                            headerNotFound = false;
                            col.ColumnNumber = ColNum;
                            break;
                        }
                    }
                    if (headerNotFound)
                    {
                        // all columns are required for this sheet
                        ErrorMessage = "The column " + col.Name + " is not in the Parameters Worksheet";
                        return false;
                    }

                }

                // validate all the entries in the columns for validity
                // at this point, we have all the columns that are required

                int RowsUsedCount = ParameterWorksheet.RowsUsed().Count();
                for (int Row = 2; Row <= RowsUsedCount; Row++)
                {
                    IXLRow XRow = ParameterWorksheet.Row(Row);

                    ColumnFormat ThisColumn;

                    // check the name - if no name, then skip this row

                    ThisColumn = ParameterFormat.Column(XLName)!;
                    string TName = XRow.Cell(ThisColumn.ColumnNumber).GetString();
                    if (TName.Length == 0) return true;
                    if (!ThisColumn.Valid(TName))
                    {
                        ErrorMessage = "Invalid Parameter name in row " + Row.ToString() + " " + TName;
                        return false;
                    }

                    // Check the parameter value

                    ThisColumn = ParameterFormat.Column(XLValue)!;
                    string TValue = XRow.Cell(ThisColumn.ColumnNumber).GetString();
                    if (!ThisColumn.Valid(TValue))
                    {
                        ErrorMessage = "Invalid Parameter value in row " + Row.ToString() + " " + TValue;
                        return false;
                    }
                    VParameterList.Add(TName, TValue);
                }

                // now check to make sure that all the required parameters are there
                string MissingParameter = "Missing parameter ";
                if (!VParameterList.TryGetValue(ParmCompanyName, out _)) { ErrorMessage = MissingParameter + ParmCompanyName; return false; }
                if (!VParameterList.TryGetValue(ParmCompanyAddr, out _)) { ErrorMessage = MissingParameter + ParmCompanyAddr; return false; }
                if (!VParameterList.TryGetValue(ParmCompanyCity, out _)) { ErrorMessage = MissingParameter + ParmCompanyCity; return false; }
                if (!VParameterList.TryGetValue(ParmCompanyState, out _)) { ErrorMessage = MissingParameter + ParmCompanyState; return false; }
                if (!VParameterList.TryGetValue(ParmCompanyZip, out _)) { ErrorMessage = MissingParameter + ParmCompanyZip; return false; }
                if (!VParameterList.TryGetValue(ParmCompanyEIN, out _)) { ErrorMessage = MissingParameter + ParmCompanyEIN; return false; }
                if (!VParameterList.TryGetValue(ParmCompanyPhone, out _)) { ErrorMessage = MissingParameter + ParmCompanyPhone; return false; }
                if (!VParameterList.TryGetValue(FirstInvoiceNumber, out _)) { ErrorMessage = MissingParameter + FirstInvoiceNumber; return false; }



                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = "Error in reading a cell " + ex.Message;
                return false;
            }
        }

        internal bool ValidateCompanyName (string  CompanyName)
        {
            ColumnFormat Col = ParameterFormat.Column(XLValue)!;
            if (!Col.Valid(CompanyName))
                return false;
            return true;
        }
        internal bool ValidateCompanyAddress(string CompanyAddress)
        {
            ColumnFormat Col = ParameterFormat.Column(XLValue)!;
            if (!Col.Valid(CompanyAddress))
                return false;
            return true;
        }
        internal bool ValidateCompanyCity(string CompanyCity)
        {
            ColumnFormat Col = ParameterFormat.Column(XLValue)!;
            if (!Col.Valid(CompanyCity))
                return false;
            return true;
        }
        internal bool ValidateCompanyState(string CompanyState)
        {
            ColumnFormat Col = ParameterFormat.Column(XLValue)!;
            if (!Col.Valid(CompanyState))
                return false;
            return true;
        }
        internal bool ValidateCompanyZipCode(string CompanyZipCode)
        {
            ColumnFormat Col = ParameterFormat.Column(XLValue)!;
            if (!Col.Valid(CompanyZipCode))
                return false;
            return true;
        }
        internal bool ValidateCompanyPhone(string CompanyPhone)
        {
            ColumnFormat Col = ParameterFormat.Column(XLValue)!;
            if (!Col.Valid(CompanyPhone))
                return false;
            return true;
        }
        internal bool ValidateCompanyEIN(string CompanyEIN)
        {
            ColumnFormat Col = ParameterFormat.Column(XLValue)!;
            if (!Col.Valid(CompanyEIN))
                return false;
            return true;
        }



        internal bool ReadFromExcelFile(XLWorkbook CheckBookXlsx)
        {
            ParameterList = new();
            IXLWorksheet ParameterWorksheet = CheckBookXlsx.Worksheet(ParameterFormat.SheetName);
            if (ParameterWorksheet != null)
            {
                int Row = 0;
                foreach (var row in ParameterWorksheet.RowsUsed())
                {
                    // The header row was processed in the validation stage 

                    Row++;
                    if (Row == 1) continue;

                    // Pull off the Parameter Values

                    IXLRow xlRow = ParameterWorksheet.Row(Row);

                    ColumnFormat? Col = ParameterFormat.Column(XLName)!;
                    string ParmName = xlRow.Cell(Col.ColumnNumber).GetString();
                    Col = ParameterFormat.Column(XLValue)!;
                    string ParmValue = xlRow.Cell(Col.ColumnNumber).GetString();

                    ParameterList.Add(ParmName, ParmValue);
                }
                return true;
            }
            return false;
        }


        internal void WriteXLParameterList(XLWorkbook CheckBookXlsx)
        {
            // add the parameter list worksheet
            CheckBookXlsx.AddWorksheet(ParameterFormat.SheetName);
            IXLWorksheet ParameterWorksheet = CheckBookXlsx.Worksheet(ParameterFormat.SheetName);

            // first build the header

            for (int i = 0; i < ParameterFormat.Count(); i++)
            {
                ColumnFormat Col = ParameterFormat.Column(i);
                ParameterWorksheet.Cell(1, Col.ColumnNumber).Value = Col.Name;
            }
            // then add all the rows

            int Row = 2;
            foreach (var Parm in ParameterList)
            {
                IXLRow xlRow = ParameterWorksheet.Row(Row);
                WriteExcelRow(xlRow, Parm.Key, Parm.Value, ParameterFormat);
                Row++;
            }
        }
        internal static void WriteExcelRow(IXLRow XRow, string name, string value, SheetFormat ParameterFormat)
        {

            ColumnFormat? Col = ParameterFormat.Column(XLName)!;
            XRow.Cell(Col.ColumnNumber).Value = name;
            Col = ParameterFormat.Column(XLValue)!;
            XRow.Cell(Col.ColumnNumber).Value = value;
        }


        internal void SetSheetFormat()
        {
            ParameterFormat = new();
            ParameterFormat.SheetName = "Parameters";
            ParameterFormat.Add(new NameColumn(1, XLName, 60, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsRequired));
            ParameterFormat.Add(new TextColumn(2, XLValue, 255, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsRequired));

        }

    }
}
