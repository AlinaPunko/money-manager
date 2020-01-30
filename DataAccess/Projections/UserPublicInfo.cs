using System;
using DataAccess.Models;

namespace DataAccess.Projections
{
    public class UserPublicInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public UserPublicInfo() { }

        public UserPublicInfo(Guid id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        public static explicit operator UserPublicInfo(User user)
        {
            return new UserPublicInfo(user.Id, user.Name, user.Email);
        }
    }
}