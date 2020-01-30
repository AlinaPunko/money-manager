using System;
using System.Collections.Generic;
using DataAccess.GenericRepository;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class TransactionRepository : GenericRepository<Transaction>
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


        public void UpdateTransaction(Transaction transaction)
        {
            Update(transaction);
        }
        
        public TransactionRepository(DbContext context) : base(context)
        {

        }
    }
}
