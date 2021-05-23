using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Common.TestUtilities;
using FoodOrdering.Common.Time;
using FoodOrdering.Modules.Auth.Contracts.Events;
using FoodOrdering.Modules.Coupons.Contracts.Events;
using FoodOrdering.Modules.Coupons.Core.EventHandlers;
using FoodOrdering.Modules.Surveys.Contracts.Events;
using Moq;
using Xunit;

namespace FoodOrdering.Modules.Coupons.Tests
{
	public class EventHandlerTests
	{
		private PublisherMock publisherMock = new();
		private Mock<IClock> clock = new();

		private SurveyCompletedEventHandler SurveyCompletedEventHandler
			=> new(clock.Object, publisherMock.Object);

		private UserRegisteredEventHandler UserRegisteredEventHandler
			=> new(clock.Object, publisherMock.Object);

		private async Task Publish(SurveyCompletedEvent e)
			=> await SurveyCompletedEventHandler.Handle(e, new System.Threading.CancellationToken());

		private async Task Publish(UserRegisteredEvent e)
			=> await UserRegisteredEventHandler.Handle(e, new System.Threading.CancellationToken());

		[Fact]
		public async Task WhenSurveyIsCompleted_CouponIsGranted()
		{
			// Arrange
			clock.SetupGet(x => x.Now).Returns(new DateTime(2020, 1, 1));
			var evnt = new SurveyCompletedEvent(Guid.NewGuid(), Guid.NewGuid());

			// Act
			await Publish(evnt);

			// Assert
			var publishedEvent = publisherMock.PublishedNotifications.OfType<CouponGrantedEvent>().Single();
			Assert.Equal(evnt.UserId, publishedEvent.UserId);
		}

		[Fact]
		public async Task WhenUserRegistered_CouponIsGranted()
		{
			// Arrange
			clock.SetupGet(x => x.Now).Returns(new DateTime(2020, 1, 1));
			var evnt = new UserRegisteredEvent(Guid.NewGuid());

			// Act
			await Publish(evnt);

			// Assert
			var publishedEvent = publisherMock.PublishedNotifications.OfType<CouponGrantedEvent>().Single();
			Assert.Equal(evnt.UserId, publishedEvent.UserId);
		}
	}
}
