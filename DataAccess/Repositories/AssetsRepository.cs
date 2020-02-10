using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DataAccess.Core;
using DataAccess.Helpers;
using DataAccess.MapperProfiles;
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

            MapperConfiguration config = new MapperConfiguration(cfg => cfg.AddProfile<AssetBalanceInfoProfile>());
            IMapper mapper = config.CreateMapper();

            return assets.Select(asset => mapper.Map<AssetBalanceInfo>(asset)).ToList();
        }
    }
}