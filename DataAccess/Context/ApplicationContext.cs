using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Context
{
    public sealed class ApplicationContext :DbContext
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

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            User[] users = DefaultValues.GenerateDefaultUsers();
            Category[] categories = DefaultValues.GenerateDefaultCategories();
            Asset[] assets =  DefaultValues.GenerateDefaultAssets(users);
            modelBuilder.Entity<User>().HasData(users);
            modelBuilder.Entity<Category>().HasData(categories);
            modelBuilder.Entity<Asset>().HasData(assets);
            modelBuilder.Entity<Transaction>().HasData(DefaultValues.GenerateDefaultTransactions(assets, categories));
        }

    }
}
