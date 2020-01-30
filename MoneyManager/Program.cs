using System;
using System.Collections.Generic;
using DataAccess.Core;
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

                var usersRepository = new UsersRepository(context);
                usersRepository.GetUserWithBalance(new Guid("7EFC54B1-3B1B-4CAA-AB0D-1C2E21D8E2CB"));
                //users.GetUserWithBalance();
                //var repository = new TransactionRepository(context);
                //repository.DeleteTransactionsOfUserInCurrentMonth(new Guid("3039D05D-57D2-4339-A3FB-1D0AD8A8E4C3"));
                //var assetRepository = new AssetsRepository(context);
                //assetRepository.GetAssetBalance(new Guid("3039D05D-57D2-4339-A3FB-1D0AD8A8E4C3"));

            }
        }
    }
}
