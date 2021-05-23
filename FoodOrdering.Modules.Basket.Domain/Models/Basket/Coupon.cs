using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Common;
using FoodOrdering.Modules.Basket.Domain.ValueObjects;

namespace FoodOrdering.Modules.Basket.Domain.Basket
{
	public class Coupon : Entity<CouponId>
	{
		public Coupon(CouponId id, Price value) : base(id)
		{
			Value = value;
		}

		public Price Value { get; }
	}
}
