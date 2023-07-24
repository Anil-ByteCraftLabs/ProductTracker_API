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

		public static string SaveCoupon => "usp_SaveCoupon";
			

		
		public static string DeleteCoupon => "DELETE FROM [CouponsData] WHERE [Id] = @CouponId";
	}
}
