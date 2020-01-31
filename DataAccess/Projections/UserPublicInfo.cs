using System;
using DataAccess.Models;

namespace DataAccess.Projections
{
    public class UserPublicInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}