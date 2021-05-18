using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Common;
using FoodOrdering.Modules.Basket.Domain.ValueObjects;
using FoodOrdering.Modules.Basket.ValueObjects;

namespace FoodOrdering.Modules.Basket.Entities
{
	class Order // AggregateRoot
	{
		private readonly DateTime validTo = DateTime.Now.AddMinutes(5);

		public OrderId Id { get; set; }
		public ClientId ClientId { get; set; }
		public List<OrderItem> OrderItems { get; set; } = new();
		public List<Guid> UsedCoupons { get; set; } = new();

		public Order()
		{

		}

		public Order(ClientId clientId, DateTime createdAt)
		{
			ClientId = clientId;
			validTo = createdAt.AddMinutes(5);
		}

		public void PlaceOrder(DateTime acceptanceTime)
		{
			if (acceptanceTime > validTo)
			{
				throw new AppException("Order expired");
			}
		}
	}

	class OrderItem
	{
		public ProductId ProductId { get; set; }
		public Price BasePrice { get; set; }
		public Price Price { get; set; }
		public Quantity Quantity { get; set; }
	}
}
