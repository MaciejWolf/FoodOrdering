using System;
using System.Collections.Generic;
using System.Linq;
using FoodOrdering.Common;
using FoodOrdering.Modules.Basket.Domain.Models.Basket.DomainEvents;
using FoodOrdering.Modules.Basket.Domain.Models.Order;
using FoodOrdering.Modules.Basket.Domain.ValueObjects;

namespace FoodOrdering.Modules.Basket.Domain.Basket
{
	public class BasketAggregate : AggregateRoot<ClientId>
	{
		private readonly List<Product> products = new();
		private Coupon appliedCoupon;

		private readonly IList<IEvent> allEvents = new List<IEvent>();
		private readonly IList<IEvent> uncommittedEvents = new List<IEvent>();

		public BasketAggregate(ClientId clientId)
		{
			AddEvent(new BasketCreatedEvent(clientId));
		}

		private BasketAggregate()
		{
		}

		public IEnumerable<Product> Products => products;
		public Price TotalPrice
		{
			get
			{
				var total = Price.Zero;

				foreach (var price in Products.Select(p => p.TotalPrice))
				{
					total += price;
				}

				if (appliedCoupon is not null)
					total -= appliedCoupon.Value;

				return total;
			}
		}

		public CouponId AppliedCoupon => appliedCoupon?.Id;

		public IEnumerable<IEvent> AllEvents => allEvents;
		public IEnumerable<IEvent> UncommittedEvents => uncommittedEvents;

		public int InitialVersion { get; private set; }
		public int Version { get; private set; }

		public void UpdateProduct(Product product)
		{
			var existingProduct = products.SingleOrDefault(WithId(product.Id));

			if (existingProduct is null)
			{
				AddToBasket(product);
			}
			else
			{
				if (product.Quantity == Quantity.Zero)
				{
					RemoveFromBasket(product.Id);
				}
				else
				{
					products.Remove(existingProduct);
					products.Add(product);
				}
			}
		}

		public void Reset()
		{
			AddEvent(new BasketResetEvent(Id));
		}

		private void AddToBasket(Product product)
		{
			var oldPrice = TotalPrice;
			products.Add(product);

			if (oldPrice != TotalPrice)
			{

			}
		}

		private void RemoveFromBasket(ProductId productId)
		{
			var oldPrice = TotalPrice;

			var product = products.SingleOrDefault(WithId(productId));

			if (product is not null)
			{
				products.Remove(product);
			}

			if (oldPrice != TotalPrice)
			{

			}
		}

		public void ApplyCoupon(Coupon coupon)
		{
			var oldPrice = TotalPrice;

			appliedCoupon = coupon;

			if (oldPrice != TotalPrice)
			{

			}
		}

		public void RemoveAppliedCoupon()
		{
			var oldPrice = TotalPrice;

			appliedCoupon = null;

			if (oldPrice != TotalPrice)
			{

			}
		}

		public OrderAggregate CreateOrder(DateTime creationTime)
		{
			return new OrderAggregate(Id, creationTime, products.Select(p => new OrderProduct(p.Id, p.Quantity)).ToList(), AppliedCoupon, TotalPrice);
		}

		private void AddEvent(IEvent evnt)
		{
			ApplyEvent(evnt);
			uncommittedEvents.Add(evnt);
		}

		public void ApplyEvent(IEvent evnt)
		{
			switch (evnt)
			{
				case BasketCreatedEvent basketCreated:
					Apply(basketCreated);
					break;
				case BasketResetEvent basketReset:
					Apply(basketReset);
					break;
				default:
					throw new InvalidOperationException("Unsupported Event.");
			}

			Version++;
			allEvents.Add(evnt);
		} 

		private void Apply(BasketCreatedEvent evnt)
		{
			Id = evnt.ClientId;
		}

		private void Apply(BasketResetEvent _)
		{
			products.Clear();
			appliedCoupon = null;
		}

		private void Apply(ProductAddedEvent evnt)
		{
		}

		private void Apply(ProductRemovedEvent evnt)
		{

		}

		public static BasketAggregate FromEvents(IEnumerable<IEvent> events)
		{
			var basket = new BasketAggregate();

			foreach (var e in events)
			{
				basket.ApplyEvent(e);
			}

			basket.InitialVersion = basket.Version;

			return basket;
		}

		private static Func<Product, bool> WithId(ProductId couponId)
			=> coupon => coupon.Id == couponId;
	}
}
