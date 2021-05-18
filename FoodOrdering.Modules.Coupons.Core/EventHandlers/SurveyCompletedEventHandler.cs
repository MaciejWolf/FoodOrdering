using System;
using System.Threading;
using System.Threading.Tasks;
using FoodOrdering.Common.Time;
using FoodOrdering.Modules.Coupons.Contracts.Events;
using FoodOrdering.Modules.Coupons.Repositories;
using FoodOrdering.Modules.Surveys.Contracts.Events;
using MediatR;

namespace FoodOrdering.Modules.Coupons.Core.EventHandlers
{
	class SurveyCompletedEventHandler : INotificationHandler<SurveyCompletedEvent>
	{
		private readonly ICouponTemplatesRepository repo;
		private readonly IPublisher publisher;
		private readonly IClock clock;

		public SurveyCompletedEventHandler(ICouponTemplatesRepository repo, IClock clock, IPublisher publisher)
		{
			this.repo = repo;
			this.clock = clock;
			this.publisher = publisher;
		}

		public async Task Handle(SurveyCompletedEvent evnt, CancellationToken cancellationToken)
		{
			var template = repo.Get();

			if (template == null)
			{
				return;
			}

			var coupon = template.GenerateCoupon(Guid.NewGuid(), evnt.UserId, clock.Now);

			await publisher.Publish(new CouponGrantedEvent(
				CouponId: coupon.Id,
				UserId: evnt.UserId,
				ProductId: coupon.MealId,
				PercentageDiscount: coupon.DiscountInPercentage,
				ValidTo: coupon.ValidTo));
		}
	}
}
