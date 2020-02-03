using System;
using DataAccess.Enums;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Core
{
    public sealed class ApplicationContext :DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public ApplicationContext(DbContextOptions options) : base(options)
        {
            if (!Database.CanConnect())
            {
                Database.Migrate();
                InsertDefaultValues();
            }
        }

        private void InsertDefaultValues()
        {
            using (var transaction = Database.BeginTransaction())
            {
                try
                {
                    var users = DefaultValues.GenerateDefaultUsers();
                    var assets = DefaultValues.GenerateDefaultAssets(users);
                    var categories = DefaultValues.GenerateDefaultCategories();
                    var transactions = 
                        DefaultValues.GenerateDefaultTransactions(assets, categories);

                    Users.AddRange(users);
                    Assets.AddRange(assets);
                    Categories.AddRange(categories);
                    Transactions.AddRange(transactions);
                    foreach (var transactionsItem in transactions)
                    {
                        if (transactionsItem.Category.Type == CategoryType.Income)
                        {
                            transactionsItem.Asset.Amount += transactionsItem.Amount;
                        }
                        else
                        {
                            transactionsItem.Asset.Amount -= transactionsItem.Amount;
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
