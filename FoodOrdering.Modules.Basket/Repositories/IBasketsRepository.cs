using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrdering.Modules.Basket.Repositories
{
	interface IBasketsRepository
	{
		Entities.Basket GetById(Guid id);
	}
}
