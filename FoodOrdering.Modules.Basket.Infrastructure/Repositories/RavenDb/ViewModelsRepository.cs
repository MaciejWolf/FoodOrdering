using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Modules.Basket.Application;
using FoodOrdering.Modules.Basket.Application.ViewModels;

namespace FoodOrdering.Modules.Basket.Infrastructure.Repositories.RavenDb
{
	class ViewModelsRepository : IViewModelsRepository
	{
		private readonly BasketDocumentStore documentStore;

		public ViewModelsRepository(BasketDocumentStore documentStore)
		{
			this.documentStore = documentStore;
		}

		public BasketVm Get(Guid clientId)
		{
			using var session = documentStore.OpenSession();
			return session.Query<BasketVm>().Single(m => m.Id == clientId);
		}

		public CouponVm GetCoupon(Guid couponId)
		{
			using var session = documentStore.OpenSession();
			return session.Query<CouponVm>().Single(m => m.Id == couponId);
		}

		public IEnumerable<CouponVm> GetCouponsForClient(Guid clientId)
		{
			using var session = documentStore.OpenSession();
			return session.Query<CouponVm>().Where(m => m.ClientId == clientId).ToArray();
		}

		public void RemoveCoupon(Guid couponId)
		{
			using var session = documentStore.OpenSession();
			var coupon = session.Query<CouponVm>().Single(m => m.Id == couponId);
			session.Delete(coupon);
			session.SaveChanges();
		}

		public void Save(BasketVm basket)
		{
			using var session = documentStore.OpenSession();
			session.Store(basket);
			session.SaveChanges();
		}

		public void Save(CouponVm coupon)
		{
			using var session = documentStore.OpenSession();
			session.Store(coupon);
			session.SaveChanges();
		}

		public void UpdateBasket(Guid id, Action<BasketVm> updateOperation)
		{
			using var session = documentStore.OpenSession();
			var basket = session.Query<BasketVm>().Single(m => m.Id == id);
			updateOperation(basket);
			session.SaveChanges();
		}

		public void Update(Guid id, Action<CouponVm> updateOperation)
		{
			using var session = documentStore.OpenSession();
			var coupon = session.Query<CouponVm>().Single(m => m.Id == id);
			updateOperation(coupon);
			session.SaveChanges();
		}
	}
}
