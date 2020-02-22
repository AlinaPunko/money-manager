using System;

namespace DataAccess.Projections
{
    public class UserPublicInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}