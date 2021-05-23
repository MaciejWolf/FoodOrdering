using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FoodOrdering.Modules.Basket.Domain.Models.Coupons;
using FoodOrdering.Modules.Basket.Domain.Repositories;
using FoodOrdering.Modules.Coupons.Contracts.Events;
using MediatR;

namespace FoodOrdering.Modules.Basket.Application.Handlers.Events
{
	public class CouponGrantedEventHandler : INotificationHandler<CouponGrantedEvent>
	{
		private readonly ICouponsRepository couponsRepository;

		public async Task Handle(CouponGrantedEvent evnt, CancellationToken cancellationToken)
		{
			var coupon = new Coupon
			{
				Id = evnt.CouponId,
				OwnerId = evnt.UserId,
				IsUsed = false
			};

			couponsRepository.Save(coupon);
		}
	}
}
