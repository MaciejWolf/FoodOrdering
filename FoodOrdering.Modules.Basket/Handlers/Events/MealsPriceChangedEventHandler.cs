using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FoodOrdering.Modules.Basket.Repositories;
using FoodOrdering.Modules.Catalog.Contracts.Events;
using MediatR;

namespace FoodOrdering.Modules.Basket.Handlers.Events
{
	class MealsPriceChangedEventHandler : INotificationHandler<MealsPriceChangedEvent>
	{
		private readonly IProductsRepository repo;

		public MealsPriceChangedEventHandler(IProductsRepository repo)
		{
			this.repo = repo;
		}

		public async Task Handle(MealsPriceChangedEvent notification, CancellationToken cancellationToken)
		{
			var product = await repo.GetById(notification.MealId);

			product.Price = notification.NewPrice;

			repo.Update(product);
		}
	}
}
