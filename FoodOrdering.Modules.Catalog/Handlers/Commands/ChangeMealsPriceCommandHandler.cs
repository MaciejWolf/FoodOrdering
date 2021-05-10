using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FoodOrdering.Modules.Catalog.Contracts.Commands;
using FoodOrdering.Modules.Catalog.Contracts.Events;
using FoodOrdering.Modules.Catalog.Repositories;
using MediatR;

namespace FoodOrdering.Modules.Catalog.Handlers.Commands
{
	class ChangeMealsPriceCommandHandler : IRequestHandler<ChangeMealsPriceCommand>
	{
		private readonly IMealsRepository repo;
		private readonly IPublisher publisher;

		public ChangeMealsPriceCommandHandler(IMealsRepository repo, IPublisher publisher)
		{
			this.repo = repo;
			this.publisher = publisher;
		}

		public async Task<Unit> Handle(ChangeMealsPriceCommand request, CancellationToken cancellationToken)
		{
			var meal = await repo.GetById(request.MealId);

			var oldPrice = meal.Price;

			meal.Price = request.NewPrice;

			repo.Update(meal);

			await publisher.Publish(new MealsPriceChangedEvent(meal.Id, oldPrice, meal.Price));

			return Unit.Value;
		}
	}
}
