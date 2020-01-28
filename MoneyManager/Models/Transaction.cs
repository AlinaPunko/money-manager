using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoneyManager.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }
        public Guid AssetId { get; set; }
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
        
        private List<Category> Categories { get; set; }
    }
}