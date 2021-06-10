using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FoodOrdering.Modules.OrderProcessing.Contracts.DTO;
using FoodOrdering.Modules.OrderProcessing.Contracts.Queries;
using FoodOrdering.Modules.OrderProcessing.Repositories;
using MediatR;

namespace FoodOrdering.Modules.OrderProcessing.Handlers.Queries
{
	public class GetActiveOrdersQueryHandler : IRequestHandler<GetActiveOrdersQuery, IEnumerable<OrderDTO>>
	{
		private readonly IOrdersRepository ordersRepository;

		public GetActiveOrdersQueryHandler(IOrdersRepository ordersRepository)
		{
			this.ordersRepository = ordersRepository;
		}

		public async Task<IEnumerable<OrderDTO>> Handle(GetActiveOrdersQuery request, CancellationToken cancellationToken)
		{
			return ordersRepository
				.GetAll()
				.Where(o => o.Status == Entities.OrderStatus.Placed)
				.Select(o => new OrderDTO { Id = o.Id, OrderItems = o.OrderItems.Select(oi => new OrderItemDTO { Id = oi.Id, Quantity = oi.Quantity }) });
		}
	}
}
