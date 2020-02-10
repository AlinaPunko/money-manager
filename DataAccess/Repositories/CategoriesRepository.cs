using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Core;
using DataAccess.Models;
using DataAccess.Projections;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class CategoriesRepository : GenericRepository<Category>
    {
        public CategoriesRepository(DbContext context) : base(context) { }

        public IReadOnlyList<Category> GetAll()
        {
            return Get()
                .ToList();
        }

        public int GetNumberOfParents(Category category,int amount)
        {
            int numberOfParents=amount;
            if (category.ParentId == null)
            {
                return numberOfParents;
            }

            numberOfParents++;
            return GetNumberOfParents(category.Parent, numberOfParents);
        }

        public List<OperationTypeInfo> GetAmountOfParentCategories(int operationType, Guid userId)
        {
            IQueryable<Category> categories = Get(c => c.Transactions.Any(t => t.Asset.UserId == userId && t.Date.Month == DateTime.Now.Month));
            List<OperationTypeInfo> operationTypeInfoList = new List<OperationTypeInfo>();

            foreach (Category category in categories)
            {
                OperationTypeInfo operationTypeInfo = new OperationTypeInfo
                {
                    Name = category.Name,
                    Amount = GetNumberOfParents(category, 0)
                };
                operationTypeInfoList.Add(operationTypeInfo);
            }

            return operationTypeInfoList;
        }
    }
}