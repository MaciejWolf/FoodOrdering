using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Modules.Basket.Entities;
using FoodOrdering.Modules.Basket.Repositories;

namespace FoodOrdering.Modules.Basket.Infrastructure.Repositories
{
	class InMemoryProductsRepository : IProductsRepository
	{
		public Task<Product> GetById(Guid id)
		{
			throw new NotImplementedException();
		}

		public void Update(Product product)
		{
			throw new NotImplementedException();
		}
	}
}
