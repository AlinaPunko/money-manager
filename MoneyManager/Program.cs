using DataAccess.Core;

namespace MoneyManager
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ApplicationContext context = new ApplicationContextFactory().Create())
            {

            }
        }
    }
}
