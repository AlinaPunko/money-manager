using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoneyManager.Models
{
    public class Asset
    {
        public Guid Id { get; set; }
        private string Name { get; set; }
        private Guid UserId { get; set; }

        private User User { get; set; }
        private List<Transaction> Transactions { get; set; }

        public Asset() { }

        public Asset(string name, Guid userId)
        {
            Id = Guid.NewGuid();
            Name = name;
            UserId = userId;
        }
    }
}