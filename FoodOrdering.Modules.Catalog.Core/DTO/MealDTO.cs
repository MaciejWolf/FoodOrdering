using System;

namespace FoodOrdering.Modules.Catalog.Core.DTO
{
	public record MealDTO(Guid Id, string Name, Guid OfferId);
}
