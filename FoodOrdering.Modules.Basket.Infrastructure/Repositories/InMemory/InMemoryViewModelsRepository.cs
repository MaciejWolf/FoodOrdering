using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Modules.Basket.Application;
using FoodOrdering.Modules.Basket.Application.ViewModels;

namespace FoodOrdering.Modules.Basket.Infrastructure.Repositories.InMemory
{
	public class InMemoryViewModelsRepository : IViewModelsRepository
	{
		private readonly List<BasketVm> baskets = new();
		private readonly List<CouponVm> coupons= new();

		public BasketVm Get(Guid clientId) => baskets.Single(b => b.Id == clientId);

		public CouponVm GetCoupon(Guid couponId) => coupons.Single(c => c.Id == couponId);

		public IEnumerable<CouponVm> GetCouponsForClient(Guid clientId) => coupons.Where(c => c.ClientId == clientId);

		public void RemoveCoupon(Guid couponId)
		{
			var c = GetCoupon(couponId);
			coupons.Remove(c);
		}

		public void Save(BasketVm basket)
		{
			baskets.Add(basket);
		}

		public void Save(CouponVm coupon)
		{
			coupons.Add(coupon);
		}

		public void UpdateBasket(Guid id, Action<BasketVm> updateOperation)
		{
			var basket = Get(id);
			updateOperation(basket);
		}

		public void Update(Guid id, Action<CouponVm> updateOperation)
		{
			var coupon = GetCoupon(id);
			updateOperation(coupon);
		}
	}
}
