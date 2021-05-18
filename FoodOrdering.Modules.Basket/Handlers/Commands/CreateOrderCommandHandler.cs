using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FoodOrdering.Common.Time;
using FoodOrdering.Modules.Basket.Contracts.Commands;
using FoodOrdering.Modules.Basket.Entities;
using FoodOrdering.Modules.Basket.Repositories;
using MediatR;

namespace FoodOrdering.Modules.Basket.Handlers.Commands
{
	class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
	{
		private readonly IBasketsRepository basketsRepository;
		private readonly IOrdersRepository ordersRepository;
		private readonly IClock clock;

		public CreateOrderCommandHandler(IBasketsRepository basketsRepository, IOrdersRepository ordersRepository, IClock clock)
		{
			this.basketsRepository = basketsRepository;
			this.ordersRepository = ordersRepository;
			this.clock = clock;
		}

		public async Task<Guid> Handle(CreateOrderCommand cmd, CancellationToken cancellationToken)
		{
			var basket = basketsRepository.GetById(cmd.BasketId);

			var order = new Order
			{
				Id = Guid.NewGuid(),
				ClientId = cmd.BasketId,
				OrderItems = basket.BasketItems.Select(bi => new OrderItem
				{
					ProductId = bi.ProductId,
					Price = bi.Price.ToDecimal(),
					Quantity = bi.Quantity.ToInt()
				}).ToList(),
				UsedCoupons = basket.AppliedCouponsIds.Select(id => id.ToGuid()).ToList()
			};

			await ordersRepository.Save(order);

			return order.Id.ToGuid();
		}
	}
}
