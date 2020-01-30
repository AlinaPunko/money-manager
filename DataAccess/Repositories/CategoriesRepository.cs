using System;
using System.Collections.Generic;
using DataAccess.GenericRepository;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class CategoriesRepository:GenericRepository<Category>
    {
        public void AddCategory(Category category)
        {
            Create(category);
        }

        public IReadOnlyList<Category> GetCategories()
        {
            return Get();
        }

        public Category GetCategoryById(Guid id)
        {
            return FindById(id);
        }

        public void DeleteCategory(Category category)
        {
            Remove(category);
        }

        public void UpdateCategory(Category category)
        {
            Update(category);
        }

        public IReadOnlyList<Category> GetCategoryByQuery(Func<Category, bool> predicate)
        {
            return Get(predicate);
        }

        public CategoriesRepository(DbContext context) : base(context)
        {

        }
    }
}