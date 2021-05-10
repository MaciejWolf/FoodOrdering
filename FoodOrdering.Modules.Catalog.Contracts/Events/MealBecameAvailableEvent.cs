using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace FoodOrdering.Modules.Catalog.Contracts.Events
{
	public record MealBecameAvailableEvent(
		Guid Id,
		string Name,
		decimal Price) : INotification;
}
