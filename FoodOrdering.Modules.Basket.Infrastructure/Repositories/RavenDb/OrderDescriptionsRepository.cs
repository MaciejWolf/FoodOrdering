using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Modules.Basket.Domain.Models.Order;
using FoodOrdering.Modules.Basket.Domain.Repositories;
using Raven.Client.Documents.Session;

namespace FoodOrdering.Modules.Basket.Infrastructure.Repositories.RavenDb
{
	class OrderDescriptionsRepository : IOrderDescriptionsRepository
	{
		private readonly BasketDocumentStore documentStore;

		public OrderDescriptionsRepository(BasketDocumentStore documentStore)
		{
			this.documentStore = documentStore;
		}

		public OrderDescription GetById(Guid orderId)
		{
			using var session = documentStore.OpenSession();
			return session.Query<OrderDescription>().Single(m => m.Id == orderId);
		}

		public void Save(OrderDescription description)
		{
			using var session = documentStore.OpenSession();
			session.Store(description);
			session.SaveChanges();
		}
	}
}
