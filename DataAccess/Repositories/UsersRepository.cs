using System;
using System.Collections.Generic;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

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

        public User GetUserById(Guid id)
        {
            return FindById(id);
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