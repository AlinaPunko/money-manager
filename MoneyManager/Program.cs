using System;
using System.Collections.Generic;
using DataAccess.Context;
using DataAccess.Models;
using DataAccess.Repositories;

namespace MoneyManager
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new ApplicationContextFactory().Create())
            {
                User user = new User("qwerty", "qwerty", "qwerty");
                var usersRepository = new UsersRepository(context);
                usersRepository.AddUser(user);
                Console.WriteLine("Hello");
                var users = usersRepository.GetUsers();
                foreach (var item in users)
                {
                    Console.WriteLine(item);
                }
            }
        }
    }
}
