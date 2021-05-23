using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Common.TestUtilities;
using FoodOrdering.Modules.OrderProcessing.Contracts.Events;
using FoodOrdering.Modules.Surveys.Contracts.Commands;
using FoodOrdering.Modules.Surveys.Contracts.Events;
using FoodOrdering.Modules.Surveys.Handlers.Events;
using FoodOrdering.Modules.Surveys.Repositories;
using Xunit;

namespace FoodOrdering.Modules.Surveys.Tests
{
	public class OrderCompletedEventHandlerTests
	{
		private readonly ISurveyRepository surveyRepository = new InMemorySurveyRepository();
		private readonly PublisherMock publisherMock = new();

		private OrderCompletedEventHandler Handler
			=> new(surveyRepository, publisherMock.Object);

		private async Task Publish(OrderCompletedEvent e)
			=> await Handler.Handle(e, new System.Threading.CancellationToken());

		[Fact]
		public async Task WhenOrderIsCompleted_SurveyIsGenerated()
		{
			// Arrange
			var evnt = new OrderCompletedEvent
			{
				ClientId = Guid.NewGuid(),
				OrderId = Guid.NewGuid()
			};

			// Act
			await Publish(evnt);

			// Assert
			surveyRepository.GetForClient(evnt.ClientId).Single();
			publisherMock.PublishedNotifications.OfType<SurveyGeneratedEvent>().Single();
		}
	}
}
