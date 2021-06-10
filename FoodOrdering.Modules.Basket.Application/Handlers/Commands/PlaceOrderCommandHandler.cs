using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FoodOrdering.Common;
using FoodOrdering.Common.Time;
using FoodOrdering.Modules.Basket.Contracts.Commands;
using FoodOrdering.Modules.Basket.Contracts.DTO;
using FoodOrdering.Modules.Basket.Contracts.Events;
using FoodOrdering.Modules.Basket.Domain.Repositories;
using MediatR;

namespace FoodOrdering.Modules.Basket.Application.Handlers.Commands
{
	public class PlaceOrderCommandHandler : IRequestHandler<PlaceOrderCommand>
	{
		private readonly IOrdersRepository ordersRepository;
		private readonly IOrderDescriptionsRepository orderDescriptionsRepository;
		private readonly IPublisher publisher;
		private readonly IClock clock;

		public PlaceOrderCommandHandler(
			IOrdersRepository ordersRepository,
			IOrderDescriptionsRepository orderDescriptionsRepository,
			IPublisher publisher, 
			IClock clock)
		{
			this.ordersRepository = ordersRepository;
			this.orderDescriptionsRepository = orderDescriptionsRepository;
			this.publisher = publisher;
			this.clock = clock;
		}

		public async Task<Unit> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
		{
			var order = ordersRepository.GetById(request.OrderId) ?? throw new AppException("Order not found");
			var description = orderDescriptionsRepository.GetById(request.OrderId) ?? throw new AppException("Order not found");

			order.PlaceOrder(clock.Now);

			await publisher.Publish(new OrderPlacedEvent(
				OrderId: order.Id.ToGuid(),
				UserId: order.ClientId,
				new OrderDTO(
				OrderItems: description.OrderItems.Select(p => new OrderItemDTO(
					p.ProductId.ToGuid(),
					p.Quantity.ToInt())),
				UsedCoupon: description.UsedCoupon?.ToGuid(),
				Price: description.Price.ToDecimal(),
				ValidTo: order.ValidTo)));

			return Unit.Value;
		}
	}
}
