using System.Collections.Generic;
using System.Linq;
using DataAccess.Enums;
using DataAccess.Models;

namespace DataAccess.Helpers
{
    public class BalanceHelper
    {
        public static double GetBalance(IReadOnlyList<Transaction> transactions)
        {
            var incomes = transactions
                .Where(transaction => transaction.Category.Type == (int)CategoryType.Income)
                .Sum(transaction => transaction.Amount);
            var outcomes = transactions.Where(transaction => transaction.Category.Type == (int)CategoryType.Expense)
                .Sum(transaction => transaction.Amount);
            return incomes - outcomes;
        }
    }
}