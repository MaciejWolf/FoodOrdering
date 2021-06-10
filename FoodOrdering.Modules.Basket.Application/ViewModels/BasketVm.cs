using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrdering.Modules.Basket.Application.ViewModels
{
	public class BasketVm
	{
		public Guid Id { get; set; }
		public List<BasketItem> BasketItems { get; set; }
		public Guid? AppliedCoupon { get; set; }
		public decimal Price { get; set; }
	}

	public class BasketItem
	{
		public Guid ProductId { get; set; }
		public int Quantity { get; set; }
	}
}
