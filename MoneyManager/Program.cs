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
                UsersRepository repository = new UsersRepository(context);
                var e = repository.GetUserWithBalance(
                    new Guid("E9E89E32-5DDA-4094-8392-13DDC797F2EF"));
                Console.WriteLine(e);
            }
        }
    }
}
