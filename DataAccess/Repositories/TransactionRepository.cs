using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.GenericRepository;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace DataAccess.Repositories
{
    class TransactionRepository : GenericRepository<Transaction>
    {
        public void AddTransaction(Transaction transaction)
        {
            Create(transaction);
        }

        public IReadOnlyList<Transaction> GetTransactions()
        {
            return Get();
        }

        public Transaction GetTransactionById(Guid id)
        {
            return FindById(id);
        }

        public void DeleteTransaction(Transaction transaction)
        {
            Remove(transaction);
        }

        public IReadOnlyList<Transaction> GetTransactionsByQuery(Func<Transaction, bool> predicate)
        {
            return Get(predicate);
        }

        public void DeleteTransactions(Guid userId)
        {

            //var transactions = context.Set<Transaction>().Include("Asset")
            //var assetsOfUser = new AssetsRepository(context).GetAssetsByQuery(asset => asset.UserId == userId);
            //var transactions = Get(transaction => transaction.Date.Month == DateTime.Today.Month).Join(assetsOfUser, transaction=>transaction.AssetId, asset=>userId, (transaction, asset) =>{id}  );
            //foreach (var transaction in transactions)
            //{
            //    Remove(transaction);
            //}
        }

        public void UpdateTransaction(Transaction transaction)
        {
            Update(transaction);
        }
        
        public TransactionRepository(DbContext context) : base(context)
        {

        }
    }
}
