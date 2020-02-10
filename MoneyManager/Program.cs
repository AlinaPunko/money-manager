using System;
using DataAccess.Core;
using DataAccess.Repositories;

namespace MoneyManager
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ApplicationContext context = new ApplicationContextFactory().Create())
            {
                TransactionRepository repository = new TransactionRepository(context);
                var e = repository.GetAll();
                foreach (var user in e)
                {
                    Console.WriteLine(user);
                }
            }
        }
    }
}
