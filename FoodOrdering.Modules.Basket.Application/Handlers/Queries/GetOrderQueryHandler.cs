using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FoodOrdering.Modules.Basket.Contracts.DTO;
using FoodOrdering.Modules.Basket.Contracts.Queries;
using FoodOrdering.Modules.Basket.Domain.Repositories;
using MediatR;

namespace FoodOrdering.Modules.Basket.Application.Handlers.Queries
{
	public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, OrderDTO>
	{
		private readonly IOrdersRepository ordersRepository;
		private readonly IOrderDescriptionsRepository orderDescriptionsRepository;

		public GetOrderQueryHandler(IOrdersRepository ordersRepository, IOrderDescriptionsRepository orderDescriptionsRepository)
		{
			this.ordersRepository = ordersRepository;
			this.orderDescriptionsRepository = orderDescriptionsRepository;
		}

		public async Task<OrderDTO> Handle(GetOrderQuery query, CancellationToken cancellationToken)
		{
			var order = ordersRepository.GetById(query.Id);
			var desc = orderDescriptionsRepository.GetById(query.Id);

			return new OrderDTO(
				desc.OrderItems.Select(oi => new OrderItemDTO(oi.ProductId, oi.Quantity)),
				desc.UsedCoupon?.ToGuid(),
				desc.Price.ToDecimal(),
				order.ValidTo);			
		}
	}
}
