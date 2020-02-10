using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Models;

namespace DataAccess.Helpers
{
    public static class ParentCategoriesHelper
    {
        public static int GetNumberOfParents(Category category)
        {
            int numberOfParents = 0;

            while (true)
            {
                if (category.ParentId == null)
                {
                    return numberOfParents;
                }
                numberOfParents++;
                category = category.Parent;
            }
        }
    }
}
