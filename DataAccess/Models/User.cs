using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace DataAccess.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }
        public virtual ICollection<Asset> Assets { get; set; }

        public User() { }

        public User(string name, string email, string password)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            Salt = GetSalt();
            byte[] hash = MD5.Create().ComputeHash((Encoding.Default.GetBytes(password + Salt)));
            Hash = Convert.ToBase64String(hash);
        }

        private static int saltLengthLimit = 32;
        private static string GetSalt()
        {
            return GetSalt(saltLengthLimit);
        }

        private static string GetSalt(int maximumSaltLength)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[maximumSaltLength];
            rng.GetBytes(buff);

            return Convert.ToBase64String(buff);
        }
    }
}