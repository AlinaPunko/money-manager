using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DataAccess.Core;
using DataAccess.Helpers;
using DataAccess.Models;
using DataAccess.Projections;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class AssetsRepository : GenericRepository<Asset>
    {
        public IReadOnlyList<Asset> GetAll()
        {
            return Get()
                .ToList();
        }

        public new Asset GetById(Guid id)
        {
            return base.GetById(id);
        }


        public List<AssetBalanceInfo> GetAssetBalance(Guid userId)
        {
            var assets = Get(a => a.UserId == userId)
                .Include(a => a.Transactions);
            var assetBalanceInfoList = new List<AssetBalanceInfo>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Asset, AssetBalanceInfo>();
            });
            var mapper = config.CreateMapper();
            foreach (var asset in assets)
            {
                var transactions = asset.Transactions.ToList();
                var balance = BalanceHelper.GetBalance(transactions);
                var assetBalanceInfo = mapper.Map<Asset, AssetBalanceInfo>(asset);
                assetBalanceInfo.Balance = balance;
                assetBalanceInfoList.Add(assetBalanceInfo);
            }
            return assetBalanceInfoList;
        }

        public AssetsRepository(DbContext context) : base(context)
        {

        }
    }
}