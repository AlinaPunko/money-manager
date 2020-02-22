using System;

namespace DataAccess.Projections
{
    public class UserBalanceInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public double Balance { get; set; }
    }
}
