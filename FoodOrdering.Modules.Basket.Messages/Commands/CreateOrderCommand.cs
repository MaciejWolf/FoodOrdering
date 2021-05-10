using System;
using MediatR;

namespace FoodOrdering.Modules.Basket.Contracts.Commands
{
	public record CreateOrderCommand(Guid BasketId) : IRequest<Guid>;
}
