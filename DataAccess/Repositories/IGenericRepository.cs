using System;
using System.Collections.Generic;

namespace DataAccess.Repositories
{
    interface IGenericRepository<TEntity> where TEntity : class
    {
        void Create(TEntity item);
        TEntity FindById(Guid id);
        IReadOnlyList<TEntity> Get();
        IReadOnlyList<TEntity> Get(Func<TEntity, bool> predicate);
        void Remove(TEntity item);
        void Update(TEntity item);
    }
}
