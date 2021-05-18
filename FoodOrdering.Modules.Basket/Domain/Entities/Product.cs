using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrdering.Modules.Basket.Entities
{
	class Product
	{
		public Guid Id { get; set; }
		public decimal BasePrice { get; set; }
		public decimal Price { get; set; }
	}
}
