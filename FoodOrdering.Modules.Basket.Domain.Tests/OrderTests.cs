using System;
using FoodOrdering.Common;
using FoodOrdering.Modules.Basket.Domain.Basket;
using Xunit;

namespace FoodOrdering.Modules.Basket.Domain.Tests
{
	public class OrderTests
	{
		[Fact]
		public void CanCreateOrder()
		{
			// Arrange
			var hamburger = new Product(Guid.NewGuid(), 5, 2);
			var now = new DateTime(2020, 01, 01, 12, 0, 0); 

			var basket = new BasketAggregate(Guid.NewGuid());
			basket.UpdateProduct(hamburger);

			// Act
			var order = basket.CreateOrder(now);

			// Assert
			Assert.Equal(basket.Id, order.ClientId);
			Assert.Equal(basket.TotalPrice, order.TotalPrice);
			Assert.Equal(now.AddMinutes(5), order.ValidTo);
			Assert.False(order.IsPlaced);
		}

		[Fact]
		public void OrderCanBePlacedIn5Minutes()
		{
			// Arrange
			var hamburger = new Product(Guid.NewGuid(), 5, 2);
			var now = new DateTime(2020, 01, 01, 12, 0, 0);

			var basket = new BasketAggregate(Guid.NewGuid());
			basket.UpdateProduct(hamburger);

			var order = basket.CreateOrder(now);

			// Act
			order.PlaceOrder(now.AddMinutes(5));

			// Assert
			Assert.True(order.IsPlaced);
		}

		[Fact]
		public void PlacingOrderTwiceThrowsAppException()
		{
			// Arrange
			var hamburger = new Product(Guid.NewGuid(), 5, 2);
			var now = new DateTime(2020, 01, 01, 12, 0, 0);

			var basket = new BasketAggregate(Guid.NewGuid());
			basket.UpdateProduct(hamburger);

			var order = basket.CreateOrder(now);
			order.PlaceOrder(now.AddMinutes(5));

			// Act & Assert
			Assert.Throws<AppException>(() => order.PlaceOrder(now.AddMinutes(5)));
		}

		[Fact]
		public void PlacingOrderAfter5MinutesThrowsAppException()
		{
			// Arrange
			var hamburger = new Product(Guid.NewGuid(), 5, 2);
			var now = new DateTime(2020, 01, 01, 12, 0, 0);

			var basket = new BasketAggregate(Guid.NewGuid());
			basket.UpdateProduct(hamburger);

			var order = basket.CreateOrder(now);

			// Act & Assert
			Assert.Throws<AppException>(() => order.PlaceOrder(now.AddMinutes(5).AddMilliseconds(1)));

			// Assert
			Assert.False(order.IsPlaced);
		}
	}
}
