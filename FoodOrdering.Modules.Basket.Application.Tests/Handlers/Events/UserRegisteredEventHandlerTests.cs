using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Modules.Auth.Contracts.Events;
using FoodOrdering.Modules.Basket.Application.Handlers;
using FoodOrdering.Modules.Basket.Domain.Repositories;
using FoodOrdering.Modules.Basket.Infrastructure.Repositories.InMemory;
using Xunit;

namespace FoodOrdering.Modules.Basket.Application.Tests.Handlers.Events
{
	public class UserRegisteredEventHandlerTests
	{
		private IBasketsRepository basketsRepository = new InMemoryBasketsRepository();
		private UserRegisteredEventHandler Handler => new UserRegisteredEventHandler(basketsRepository);

		[Fact]
		public async Task BasketIsCreated()
		{
			// Arrange
			var evnt = new UserRegisteredEvent(Guid.NewGuid());

			// Act
			await Handler.Handle(evnt, new System.Threading.CancellationToken());

			// Assert
			Assert.NotNull(basketsRepository.GetById(evnt.UserId));
		}
	}
}
