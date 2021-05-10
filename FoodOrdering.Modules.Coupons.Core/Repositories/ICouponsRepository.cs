using System;
using FoodOrdering.Modules.Coupons.Core.Entities;

namespace FoodOrdering.Modules.Coupons.Core.EventHandlers
{
	internal interface ICouponsRepository
	{
		Coupon GetById(Guid id);
		void Update(Coupon coupon);
	}
}
