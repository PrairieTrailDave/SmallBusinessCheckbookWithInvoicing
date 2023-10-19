//**********************************************************************
//
//          Copyright © 2023 Prairie Trail Software, Inc.
//
//**********************************************************************

using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCheckBook.DataStore
{
    public class InvoiceFormat
    {
        MyCheckbook ActiveBook { get; set; } = new();






        // identifiers for company information to print
        internal const string idCompanyName = "CompName";
        internal const string idCompanyAdr = "CompAdr";
        internal const string idCompanyAdr2 = "CompAdr2";
        internal const string idCompanyAdr3 = "CompAdr3";
        internal const string idCompanyEIN = "CompEIN";
        internal const string idCompanyPhone = "CompPhone";

        public InvoiceFormat() { }

        public InvoiceFormat(MyCheckbook nCheck)
        {
            ActiveBook = nCheck;
        }

        internal void FormatInvoice(Invoice CurrentInvoiceToPrint, InvoicePrintLayout CurrentPrintLayout, PrintPageEventArgs ev)
        {
            // print the logo

            if (CurrentPrintLayout.LogoFileName.IfToPrintThisItem)
            {
                System.Drawing.Image img = System.Drawing.Image.FromFile(CurrentPrintLayout.LogoFileName.VariableNameToPrint);
                Rectangle Rex = new(CurrentPrintLayout.LogoFileName.XPos,
                    CurrentPrintLayout.LogoFileName.YPos,
                    CurrentPrintLayout.LogoFileName.Width,
                    CurrentPrintLayout.LogoFileName.Height);
                ev.Graphics!.DrawImage(img, Rex);
            }

            // print company information

            foreach (PrintItemLayout PIL in CurrentPrintLayout.CompanyInformationItems)
            {
                // get what to print

                string WhatToPrint = "";
                switch (PIL.VariableNameToPrint)
                {
                    case idCompanyName:
                        WhatToPrint = ActiveBook.CompanyInformation.
                            GetParameter(CompanyParameters.ParmCompanyName.Name);
                        break;
                    case idCompanyAdr:
                        WhatToPrint = ActiveBook.CompanyInformation.
                           GetAddressLine(1);
                        break;
                    case idCompanyAdr2:
                        WhatToPrint = ActiveBook.CompanyInformation.
                           GetAddressLine(2);
                        break;
                    case idCompanyAdr3:
                        WhatToPrint = ActiveBook.CompanyInformation.
                           GetAddressLine(3);
                        break;
                    case idCompanyEIN:
                        WhatToPrint = ActiveBook.CompanyInformation.
                            GetParameter(CompanyParameters.ParmCompanyEIN.Name);
                        break;
                    case idCompanyPhone:
                        WhatToPrint = ActiveBook.CompanyInformation.
                            GetParameter(CompanyParameters.ParmCompanyPhone.Name);
                        break;
                }

                if (PIL.IfToPrintThisItem)
                {
                    ev.Graphics!.DrawString(WhatToPrint, PIL.ItemFont, Brushes.Black, PIL.XPos, PIL.YPos, new StringFormat());
                }

            }

            // draw the boxes

            foreach (Rectangle rex in CurrentPrintLayout.BoxRects)
            {
                ev.Graphics!.DrawRectangle(new Pen(Brushes.Black, 1.0F), rex);
            }


            // print the standard text

            foreach (PrintItemLayout PIL in CurrentPrintLayout.StandardTexts)
            {
                if (PIL.IfToPrintThisItem)
                {
                    ev.Graphics!.DrawString(PIL.VariableNameToPrint, PIL.ItemFont, Brushes.Black, PIL.XPos, PIL.YPos, new StringFormat());
                }
            }

            // print the invoice text
            foreach (PrintItemLayout PIL in CurrentPrintLayout.TextToPrintLayouts)
            {
                if (PIL.IfToPrintThisItem)
                {
                    if (PIL.VariableNameToPrint == "BreakdownDescription")
                    {

                    }
                    else
                    {
                        if (PIL.VariableNameToPrint == "BreakdownAmount")
                        {

                        }
                        else
                        {
                            var InvoiceTextField = CurrentInvoiceToPrint.GetType().GetProperty(PIL.VariableNameToPrint, BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty);
                            if (InvoiceTextField != null)
                            {
                                var mthod = InvoiceTextField.GetGetMethod();
                                if (mthod != null)
                                {
                                    string fieldvalue = mthod.Invoke(CurrentInvoiceToPrint, null)?.ToString() ?? "";
                                    Rectangle Rex = new(PIL.XPos, PIL.YPos, PIL.Width, PIL.Height);
                                    StringFormat SRF = new();
                                    switch (PIL.HowToJustify)
                                    {
                                        case PrintItemLayout.Justification.LeftJustify: break;
                                        case PrintItemLayout.Justification.RightJustify:
                                            SRF.Alignment = StringAlignment.Far;
                                            break;
                                        case PrintItemLayout.Justification.Center:
                                            SRF.Alignment = StringAlignment.Center;
                                            break;
                                    }
                                    ev.Graphics!.DrawString(fieldvalue, PIL.ItemFont, Brushes.Black, Rex, SRF);
                                }
                            }
                        }
                    }
                }
            }
            FormatBreakdown(CurrentInvoiceToPrint, CurrentPrintLayout, ev);
        }

        internal static void FormatBreakdown(Invoice CurrentInvoiceToPrint, InvoicePrintLayout CurrentPrintLayout, PrintPageEventArgs ev)
        {
            string WhatToPrint;
            int XPos;
            int YPos;
            int Height;
            // initialize these as don't print so that we can see if they are found
            PrintItemLayout PILDescr = new()
            {
                IfToPrintThisItem = false
            };
            PrintItemLayout PILAmt = new()
            {
                IfToPrintThisItem = false
            };

            // find the two layouts we need

            foreach (PrintItemLayout PIL in CurrentPrintLayout.TextToPrintLayouts)
            {
                if (PIL.VariableNameToPrint == "BreakdownDescription")
                    PILDescr = PIL;
                else
                    if (PIL.VariableNameToPrint == "BreakdownAmount")
                    PILAmt = PIL;
            }

            // initialize the line positions

            YPos = PILDescr.YPos;
            Height = PILDescr.Height;

            foreach (InvoiceItem invBreakdown in CurrentInvoiceToPrint.InvoiceBreakdown)
            {
                XPos = PILDescr.XPos;
                Rectangle Rex = new(XPos, YPos, PILDescr.Width, Height);
                WhatToPrint = invBreakdown.ItemDescription;
                ev.Graphics!.DrawString(WhatToPrint, PILDescr.ItemFont, Brushes.Black, Rex, new StringFormat());

                XPos = PILAmt.XPos;
                // YPos = PILAmt.YPos;
                Rectangle RexAmt = new(XPos, YPos, PILAmt.Width, Height);
                StringFormat SRF = new() { Alignment = StringAlignment.Far };
                ev.Graphics!.DrawString(invBreakdown.ItemPrice.ToString(), PILAmt.ItemFont, Brushes.Black, RexAmt, SRF);

                // figure out how much space was used by the text and adjust the spacing 

                SizeF TextSize = ev.Graphics.MeasureString(WhatToPrint, PILDescr.ItemFont);
                YPos += (int)(TextSize.Height * 1.3);  // with a margin
            }
        }


        internal static InvoicePrintLayout BuildInvoiceLayout()
        {
            InvoicePrintLayout CurrentPrintLayout;

            CurrentPrintLayout = new();
            CurrentPrintLayout.LogoFileName = new()
            {
                IfToPrintThisItem = true,
                VariableNameToPrint = "Logo.BMP",
                YPos = 70,
                XPos = 40,
                Height = 40,
                Width = 535
            };

            // company information items

            CurrentPrintLayout.CompanyInformationItems = new()
            {
                new PrintItemLayout
                {
                    IfToPrintThisItem = true,
                    VariableNameToPrint = idCompanyAdr,
                    ItemFont = new Font("Times New Roman", 12),
                    YPos = 130,
                    XPos = 110,
                    Height = 15,
                    Width = 150
                },
                new PrintItemLayout
                {
                    IfToPrintThisItem = true,
                    VariableNameToPrint = idCompanyAdr2,
                    ItemFont = new Font("Times New Roman", 12),
                    YPos = 148,
                    XPos = 110,
                    Height = 15,
                    Width = 150
                },
                new PrintItemLayout
                {
                    IfToPrintThisItem = true,
                    VariableNameToPrint = idCompanyAdr3,
                    ItemFont = new Font("Times New Roman", 12),
                    YPos = 172,
                    XPos = 110,
                    Height = 15,
                    Width = 150
                }

            };

            // add boxes
            CurrentPrintLayout.BoxRects = new()
            {
                new Rectangle(585, 94, 88, 30), // date
                new Rectangle(673, 94, 93, 30), // invoice #
                new Rectangle(585, 124, 88, 30), // actual date
                new Rectangle(673, 124, 93, 30), // actual invoice #

                new Rectangle(110, 338, 324, 76), // bill to
                new Rectangle(674, 408, 93, 30),  // Due Date:
                new Rectangle(674, 438, 93, 27),  // actual due date

                new Rectangle(114, 465, 490, 30), // description
                new Rectangle(604, 465, 165, 30), // amount
                new Rectangle(114, 495, 490, 330), // invoice breakdown
                new Rectangle(604, 495, 165, 330), // breakdown amounts

                new Rectangle(114, 825, 392, 40),  // empty
                new Rectangle(506, 825, 263, 40),  // total

                new Rectangle(506, 915, 263, 30),  // payments/credits
                new Rectangle(506, 945, 263, 40)  // balance due
            };

            // add the standard texts

            CurrentPrintLayout.StandardTexts = new()
            {
                new PrintItemLayout
                {
                    IfToPrintThisItem = true,
                    VariableNameToPrint = "Invoice",
                    ItemFont = new Font("Lucida Sans", 20, FontStyle.Bold),
                    YPos = 50,
                    XPos = 650,
                    Height = 30,
                    Width = 200
                },
                new PrintItemLayout
                {
                    IfToPrintThisItem = true,
                    VariableNameToPrint = "Date",
                    ItemFont = new Font("Arial", 9),
                    YPos = 100,
                    XPos = 610,
                    Height = 12,
                    Width = 150
                },
                new PrintItemLayout
                {
                    IfToPrintThisItem = true,
                    VariableNameToPrint = "Invoice #",
                    ItemFont = new Font("Arial", 9),
                    YPos = 100,
                    XPos = 685,
                    Height = 12,
                    Width = 150
                },
                new PrintItemLayout
                {
                    IfToPrintThisItem = true,
                    VariableNameToPrint = "Bill To:",
                    ItemFont = new Font("Arial", 10),
                    YPos = 310,
                    XPos = 140,
                    Height = 12,
                    Width = 150
                },
                new PrintItemLayout
                {
                    IfToPrintThisItem = true,
                    VariableNameToPrint = "Due Date:",
                    ItemFont = new Font("Arial", 8),
                    YPos = 413,
                    XPos = 690,
                    Height = 12,
                    Width = 150
                },
                new PrintItemLayout
                {
                    IfToPrintThisItem = true,
                    VariableNameToPrint = "Description",
                    ItemFont = new Font("Arial", 8),
                    YPos = 475,
                    XPos = 325,
                    Height = 12,
                    Width = 150
                },
                new PrintItemLayout
                {
                    IfToPrintThisItem = true,
                    VariableNameToPrint = "Amount",
                    ItemFont = new Font("Arial", 8),
                    YPos = 475,
                    XPos = 685,
                    Height = 12,
                    Width = 150
                },
                new PrintItemLayout
                {
                    IfToPrintThisItem = true,
                    VariableNameToPrint = "Total",
                    ItemFont = new Font("Arial", 15, FontStyle.Bold),
                    YPos = 830,
                    XPos = 510,
                    Height = 12,
                    Width = 150
                },
                new PrintItemLayout
                {
                    IfToPrintThisItem = true,
                    VariableNameToPrint = "Payments/Credits",
                    ItemFont = new Font("Arial", 10, FontStyle.Bold),
                    YPos = 922,
                    XPos = 510,
                    Height = 12,
                    Width = 150
                },
                new PrintItemLayout
                {
                    IfToPrintThisItem = true,
                    VariableNameToPrint = "Balance Due:",
                    ItemFont = new Font("Arial", 15, FontStyle.Bold),
                    YPos = 952,
                    XPos = 510,
                    Height = 12,
                    Width = 150
                }
            };

            CurrentPrintLayout.TextToPrintLayouts = new()
            {
                new PrintItemLayout
                {
                    IfToPrintThisItem = true,
                    VariableNameToPrint = nameof(Invoice.BillingAddress1),
                    ItemFont = new Font("Times New Roman", 9),
                    YPos = 340,
                    XPos = 115,
                    Height = 12,
                    Width = 150
                },
                new PrintItemLayout
                {
                    IfToPrintThisItem = true,
                    VariableNameToPrint = nameof(Invoice.BillingAddress2),
                    ItemFont = new Font("Times New Roman", 9),
                    YPos = 355,
                    XPos = 115,
                    Height = 12,
                    Width = 150
                },
                new PrintItemLayout
                {
                    IfToPrintThisItem = true,
                    VariableNameToPrint = nameof(Invoice.BillingAddress3),
                    ItemFont = new Font("Times New Roman", 9),
                    YPos = 370,
                    XPos = 115,
                    Height = 12,
                    Width = 150
                },
                new PrintItemLayout
                {
                    IfToPrintThisItem = true,
                    VariableNameToPrint = nameof(Invoice.BillingDate),
                    ItemFont = new Font("Times New Roman", 9),
                    YPos = 131,
                    XPos = 590,
                    Height = 12,
                    Width = 75,
                    HowToJustify = PrintItemLayout.Justification.Center
                },
                new PrintItemLayout
                {
                    IfToPrintThisItem = true,
                    VariableNameToPrint = nameof(Invoice.InvoiceNumber),
                    ItemFont = new Font("Times New Roman", 9),
                    YPos = 131,
                    XPos = 680,
                    Height = 12,
                    Width = 75,
                    HowToJustify = PrintItemLayout.Justification.Center
                },
                new PrintItemLayout
                {
                    IfToPrintThisItem = true,
                    VariableNameToPrint = nameof(Invoice.DueDate),
                    ItemFont = new Font("Times New Roman", 9),
                    YPos = 442,
                    XPos = 680,
                    Height = 12,
                    Width = 75,
                    HowToJustify = PrintItemLayout.Justification.Center
                },
                new PrintItemLayout
                {
                    IfToPrintThisItem = true,
                    VariableNameToPrint = nameof(Invoice.Total),
                    ItemFont = new Font("Times New Roman", 12),
                    YPos = 833,
                    XPos = 600,
                    Height = 19,
                    Width = 150,
                    HowToJustify = PrintItemLayout.Justification.RightJustify
                },
                new PrintItemLayout
                {
                    IfToPrintThisItem = true,
                    VariableNameToPrint = nameof(Invoice.AmountPaid),
                    ItemFont = new Font("Times New Roman", 9),
                    YPos = 923,
                    XPos = 640,
                    Height = 12,
                    Width = 110,
                    HowToJustify = PrintItemLayout.Justification.RightJustify
                },
                new PrintItemLayout
                {
                    IfToPrintThisItem = true,
                    VariableNameToPrint = nameof(Invoice.BalanceDue),
                    ItemFont = new Font("Times New Roman", 12),
                    YPos = 955,
                    XPos = 640,
                    Height = 19,
                    Width = 110,
                    HowToJustify = PrintItemLayout.Justification.RightJustify
                },
                new PrintItemLayout
                {
                    IfToPrintThisItem = true,
                    VariableNameToPrint = "BreakdownDescription",
                    ItemFont = new Font("Times New Roman", 12),
                    YPos = 497,
                    XPos = 116,
                    Height = 325,
                    Width = 480
                },
                new PrintItemLayout
                {
                    IfToPrintThisItem = true,
                    VariableNameToPrint = "BreakdownAmount",
                    ItemFont = new Font("Times New Roman", 12),
                    YPos = 497,
                    XPos = 610,
                    Height = 325,
                    Width = 140
                }

              
            };
            return CurrentPrintLayout;
        }
    }
}
