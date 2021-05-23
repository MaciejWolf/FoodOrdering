using System;
using System.Collections.Generic;

namespace FoodOrdering.Modules.OrderProcessing.Entities
{
	public class Order
	{
		public Guid Id { get; set; }
		public Guid ClientId { get; set; }
		public List<OrderItem> OrderItems { get; set; }
		public OrderStatus Status { get; set; }
		public decimal Price { get; set; }
	}
}
