using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace MoneyManager.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }

        public User() { }

        public User(string name, string email, string password)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            byte[] salt = GetSalt();
            Salt = Encoding.Default.GetString(salt, 0, salt.Length);
            byte[] hash = MD5.Create().ComputeHash((Encoding.Default.GetBytes(password + Salt)));
            Hash = Encoding.Default.GetString(hash, 0, hash.Length);
        }

        private static int saltLengthLimit = 32;
        private static byte[] GetSalt()
        {
            return GetSalt(saltLengthLimit);
        }

        private static byte[] GetSalt(int maximumSaltLength)
        {
            var salt = new byte[maximumSaltLength];
            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }

            return salt;
        }

        private List<Asset> Assets { get; set; }

    }
}