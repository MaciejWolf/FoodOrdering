using System;
using FoodOrdering.Common.Time;
using FoodOrdering.Modules.Coupons.Core.Events;
using MediatR;

namespace FoodOrdering.Modules.Coupons.Core
{
	public class CouponsService
	{
		private readonly IPublisher publisher;
		private readonly IClock clock;

		public CouponsService(IClock clock, IPublisher publisher)
		{
			this.clock = clock;
			this.publisher = publisher;
		}

		public void OnSurveyCompleted(Guid userId)
		{
			publisher.Publish(new CouponGrantedToUserEvent(
				CouponId: Guid.NewGuid(),
				OwnerId: userId,
				ValidTo: clock.Now.AddDays(14),
				DiscountInPercentage: 20));
		}
	}
}
