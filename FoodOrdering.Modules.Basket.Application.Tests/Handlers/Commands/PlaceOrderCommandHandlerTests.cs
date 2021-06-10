using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Common.TestUtilities;
using FoodOrdering.Common.Time;
using FoodOrdering.Modules.Basket.Application.Handlers.Commands;
using FoodOrdering.Modules.Basket.Contracts.Commands;
using FoodOrdering.Modules.Basket.Contracts.Events;
using FoodOrdering.Modules.Basket.Domain.Models.Order;
using FoodOrdering.Modules.Basket.Domain.Repositories;
using FoodOrdering.Modules.Basket.Infrastructure.Repositories.InMemory;
using Moq;
using Xunit;

namespace FoodOrdering.Modules.Basket.Application.Tests.Handlers.Commands
{
	public class PlaceOrderCommandHandlerTests
	{
		private IOrdersRepository ordersRepository = new InMemoryOrdersRepository();
		private IOrderDescriptionsRepository orderDescriptionsRepository = new InMemoryOrderDescriptionsRepository();
		private PublisherMock publisherMock = new();
		private Mock<IClock> clock = new();

		private PlaceOrderCommandHandler Handler
			=> new(ordersRepository, orderDescriptionsRepository, publisherMock.Object, clock.Object);

		private async Task Send(PlaceOrderCommand cmd)
			=> await Handler.Handle(cmd, new System.Threading.CancellationToken());

		[Fact]
		public async Task WhenOrderIsPlaced_EventIsPublished()
		{
			//// Arrange
			//var time = new DateTime(2020, 1, 1);

			//var order = new OrderAggregate(
			//	Guid.NewGuid(),
			//	time,
			//	new[] { new OrderProduct(Guid.NewGuid(), 3) }.ToList(),
			//	Guid.NewGuid(),
			//	5);
			//ordersRepository.Save(order);

			//clock.SetupGet(x => x.Now).Returns(time.AddMinutes(3));

			//var cmd = new PlaceOrderCommand(order.Id.ToGuid());

			//// Act
			//await Send(cmd);

			//// Assert
			//var evnt = publisherMock.PublishedNotifications.OfType<OrderPlacedEvent>().Single();
			//Assert.Equal(order.Id, evnt.OrderDTO.Id);
			//Assert.Equal(order.ClientId, evnt.OrderDTO.ClientId); 
		}
	}
}
