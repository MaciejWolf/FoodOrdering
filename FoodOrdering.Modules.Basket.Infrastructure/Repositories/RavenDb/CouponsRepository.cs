using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Modules.Basket.Domain.Models;
using FoodOrdering.Modules.Basket.Domain.Repositories;
using FoodOrdering.Modules.Basket.Domain.ValueObjects;

namespace FoodOrdering.Modules.Basket.Infrastructure.Repositories.RavenDb
{
	public class CouponsRepository : ICouponsRepository
	{
		private readonly BasketDocumentStore documentStore;

		public CouponsRepository(BasketDocumentStore documentStore)
		{
			this.documentStore = documentStore;
		}

		public Coupon GetById(CouponId couponId)
		{
			using var session = documentStore.OpenSession();
			return session
				.Query<Coupon>()
				//.Customize(x => x.WaitForNonStaleResults(TimeSpan.FromSeconds(5)))
				.Single(m => m.Id == couponId);
		}

		public void Save(Coupon coupon)
		{
			using var session = documentStore.OpenSession();
			session.Store(coupon);
			session.SaveChanges();
			//session.Advanced.WaitForIndexesAfterSaveChanges();
		}

		public void Update(CouponId couponId, Action<Coupon> updateOperation)
		{
			using var session = documentStore.OpenSession();
			var coupon = session.Query<Coupon>().Single(m => m.Id == couponId);
			updateOperation(coupon);
			session.SaveChanges();
		}
	}
}
