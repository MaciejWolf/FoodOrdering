using System;
using MediatR;

namespace FoodOrdering.Modules.Catalog.Contracts.Commands
{
	public record CreateMealCommand(string Name, decimal Price) : IRequest<Guid>;
}
