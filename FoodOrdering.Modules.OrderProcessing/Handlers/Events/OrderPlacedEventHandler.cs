using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FoodOrdering.Modules.Basket.Contracts.Events;
using FoodOrdering.Modules.OrderProcessing.Entities;
using FoodOrdering.Modules.OrderProcessing.Repositories;
using MediatR;

namespace FoodOrdering.Modules.OrderProcessing.Handlers.Events
{
	public class OrderPlacedEventHandler : INotificationHandler<OrderPlacedEvent>
	{
		private readonly IOrdersRepository ordersRepository;

		public OrderPlacedEventHandler(IOrdersRepository ordersRepository)
		{
			this.ordersRepository = ordersRepository;
		}

		public async Task Handle(OrderPlacedEvent evnt, CancellationToken cancellationToken)
		{
			var dto = evnt.OrderDTO;

			var order = new Order
			{
				Id = evnt.OrderId,
				ClientId = evnt.UserId,
				OrderItems = dto.OrderItems.Select(oi => new OrderItem
				{
					Id = oi.ProductId,
					Quantity = oi.Quantity
				}).ToList(),
				Status = OrderStatus.Placed,
				Price = dto.Price
			};

			ordersRepository.Save(order);
		}
	}
}
