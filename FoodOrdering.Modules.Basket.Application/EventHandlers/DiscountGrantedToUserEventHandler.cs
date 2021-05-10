using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FoodOrdering.Modules.Basket.Core.Entities;
using FoodOrdering.Modules.Basket.Core.Repositories;
using FoodOrdering.Modules.Discounts.Core.Events;
using MediatR;

namespace FoodOrdering.Modules.Basket.Application.Baskets.EventHandlers
{
	class DiscountGrantedToUserEventHandler : INotificationHandler<DiscountGrantedToUserEvent>
	{
		private readonly IUsersRepository usersRepository;

		public async Task Handle(DiscountGrantedToUserEvent evnt, CancellationToken cancellationToken)
		{
			var user = usersRepository.Get(evnt.UserId);

			user.GrantedDiscounts.Add(new Discount
			{
				Id = evnt.DiscountId,
				Percentage = evnt.DiscountInPercentage
			});

			usersRepository.Update(user);
		}
	}
}
