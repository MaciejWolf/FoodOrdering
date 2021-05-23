using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Modules.Basket.Domain.Basket;
using FoodOrdering.Modules.Basket.Domain.ValueObjects;
using Shouldly;
using Xunit;

namespace FoodOrdering.Modules.Basket.Domain.Tests
{
	public class BasketTests
	{
		[Fact]
		public void WhenBasketIsCreated_TotalPriceIsZero()
		{
			// Act
			var basket = CreateBasket();

			// Assert
			basket.TotalPrice.ShouldBe(Price.Zero);
		}

		[Fact]
		public void WhenItemIsAdded_TotalPriceChanges()
		{
			// Arrange
			var basket = CreateBasket();

			// Act
			basket.UpdateProduct(new Product(Guid.NewGuid(), new Price(5)));

			// Assert
			basket.TotalPrice.ShouldBe(new Price(5));
		}

		[Theory]
		[InlineData(5, 2, 3)]
		[InlineData(5, 5, 0)]
		[InlineData(5, 6, 0)]
		public void TotalPriceIsLoweredByAppliedCouponValue(decimal productPrice, decimal discountValue, decimal expectedTotalPrice)
		{
			// Arrange
			var basket = CreateBasket();
			basket.UpdateProduct(new Product(Guid.NewGuid(), new Price(productPrice)));

			// Act
			basket.ApplyCoupon(new Coupon(Guid.NewGuid(), new Price(discountValue)));

			// Assert
			basket.TotalPrice.ShouldBe(new Price(expectedTotalPrice));
		}

		[Fact]
		public void WhenDiscountCouponIsRemoved_TotalPriceIsSumOfProductsPriceValues()
		{
			// Arrange
			var basket = CreateBasket();
			var product = CreateProduct(price: 5, quantity: 2);
			var discountCoupon = CreateDiscountCoupon(priceValue: 3);

			basket.UpdateProduct(product);
			basket.ApplyCoupon(discountCoupon);

			// Act
			basket.RemoveAppliedCoupon();

			// Assert
			Assert.Equal(new Price(10), basket.TotalPrice);
		}

		private static Basket.BasketAggregate CreateBasket()
		{
			var clientId = new ClientId(Guid.NewGuid());
			return new Basket.BasketAggregate(clientId);
		}

		private static Product CreateProduct(decimal price, int quantity = 1)
			=> new(Guid.NewGuid(), new Price(price), new Quantity(quantity));

		private static Coupon CreateDiscountCoupon(decimal priceValue)
			=> new(Guid.NewGuid(), new Price(priceValue));
	}
}
