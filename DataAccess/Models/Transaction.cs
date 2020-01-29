using JetBrains.Annotations;
using System;

namespace DataAccess.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }
        [NotNull]
        public double Amount { get; set; }
        [NotNull]
        public DateTime Date { get; set; }
        public string Comment { get; set; }
        [NotNull]
        public Guid AssetId { get; set; }
        [NotNull]
        public Guid CategoryId { get; set; }

        public Transaction() { }

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