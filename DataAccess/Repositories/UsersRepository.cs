using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DataAccess.Core;
using DataAccess.Helpers;
using DataAccess.MapperProfiles;
using DataAccess.Models;
using DataAccess.Projections;
using Microsoft.EntityFrameworkCore;
namespace DataAccess.Repositories
{
    public class UsersRepository : GenericRepository<User>
    {
        public UsersRepository(DbContext context) : base(context) { }

        public IReadOnlyList<User> GetAll()
        {
            return Get()
                .ToList();
        }

        public IReadOnlyList<UserPublicInfo> GetUsersSortedByName()
        {
            List<User> orderedUsers =  Get()
                .OrderBy(user=>user.Name)
                .ToList();

            MapperConfiguration config = new MapperConfiguration(cfg => cfg.AddProfile<UserPublicInfoProfile>());
            IMapper mapper = config.CreateMapper();

            return mapper.Map<IReadOnlyList<UserPublicInfo>>(orderedUsers);
        }

        public User GetByEmail(string email)
        {
            return Get(user => user.Email == email)
                .FirstOrDefault();
        }

        public UserBalanceInfo GetUserWithBalance(Guid id)
        {
            User user = Get(u => u.Id==id)
                .Include(u => u.Assets.Select(asset => asset.Transactions))
                .FirstOrDefault();

            if (user == null)
            {
                return null;
            }

            MapperConfiguration config = new MapperConfiguration(cfg => cfg.AddProfile<UserBalanceInfoProfile>());
            IMapper mapper = config.CreateMapper();

            UserBalanceInfo userBalanceInfo = mapper.Map<UserBalanceInfo>(user);
            return userBalanceInfo;
        }
    }
}