using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FoodOrdering.Modules.Catalog.Contracts.Commands;
using FoodOrdering.Modules.Catalog.Contracts.Events;
using FoodOrdering.Modules.Catalog.Models;
using FoodOrdering.Modules.Catalog.Repositories;
using MediatR;

namespace FoodOrdering.Modules.Catalog.Handlers.Commands
{
	class CreateMealCommandHandler : IRequestHandler<CreateMealCommand, Guid>
	{
		private readonly IMealsRepository mealsRepository;
		private readonly IPublisher publisher;

		public CreateMealCommandHandler(IMealsRepository mealsRepository, IPublisher publisher)
		{
			this.mealsRepository = mealsRepository;
			this.publisher = publisher;
		}

		public async Task<Guid> Handle(CreateMealCommand request, CancellationToken cancellationToken)
		{
			var meal = new Meal
			{
				Id = Guid.NewGuid(),
				Name = request.Name,
				Price = request.Price
			};

			await mealsRepository.AddAsync(meal);
			await publisher.Publish(new MealBecameAvailableEvent(meal.Id, meal.Name, meal.Price));

			return meal.Id;
		}
	}
}
