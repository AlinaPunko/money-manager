using System;

namespace DataAccess.Projections
{
    public class FullTransactionInfo
    {
        public string AssetName { get; set; }
        public string CategoryName { get; set; }
        public string ParentName { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }

        public FullTransactionInfo(string assetName, string categoryName, string parentName, double amount,
            DateTime date, string comment)
        {
            AssetName = assetName;
            CategoryName = categoryName;
            ParentName = parentName;
            Amount = amount;
            Date = date;
            Comment = comment;
        }
    }
}