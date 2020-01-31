using System;
using System.Linq;
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
