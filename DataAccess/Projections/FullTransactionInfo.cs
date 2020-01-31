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
    }
}