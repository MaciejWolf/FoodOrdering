using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Modules.Basket.Domain.Basket;
using FoodOrdering.Modules.Basket.Domain.ValueObjects;

namespace FoodOrdering.Modules.Basket.Domain.Repositories
{
	public interface IBasketsRepository
	{
		BasketAggregate GetById(ClientId clientId);
		IEnumerable<BasketAggregate> GetAll();
		void Save(BasketAggregate basket);
		void Update(BasketAggregate basket);
	}
}
