using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracker.Sql.Queries
{
    [ExcludeFromCodeCoverage]
    public static class ProductQueries
    {
		public static string AllProduct => "SELECT * FROM [ProductData] (NOLOCK)";

		public static string ProductById => "SELECT * FROM [ProductData] (NOLOCK) WHERE [Id] = @ProductId";

		public static string SaveProduct => "usp_SaveProduct";
			


		public static string DeleteProduct => "DELETE FROM [ProductData] WHERE [Id] = @ProductId";

	}
}
