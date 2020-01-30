using System;
using System.Collections.Generic;
using DataAccess.GenericRepository;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class AssetsRepository:GenericRepository<Asset>
    {
        public void AddAsset(Asset asset)
        {
            Create(asset);
        }

        public IReadOnlyList<Asset> GetAssets()
        {
            return Get();
        }

        public IReadOnlyList<Asset> GetAssetsByQuery(Func<Asset, bool> predicate)
        {
            return Get(predicate);
        }

        public Asset GetAssetById(Guid id)
        {
            return FindById(id);
        }

        public void DeleteAsset(Asset asset)
        {
            Remove(asset);
        }

        public void UpdateAsset(Asset asset)
        {
            Update(asset);
        }

        public AssetsRepository(DbContext context) : base(context)
        {

        }
    }
}