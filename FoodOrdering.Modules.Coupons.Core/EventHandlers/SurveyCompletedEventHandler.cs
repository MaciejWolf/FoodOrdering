using System;
using System.Threading;
using System.Threading.Tasks;
using FoodOrdering.Common.Time;
using FoodOrdering.Modules.Coupons.Contracts.Events;
using FoodOrdering.Modules.Surveys.Contracts.Events;
using MediatR;

namespace FoodOrdering.Modules.Coupons.Core.EventHandlers
{
	public class SurveyCompletedEventHandler : INotificationHandler<SurveyCompletedEvent>
	{
		private readonly IPublisher publisher;
		private readonly IClock clock;

		public SurveyCompletedEventHandler(IClock clock, IPublisher publisher)
		{
			this.clock = clock;
			this.publisher = publisher;
		}

		public async Task Handle(SurveyCompletedEvent evnt, CancellationToken cancellationToken)
		{
			await publisher.Publish(new CouponGrantedEvent(
				CouponId: Guid.NewGuid(),
				UserId: evnt.UserId,
				Price: 5,
				ValidTo: clock.Now.AddDays(14)));
		}
	}
}
