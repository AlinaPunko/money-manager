using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Core
{
    public abstract class DesignTimeDbContextFactoryBase<TContext> : IDesignTimeDbContextFactory<TContext> where TContext : DbContext
    {
        public TContext CreateDbContext(string[] args)
        {
            return Create(Directory.GetCurrentDirectory(), Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
        }

        protected abstract TContext CreateNewInstance(DbContextOptions<TContext> options);

        public TContext Create()
        {
            string environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            string basePath = AppContext.BaseDirectory;

            return Create(basePath, environmentName);
        }

        private TContext Create(string basePath, string environmentName)
        {
            Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory);
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("/../MoneyManager/MoneyManager/appsettings.json")
                .AddJsonFile($"../MoneyManager/MoneyManager/appsettings.{environmentName}.json", true)
                .AddEnvironmentVariables();

            IConfigurationRoot config = builder.Build();

            string connectionString = config.GetConnectionString("default");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("Could not find a connection string named 'default'.");
            }
            return Create(connectionString);
        }

        private TContext Create(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException($"{nameof(connectionString)} is null or empty.", nameof(connectionString));

            DbContextOptionsBuilder<TContext> optionsBuilder = new DbContextOptionsBuilder<TContext>();
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer(connectionString);
            DbContextOptions<TContext> options = optionsBuilder.Options;

            return CreateNewInstance(options);
        }
    }
}