using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrdering.Modules.OrderProcessing.Contracts.DTO
{
	public class OrderDTO
	{
		public Guid Id { get; set; }
		public IEnumerable<OrderItemDTO> OrderItems { get; set; }
	}
}
