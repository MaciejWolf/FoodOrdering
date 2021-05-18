using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Modules.Basket.Domain.Entities;

namespace FoodOrdering.Modules.Basket.Domain.Repositories
{
	interface IUsedCouponsRepository
	{
		void Save(UsedCoupon usedCoupon);
		UsedCoupon Get(Guid id);
		void Remove(Guid id);
	}
}
