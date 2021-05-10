using System;

namespace FoodOrdering.Modules.Coupons.Core.Entities
{
	class Coupon
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public DateTime ValidTo { get; set; }
		public int DiscountInPercentage { get; set; }
		public bool IsUsed { get; set; }
	}
}
