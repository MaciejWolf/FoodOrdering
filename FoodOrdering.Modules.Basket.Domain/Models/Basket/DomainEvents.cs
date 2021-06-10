using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Common;
using FoodOrdering.Modules.Basket.Domain.ValueObjects;

namespace FoodOrdering.Modules.Basket.Domain.Models.Basket
{
	public record BasketCreatedEvent(ClientId ClientId) : IEvent;
	public record BasketResetEvent(ClientId ClientId) : IEvent;
	public record CouponAppliedEvent(ClientId ClientId, CouponId CouponId) : IEvent;
	public record AppliedCouponRemovedEvent(ClientId ClientId, CouponId CouponId) : IEvent;
	public record PriceChangedEvent(ClientId ClientId, Price OldPrice, Price NewPrice) : IEvent;
	public record ProductAddedEvent(ClientId ClientId, ProductId ProductId, Quantity Quantity) : IEvent;
	public record ProductRemovedFromBasketEvent(ClientId ClientId, ProductId ProductId) : IEvent;
	public record ProductsQuantityChanged(ClientId ClientId, ProductId ProductId, Quantity Quantity) : IEvent;
	public record CouponGranted(ClientId ClientId, CouponId CouponId) : IEvent;
	public record CouponDisabled(ClientId ClientId, CouponId CouponId) : IEvent;
}
