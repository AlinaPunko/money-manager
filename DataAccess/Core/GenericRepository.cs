using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Core
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected DbContext Context;
        protected DbSet<TEntity> DbSet;

        public GenericRepository(DbContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        protected IQueryable<TEntity> Get(Func<TEntity, bool> predicate=null)
        {
            return predicate == null ? DbSet.AsQueryable() : DbSet.Where(predicate).AsQueryable();
        }

        public TEntity GetById(Guid id)
        {
            return DbSet.Find(id);
        }

        public void Add(TEntity item)
        {
            DbSet.Add(item);
        }

        public void Update(TEntity item)
        {
            Context.Entry(item).State = EntityState.Modified;
        }

        public void Remove(TEntity item)
        {
            DbSet.Remove(item);
        }
    }
}