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

        public IReadOnlyList<Category> GetAll()
        {
            return Get()
                .ToList();
        }

        public new Category GetById(Guid id)
        {
            return base.GetById(id);
        }

        public int GetNumberOfParents(Category category,int amount)
        {
            int numberOfParents=amount;
            if (category.ParentId != null)
            {
                numberOfParents++;
                return GetNumberOfParents(category.Parent, numberOfParents);
            }

            return numberOfParents;
        }

        public List<OperationTypeInfo> GetAmountOfParentCategories(int operationType, Guid userId)
        {
            var categories = Get(c => c.Transactions.Any(t => t.Asset.UserId == userId && t.Date.Month == DateTime.Now.Month));
            List<OperationTypeInfo> operationTypeInfoList = new List<OperationTypeInfo>();
            foreach (var category in categories)
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

        public CategoriesRepository(DbContext context) : base(context)
        {

        }
    }
}