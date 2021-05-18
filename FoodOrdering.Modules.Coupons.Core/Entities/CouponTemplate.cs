using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Common.Time;
using FoodOrdering.Modules.Coupons.Core.Entities;

namespace FoodOrdering.Modules.Coupons.Entities
{
	class CouponTemplate
	{
		public Guid MealId { get; set; }
		public int PercentageDiscount { get; set; }
		public int ValidForDays { get; set; }

		public Coupon GenerateCoupon(Guid id, Guid userId, DateTime generatedAt)
		{
			return new Coupon
			{
				Id = id,
				UserId = userId,
				MealId = MealId,
				DiscountInPercentage = PercentageDiscount,
				ValidTo = generatedAt.AddDays(ValidForDays)
			};
		}
	}
}
