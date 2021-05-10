using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Modules.Basket.Entities;

namespace FoodOrdering.Modules.Basket.Repositories
{
	interface IProductsRepository
	{
		Task<Product> GetById(Guid id);
		void Update(Product product);
	}
}
