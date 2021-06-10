using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Modules.Basket.Domain.Models.Order;
using FoodOrdering.Modules.Basket.Domain.Repositories;

namespace FoodOrdering.Modules.Basket.Infrastructure.Repositories.InMemory
{
	public class InMemoryOrderDescriptionsRepository : IOrderDescriptionsRepository
	{
		private readonly List<OrderDescription> orderDescriptions = new();

		public OrderDescription GetById(Guid orderId) 
			=> orderDescriptions.SingleOrDefault(od => od.Id == orderId);

		public void Save(OrderDescription description)
		{
			orderDescriptions.Add(description);
		}
	}
}
