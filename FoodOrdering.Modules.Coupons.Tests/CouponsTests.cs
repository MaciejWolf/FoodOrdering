using System;
using System.Linq;
using FoodOrdering.Common.TestUtilities;
using FoodOrdering.Common.Time;
using FoodOrdering.Modules.Coupons.Core;
using FoodOrdering.Modules.Coupons.Core.Events;
using MediatR;
using Moq;
using Shouldly;
using Xunit;

namespace FoodOrdering.Modules.Coupons.Tests
{
	public class CouponsTests
	{
		[Fact]
		public void WhenSurveyIsCompletedCouponIsGenerated()
		{
			// Arramge
			var mediator = new MediatorMock();
			var clock = new Mock<IClock>();

			var sampleDay = new DateTime(2000, 1, 1);
			var userId = Guid.NewGuid();

			clock.SetupGet(x => x.Now).Returns(sampleDay);

			DateTime.Now.AddDays(14);

			var sut = new CouponsService(clock.Object, mediator.Object);

			// Act
			sut.OnSurveyCompleted(userId);

			// Assert
			var evnt = mediator.PublishedNotifications.OfType<CouponGrantedToUserEvent>().Single();
			evnt.OwnerId.ShouldBe(userId);
			evnt.ValidTo.ShouldBe(sampleDay.AddDays(14));
			evnt.DiscountInPercentage.ShouldBe(20);
		}
	}
}
