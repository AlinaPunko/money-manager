using System;

namespace DataAccess.Core
{
    interface IGenericRepository<TEntity> where TEntity : class
    {
        void Add(TEntity item);
        TEntity GetById(Guid id);
        void Remove(TEntity item);
        void Update(TEntity item);
    }
}
