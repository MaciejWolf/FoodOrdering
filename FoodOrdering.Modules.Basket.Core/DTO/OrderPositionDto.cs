using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrdering.Modules.Basket.Application.DTO
{
	public class OrderPositionDto
	{
		public Guid MealId { get; set; }
		public int Quantity { get; set; }
	}
}
