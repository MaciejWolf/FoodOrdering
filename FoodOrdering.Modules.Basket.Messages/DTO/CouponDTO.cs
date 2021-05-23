using System;

namespace FoodOrdering.Modules.Basket.Messages.DTO
{
	public record CouponDTO(ProductDTO Id, ProductDTO ProductId, int PercentageDiscount, DateTime ValidTo);
}
