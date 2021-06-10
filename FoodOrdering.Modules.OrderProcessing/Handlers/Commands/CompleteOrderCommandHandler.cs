using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FoodOrdering.Common;
using FoodOrdering.Modules.OrderProcessing.Contracts.Commands;
using FoodOrdering.Modules.OrderProcessing.Contracts.Events;
using FoodOrdering.Modules.OrderProcessing.Entities;
using FoodOrdering.Modules.OrderProcessing.Repositories;
using MediatR;

namespace FoodOrdering.Modules.OrderProcessing.Handlers.Commands
{
	public class CompleteOrderCommandHandler : IRequestHandler<CompleteOrderCommand>
	{
		private readonly IOrdersRepository ordersRepository;
		private readonly IPublisher publisher;

		public CompleteOrderCommandHandler(IOrdersRepository ordersRepository, IPublisher publisher)
		{
			this.ordersRepository = ordersRepository;
			this.publisher = publisher;
		}

		public async Task<Unit> Handle(CompleteOrderCommand request, CancellationToken cancellationToken)
		{
			OrderCompletedEvent evnt = null;

			ordersRepository.Update(request.OrderId, order =>
			{
				if (order.Status != OrderStatus.Placed)
				{
					throw new AppException();
				}

				order.Status = OrderStatus.Completed;

				evnt = new OrderCompletedEvent
				{
					OrderId = order.Id,
					ClientId = order.ClientId
				};
			});

			await publisher.Publish(evnt);

			return Unit.Value;
		}
	}
}
