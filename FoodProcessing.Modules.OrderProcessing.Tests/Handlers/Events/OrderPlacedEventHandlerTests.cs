using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Modules.Basket.Contracts.DTO;
using FoodOrdering.Modules.Basket.Contracts.Events;
using FoodOrdering.Modules.OrderProcessing.Entities;
using FoodOrdering.Modules.OrderProcessing.Handlers.Events;
using FoodOrdering.Modules.OrderProcessing.Repositories;
using Xunit;

namespace FoodProcessing.Modules.OrderProcessing.Tests.Handlers.Events
{
	public class OrderPlacedEventHandlerTests
	{
		private readonly IOrdersRepository ordersRepository = new InMemoryOrdersRepository();

		private OrderPlacedEventHandler Handler
			=> new(ordersRepository);

		private async Task Publish(OrderPlacedEvent evnt)
			=> await Handler.Handle(evnt, new System.Threading.CancellationToken());

		[Fact]
		public async Task PlacedOrderIsPersisted()
		{
			// Arrange
			var evnt = new OrderPlacedEvent(new OrderDTO(
				Guid.NewGuid(),
				Guid.NewGuid(),
				new[]
				{
					new OrderItemDTO(Guid.NewGuid(), 3)
				},
				Guid.NewGuid(),
				30));

			// Act
			await Publish(evnt);

			// Assert
			var order = ordersRepository.GetById(evnt.OrderDTO.Id);
			Assert.Equal(OrderStatus.Placed, order.Status);
		}
	}
}
