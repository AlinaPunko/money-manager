using System;
using DataAccess.Helpers;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace DataAccess.Core
{
    public sealed class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public ApplicationContext(DbContextOptions options) : base(options)
        {
            if (Database.CanConnect())
            {
                return;
            }
                
            Database.Migrate();
            InsertDefaultValues();
        }

        private void InsertDefaultValues()
        {
            User[] users = DefaultValuesHelper.GenerateDefaultUsers();
            Asset[] assets = DefaultValuesHelper.GenerateDefaultAssets(users);
            Category[] categories = DefaultValuesHelper.GenerateDefaultCategories();
            Transaction[] transactions = DefaultValuesHelper.GenerateDefaultTransactions(assets, categories);

            using (IDbContextTransaction transaction = Database.BeginTransaction())
            {
                try
                {
                    Users.AddRange(users);
                    Assets.AddRange(assets);
                    Categories.AddRange(categories);
                    Transactions.AddRange(transactions);
                    foreach (Transaction transactionsItem in transactions)
                    {
                        if (transactionsItem.Category.Type == CategoryType.Income)
                        {
                            transactionsItem.Asset.Balance += transactionsItem.Amount;
                        }
                        else
                        {
                            transactionsItem.Asset.Balance -= transactionsItem.Amount;
                        }
                    }

                    SaveChanges();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    transaction.Rollback();
                }
            }
        }
    }
}
