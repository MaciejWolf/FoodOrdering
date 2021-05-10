using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrdering.Modules.Basket.Messages.DTO
{
	public class OrderPositionDTO
	{
		public Guid MealId { get; set; }
		public int Quantity { get; set; }
	}
}
