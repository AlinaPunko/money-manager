using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Core;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class CategoriesRepository:GenericRepository<Category>
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

        public CategoriesRepository(DbContext context) : base(context)
        {

        }
    }
}