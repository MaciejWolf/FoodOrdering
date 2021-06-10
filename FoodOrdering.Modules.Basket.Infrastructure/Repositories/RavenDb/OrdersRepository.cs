using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Modules.Basket.Domain.Models.Order;
using FoodOrdering.Modules.Basket.Domain.Models.ValueObjects;
using FoodOrdering.Modules.Basket.Domain.Repositories;

namespace FoodOrdering.Modules.Basket.Infrastructure.Repositories.RavenDb
{
	class OrdersRepository : IOrdersRepository
	{
		private readonly BasketDocumentStore documentStore;

		public OrdersRepository(BasketDocumentStore documentStore)
		{
			this.documentStore = documentStore;
		}

		public OrderAggregate GetById(OrderId id)
		{
			using var session = documentStore.OpenSession();
			return session.Query<OrderAggregate>().Single(m => m.Id == id);
		}

		public void Save(OrderAggregate order)
		{
			using var session = documentStore.OpenSession();
			session.Store(order);
			session.SaveChanges();
		}

		public void Update(OrderId orderId, Action<OrderAggregate> updateOperation)
		{
			using var session = documentStore.OpenSession();
			var order = session.Query<OrderAggregate>().Single(m => m.Id == orderId);
			updateOperation(order);
			session.SaveChanges();
		}
	}
}
