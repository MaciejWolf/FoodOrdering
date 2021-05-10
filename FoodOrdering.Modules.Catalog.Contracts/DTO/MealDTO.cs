using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrdering.Modules.Catalog.Contracts.DTO
{
	public record MealDTO(Guid Id, string Name, decimal Price);
}
