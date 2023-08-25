//
//  Copyright 2021 David Randolph
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCheckBook.DataStore
{
    public class AccountCategory
    {
        public enum CategoryType { Income, Expense }

        public CategoryType WhatType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public AccountCategory()
        {
            Name = string.Empty;
            Description = string.Empty;
        }

    }
}
