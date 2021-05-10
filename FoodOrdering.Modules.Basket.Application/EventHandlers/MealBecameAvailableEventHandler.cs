using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FoodOrdering.Modules.Basket.Core.Entities;
using FoodOrdering.Modules.Basket.Core.Repositories;
using FoodOrdering.Modules.Catalog.Core.Events;
using MediatR;

namespace FoodOrdering.Modules.Basket.Application.Baskets.EventHandlers
{
	class MealBecameAvailableEventHandler : INotificationHandler<MealBecameAvailableEvent>
	{
		private readonly IMealsRepository mealsRepository;

		public async Task Handle(MealBecameAvailableEvent evnt, CancellationToken cancellationToken)
		{
			var meal = new Meal { Id = evnt.MealId };
			mealsRepository.Save(meal);
		}
	}
}
