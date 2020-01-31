using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace DataAccess.Models
{
    public class Asset
    {
        public Guid Id { get; set; }
        [NotNull]
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public double Amount { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }

        public Asset() { }

        public Asset(string name, Guid userId)
        {
            Id = Guid.NewGuid();
            Name = name;
            UserId = userId;
            Amount = 0;
        }

        public Asset(string name, Guid userId, double amount)
        {
            Id = Guid.NewGuid();
            Name = name;
            UserId = userId;
            Amount = amount;
        }
    }
}