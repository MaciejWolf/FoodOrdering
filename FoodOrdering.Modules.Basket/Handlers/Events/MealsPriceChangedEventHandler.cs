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
		private readonly IProductsRepository productsRepository;
		private readonly IBasketsRepository basketsRepository;

		public MealsPriceChangedEventHandler(IProductsRepository productsRepository, IBasketsRepository basketsRepository)
		{
			this.productsRepository = productsRepository;
			this.basketsRepository = basketsRepository;
		}

		public async Task Handle(MealsPriceChangedEvent notification, CancellationToken cancellationToken)
		{
			await UpdateProduct(notification.MealId, notification.NewPrice);
			await UpdateBaskets(notification.MealId, notification.NewPrice);
		}

		private async Task UpdateProduct(Guid mealId, decimal newPrice)
		{
			var product = await productsRepository.GetById(mealId);

			product.Price = newPrice;

			productsRepository.Update(product);
		}

		private async Task UpdateBaskets(Guid mealId, decimal newPrice)
		{
			var baskets = basketsRepository.GetAll();

			foreach (var basket in baskets)
			{
				basket.UpdateBasePriceForProduct(mealId, newPrice);
				await basketsRepository.Save(basket);
			}
		}
	}
}
