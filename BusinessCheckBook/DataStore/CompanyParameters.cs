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
        
        public class ParameterName
        {
            internal string Name { get; set; } = string.Empty;
            internal bool IsRequired { get; set; }
            internal string DefaultValue { get; set; } = string.Empty;
            internal ParameterName (string nm, bool isRequired, string defaultv)
            {
                Name = nm;
                IsRequired = isRequired;
                DefaultValue = defaultv;
            }
        }
        List<ParameterName> ParameterNameList { get; set; } = new();

        readonly internal static ParameterName ParmCompanyName = new("CompanyName", true, "");
        readonly internal static ParameterName ParmCompanyAddr = new("Address", true, "");
        readonly internal static ParameterName ParmCompanyAdr2 = new("Address2", false, "");
        readonly internal static ParameterName ParmCompanyCity = new("City", true, "");
        readonly internal static ParameterName ParmCompanyState = new("State", true, "");
        readonly internal static ParameterName ParmCompanyZip = new("ZipCode", true, "");
        readonly internal static ParameterName ParmCompanyPhone = new("Phone", true, "");
        readonly internal static ParameterName ParmCompanyEIN = new("EIN", true, "");
        readonly internal static ParameterName CheckFormat = new("CheckFormat", false, "Laser 1PT");
        readonly internal static ParameterName FirstInvoiceNumber = new("FirstInvoice", true, "1");


        internal CompanyParameters() 
        {
            SetSheetFormat();
            ParameterNameList = new List<ParameterName>();
            ParameterNameList.Add(ParmCompanyName);
            ParameterNameList.Add(ParmCompanyAddr);
            ParameterNameList.Add(ParmCompanyAdr2);
            ParameterNameList.Add(ParmCompanyCity);
            ParameterNameList.Add(ParmCompanyState);
            ParameterNameList.Add(ParmCompanyZip);
            ParameterNameList.Add(ParmCompanyPhone);
            ParameterNameList.Add(ParmCompanyEIN);
            ParameterNameList.Add(CheckFormat);
            ParameterNameList.Add(FirstInvoiceNumber);
        }


        internal void Clear()
        {
            ParameterList.Clear();
            foreach(ParameterName pn in  ParameterNameList)
            {
                ParameterList.Add(pn.Name, pn.DefaultValue);
            }
        }

        internal string GetAddressLine(int LineNumber)
        {
            switch (LineNumber)
            {
                case 0: return ParameterList[ParmCompanyName.Name];
                case 1: return ParameterList[ParmCompanyAddr.Name];
                case 2:
                    if (ParameterList.TryGetValue(ParmCompanyAdr2.Name, out string? results)) return results;
                    else
                        return ParameterList[ParmCompanyCity.Name] + ", "
                            + ParameterList[ParmCompanyState.Name] + " "
                            + ParameterList[ParmCompanyZip.Name];
                case 3:
                    if (ParameterList.TryGetValue(ParmCompanyAdr2.Name, out _))
                        return ParameterList[ParmCompanyCity.Name] + ", "
                        + ParameterList[ParmCompanyState.Name] + " "
                        + ParameterList[ParmCompanyZip.Name];
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

                    // check the name - if no name, then done

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

                    // find if this parameter is required
                    foreach (ParameterName pn in ParameterNameList)
                    {
                        if (pn.Name == TName)
                        {
                            if (pn.IsRequired)
                            {
                                if (!ThisColumn.Valid(TValue))
                                {
                                    ErrorMessage = "Invalid Parameter value in row " + Row.ToString() + " " + TValue;
                                    return false;
                                }
                            }
                            else
                            {
                                if (TValue.Trim().Length > 0)
                                {
                                    if (!ThisColumn.Valid(TValue))
                                    {
                                        ErrorMessage = "Invalid Parameter value in row " + Row.ToString() + " " + TValue;
                                        return false;
                                    }
                                }
                            }
                        }
                    }

                    VParameterList.Add(TName, TValue);
                }

                // now check to make sure that all the required parameters are there
                string MissingParameter = "Missing parameter ";
                foreach (ParameterName pn in ParameterNameList)
                {
                    if (pn.IsRequired)
                    {
                        if (!VParameterList.TryGetValue(pn.Name, out _)) { ErrorMessage = MissingParameter + pn.Name; return false; }
                    }
                }

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
        internal bool ValidateCompanyAddress2(string CompanyAddress2)
        {
            if (CompanyAddress2.Trim().Length > 0)
            {
                ColumnFormat Col = ParameterFormat.Column(XLValue)!;
                if (!Col.Valid(CompanyAddress2))
                    return false;
            }
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
