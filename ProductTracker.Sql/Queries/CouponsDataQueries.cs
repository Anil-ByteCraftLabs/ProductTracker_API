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
		public static string BatchAllCoupons => "usp_GetBatchCoupons";
		public static string CouponById => "usp_GetCoupon";

		public static string SaveCoupon => "usp_SaveCoupon";

		public static string GenerateCoupons => "usp_GenerateCoupons";
		public static string UpdateCoupons => "usp_UpdateCoupons";



		public static string DeleteCoupon => "DELETE FROM [CouponsData] WHERE [Id] = @CouponId";
	}
}
