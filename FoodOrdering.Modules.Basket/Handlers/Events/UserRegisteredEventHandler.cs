using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FoodOrdering.Modules.Auth.Contracts.Events;
using FoodOrdering.Modules.Basket.Repositories;
using FoodOrdering.Modules.Basket.ValueObjects;
using MediatR;

namespace FoodOrdering.Modules.Basket.Handlers.Events
{
	class UserRegisteredEventHandler : INotificationHandler<UserRegisteredEvent>
	{
		private readonly IBasketsRepository basketsRepository;

		public UserRegisteredEventHandler(IBasketsRepository basketsRepository)
		{
			this.basketsRepository = basketsRepository;
		}

		public async Task Handle(UserRegisteredEvent evnt, CancellationToken cancellationToken)
		{
			var basket = new Entities.Basket(new ClientId(evnt.UserId));
			await basketsRepository.Save(basket);
		}
	}
}
