using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodOrdering.Modules.Catalog.Models;

namespace FoodOrdering.Modules.Catalog.Repositories
{
	public class InMemoryMealsRepository : IMealsRepository
	{
		private readonly List<Meal> meals = new();

		public async Task AddAsync(Meal meal)
		{
			meals.Add(meal);
		}

		public async Task<Meal[]> GetAll()
		{
			return meals.ToArray();
		}

		public async Task<Meal> GetById(Guid id) => meals.SingleOrDefault(m => m.Id == id);

		public void Update(Meal meal)
		{
			throw new NotImplementedException();
		}
	}
}
