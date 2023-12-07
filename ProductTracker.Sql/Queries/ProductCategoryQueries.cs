using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracker.Sql.Queries
{
    public class ProductCategoryQueries
    {
        public static string AllProductCategory => "usp_GetProductCategory";

        public static string ProductCategoryById => "SELECT * FROM [ProductCategory] (NOLOCK) WHERE [Id] = @Id";

        public static string SaveProductCategory => "usp_SaveProductCategory";



        public static string DeleteProductCategory => "DELETE FROM [ProductCategory] WHERE [Id] = @Id";

    }
}
