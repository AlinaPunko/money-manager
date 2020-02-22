using Microsoft.EntityFrameworkCore;

namespace DataAccess.Core
{
    public class ApplicationContextFactory : DesignTimeDbContextFactoryBase<ApplicationContext>
    {
        private ApplicationContext context;

        protected override ApplicationContext CreateNewInstance(DbContextOptions<ApplicationContext> options)
        {
            return context ?? (context = new ApplicationContext(options));
        }
    }
}