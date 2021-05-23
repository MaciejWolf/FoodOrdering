using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Common;
using FoodOrdering.Modules.Basket.Application.Handlers.Queries;
using FoodOrdering.Modules.Basket.Contracts.DTO;
using FoodOrdering.Modules.Basket.Contracts.Queries;
using FoodOrdering.Modules.Basket.Domain.Basket;
using FoodOrdering.Modules.Basket.Domain.Models.Product;
using FoodOrdering.Modules.Basket.Domain.Repositories;
using FoodOrdering.Modules.Basket.Infrastructure.Repositories.InMemory;
using Xunit;

namespace FoodOrdering.Modules.Basket.Application.Tests.Handlers.Queries
{
	public class GetBasketQueryHandlerTests
	{
		private IBasketsRepository basketsRepository = new InMemoryBasketsRepository();

		private GetBasketQueryHandler Handler => new GetBasketQueryHandler(basketsRepository);

		private async Task<BasketDTO> Send(GetBasketQuery query)
			=> await Handler.Handle(query, new System.Threading.CancellationToken());

		[Fact]
		public async Task ReturnsBasketDTO()
		{
			// Arrange
			var basket = new BasketAggregate(Guid.NewGuid());
			var product = new ProductAggregate(Guid.NewGuid(), 5);
			basket.UpdateProduct(new Product(product.Id, product.Price, 3));
			basketsRepository.Save(basket);

			var query = new GetBasketQuery(basket.Id.ToGuid());

			// Act
			var dto = await Send(query);

			// Assert
			Assert.Equal(15, dto.TotalPrice);
			Assert.Equal(3, dto.BasketItems.Single().Quantity);
			Assert.Equal(product.Id.ToGuid(), dto.BasketItems.Single().Id);
		}

		[Fact]
		public async Task IfBasketDouesntExists_AppExceptionIsThrown()
		{
			// Arrange
			var query = new GetBasketQuery(Guid.NewGuid());

			// Act & Assert
			await Assert.ThrowsAsync<AppException>(() => Send(query));
		}
	}
}
