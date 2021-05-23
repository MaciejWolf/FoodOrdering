using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Common;
using FoodOrdering.Modules.Basket.Domain.Models.Order;
using FoodOrdering.Modules.Basket.Domain.Models.ValueObjects;
using FoodOrdering.Modules.Basket.Domain.Repositories;

namespace FoodOrdering.Modules.Basket.Infrastructure.Repositories.InMemory
{
	public class InMemoryOrdersRepository : IOrdersRepository
	{
		private readonly List<OrderAggregate> orders = new();

		public OrderAggregate GetById(OrderId id)
		{
			return orders.SingleOrDefault(o => o.Id == id);
		}

		public void Save(OrderAggregate order)
		{
			if (orders.Any(o => o.Id == order.Id))
				throw new AppException("Order already exists");

			orders.Add(order);
		}
	}
}
