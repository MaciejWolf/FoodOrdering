using System.Threading;
using System.Threading.Tasks;
using FoodOrdering.Modules.Basket.Domain.Models;
using FoodOrdering.Modules.Basket.Domain.Repositories;
using FoodOrdering.Modules.Catalog.Contracts.Events;
using MediatR;

namespace FoodOrdering.Modules.Basket.Application.Handlers
{
	public class MealBecameAvailableEventHandler : INotificationHandler<MealBecameAvailableEvent>
	{
		private readonly IProductsRepository productsRepository;

		public MealBecameAvailableEventHandler(IProductsRepository productsRepository)
		{
			this.productsRepository = productsRepository;
		}

		public async Task Handle(MealBecameAvailableEvent notification, CancellationToken cancellationToken)
		{
			var product = new Product(notification.Id, notification.Price);

			productsRepository.Save(product);
		}
	}
}
