using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FoodOrdering.Modules.Basket.Core.Entities;
using FoodOrdering.Modules.Basket.Core.Repositories;
using FoodOrdering.Modules.Users.Core.Events;
using MediatR;

namespace FoodOrdering.Modules.Basket.Application.Baskets.EventHandlers
{
	public class UserCreatedEventHandler : INotificationHandler<UserCreatedEvent>
	{
		private readonly IUsersRepository usersRepository;

		public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
		{
			var user = new User { Id = notification.UserId };
			usersRepository.Save(user);
		}
	}
}
