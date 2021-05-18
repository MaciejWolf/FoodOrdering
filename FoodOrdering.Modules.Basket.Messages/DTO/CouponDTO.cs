using System;

namespace FoodOrdering.Modules.Basket.Messages.DTO
{
	public record CouponDTO(Guid Id, Guid ProductId, int PercentageDiscount, DateTime ValidTo);
}
