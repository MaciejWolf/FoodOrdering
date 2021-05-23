using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Common;
using FoodOrdering.Modules.Basket.Domain.ValueObjects;

namespace FoodOrdering.Modules.Basket.Domain.Models.Product
{
	public class ProductAggregate : AggregateRoot<ProductId>
	{
		public Price Price { get; private set; }

		public ProductAggregate(ProductId productId, Price price)
		{
			Id = productId;
			Price = price;
		}
	}
}
