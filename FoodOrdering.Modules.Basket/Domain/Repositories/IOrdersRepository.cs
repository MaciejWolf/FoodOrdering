using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Modules.Basket.Entities;

namespace FoodOrdering.Modules.Basket.Repositories
{
	interface IOrdersRepository
	{
		Task<Order> GetById(Guid id);
		Task Save(Order order);
	}
}
