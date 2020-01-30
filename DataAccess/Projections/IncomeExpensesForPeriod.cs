using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Projections
{
    class IncomeExpensesForPeriod
    {
        public double Income { get; set; }
        public double Expenses { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        public IncomeExpensesForPeriod(double income, double expenses, DateTime date)
        {
            Income = income;
            Expenses = expenses;
            Month = date.Month;
            Year = date.Year;
        }
    }
}
