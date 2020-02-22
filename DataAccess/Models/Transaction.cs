using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }

        [Required]
        public double Amount { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string Comment { get; set; }
        public Guid AssetId { get; set; }
        public Guid CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Asset Asset { get; set; }
        
        public Transaction(double amount, DateTime date, string comment, Guid assetId, Guid categoryId)
        {
            Id = Guid.NewGuid();
            Amount = amount;
            Date = date;
            Comment = comment;
            AssetId = assetId;
            CategoryId = categoryId;
        }
    }
}