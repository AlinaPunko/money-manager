using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;

namespace DataAccess.Models
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; }
        [NotNull]
        public string Name { get; set; }
        public int Type { get; set; }
        public Guid? ParentId { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }

        public virtual ICollection<Category> Children { get; set; }
        public virtual Category Parent { get; set; }
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