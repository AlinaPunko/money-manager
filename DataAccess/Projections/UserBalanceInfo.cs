using System;
using DataAccess.Models;

namespace DataAccess.Projections
{
    public class UserBalanceInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public double Balance { get; set; }

        public UserBalanceInfo() { }

        public UserBalanceInfo(Guid id, string name, string email, double balance)
        {
            Id = id;
            Name = name;
            Email = email;
            Balance = balance;
        }
    }
}
