using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FoodOrdering.Modules.Basket.Core.Entities;
using FoodOrdering.Modules.Basket.Core.Repositories;
using FoodOrdering.Modules.Coupons.Core.Events;
using MediatR;

namespace FoodOrdering.Modules.Basket.Application.Baskets.EventHandlers
{
	public class CouponGrantedToUserEventHandler : INotificationHandler<CouponGrantedToUserEvent>
	{
		private readonly IUsersRepository usersRepository;

		public async Task Handle(CouponGrantedToUserEvent notification, CancellationToken cancellationToken)
		{
			var user = usersRepository.Get(notification.OwnerId);
			user.GrantedCoupons.Add(new Coupon
			{
				Id = notification.CouponId,
				Percentage = notification.DiscountInPercentage,
				ValidTo = notification.ValidTo
			});

			usersRepository.Update(user);
		}
	}
}
