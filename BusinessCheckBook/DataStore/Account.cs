//**********************************************************************
//
//          Copyright © 2023 Prairie Trail Software, Inc.
//
//**********************************************************************

using BusinessCheckBook.Validation;
using ClosedXML.Excel;

namespace BusinessCheckBook.DataStore
{
    public class Account
    {
        public enum Type { CheckingAccount, Income, Expense, SubAccount, Withholdings }

        public bool IsActive { get; set; } = true;
        public Type WhatType { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string SubAccountOf { get; set; } = string.Empty;
        public string Fed1120Mapping { get; set; } = string.Empty;


        // Column names and header values

        private const string XLTypeOfAccount = "TypeOfAccount";
        private const string XLAccountName = "Name";
        private const string XLDescription = "Description";
        private const string XLSubAccountTo = "SubAccountTo";
        private const string XLFed1120Mapping = "Fed1120Mapping";
        private const string XLIsActive = "IsActive";

        // IIF column names

        private const int IIFNAME = 1;
        //private const int IIFREFNUM = 2;
        //private const int IIFTIMESTAMP = 3;
        private const int IIFACCNTTYPE = 4;
        //private const int IIFOBAMOUNT = 5;
        private const int IIFDESC = 6;
        //private const int IIFACCNUM = 7;
        //private const int IIFSCD = 8;
        //private const int IIFBANKNUM = 9;
        //private const int IIFEXTRA = 10;
        private const int IIFHIDDEN = 11;
        //private const int IIFDELCOUNT = 12;
        //private const int IIFUSEID = 13;



        public Account()
        {
        }
        public Account(bool active, Type whatType, string name, string description, string subAccountOf, string fedMapping)
        {
            IsActive = active;
            WhatType = whatType;
            Name = name;
            Description = description;
            SubAccountOf = subAccountOf;
            Fed1120Mapping = fedMapping;
        }

        internal static string GetSubAccountColumnName ()
        { return XLSubAccountTo; }  
        internal static string GetAccountColumnName()
        { return XLAccountName;  }

        internal static bool IsValidAccountType(string accountType)
        {
            foreach (var tipe in Enum.GetNames<Type>())
            {
                if (tipe ==  accountType) return true;
            }
            return false;
        }
        internal static Type ParseType(string type)
        {
            foreach (var tipe in Enum.GetValues<Type>())
            {
                if (tipe.ToString() == type) return tipe;
            }
            throw new Exception("Invalid account type:" + type);
        }

        internal static Type? MapIIFToAccountType(string IIFType)
        {
            return IIFType switch
            {
                "BANK" => null,
                "AR" => null,
                "OCASSET" => null,
                "AP" => null,
                "OCLIAB" => null,
                "LTLIAB" => null,
                "EQUITY" => null,
                "INC" => (Type?)Type.Income,
                "EXP" => (Type?)Type.Expense,
                "EXINC" => (Type?)Type.Income,
                "EXEXP" => (Type?)Type.Expense,
                "NONPOSTING" => null,
                _ => throw new Exception("Unable to map " + IIFType + " to an internal account type"),
            };
        }


        internal void ParseExcelRow (IXLRow XRow, SheetFormat ChartOfAccountsFormat)
        {
            ColumnFormat ThisColumn;
            ThisColumn = ChartOfAccountsFormat.Column(XLIsActive)!;
            IsActive = TrueFalseColumn.Parse(XRow.Cell(ThisColumn.ColumnNumber).GetString());
            ThisColumn = ChartOfAccountsFormat.Column(XLTypeOfAccount)!;
            WhatType = Account.ParseType(XRow.Cell(ThisColumn.ColumnNumber).GetString());
            ThisColumn = ChartOfAccountsFormat.Column(XLAccountName)!;
            Name = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            ThisColumn = ChartOfAccountsFormat.Column(XLDescription)!;
            Description = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            ThisColumn = ChartOfAccountsFormat.Column(XLSubAccountTo)!;
            SubAccountOf = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            ThisColumn = ChartOfAccountsFormat.Column(XLFed1120Mapping)!;
            Fed1120Mapping = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            if (SubAccountOf != null) 
            { 
                if  (SubAccountOf.Length > 0)
                {
                    WhatType = Type.SubAccount;
                } 
            }
        }

        internal Type? ParseIIFRow(string[] fields)
        {
            string IIFType = fields[IIFACCNTTYPE];
            Type? type = MapIIFToAccountType(IIFType);
            if (type == null) return null; // if not a mappable type, do not add this account

            WhatType = (Type)type!;
            string[] NameFlds = fields[IIFNAME].Split(':');
            if (NameFlds.Length > 1)
            {
                Name = NameFlds[1];
                WhatType = Account.Type.SubAccount;
                SubAccountOf = NameFlds[0];
            }
            else
            {
                Name = fields[IIFNAME];
            }

            // since we make the description manditory, 
            // put the name in blank descriptions.

            Description = fields[IIFDESC];
            if (Description.Trim().Length == 0)
                Description = Name;
            IsActive = true;
            if (fields[IIFHIDDEN] == "Y")
                IsActive = false;
            return WhatType;
        }



        #region Validation

        internal static bool ValidateIIFFields(string[] fields, SheetFormat ChartOfAccountsFormat, out string ErrorMessage)
        {
            ErrorMessage = string.Empty;

            string IIFType = fields[IIFACCNTTYPE];
            Type? type = MapIIFToAccountType(IIFType);
            if (type == null) return true; // if not a mappable type, pass it on
            if (!ValidateAccountType(type.Value.ToString(), ChartOfAccountsFormat)) { ErrorMessage = "Invalid Account Type " + IIFType; return false; }
            if (!Account.IsValidAccountType(type.Value.ToString())) { ErrorMessage = "Invalid Account Type " + IIFType; return false; }

            string IIFName;
            string IIFSubAccount = "";
            string[] NameFlds = fields[IIFNAME].Split(':');
            if (NameFlds.Length > 1)
            {
                IIFName = NameFlds[1];
                IIFSubAccount = NameFlds[0];
            }
            else
                IIFName = fields[IIFNAME];
            if (!ValidateAccountName(IIFName, ChartOfAccountsFormat)) { ErrorMessage = "Invalid Account Name " + IIFName; return false; }
            if (IIFSubAccount.Length > 0)
                if (!ValidateAccountName(IIFSubAccount, ChartOfAccountsFormat)) { ErrorMessage = "Invalid Account Name " + IIFSubAccount; return false; }
            string IIFDescription = fields[IIFDESC];
            if (IIFDescription.Trim().Length == 0)
                IIFDescription = IIFName;
            if (!ValidateAccountDescription(IIFDescription, ChartOfAccountsFormat)) { ErrorMessage = "Invalid Description " + IIFDescription; return false; }

            return true;
        }


        internal static bool ValidateExcelRow(IXLRow XRow, SheetFormat ChartOfAccountsFormat, out string ErrorMessage)
        {
            ErrorMessage = "";

            // check the account type

            ColumnFormat ThisColumn = ChartOfAccountsFormat.Column(XLTypeOfAccount)!;
            string AccountType = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            if (AccountType.Length == 0) return true;

            // test first for text validity
            if (!ThisColumn.Valid(AccountType))
            {
                ErrorMessage = "Invalid type of account " + AccountType;
                return false;
            }

            // then test for proper account type
            if (!Account.IsValidAccountType(AccountType))
            {
                ErrorMessage = "Invalid type of account " + AccountType;
                return false;
            }

            // Check the account name

            ThisColumn = ChartOfAccountsFormat.Column(XLAccountName)!;
            string TName = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            if (!ThisColumn.Valid(TName))
            {
                ErrorMessage = "Invalid account name " + TName;
                return false;
            }

            // Check the account description

            ThisColumn = ChartOfAccountsFormat.Column(XLDescription)!;
            string TDescription = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            if (!ThisColumn.Valid(TDescription))
            {
                ErrorMessage = "Invalid account description " + TDescription;
                return false;
            }

            // Check the account subaccount field

            ThisColumn = ChartOfAccountsFormat.Column(XLSubAccountTo)!;
            string TSubField = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            if (TSubField != null)
            {
                if (TSubField.Length > 0)   // optional field, if missing is ok
                {
                    if (!ThisColumn.Valid(TSubField))
                    {
                        ErrorMessage = "Invalid account subfield " + TSubField;
                        return false;
                    }
                }
            }

            // check the Fed 1120 mapping

            ThisColumn = ChartOfAccountsFormat.Column(XLFed1120Mapping)!;
            string TFedField = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            if (TFedField != null)
            {
                if (TFedField.Length > 0)  // optional field, if missing is ok
                {
                    if (!ThisColumn.Valid(TFedField))
                    {
                        ErrorMessage = "Invalid federal 1120 mapping " + TFedField;
                        return false;
                    }
                }
            }
            // check the IsActive flag

            ThisColumn = ChartOfAccountsFormat.Column(XLIsActive)!;
            string TActive = XRow.Cell(ThisColumn.ColumnNumber).GetString();
            if (!ThisColumn.Valid(TActive))
            {
                ErrorMessage = "Invalid account active flag " + TActive;
                return false;
            }

            return true;
        }



        // used on user entry
        internal static bool ValidAccountFields(SheetFormat ChartOfAccountsFormat, string AccountType, string AccountName, string AccountDescription, string AccountSubField, string AccountFedField, out string ErrorMessage)
        {
            ColumnFormat ThisColumn = ChartOfAccountsFormat.Column(XLTypeOfAccount)!;
            if (AccountType.Length == 0)
            {
                ErrorMessage = "Please select a valid account type";
                return false;
            }

            // test first for text validity
            if (!ThisColumn.Valid(AccountType))
            {
                ErrorMessage = "Invalid type of account " + AccountType;
                return false;
            }

            // Check the account name

            ThisColumn = ChartOfAccountsFormat.Column(XLAccountName)!;
            if (!ThisColumn.Valid(AccountName))
            {
                ErrorMessage = "Invalid account name " + AccountName;
                return false;
            }

            // Check the account description

            ThisColumn = ChartOfAccountsFormat.Column(XLDescription)!;
            if (!ThisColumn.Valid(AccountDescription))
            {
                ErrorMessage = "Invalid account description " + AccountDescription;
                return false;
            }

            // Check the account subaccount field

            ThisColumn = ChartOfAccountsFormat.Column(XLSubAccountTo)!;
            if (AccountSubField != null)
            {
                if (AccountSubField.Length > 0)   // optional field, if missing is ok
                {
                    if (!ThisColumn.Valid(AccountSubField))
                    {
                        ErrorMessage = "Invalid account subfield " + AccountSubField;
                        return false;
                    }
                }
            }

            // check the Fed 1120 mapping

            ThisColumn = ChartOfAccountsFormat.Column(XLFed1120Mapping)!;
            if (AccountFedField != null)
            {
                if (AccountFedField.Length > 0)  // optional field, if missing is ok
                {
                    if (!ThisColumn.Valid(AccountFedField))
                    {
                        ErrorMessage = "Invalid federal 1120 mapping " + AccountFedField;
                        return false;
                    }
                }
            }
            ErrorMessage = "";
            return true;
        }


        internal static bool ValidateAccountType (string AccountType, SheetFormat ChartOfAccountsFormat)
        {
            ColumnFormat Col; 
            Col = ChartOfAccountsFormat.Column(XLTypeOfAccount)!;
            if (!Col.Valid(AccountType))
                return false;
            return true;
        }
        internal static bool ValidateAccountName(string AccountName, SheetFormat ChartOfAccountsFormat)
        {
            ColumnFormat Col;
            Col = ChartOfAccountsFormat.Column(XLAccountName)!;
            if (!Col.Valid(AccountName))
                return false;
            return true;
        }
        internal static bool ValidateAccountDescription(string AccountDescription, SheetFormat ChartOfAccountsFormat)
        {
            ColumnFormat Col;
            Col = ChartOfAccountsFormat.Column(XLDescription)!;
            if (!Col.Valid(AccountDescription))
                return false;
            return true;
        }
        #endregion Validation
        internal void WriteExcelRow(IXLRow XRow, SheetFormat ChartOfAccountsFormat)
        {
            ColumnFormat? Col = ChartOfAccountsFormat.Column(XLIsActive)!;
            XRow.Cell(Col.ColumnNumber).Value = IsActive.ToString();
            Col = ChartOfAccountsFormat.Column(XLTypeOfAccount)!;
            XRow.Cell(Col.ColumnNumber).Value = WhatType.ToString();
            Col = ChartOfAccountsFormat.Column(XLAccountName)!;
            XRow.Cell(Col.ColumnNumber).Value = Name;
            Col = ChartOfAccountsFormat.Column(XLDescription)!;
            XRow.Cell(Col.ColumnNumber).Value = Description;
            Col = ChartOfAccountsFormat.Column(XLSubAccountTo)!;
            XRow.Cell(Col.ColumnNumber).Value = SubAccountOf;
            Col = ChartOfAccountsFormat.Column(XLFed1120Mapping)!;
            XRow.Cell(Col.ColumnNumber).Value = Fed1120Mapping;
        }



        internal static void AddSheetColumns(SheetFormat ChartOfAccountsFormat)
        {
            ChartOfAccountsFormat.Add(new TrueFalseColumn(1, XLIsActive, 6, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsRequired));
            ChartOfAccountsFormat.Add(new TextColumn(2, XLTypeOfAccount, 255, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsRequired));
            ChartOfAccountsFormat.Add(new NameColumn(3, XLAccountName, 255, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsRequired));
            ChartOfAccountsFormat.Add(new TextColumn(4, XLDescription, 255, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsRequired));
            ChartOfAccountsFormat.Add(new TextColumn(5, XLSubAccountTo, 255, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsOptional));
            ChartOfAccountsFormat.Add(new ListOfValuesColumn(6, XLFed1120Mapping, Fed1120.F1120Fields, 50, ColumnFormat.ColumnIsRequired, ColumnFormat.FieldIsOptional));
        }
    }
}
