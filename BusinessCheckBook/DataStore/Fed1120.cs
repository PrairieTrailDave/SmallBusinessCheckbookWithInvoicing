//**********************************************************************
//
//          Copyright © 2023 Prairie Trail Software, Inc.
//
//**********************************************************************

namespace BusinessCheckBook.DataStore
{
    internal class Fed1120
    {
        // This class holds information about the Federal 1120 tax form
        public static string OfficerCompensation = "12. Officer compensation";
        public static string SalariesAndWages = "13. Salaries and wages";

        public static readonly List<string> F1120Fields = new()
        {
            //"Name",
            //"Address",
            //"City",
            "B:EIN",
            "C:DateIncorporated",
            //"D:TotalAssets",
            "1a. Gross Receipts", 
            "1b. Returns",
            //"1c. Balance",
            "2. Cost Of Goods",
            "3. Gross Profit",
            "4. Dividends",
            "5. Interest Income",
            "6. Gross Rents",
            "7. Gross Royalties",
            "8. Capital Gain",
            "9. Net gain",
            "10. Other income",
            //"11. Total Income",
            OfficerCompensation,
            SalariesAndWages,
            "14. Repairs and maintenance",
            "15. Bad Debts",
            "16. Rents",
            "17. Taxes & Licenses",
            "18. Interest",
            "19. Charitable contributions",
            "20. Depreciation",
            "21. Depletion",
            "22. Advertising",
            "23. Pension",
            "24. Employee Benefit programs",
            //"25. Reserved future use",
            "26. Other deductions",
            "27. Total Deductions",
            //"28. Taxable Income",
            "29. Net operating loss",
                "29b. special deductions",
            //    "c. add the lines together",
            //"30. Taxable Income",
            //"31. Total Tax",
            //"32. Reserved for future use",
            "33. Total Payments and credits",
            //"34. Estimated Tax Penalty",
            //"35. Amount Owed",
            //"36. Overpayment",
            //"37. To Be refunded"
            "L1b. Cash"
        };
    }
}
