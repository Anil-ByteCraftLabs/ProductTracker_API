using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracker.Sql.Queries
{
    [ExcludeFromCodeCoverage]
    public class ProductDataQueries
    {
		public static string AllProduct => "usp_GetProducts";

		public static string ProductById => "SELECT * FROM [ProductData] (NOLOCK) WHERE [Id] = @ProductId";

		public static string SaveProduct => "usp_SaveProduct";

		public static string UpdateProduct =>
			@"UPDATE [Contact] 
            SET [FirstName] = @FirstName, 
				[LastName] = @LastName, 
				[Email] = @Email, 
				[PhoneNumber] = @PhoneNumber
            WHERE [ContactId] = @ContactId";

		public static string DeleteProduct => "DELETE FROM [ProductData] WHERE [Id] = @ProductId";
	}
}
