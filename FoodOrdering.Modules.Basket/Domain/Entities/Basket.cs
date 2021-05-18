using System;
using System.Collections.Generic;
using System.Linq;
using FoodOrdering.Modules.Basket.Domain.ValueObjects;
using FoodOrdering.Modules.Basket.ValueObjects;

namespace FoodOrdering.Modules.Basket.Entities
{
	class Basket // EventSourcedAggregateRoot
	{
		private readonly List<BasketItem> basketItems = new();
		private readonly List<Coupon> availableCoupons = new();
		private readonly List<Coupon> appliedCoupons = new();

		public Basket(ClientId clientId)
		{
			Id = clientId;
		}

		public ClientId Id { get; }
		public IEnumerable<BasketItem> BasketItems => basketItems;
		public IEnumerable<CouponId> AvailableCoupons => availableCoupons.Select(c => c.Id);
		public IEnumerable<CouponId> AppliedCouponsIds => appliedCoupons.Select(c => c.Id);
		public Price TotalPrice
		{
			get
			{
				var price = Price.Zero;

				foreach (var basket in basketItems)
				{
					price += basket.Price;
				}

				foreach (var coupon in appliedCoupons)
				{
					price += coupon.Price;
				}

				return price;
			}
		}

		public void UpdateProduct(ProductId productId, Price price, Quantity quantity)
		{
			if (basketItems.Any(WithId(productId)))
			{
				if (quantity == 0)
				{
					RemoveProduct(productId);
				}
				else
				{
					UpdateBasketItem(productId, price, quantity);
				}
			}
			else
			{
				if (quantity > 0)
				{
					AddBasketItem(productId, price, quantity);
				}
			}
		}

		private void AddBasketItem(ProductId productId, Price price, Quantity quantity)
		{
			basketItems.Add(new BasketItem
			{
				ProductId = productId,
				Quantity = quantity,
				Price = price
			});
		}

		private void UpdateBasketItem(ProductId productId, Price price, Quantity quantity)
		{
			var item = basketItems.Single(WithId(productId));
			item.Quantity = quantity;
			item.Price = price;
		}

		private void RemoveProduct(ProductId productId)
		{
			var item = basketItems.Single(WithId((ProductId)productId));
			basketItems.Remove(item);
		}

		public void AddCoupon(CouponId couponId, ProductId productId, Price basePrice, Discount discount, DateTime validTo)
		{
			var coupon = new Coupon
			{
				Id = couponId,
				ProductId = productId,
				Discount = discount,
				BasePrice = basePrice,
				ValidTo = validTo
			};

			availableCoupons.Add(coupon);
		}

		public void RemoveCoupon(CouponId couponId)
		{
			var coupon = appliedCoupons.Single(WithId(couponId));
			appliedCoupons.Remove(coupon);
		}

		public void RemoveExpiredCoupons(DateTime now)
		{
			availableCoupons.RemoveAll(c => c.ValidTo < now);
			appliedCoupons.RemoveAll(c => c.ValidTo < now);
		}

		public void ApplyCoupon(Guid couponId)
		{
			var coupon = availableCoupons.Single(WithId((CouponId)couponId));
			availableCoupons.Remove(coupon);
			appliedCoupons.Add(coupon);
		}

		public void UpdateBasePriceForProduct(ProductId productId, Price price)
		{
			var item = basketItems.Single(WithId(productId));
			item.Price = price;

			foreach (var coupon in availableCoupons.Where(HasProduct(productId)))
			{
				coupon.BasePrice = price;
			}

			foreach (var coupon in appliedCoupons.Where(HasProduct(productId)))
			{
				coupon.BasePrice = price;
			}
		}

		public void ApplyPromotedPriceForProduct(ProductId productId, Price price)
		{
			var item = basketItems.Single(WithId(productId));
			item.Price = price;
		}

		private static Func<BasketItem, bool> WithId(ProductId productId)
			=> basketItem => basketItem.ProductId == productId;

		private static Func<Coupon, bool> WithId(CouponId couponId)
			=> coupon => coupon.Id == couponId;

		private static Func<Coupon, bool> HasProduct(ProductId productId)
			=> coupon => coupon.ProductId == productId;
	}

	class BasketItem // Entity
	{
		public ProductId ProductId { get; set; }
		public Quantity Quantity { get; set; }
		public Price Price { get; set; }
	}

	class Coupon // Entity
	{
		public CouponId Id { get; set; }
		public ProductId ProductId { get; set; }
		public Discount Discount { get; set; }
		public Price BasePrice { get; set; }
		public DateTime ValidTo { get; set; }
		public Price Price => BasePrice.WithDiscount(Discount);
	}
}
