using System;
using System.Linq;
using System.Threading.Tasks;
using FoodOrdering.Common;
using FoodOrdering.Modules.Basket.Application.Handlers.Commands;
using FoodOrdering.Modules.Basket.Contracts.Commands;
using FoodOrdering.Modules.Basket.Domain.Basket;
using FoodOrdering.Modules.Basket.Domain.Models;
using FoodOrdering.Modules.Basket.Domain.Repositories;
using FoodOrdering.Modules.Basket.Infrastructure.Repositories.InMemory;
using Xunit;

namespace FoodOrdering.Modules.Basket.Application.Tests.Handlers.Commands
{
	public class UpdateProductInBasketCommandHandlerTests
	{
		private IBasketsRepository basketsRepository = new InMemoryBasketsRepository();
		private IProductsRepository productsRepository = new InMemoryProductsRepository();

		private UpdateProductInBasketCommandHandler Handler
			=> new UpdateProductInBasketCommandHandler(basketsRepository, productsRepository);

		private async Task Send(UpdateProductInBasketCommand cmd)
			=> await Handler.Handle(cmd, new System.Threading.CancellationToken());

		[Fact]
		public async Task IfBasketDoesntExist_AppExceptionIsThrown()
		{
			// Arrange
			var hamburger = new Product(Guid.NewGuid(), 5);
			productsRepository.Save(hamburger);

			var cmd = new UpdateProductInBasketCommand(Guid.NewGuid(), hamburger.Id, 2);

			// Act & Assert
			await Assert.ThrowsAsync<AppException>(() => Send(cmd));
		}

		[Fact]
		public async Task IfProductDoesntExist_AppExceptionIsThrown()
		{
			// Arrange
			var basket = new BasketAggregate(Guid.NewGuid());
			basketsRepository.Save(basket);

			var cmd = new UpdateProductInBasketCommand(basket.Id.ToGuid(), Guid.NewGuid(), 2);

			// Act & Assert
			await Assert.ThrowsAsync<AppException>(() => Send(cmd));
		}
	}
}
