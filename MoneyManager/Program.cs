using System;
using System.Collections.Generic;
using DataAccess.Core;
using DataAccess.Enums;
using DataAccess.Models;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;

namespace MoneyManager
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new ApplicationContextFactory().Create())
            {
                var repository = new CategoriesRepository(context);
                var x = repository.GetAmountOfParentCategories((int)CategoryType.Expense, new Guid("32C016A0-0035-4A4F-A334-7FA0A8F4E964"));
            }
        }
    }
}
