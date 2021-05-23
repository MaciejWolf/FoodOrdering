using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FoodOrdering.Modules.Auth.Contracts.Events;
using FoodOrdering.Modules.Basket.Domain.Basket;
using FoodOrdering.Modules.Basket.Domain.Repositories;
using MediatR;

namespace FoodOrdering.Modules.Basket.Application.Handlers
{
	public class UserRegisteredEventHandler : INotificationHandler<UserRegisteredEvent>
	{
		private readonly IBasketsRepository basketsRepository;

		public UserRegisteredEventHandler(IBasketsRepository basketsRepository)
		{
			this.basketsRepository = basketsRepository;
		}

		public async Task Handle(UserRegisteredEvent notification, CancellationToken cancellationToken)
		{
			var basket = new BasketAggregate(notification.UserId);
			basketsRepository.Save(basket);
		}
	}
}
