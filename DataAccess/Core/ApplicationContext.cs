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
            Database.Migrate();
            InsertDefaultValues();
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
                    if (Users.Count() < 11)
                    {
                        Users.AddRange(users);
                    }

                    if (Assets.Count() < 21)
                    {
                        Assets.AddRange(assets);
                    }

                    if (Categories.Count() < 11)
                    {
                        Categories.AddRange(categories);
                    }

                    if (Transactions.Count() < 1000)
                    {
                        Transactions.AddRange(DefaultValues.GenerateDefaultTransactions(assets, categories));
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
