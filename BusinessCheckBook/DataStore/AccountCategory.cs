//**********************************************************************
//
//          Copyright © 2023 Prairie Trail Software, Inc.
//
//**********************************************************************




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
