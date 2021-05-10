using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Modules.Catalog.Models;

namespace FoodOrdering.Modules.Catalog.Repositories
{
	interface IMealsRepository
	{
		Task AddAsync(Meal meal);
		Task<Meal> GetById(Guid id);
		Task<Meal[]> GetAll();
		void Update(Meal meal);
	}
}
