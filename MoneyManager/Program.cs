using System;
using DataAccess.Core;
using DataAccess.Models;
using DataAccess.Repositories;

namespace MoneyManager
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ApplicationContext context = new ApplicationContextFactory().Create())
            {
                UsersRepository repository = new UsersRepository(context);
                var e = repository.GetAll();
                foreach (User user in e)
                {
                    Console.WriteLine(user);
                }
            }
        }
    }
}
