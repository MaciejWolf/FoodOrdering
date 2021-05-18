using System;

namespace FoodOrdering.Modules.Basket.Contracts.DTO
{
	public record OrderItemDTO(Guid ProductId, int Quantity, decimal BasePrice, decimal Price);
}
