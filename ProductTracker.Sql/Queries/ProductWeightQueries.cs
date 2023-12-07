using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracker.Sql.Queries
{
    public class ProductWeightQueries
    {
        public static string AllProductWeight => "SELECT * FROM [ProductWeight] (NOLOCK)";

        public static string ProductWeightById => "SELECT * FROM [ProductWeight] (NOLOCK) WHERE [Id] = @Id";

        public static string SaveProductWeight => "usp_SaveProductWeight";



        public static string DeleteProductWeight => "DELETE FROM [ProductWeight] WHERE [Id] = @Id";

    }
}
