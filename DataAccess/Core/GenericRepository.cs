using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Core
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        public DbContext context;
        public DbSet<TEntity> dbSet;

        public GenericRepository(DbContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }

        protected IQueryable<TEntity> Get(Func<TEntity, bool> predicate=null)
        {
            if (predicate == null)
            {
                return dbSet.AsQueryable();
            }
            return dbSet.Where(predicate).AsQueryable();
        }

        public TEntity GetById(Guid id)
        {
            return dbSet.Find(id);
        }

        public void Add(TEntity item)
        {
            dbSet.Add(item);
            context.SaveChanges();
        }
        public void Update(TEntity item)
        {
            context.Entry(item).State = EntityState.Modified;
            context.SaveChanges();
        }
        public void Remove(TEntity item)
        {
            dbSet.Remove(item);
            context.SaveChanges();
        }
    }
}