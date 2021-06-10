using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Modules.Basket.Application.ViewModels;
using FoodOrdering.Modules.Basket.Domain.ValueObjects;

namespace FoodOrdering.Modules.Basket.Application
{
	public interface IViewModelsRepository
	{
		void Save(BasketVm basket);
		BasketVm Get(Guid clientId);
		void UpdateBasket(Guid id, Action<BasketVm> updateOperation);
		void Save(CouponVm coupon);
		CouponVm GetCoupon(Guid couponId);
		IEnumerable<CouponVm> GetCouponsForClient(Guid clientId);
		void RemoveCoupon(Guid couponId);
		void Update(Guid id, Action<CouponVm> updateOperation);
	}
}
