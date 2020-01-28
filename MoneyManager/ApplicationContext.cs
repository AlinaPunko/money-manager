using System.Configuration;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MoneyManager.Models;

namespace MoneyManager
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public ApplicationContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            //string connectionString = ConfigurationManager.ConnectionStrings["MoneyDatabase"].ConnectionString;
            options.UseSqlServer("Server=WSB-045-71;Database=MoneyManager;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            User[] users = Autogenerator.GenerateDefaultUsers();
            Category[] categories = Autogenerator.GenerateDefaultCategories();
            Asset[] assets =  Autogenerator.GenerateDefaultAssets(users);
            modelBuilder.Entity<User>().HasData(users);
            modelBuilder.Entity<Category>().HasData(categories);
            modelBuilder.Entity<Asset>().HasData(assets);
            modelBuilder.Entity<Transaction>().HasData(Autogenerator.GenerateDefaultTransactions(assets, categories));
        }

    }
}
