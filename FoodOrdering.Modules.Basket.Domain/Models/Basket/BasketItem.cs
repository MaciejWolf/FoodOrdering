using FoodOrdering.Common;
using FoodOrdering.Modules.Basket.Domain.ValueObjects;

namespace FoodOrdering.Modules.Basket.Domain.Basket
{
	public class BasketItem : IEntity<ProductId>
	{
		public ProductId Id { get; }
		public Quantity Quantity { get; private set; }

		public BasketItem(ProductId id, Quantity quantity)
		{
			Id = id;
			Quantity = quantity;
		}

		public void UpdateQuantity(Quantity quantity)
		{
			if (quantity.ToInt() < 1)
			{
				throw new AppException();
			}

			Quantity = quantity;
		}
	}
}
