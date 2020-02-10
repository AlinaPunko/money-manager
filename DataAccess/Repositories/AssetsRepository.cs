using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DataAccess.Core;
using DataAccess.Helpers;
using DataAccess.Models;
using DataAccess.Projections;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace DataAccess.Repositories
{
    public class AssetsRepository : GenericRepository<Asset>
    {
        public AssetsRepository(DbContext context) : base(context) { }

        public IReadOnlyList<Asset> GetAll()
        {
            return Get()
                .ToList();
        }

        public List<AssetBalanceInfo> GetAssetBalance(Guid userId)
        {
            IIncludableQueryable<Asset, ICollection<Transaction>> assets = Get(a => a.UserId == userId)
                .Include(a => a.Transactions);
            List<AssetBalanceInfo> assetBalanceInfoList = new List<AssetBalanceInfo>();

            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Asset, AssetBalanceInfo>();
            });
            IMapper mapper = config.CreateMapper();

            foreach (var asset in assets)
            {
                List<Transaction> transactions = asset.Transactions.ToList();
                double balance = BalanceHelper.GetBalance(transactions);
                AssetBalanceInfo assetBalanceInfo = mapper.Map<Asset, AssetBalanceInfo>(asset);
                assetBalanceInfo.Balance = balance;
                assetBalanceInfoList.Add(assetBalanceInfo);
            }
            return assetBalanceInfoList;
        }
    }
}