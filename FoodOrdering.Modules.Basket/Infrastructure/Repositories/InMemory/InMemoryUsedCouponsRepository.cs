using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Modules.Basket.Domain.Entities;
using FoodOrdering.Modules.Basket.Domain.Repositories;

namespace FoodOrdering.Modules.Basket.Infrastructure.Repositories
{
	class InMemoryUsedCouponsRepository : IUsedCouponsRepository
	{
		public UsedCoupon Get(Guid id)
		{
			throw new NotImplementedException();
		}

		public void Remove(Guid id)
		{
			throw new NotImplementedException();
		}

		public void Save(UsedCoupon usedCoupon)
		{
			throw new NotImplementedException();
		}
	}
}
