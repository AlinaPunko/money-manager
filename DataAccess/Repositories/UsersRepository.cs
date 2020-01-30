using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.GenericRepository;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace DataAccess.Repositories
{
    public class UsersRepository : GenericRepository<User>
    {
        
        public void AddUser(User user)
        {
            Create(user);
        }

        public IReadOnlyList<User> GetUsers()
        {
            return Get();
        }

        public IReadOnlyList<User> GetUsersSortedByName()
        {
            return Get().OrderBy(user=>user.Name).ToList();
        }

        public User GetUserById(Guid id)
        {
            return FindById(id);
        }

        public User GetUserByEmail(string email)
        {
            return Get(user => user.Email == email).FirstOrDefault();
        }

        public void DeleteUser(User user)
        {
            Remove(user);
        }

        public void UpdateUser(User user)
        {
            Update(user);
        }

        public UsersRepository(DbContext context) : base(context)
        {
            
        }
    }
}