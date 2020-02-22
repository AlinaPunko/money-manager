using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
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

        public List<OperationTypeInfo> GetAmountOfParentCategories(CategoryType categoryType, Guid userId)
        {
            IQueryable<Category> categories = Get(c => c.Transactions.Any(t => t.Asset.UserId == userId && t.Date.Month == DateTime.Now.Month))
                .Where(c => c.Type== categoryType);
            List<OperationTypeInfo> operationTypeInfoList = new List<OperationTypeInfo>();

            IMapper mapper = MapperWrapper.GetMapper();

            foreach (Category category in categories)
            {
                OperationTypeInfo operationTypeInfo = mapper.Map<OperationTypeInfo>(category);
                operationTypeInfoList.Add(operationTypeInfo);
            }

            return operationTypeInfoList;
        }
    }
}