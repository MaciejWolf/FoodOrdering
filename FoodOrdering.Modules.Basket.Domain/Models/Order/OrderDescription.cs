using System.Collections.Generic;
using FoodOrdering.Modules.Basket.Domain.Models.ValueObjects;
using FoodOrdering.Modules.Basket.Domain.ValueObjects;

namespace FoodOrdering.Modules.Basket.Domain.Models.Order
{
	public record OrderDescription(
		OrderId Id,
		IEnumerable<OrderItem> OrderItems,
		CouponId UsedCoupon,
		Price Price);

	public record OrderItem(ProductId ProductId, Quantity Quantity);
}
