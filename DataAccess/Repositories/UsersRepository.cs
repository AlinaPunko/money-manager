using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Core;
using DataAccess.Models;
using DataAccess.Projections;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace DataAccess.Repositories
{
    public class UsersRepository : GenericRepository<User>
    {
        
        public void AddUser(User user)
        {
            Add(user);
        }

        public IReadOnlyList<User> GetAll()
        {
            return Get()
                .ToList();
        }

        public IReadOnlyList<User> GetUsersSortedByName()
        {
            return Get().OrderBy(user=>user.Name)
                .ToList();
        }

        public new User GetById(Guid id)
        {
            return base.GetById(id);
        }
        
        public User GetByEmail(string email)
        {
            return Get(user => user.Email == email)
                .FirstOrDefault();
        }

        public UserBalanceInfo GetUserWithBalance(Guid id)
        {
            var user = Get(u => u.Id==id).Include(u => u.Assets.Select(asset => asset.Transactions));
            //return new UserBalanceInfo(user.Id, user.Name, user.Email, new TransactionRepository(context).GetBalanceOfTheUser(id));
            return null;
            
        }

        public UsersRepository(DbContext context) : base(context)
        {
            
        }
    }
}