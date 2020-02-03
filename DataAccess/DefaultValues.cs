using System;
using DataAccess.Enums;
using DataAccess.Models;

namespace DataAccess
{
    public class DefaultValues
    {
        public static User[] GenerateDefaultUsers()
        {
            User[] users = new User[11];
            users[0] = new User("Alina Punko", "iakram.zoughbi1@soldnmyyb.shop", "pa$$w0rd");
            users[1] = new User("John Doe", "bnikky.parmar@blackpetty.recipes", "pa$$w0rd");
            users[2] = new User("Jane Doe", "ogfgog5t@quantums-code.site", "pa$$w0rd");
            users[3] = new User("Peter Parker", "oabogodhaua@scruto.xyz", "pa$$w0rd");
            users[4] = new User("Tony Stark", "2mustafa.fierb@fox-skin.fun", "pa$$w0rd");
            users[5] = new User("Steve Rogers", "zcherif.driouechk@cuisi.xyz", "pa$$w0rd");
            users[6] = new User("Clint Barton", "veslamsalem323p@michaelbollhoefer.com", "pa$$w0rd");
            users[7] = new User("Nick Fury", "mabdelazem00@leadingeu.site", "pa$$w0rd");
            users[8] = new User("Bucky Barnes", "lpipo.2005y@appleshps.website", "pa$$w0rd");
            users[9] = new User("Bruce Wayne", "zdjg@squeezetv.com", "pa$$w0rd");
            users[10] = new User("Natasha Romanoff", "tnfseaahr@promistral.website", "pa$$w0rd");
            return users;
        }

        public static Category[] GenerateDefaultCategories()
        {
            Category[] categories = new Category[13];
            categories[0] = new Category("Transportation", CategoryType.Expense);
            categories[1] = new Category("Food", CategoryType.Expense);
            categories[2] = new Category("Social Life", CategoryType.Expense);
            categories[3] = new Category("Culture", CategoryType.Expense);
            categories[4] = new Category("Self-development", CategoryType.Expense);
            categories[5] = new Category("Salary", CategoryType.Income);
            categories[6] = new Category("Bonus", CategoryType.Income);
            categories[7] = new Category("Petty cash", CategoryType.Income);
            categories[8] = new Category("Taxi", CategoryType.Expense, categories[0].Id);
            categories[9] = new Category("Public transport", CategoryType.Expense, categories[0].Id);
            categories[10] = new Category("Parking", CategoryType.Expense, categories[0].Id);
            categories[11] = new Category("Bus", CategoryType.Expense, categories[9].Id);
            categories[12] = new Category("Trolleybus", CategoryType.Expense, categories[9].Id);
            return categories;
        }

        public static Asset[] GenerateDefaultAssets(User[] users)
        {
            Asset[] assets = new Asset[21];

            for (int i = 0; i <= assets.Length-1; i++)
            {
                assets[i] = new Asset("qwerty", users[i/2].Id);
            }
            return assets;
            
        }

        static readonly Random rnd = new Random();

        public static DateTime GetRandomDate(DateTime from, DateTime to)
        {
            var range = to - from;

            var randTimeSpan = new TimeSpan((long)(rnd.NextDouble() * range.Ticks));

            return from + randTimeSpan;
        }

        public static Transaction[] GenerateDefaultTransactions(Asset[] assets, Category[] categories)
        {
            Transaction[] transactions = new Transaction[1001];
            Random random = new Random();
            for (int i = 0; i <= transactions.Length - 1; i++)
            {
                transactions[i] = new Transaction(random.NextDouble() * 100,
                        GetRandomDate(new DateTime(2018, 12, 1), DateTime.Today), null,
                        assets[i / 50].Id, categories[i / 80].Id);
            }
            return transactions;
        }
    }
}