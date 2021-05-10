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
	public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
	{
		private readonly IBasketsRepository basketsRepository;
		private readonly IOrdersRepository ordersRepository;
		private readonly IClock clock;

		public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
		{
			var basket = basketsRepository.GetById(request.BasketId);

			var order = new Order
			{
				Id = Guid.NewGuid(),
				UserId = request.BasketId,
				OrderItems = basket.BasketItems.Select(bi => new OrderItem
				{
					Id = bi.ProductId,
					//Price = bi.Price,
					//BasePrice = bi.BasePrice	
				}).ToList()
			};

			await ordersRepository.Save(order);

			return order.Id;
		}
	}
}
