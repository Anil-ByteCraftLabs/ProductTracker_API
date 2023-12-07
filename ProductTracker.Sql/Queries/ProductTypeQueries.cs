using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracker.Sql.Queries
{
    public static class ProductTypeQueries
    {
        public static string AllProductType => "SELECT * FROM [ProductType] (NOLOCK)";

        public static string ProductTypeById => "SELECT * FROM [ProductType] (NOLOCK) WHERE [Id] = @TypeIdId";

        public static string SaveProductType => "usp_SaveProductType";



        public static string DeleteProductType => "DELETE FROM [ProductType] WHERE [Id] = @TypeIdId";

    }
}
