using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Core;
using DataAccess.Enums;
using DataAccess.Helpers;
using DataAccess.Models;
using DataAccess.Projections;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class TransactionRepository : GenericRepository<Transaction>
    {

        public IReadOnlyList<Transaction> GetAll()
        {
            return Get()
                .ToList();
        }

        public new Transaction GetById(Guid id)
        {
            return base.GetById(id);
        }
        
        public double GetBalanceOfTheAsset(Guid assetId)
        {
            var transactions = Get(t => t.AssetId == assetId)
                .ToList();
            return BalanceHelper.GetBalance(transactions);
        }

        public double GetBalanceOfTheUser(Guid userId)
        {
            var transactions = Get(t => t.Asset.UserId == userId)
                .ToList();
            return BalanceHelper.GetBalance(transactions);
        }

        public IReadOnlyList<Transaction> GetTransactionsByUser(Guid userId)
        {
            return Get(t => t.Asset.UserId == userId)
                .OrderByDescending(t => t.Date)
                .ThenBy(t => t.Asset.Name)
                .ThenBy(t => t.Category.Name).ToList(); 
        }

        public IReadOnlyList<Transaction> GetIncomeExpensesForPeriod(Guid userId)
        {
            //return Get(t => t.Asset.UserId == userId).ToList().OrderBy(t => t.Date);
            return null;
        }

        public void DeleteTransactionsOfUserInCurrentMonth(Guid userId)
        {
            var transactions = Get(t => t.Asset.UserId == userId && t.Date.Month==DateTime.Now.Month);
            foreach (var transaction in transactions)
            {
                Remove(transaction);
            }
        }

        public TransactionRepository(DbContext context) : base(context)
        {

        }
    }
}
