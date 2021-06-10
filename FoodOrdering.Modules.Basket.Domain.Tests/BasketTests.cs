using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Modules.Basket.Domain.Basket;
using FoodOrdering.Modules.Basket.Domain.Models.Basket;
using FoodOrdering.Modules.Basket.Domain.ValueObjects;
using Shouldly;
using Xunit;

namespace FoodOrdering.Modules.Basket.Domain.Tests
{
	public class BasketTests
	{
		[Fact]
		public void ProductCanBeAddedToBasket()
		{
			// Arrange
			var basket = CreateBasket();
			var productId = new ProductId(Guid.NewGuid());

			// Act
			basket.UpdateProduct(productId, 5);

			// Assert
			var evnt = basket.AllEvents.OfType<ProductAddedEvent>().Single();
			evnt.ClientId.ShouldBe(basket.Id);
			evnt.ProductId.ShouldBe(new ProductId(productId));
			evnt.Quantity.ToInt().ShouldBe(5);
		}

		[Fact]
		public void ProductQuantityCanBeChanged()
		{
			// Arrange
			var basket = CreateBasket();
			var productId = new ProductId(Guid.NewGuid());
			basket.UpdateProduct(productId, 5);

			// Act
			basket.UpdateProduct(productId, 6);

			// Assert
			var evnt = basket.AllEvents.OfType<ProductsQuantityChanged>().Single();
			evnt.ClientId.ShouldBe(basket.Id);
			evnt.ProductId.ShouldBe(new ProductId(productId));
			evnt.Quantity.ToInt().ShouldBe(6);
		}

		private static BasketAggregate CreateBasket()
		{
			var clientId = new ClientId(Guid.NewGuid());
			return new BasketAggregate(clientId);
		}
	}
}
