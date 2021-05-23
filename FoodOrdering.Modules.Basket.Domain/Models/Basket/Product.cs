using FoodOrdering.Common;
using FoodOrdering.Modules.Basket.Domain.ValueObjects;

namespace FoodOrdering.Modules.Basket.Domain.Basket
{
	public class Product : Entity<ProductId>
	{
		public Price Price { get; }
		public Price TotalPrice => Price * Quantity;
		public Quantity Quantity { get; } = new Quantity(1);

		public Product(ProductId id, Price price) : base(id)
		{
			Price = price;
		}

		public Product(ProductId id, Price price, Quantity quantity) : base(id)
		{
			Price = price;
			Quantity = quantity;
		}
	}
}
