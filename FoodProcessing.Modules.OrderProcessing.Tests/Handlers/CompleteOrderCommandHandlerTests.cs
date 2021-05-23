using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Common.TestUtilities;
using FoodOrdering.Modules.OrderProcessing.Contracts.Commands;
using FoodOrdering.Modules.OrderProcessing.Contracts.Events;
using FoodOrdering.Modules.OrderProcessing.Entities;
using FoodOrdering.Modules.OrderProcessing.Handlers.Commands;
using FoodOrdering.Modules.OrderProcessing.Repositories;
using Moq;
using Xunit;

namespace FoodProcessing.Modules.OrderProcessing.Tests.Handlers
{
	public class CompleteOrderCommandHandlerTests
	{
		private readonly IOrdersRepository ordersRepository = new InMemoryOrdersRepository();
		private readonly PublisherMock publisherMock = new();

		private CompleteOrderCommandHandler Handler
			=> new(ordersRepository, publisherMock.Object);

		private async Task Send(CompleteOrderCommand cmd)
			=> await Handler.Handle(cmd, new System.Threading.CancellationToken());

		[Fact]
		public async Task WhenOrderIsCompleted_OrderCompletedEventIsPublished()
		{
			// Arrange
			var order = new Order
			{
				Id = Guid.NewGuid(),
				ClientId = Guid.NewGuid(),
				Status = OrderStatus.Placed,
				Price = 30,
				OrderItems = new[]
				{
					new OrderItem
					{
						Id = Guid.NewGuid(),
						Quantity = 3
					}
				}.ToList()
			};
			ordersRepository.Save(order);

			var cmd = new CompleteOrderCommand
			{
				OrderId = order.Id
			};

			// Act
			await Send(cmd);

			// Assert
			var evnt = publisherMock.PublishedNotifications.OfType<OrderCompletedEvent>().Single();
			Assert.Equal(order.ClientId, evnt.ClientId);
		}
	}
}
