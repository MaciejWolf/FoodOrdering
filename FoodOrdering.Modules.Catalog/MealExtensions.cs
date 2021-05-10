using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Modules.Catalog.Contracts.DTO;
using FoodOrdering.Modules.Catalog.Models;

namespace FoodOrdering.Modules.Catalog
{
	static class MealExtensions
	{
		public static MealDTO ToDTO(this Meal meal)
		{
			return new MealDTO(meal.Id, meal.Name, meal.Price);
		}
	}
}
