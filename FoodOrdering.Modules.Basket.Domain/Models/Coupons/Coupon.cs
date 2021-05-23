using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Modules.Basket.Domain.ValueObjects;

namespace FoodOrdering.Modules.Basket.Domain.Models.Coupons
{
	public class Coupon
	{
		public CouponId Id { get; set; }
		public ClientId OwnerId { get; set; }
		public bool IsUsed { get; set; }
		public Price Value { get; set; }
	}
}
