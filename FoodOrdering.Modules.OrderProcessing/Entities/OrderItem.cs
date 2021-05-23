using System;

namespace FoodOrdering.Modules.OrderProcessing.Entities
{
	public class OrderItem
	{
		public Guid Id { get; set; }
		public int Quantity { get; set; }
	}
}
