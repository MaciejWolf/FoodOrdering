using System;
using System.Collections.Generic;
using System.Linq;
using FoodOrdering.Common;
using FoodOrdering.Modules.Basket.Domain.DomainServices;
using FoodOrdering.Modules.Basket.Domain.Models;
using FoodOrdering.Modules.Basket.Domain.Models.Basket;
using FoodOrdering.Modules.Basket.Domain.Models.Order;
using FoodOrdering.Modules.Basket.Domain.ValueObjects;

namespace FoodOrdering.Modules.Basket.Domain.Basket
{

	public class BasketAggregate : AggregateRoot<ClientId>
	{
		private readonly List<BasketItem> basketItems = new();
		private readonly List<CouponId> availableCoupons = new();

		private CouponId appliedCoupon;

		private readonly IList<IEvent> allEvents = new List<IEvent>();
		private readonly IList<IEvent> uncommittedEvents = new List<IEvent>();

		public BasketAggregate(ClientId clientId)
		{
			AddEvent(new BasketCreatedEvent(clientId));
		}

		private BasketAggregate()
		{
		}

		public IEnumerable<IEvent> AllEvents => allEvents;
		public IEnumerable<IEvent> UncommittedEvents => uncommittedEvents;

		public int InitialVersion { get; private set; }
		public int Version { get; private set; }

		public void UpdateProduct(ProductId productId, Quantity quantity)
		{
			var existingItem = basketItems.SingleOrDefault(bi => bi.Id == productId);

			if (existingItem is not null)
			{
				if (quantity == 0)
				{
					RemoveBasketItem(productId);
				}
				else
				{
					UpdateQuantity(productId, quantity);
				}
			}
			else
			{
				AddBasketItem(productId, quantity);
			}
		}

		private void RemoveBasketItem(ProductId productId)
		{
			AddEvent(new ProductRemovedFromBasketEvent(Id, productId));
		}

		private void UpdateQuantity(ProductId productId, Quantity quantity)
		{
			AddEvent(new ProductsQuantityChanged(Id, productId, quantity));
		}

		private void AddBasketItem(ProductId productId, Quantity quantity)
		{
			AddEvent(new ProductAddedEvent(Id, productId, quantity));
		}

		public void Reset()
		{
			if (appliedCoupon != null)
				AddEvent(new CouponDisabled(Id, appliedCoupon));

			AddEvent(new BasketResetEvent(Id));
		}


		public void ApplyCoupon(CouponId couponId)
		{
			if (availableCoupons.Contains(couponId))
			{
				AddEvent(new CouponAppliedEvent(Id, couponId));
			}
			else
			{
				throw new AppException();
			}
		}

		public void RemoveAppliedCoupon()
		{
			if (appliedCoupon is not null)
			{
				AddEvent(new AppliedCouponRemovedEvent(Id, appliedCoupon));
			}
		}

		public void GrantCoupon(CouponId couponId)
		{
			if (!availableCoupons.Contains(couponId))
			{
				AddEvent(new CouponGranted(Id, couponId));
			}
		}

		public void DisableCoupon(CouponId couponId)
		{
			if (availableCoupons.Contains(couponId))
			{
				AddEvent(new CouponDisabled(Id, couponId));
			}

			if (appliedCoupon == couponId)
			{
				RemoveAppliedCoupon();
			}
		}

		public (OrderAggregate, OrderDescription) CreateOrder(DateTime creationTime, Func<ProductId, Product> productProvider, Func<CouponId, Coupon> couponProvider)
		{
			if (basketItems.Count == 0)
			{
				throw new AppException("Cannot create order from empty basket");
			}

			var orderProducts = basketItems.Select(bi => new OrderItem(bi.Id, bi.Quantity));

			var totalPrice = PriceCalculator.Calculate(
				basketItems.Select(bi => (productProvider(bi.Id), bi.Quantity)), 
				appliedCoupon != null ? couponProvider(appliedCoupon) : null);

			var order = OrderAggregate.Create(Id, creationTime);
			var description = new OrderDescription(order.Id, orderProducts, appliedCoupon, totalPrice);

			return (order, description);
		}

		private void AddEvent(IEvent evnt)
		{
			ApplyEvent(evnt);
			uncommittedEvents.Add(evnt);
		}

		public void ApplyEvent(IEvent evnt)
		{
			Apply((dynamic)evnt);

			//switch (evnt)
			//{
			//	case BasketCreatedEvent basketCreated:
			//		Apply(basketCreated);
			//		break;
			//	case BasketResetEvent basketReset:
			//		Apply(basketReset);
			//		break;
			//	case ProductAddedEvent productAdded:
			//		Apply(productAdded);
			//		break;
			//	case ProductRemovedFromBasketEvent productRemoved:
			//		Apply(productRemoved);
			//		break;
			//	case ProductsQuantityChanged quantityChanged:
			//		Apply(quantityChanged);
			//		break;
			//	case CouponAppliedEvent couponApplied:
			//		Apply(couponApplied);
			//		break;
			//	case AppliedCouponRemovedEvent appliedCouponRemoved:
			//		Apply(appliedCouponRemoved);
			//		break;
			//	default:
			//		throw new InvalidOperationException("Unsupported Event.");
			//}

			Version++;
			allEvents.Add(evnt);
		}

		private void Apply(AppliedCouponRemovedEvent _)
		{
			appliedCoupon = null;
		}

		private void Apply(CouponAppliedEvent evnt)
		{
			appliedCoupon = evnt.CouponId;
		}

		private void Apply(BasketCreatedEvent evnt)
		{
			Id = evnt.ClientId;
		}

		private void Apply(BasketResetEvent _)
		{
			basketItems.Clear();
			appliedCoupon = null;
		}

		private void Apply(ProductAddedEvent evnt)
		{
			basketItems.Add(new BasketItem(evnt.ProductId, evnt.Quantity));
		}

		private void Apply(ProductRemovedFromBasketEvent evnt)
		{
			var item = basketItems.Single(bi => bi.Id == evnt.ProductId);
			basketItems.Remove(item);
		}

		private void Apply(ProductsQuantityChanged evnt)
		{
			var item = basketItems.Single(bi => bi.Id == evnt.ProductId);
			item.UpdateQuantity(evnt.Quantity);
		}

		private void Apply(CouponGranted evnt)
		{
			availableCoupons.Add(evnt.CouponId);
		}

		private void Apply(CouponDisabled evnt)
		{
			availableCoupons.Remove(evnt.CouponId);
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

		public void ClearUncommittedEvents()
		{
			uncommittedEvents.Clear();
		}
	}
}
