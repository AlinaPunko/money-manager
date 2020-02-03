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
            return GetIncomes(transactions) - GetOutcomes(transactions);
        }

        public static double GetIncomes(IReadOnlyList<Transaction> transactions)
        {
            return transactions
                .Where(transaction => transaction.Category.Type == CategoryType.Income)
                .Sum(transaction => transaction.Amount);
        }

        public static double GetOutcomes(IReadOnlyList<Transaction> transactions)
        {
            return transactions
                .Where(transaction => transaction.Category.Type == CategoryType.Expense)
                .Sum(transaction => transaction.Amount);
        }
    }
}