using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace FoodOrdering.Modules.Catalog.Contracts.Commands
{
	public record ChangeMealsPriceCommand(Guid MealId, decimal NewPrice) : IRequest;
}
