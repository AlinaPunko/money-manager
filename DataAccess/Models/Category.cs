using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public CategoryType Type { get; set; }

        public Guid? ParentId { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual Category Parent { get; set; }

        public Category(string name, CategoryType type, Guid parentId)
        {
            Id = Guid.NewGuid();
            Name = name;
            Type = type;
            ParentId = parentId;
        }

        public Category(string name, CategoryType type)
        {
            Id = Guid.NewGuid();
            Name = name;
            Type = type;
        }

    }
}