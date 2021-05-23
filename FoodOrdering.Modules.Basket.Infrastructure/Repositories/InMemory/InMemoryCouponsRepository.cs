using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Common;
using FoodOrdering.Modules.Basket.Domain.Models.Coupons;
using FoodOrdering.Modules.Basket.Domain.Repositories;
using FoodOrdering.Modules.Basket.Domain.ValueObjects;

namespace FoodOrdering.Modules.Basket.Infrastructure.Repositories.InMemory
{
	public class InMemoryCouponsRepository : ICouponsRepository
	{
		private readonly List<Coupon> coupons = new();

		public Coupon GetById(CouponId couponId)
		{
			return coupons.SingleOrDefault(c => c.Id == couponId);
		}

		public void Save(Coupon coupon)
		{
			if (GetById(coupon.Id) != null)
			{
				throw new AppException("Coupon already exists");
			}

			coupons.Add(coupon);
		}

		public void Update(Coupon coupon)
		{
			var existingCoupon = GetById(coupon.Id) ?? throw new AppException("Coupon doesn't exists");

			coupons.Remove(existingCoupon);
			coupons.Add(coupon);
		}
	}
}
