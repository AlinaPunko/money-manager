using System.Collections.Generic;
using System.Linq;
using DataAccess.Models;

namespace DataAccess.Helpers
{
    public static class BalanceHelper
    {
        public static double GetBalance(IReadOnlyList<Transaction> transactions)
        {
            return GetSumMoney(transactions, CategoryType.Income) - GetSumMoney(transactions, CategoryType.Expense);
        }

        public static double GetUserBalance(User user)
        {
            return user.Assets
                .Select(asset => asset.Transactions.ToList())
                .Select(GetBalance)
                .Sum();
        }

        public static double GetSumMoney(IReadOnlyList<Transaction> transactions, CategoryType categoryType)
        {
            if (categoryType == CategoryType.Income)
            {
                return transactions
                    .Where(transaction => transaction.Category.Type == CategoryType.Income)
                    .Sum(transaction => transaction.Amount);
            }

            return transactions
                .Where(transaction => transaction.Category.Type == CategoryType.Expense)
                .Sum(transaction => transaction.Amount);
        }
    }
}