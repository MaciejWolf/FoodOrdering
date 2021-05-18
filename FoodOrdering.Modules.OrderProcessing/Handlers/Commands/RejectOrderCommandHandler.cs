using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FoodOrdering.Modules.OrderProcessing.Contracts.Commands;
using FoodOrdering.Modules.OrderProcessing.Contracts.Events;
using FoodOrdering.Modules.OrderProcessing.Entities;
using FoodOrdering.Modules.OrderProcessing.Repositories;
using MediatR;

namespace FoodOrdering.Modules.OrderProcessing.Handlers.Commands
{
	class RejectOrderCommandHandler : IRequestHandler<RejectOrderCommand>
	{
		private readonly IOrdersRepository ordersRepository;
		private readonly IPublisher publisher;

		public RejectOrderCommandHandler(IOrdersRepository ordersRepository, IPublisher publisher)
		{
			this.ordersRepository = ordersRepository;
			this.publisher = publisher;
		}

		public async Task<Unit> Handle(RejectOrderCommand request, CancellationToken cancellationToken)
		{
			var order = ordersRepository.GetById(request.OrderId);

			if (order.Status == OrderStatus.Completed)
			{
				throw new Exception();
			}

			order.Status = OrderStatus.Rejected;

			await publisher.Publish(new OrderRejectedEvent(order.Id));

			return Unit.Value;
		}
	}
}
