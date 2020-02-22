using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class Asset
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public Guid UserId { get; set; }
        public double Balance { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }

        public Asset(string name, Guid userId)
        {
            Id = Guid.NewGuid();
            Name = name;
            UserId = userId;
            Balance = 0;
        }
    }
}