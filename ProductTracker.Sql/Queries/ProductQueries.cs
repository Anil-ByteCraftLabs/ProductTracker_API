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
		public static string AllProduct => "SELECT * FROM [ProductDetail] (NOLOCK)";

		public static string ProductById => "SELECT * FROM [ProductDetail] (NOLOCK) WHERE [Id] = @ProductId";

		public static string AddProduct =>
			@"INSERT INTO [Contact] ([FirstName], [LastName], [Email], [PhoneNumber]) 
				VALUES (@FirstName, @LastName, @Email, @PhoneNumber)";

		public static string UpdateProduct =>
			@"UPDATE [Contact] 
            SET [FirstName] = @FirstName, 
				[LastName] = @LastName, 
				[Email] = @Email, 
				[PhoneNumber] = @PhoneNumber
            WHERE [ContactId] = @ContactId";

		public static string DeleteProduct => "DELETE FROM [Product] WHERE [Id] = @ProductId";

	}
}
