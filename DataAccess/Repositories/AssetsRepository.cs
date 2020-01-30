using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Core;
using DataAccess.Models;
using DataAccess.Projections;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class AssetsRepository:GenericRepository<Asset>
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
            var assets = Get(asset => asset.UserId == userId);
            var transactionRepository = new TransactionRepository(context);
            var assetBalanceInfoList = new List<AssetBalanceInfo>();
            foreach (var asset in assets)
            {
                assetBalanceInfoList.Add(new AssetBalanceInfo(asset.Id, asset.Name, transactionRepository.GetBalanceOfTheAsset(asset.Id)));
            }
            return assetBalanceInfoList;
        }

        public AssetsRepository(DbContext context) : base(context)
        {

        }
    }
}