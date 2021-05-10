using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace FoodOrdering.Modules.Catalog.Contracts.Events
{
	public record MealsPriceChangedEvent(
		Guid MealId,
		decimal OldPrice,
		decimal NewPrice) : INotification;
}
