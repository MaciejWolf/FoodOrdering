using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Common;
using FoodOrdering.Modules.OrderProcessing.Entities;

namespace FoodOrdering.Modules.OrderProcessing.Repositories
{
	public class InMemoryOrdersRepository : IOrdersRepository
	{
		private readonly List<Order> orders = new();

		public IEnumerable<Order> GetAll() => orders;

		public Order GetById(Guid orderId)
		{
			return orders.SingleOrDefault(o => o.Id == orderId);
		}

		public void Save(Order order)
		{
			if (GetById(order.Id) is not null)
				throw new AppException("Order exists");

			orders.Add(order);
		}

		public void Update(Order order)
		{
			var existingOrder = GetById(order.Id) ?? throw new AppException("Order doesnt exist");

			orders.Remove(existingOrder);
			orders.Add(order);
		}

		public void Update(Guid orderId, Action<Order> updateOperation)
		{
			var order = GetById(orderId) ?? throw new AppException("Order doesnt exist");
			updateOperation(order);
		}
	}
}
