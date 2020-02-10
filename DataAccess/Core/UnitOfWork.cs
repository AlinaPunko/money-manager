using System;
using DataAccess.Repositories;

namespace DataAccess.Core
{
    class UnitOfWork : IDisposable
    {
        private readonly ApplicationContext context = new ApplicationContextFactory().Create();
        private bool disposed;

        public void Save()
        {
            context.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                context.Dispose();
            }

            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
