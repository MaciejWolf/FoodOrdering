using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FoodOrdering.Common.Time;
using FoodOrdering.Modules.Basket.Contracts.Commands;
using FoodOrdering.Modules.Basket.Contracts.DTO;
using FoodOrdering.Modules.Basket.Contracts.Events;
using FoodOrdering.Modules.Basket.Repositories;
using MediatR;

namespace FoodOrdering.Modules.Basket.Handlers.Commands
{
	class PlaceOrderCommandHandler : IRequestHandler<PlaceOrderCommand>
	{
		private readonly IOrdersRepository ordersRepository;
		private readonly IPublisher publisher;
		private readonly IClock clock;

		public PlaceOrderCommandHandler(IOrdersRepository ordersRepository, IClock clock, IPublisher publisher)
		{
			this.ordersRepository = ordersRepository;
			this.clock = clock;
			this.publisher = publisher;
		}

		public async Task<Unit> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
		{
			var order = await ordersRepository.GetById(request.OrderId);

			order.PlaceOrder(clock.Now);

			await publisher.Publish(new OrderPlacedEvent(
				new OrderDTO(
					Id: order.Id.ToGuid(),
					OrderItems: order.OrderItems.Select(i => new OrderItemDTO(
						ProductId: i.ProductId,
						Quantity: i.Quantity.ToInt(),
						BasePrice: i.BasePrice.ToDecimal(),
						Price: i.Price.ToDecimal())),
					UsedCoupons: order.UsedCoupons)));

			return Unit.Value;
		}
	}
}
