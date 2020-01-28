using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoneyManager.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        private string Name { get; set; }
        private int Type { get; set; }
        private Guid ParentId { get; set; }

        public Category() { }

        public Category(string name, int type, Guid parentId)
        {
            Id = Guid.NewGuid();
            Name = name;
            Type = type;
            ParentId = parentId;
        }

        private List<Category> Categories { get; set; }
        private List<Transaction> Transactions { get; set; }
    }
}