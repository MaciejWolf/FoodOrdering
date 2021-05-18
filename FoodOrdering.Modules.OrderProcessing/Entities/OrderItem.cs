using System;

namespace FoodOrdering.Modules.OrderProcessing.Entities
{
	class OrderItem
	{
		public Guid Id { get; set; }
		public int Quantity { get; set; }
	}
}
