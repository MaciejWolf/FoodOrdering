using FoodOrdering.Modules.Coupons.Entities;

namespace FoodOrdering.Modules.Coupons.Repositories
{
	class CouponTemplatesRepository : ICouponTemplatesRepository
	{
		private CouponTemplate template;

		public void Save(CouponTemplate template) => this.template = template;

		public CouponTemplate Get() => template;
	}
}
