using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Projections
{
    public class BudgetInfo
    {
        public double Income { get; set; }
        public double Expenses { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        public BudgetInfo(double income, double expenses, int month, int year)
        {
            Income = income;
            Expenses = expenses;
            Month = month;
            Year = year;
        }
    }
}
