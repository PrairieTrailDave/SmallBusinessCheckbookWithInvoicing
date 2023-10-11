# Business Checking
A Business Checkbook application with user interface, Excel database, prints checks, and manages invoices

This is a quick and dirty checkbook program that I wrote to replace my old QuickBooks program. I was tired of QuickBooks constantly telling me to upgrade and I didn't want to pay for the latest version. No, I'm not buying the online version either.
This is NOT AN ACCOUNTING PACKAGE. It is a checkbook program. This is useful only for CASH ONLY business.


When I looked at the C# checkbook programs here, most lacked a user interface and weren't attractive to me.

This program uses an Excel .XLSX file as the database. That offers several benefits:
1. The user can write their own reports in Excel or in Power BI
2. The database doesn't need a fancy database system to run.
3. It makes sharing data with other programs very easy.

This program also is able to import data from Quickbooks IIF files and exported journal report which can give you the basic lists. I thought about reverse engineering the QuickBooks transfer file (it is a compound file) but decided that it wasn't a good use of my time.

Right now, the functions include

1. Pull in the database and compute the current balance
2. Add a transaction to the database and continue the balance
3. Put the transaction in a category
4. Remember the last transaction to a payee and auto fill this transaction with that data
5. Reconcile the ledger with the bank statement
6. Allow transactions to be broken up into multiple sub-categories
7. Reporting split up by category (helps with taxes)
8. Import some values from QuickBooks
9. Print Checks
10. Print Invoices
11. Receive payments and marks invoices as paid

USE THIS PROGRAM AT YOUR OWN RISK!!!!

I wrote this for my own use. Your use of this program may open you up to financial mistakes. In other words, I ain't liable for any mistakes, omissions, or bugs if you use this. I'm using it for my business.
If you take this program and try to sell it, you will have to handle all the customer support issues, any legal challenges from Intuit, and the headaches of adding all the customer requests. I have no interest in handling customer service issues. I won't charge you to use this and I won't take your calls for help.

For you Intuit Lawyers with more time on your hands:
BUY QUICKBOOKS!
If you need an accounting package, BUY QUICKBOOKS.
If you have any employees, you need an accounting package. BUY QUICKBOOKS.
If you can't afford Quickbooks, you are not in business yet.



Example user interface:
Main screen showing the current ledger


How to add a transaction to the ledger

Adding transaction with split categories


Reconciliation Screen

Report Screen




This program imports lists that are exported from Quickbooks into an IIF file with the following changes:
1. Not all fields are imported
2. Vendors and "other names" are combined into "Payees"

When importing the chart of accounts from the IIF file, the accounts are not connected to the tax form fields. You will have to do that manually. If you start with a new chart of accounts, the default includes the connection to the tax form fields.

How to get an IIF from Quickbooks: Go into Quickbooks and select File -> Utillities -> Export -> Lists to IIF Files

I tried not inporting inactive customers, vendors, and other names. However, I found it easier to go into the Excel file and clear out the ones I didn't want. 

This program imports a Journal report from Quickbooks that has been exported to Excel. To export the Journal report, go to Reports -> Accountant & Taxes reports -> Journal. Once you have selected the range of dates with the entries you want, go up to the top to the Excel tab and select export. Create a new Excel file. This file can be imported via the file -> Import -> Journal file.

The import validates the entries one at a time. That means that if an entry fails validation, part of the import has already happened and your ledger and invoices have been affected already. It is best to simply reopen the current ledger and start over after you have corrected the validation error.

Also when importing journal entries, I originally thought about asking if to rearrainge transaction order when a transaction isn't in order, but decided to go ahead and simply add it in the correct date & check number order. Similarly, the program adds accounts that are not currently in the chart of accounts. Once the accounts are added, then you need to manually connect those accounts to the federal 1120 fields.

WARNING: Because this program is a checking program and not an accounting program, it does not properly import payroll checks. On the Federal 1120 report, the officer compensation and employee payroll lines do not report a value. The reason is that the amounts to put on those lines are not the check values. WARNING

WARNING: When importing payroll liability checks, put the employee withholdings into a withholding account and the business portion of the check into the tax: business portion of that tax. WARNING

When importing checks, the check numbers have to be actual numbers. When I had "EFTPS" as the check number, it failed.

I thought long about adding logging to this program. I actually started adding logging. Then, I realized that with an "open database", logging is meaningless. The value of the open database is that you, as the owner, can use Excel to modify the database when you have made a mistake. There is no way to log what you do when doing such modification. Ergo, logging is useless as it can't properly capture all changes to the database.


The Chart of Accounts can be connected to the Federal 1120 form. The following are the acceptable values for that field.
1. "B:EIN"
2. "C:DateIncorporated"
3. "1a. Gross Receipts"
4. "1b. Returns"
5. "2. Cost Of Goods"
6. "3. Gross Profit"
7. "4. Dividends"
8. "5. Interest Income"
9. "6. Gross Rents"
10. "7. Gross Royalties"
11. "8. Capital Gain"
12. "9. Net gain"
13. "10. Other income"
14. "12. Officer compensation"
15. "13. Salaries and wages"
16. "14. Repairs and maintenance"
17. "15. Bad Debts"
18. "16. Rents"
19. "17. Taxes & Licenses"
20. "18. Interest"
21. "19. Charitable contributions"
22. "20. Depreciation"
23. "21. Depletion"
24. "22. Advertising"
25. "23. Pension"
26. "24. Employee Benefit programs"
27. "26. Other deductions"
28. "29. Net operating loss"
29. "29b. special deductions"
30. "33. Total Payments and credits"
31. "L1b. Cash"


Known Issues:

Quickbooks does not export long items in invoices. Therefore they are not available to import.
When importing from a journal file, the pay to people are not validated.
When importing from a journal file, the accounts on an invoice are not validated to make sure they are in the chart of accounts.
