using System;
using DataAccess.Core;
using DataAccess.Repositories;

namespace MoneyManager
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new ApplicationContextFactory().Create())
            {
                var repository = new TransactionRepository(context);
                var transaction =
                    repository.GetById(new Guid("C5686806-E088-48B8-8FB1-019DC4474979"));
                transaction.Amount = 10;
                repository.Update(transaction);
            }
        }
    }
}
