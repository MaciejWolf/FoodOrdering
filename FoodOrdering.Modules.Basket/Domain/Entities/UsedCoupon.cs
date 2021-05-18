using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrdering.Modules.Basket.Domain.Entities
{
	class UsedCoupon
	{
		public Guid Id { get; set; }
		public Guid ProductId { get; set; }
		public int PercentageDiscount { get; set; }
		public DateTime ValidTo { get; set; }
	}
}
