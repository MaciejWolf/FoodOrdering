using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Modules.Basket.ValueObjects;

namespace FoodOrdering.Modules.Basket.Repositories
{
	interface IBasketsRepository
	{
		IEnumerable<Entities.Basket> GetAll();
		Entities.Basket GetById(ClientId id);
		Task Save(Entities.Basket basket);
		Task Update(Entities.Basket basket);
	}
}
