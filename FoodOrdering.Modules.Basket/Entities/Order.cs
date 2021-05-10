using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrdering.Modules.Basket.Entities
{
	class Order
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public List<OrderItem> OrderItems { get; set; }
	}

	class OrderItem
	{
		public Guid Id { get; set; }
		public decimal BasePrice { get; set; }
		public decimal Price { get; set; }
	}
}
