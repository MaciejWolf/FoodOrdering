using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Modules.Basket.Entities;
using FoodOrdering.Modules.Basket.Repositories;

namespace FoodOrdering.Modules.Basket.Infrastructure.Repositories
{
	class InMemoryOrdersRepository : IOrdersRepository
	{
		public Task<Order> GetById(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task Save(Order order)
		{
			throw new NotImplementedException();
		}
	}
}
