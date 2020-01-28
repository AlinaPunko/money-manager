using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MoneyManager.Models
{
    public class Asset
    {
        public Guid Id { get; set; }
        [NotNull]
        public string Name { get; set; }
        [NotNull]
        public Guid UserId { get; set; }

        public User User { get; set; }
        public List<Transaction> Transactions { get; set; }

        public Asset() { }

        public Asset(string name, Guid userId)
        {
            Id = Guid.NewGuid();
            Name = name;
            UserId = userId;
        }
    }
}