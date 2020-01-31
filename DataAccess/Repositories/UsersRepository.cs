using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DataAccess.Core;
using DataAccess.Helpers;
using DataAccess.Models;
using DataAccess.Projections;
using Microsoft.EntityFrameworkCore;
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

        public IReadOnlyList<UserPublicInfo> GetUsersSortedByName()
        {
            var orderedUsers =  Get().OrderBy(user=>user.Name)
                .ToList();
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<User, UserPublicInfo>();
            });

            IMapper mapper = config.CreateMapper();
            return mapper.Map<List<User>, IReadOnlyList<UserPublicInfo>>(orderedUsers);
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
            var user = Get(u => u.Id==id)
                .Include(u => u.Assets.Select(asset => asset.Transactions))
                .FirstOrDefault();
            double balance = 0;
            if (user != null)
            {
                foreach (var asset in user.Assets)
                {
                    var transactions = asset.Transactions.ToList();
                    balance += BalanceHelper.GetBalance(transactions);
                }

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<User, UserBalanceInfo>();
                });
                var mapper = config.CreateMapper();
                var userBalanceInfo = mapper.Map<User, UserBalanceInfo>(user);
                userBalanceInfo.Balance = balance;
                return userBalanceInfo;
            }

            return null;
        }

        public UsersRepository(DbContext context) : base(context)
        {
            
        }
    }
}