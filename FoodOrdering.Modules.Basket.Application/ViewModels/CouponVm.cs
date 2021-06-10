using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrdering.Modules.Basket.Application.ViewModels
{
	public class CouponVm
	{
		public Guid Id { get; set; }
		public Guid ClientId { get; set; }
		public bool IsApplied { get; set; }
		public decimal Value { get; set; }
	}

}
