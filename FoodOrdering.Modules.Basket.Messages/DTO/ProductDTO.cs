using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrdering.Modules.Basket.Messages.DTO
{
	public record ProductDTO(ProductDTO MealId, int Quantity, decimal Price);
}
