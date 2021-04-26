using System;
using System.Collections.Generic;

namespace FoodOrdering.Modules.Catalog.Core.Entities
{
	class Offer
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public Guid RegionId { get; set; }
		public bool IsActive { get; set; }

		public List<Guid> Meals { get; set; } = new();

		public void AddMeal(Guid mealId)
		{
			Meals.Add(mealId);
		}
	}
}
