using System.Collections.Generic;
using System.Linq;
using DataAccess.Models;

namespace DataAccess.Helpers
{
    public static class BalanceHelper
    {
        public static double GetBalance(IReadOnlyList<Transaction> transactions)
        {
            return SumMoney(transactions, CategoryType.Income) - SumMoney(transactions, CategoryType.Expense);
        }

        public static double GetUserBalance(User user)
        {
            return user.Assets
                .Select(asset => asset.Transactions.ToList())
                .Select(GetBalance)
                .Sum();
        }

        public static double SumMoney(IReadOnlyList<Transaction> transactions, CategoryType categoryType)
        {
            return transactions
                .Where(transaction => transaction.Category.Type == categoryType)
                .Sum(transaction => transaction.Amount);
        }
    }
}