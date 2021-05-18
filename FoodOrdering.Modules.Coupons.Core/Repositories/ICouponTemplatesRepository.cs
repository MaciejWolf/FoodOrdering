using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Modules.Coupons.Entities;

namespace FoodOrdering.Modules.Coupons.Repositories
{
	interface ICouponTemplatesRepository
	{
		CouponTemplate Get();
		void Save(CouponTemplate template);
	}
}
