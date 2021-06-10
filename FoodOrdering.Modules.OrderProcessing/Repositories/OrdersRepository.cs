using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Modules.OrderProcessing.Entities;

namespace FoodOrdering.Modules.OrderProcessing.Repositories
{
	public class OrdersRepository : IOrdersRepository
	{
		private readonly OrderProcessingDocumentStore documentStore;

		public OrdersRepository(OrderProcessingDocumentStore documentStore)
		{
			this.documentStore = documentStore;
		}

		public IEnumerable<Order> GetAll()
		{
			using var session = documentStore.OpenSession();
			return session.Query<Order>().ToArray();
		}

		public Order GetById(Guid orderId)
		{
			using var session = documentStore.OpenSession();
			return session.Query<Order>().Single(o => o.Id == orderId);
		}

		public void Save(Order order)
		{
			using var session = documentStore.OpenSession();
			session.Store(order);
			session.SaveChanges();
		}

		public void Update(Guid orderId, Action<Order> updateOperation)
		{
			using var session = documentStore.OpenSession();
			var order = session.Query<Order>().Single(o => o.Id == orderId);
			updateOperation(order);
			session.SaveChanges();
		}
	}
}
