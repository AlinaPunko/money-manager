using Microsoft.EntityFrameworkCore;

namespace DataAccess.Context
{
    public class ApplicationContextFactory :
        DesignTimeDbContextFactoryBase<ApplicationContext>
    {
        protected override ApplicationContext CreateNewInstance(
            DbContextOptions<ApplicationContext> options)
        {
            return new ApplicationContext(options);
        }
    }
}