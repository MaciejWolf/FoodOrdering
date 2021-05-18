using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FoodOrdering.Modules.OrderProcessing.Contracts.Commands;
using FoodOrdering.Modules.OrderProcessing.Entities;
using FoodOrdering.Modules.OrderProcessing.Repositories;
using MediatR;

namespace FoodOrdering.Modules.OrderProcessing.Handlers.Commands
{
	class MarkOrderPreparedCommandHandler : IRequestHandler<MarkOrderPreparedCommand>
	{
		private readonly IOrdersRepository ordersRepository;

		public MarkOrderPreparedCommandHandler(IOrdersRepository ordersRepository)
		{
			this.ordersRepository = ordersRepository;
		}

		public async Task<Unit> Handle(MarkOrderPreparedCommand request, CancellationToken cancellationToken)
		{
			var order = ordersRepository.GetById(request.OrderId);

			if (order.Status != OrderStatus.InPreparation)
			{
				throw new Exception();
			}

			order.Status = OrderStatus.Prepared;

			return Unit.Value;
		}
	}
}
