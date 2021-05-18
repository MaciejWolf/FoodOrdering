using System;
using FoodOrdering.Modules.Basket.Contracts.DTO;
using MediatR;

namespace FoodOrdering.Modules.Basket.Contracts.Queries
{
	public record GetBasketQuery(Guid BasketId) : IRequest<BasketDTO>;
}
