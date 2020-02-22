using System;

namespace DataAccess.Projections
{
    public class AssetBalanceInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Balance { get; set; }
    }
}