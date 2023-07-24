using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracker.Sql.Queries
{
    [ExcludeFromCodeCoverage]
    public class CouponsDataQueries
    {
		public static string AllCoupons => "SELECT * FROM [CouponsData] (NOLOCK)";

		public static string CouponById => "SELECT * FROM [CouponsData] (NOLOCK) WHERE [Id] = @CouponId";

		public static string AddCoupon =>
			@"INSERT INTO [Contact] ([FirstName], [LastName], [Email], [PhoneNumber]) 
				VALUES (@FirstName, @LastName, @Email, @PhoneNumber)";

		public static string UpdateCoupon =>
			@"UPDATE [Contact] 
            SET [FirstName] = @FirstName, 
				[LastName] = @LastName, 
				[Email] = @Email, 
				[PhoneNumber] = @PhoneNumber
            WHERE [ContactId] = @ContactId";

		public static string DeleteCoupon => "DELETE FROM [Contact] WHERE [Id] = @OrgId";
	}
}
