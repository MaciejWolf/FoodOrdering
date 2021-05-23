using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Modules.Basket.Domain.Models.Coupons;
using FoodOrdering.Modules.Basket.Domain.ValueObjects;

namespace FoodOrdering.Modules.Basket.Domain.Repositories
{
	public interface ICouponsRepository
	{
		void Save(Coupon coupon);
		Coupon GetById(CouponId couponId);
		void Update(Coupon coupon);
	}
}
