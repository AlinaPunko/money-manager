using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MoneyManager.Models
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; }
        [NotNull]
        public string Name { get; set; }
        [NotNull]
        public int Type { get; set; }
        public Guid ParentId { get; set; }
        private List<Category> Categories { get; set; }
        private List<Transaction> Transactions { get; set; }

        public Category() { }

        public Category(string name, int type, Guid parentId)
        {
            Id = Guid.NewGuid();
            Name = name;
            Type = type;
            ParentId = parentId;
        }

        public Category(string name, int type)
        {
            Id = Guid.NewGuid();
            Name = name;
            Type = type;
        }

    }
}