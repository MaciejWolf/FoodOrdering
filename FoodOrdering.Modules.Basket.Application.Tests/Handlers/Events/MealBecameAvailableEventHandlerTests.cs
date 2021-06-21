using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Modules.Basket.Application.Handlers;
using FoodOrdering.Modules.Basket.Domain.Repositories;
using FoodOrdering.Modules.Basket.Infrastructure.Repositories.InMemory;
using FoodOrdering.Modules.Catalog.Contracts.Events;
using Xunit;

namespace FoodOrdering.Modules.Basket.Application.Tests.Handlers.Events
{
	public class MealBecameAvailableEventHandlerTests
	{
		private IProductsRepository productsRepository = new InMemoryProductsRepository();
		private MealBecameAvailableEventHandler Handler => new MealBecameAvailableEventHandler(productsRepository);

		[Fact]
		public async Task ProductIsCreated()
		{
			// Arrrange
			var evnt = new MealBecameAvailableEvent(Guid.NewGuid(), "Hamburger", 4.99m);

			// Act
			await Handler.Handle(evnt, new System.Threading.CancellationToken());

			// Assert
			var product = productsRepository.GetById(evnt.Id);
			Assert.Equal<decimal>(4.99m, product.Price);
		}
	}
}
