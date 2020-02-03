using System;
using DataAccess.Core;
using DataAccess.Repositories;

namespace MoneyManager
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new ApplicationContextFactory().Create())
            {

            }
        }
    }
}
