using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Modules.Basket.Core.Entities;

namespace FoodOrdering.Modules.Basket.Core.Repositories
{
	public interface IMealsRepository
	{
		void Save(Meal meal);
		bool Exists(Guid itemId);
	}
}
