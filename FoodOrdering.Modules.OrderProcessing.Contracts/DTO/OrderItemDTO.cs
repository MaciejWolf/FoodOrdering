using System;

namespace FoodOrdering.Modules.OrderProcessing.Contracts.DTO
{
	public class OrderItemDTO
	{
		public Guid Id { get; set; }
		public int Quantity { get; set; }
	}
}
