using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodOrdering.Modules.Basket.Entities
{
	class Basket // AggregateRoot
	{
		private readonly List<BasketItem> basketItems = new();

		public Guid BuyerId { get; }
		public IEnumerable<BasketItem> BasketItems => basketItems;

		public void AddProduct(Guid productId, int quantity)
		{
			if (basketItems.Any(bi => bi.ProductId == productId))
			{
				var basketItem = basketItems.Single(bi => bi.ProductId == productId);
				basketItem.Quantity += quantity;
			}
			else
			{
				basketItems.Add(new BasketItem
				{
					ProductId = productId,
					Quantity = quantity
				});
			}
		}
	}

	class BasketItem
	{
		public Guid ProductId { get; set; }
		public int Quantity { get; set; }
	}
}
