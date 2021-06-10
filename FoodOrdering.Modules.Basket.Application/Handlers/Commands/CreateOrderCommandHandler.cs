using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FoodOrdering.Common;
using FoodOrdering.Common.Time;
using FoodOrdering.Modules.Basket.Contracts.Commands;
using FoodOrdering.Modules.Basket.Domain.Repositories;
using MediatR;

namespace FoodOrdering.Modules.Basket.Application.Handlers.Commands
{
	public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
	{
		private readonly IBasketsRepository basketsRepository;
		private readonly IOrdersRepository ordersRepository;
		private readonly IOrderDescriptionsRepository orderDescriptionsRepository;
		private readonly IProductsRepository productsRepository;
		private readonly ICouponsRepository couponsRepository;
		private readonly IClock clock;

		public CreateOrderCommandHandler(
			IBasketsRepository basketsRepository,
			IOrdersRepository ordersRepository,
			IOrderDescriptionsRepository orderDescriptionsRepository,
			IProductsRepository productsRepository, 
			ICouponsRepository couponsRepository,
			IClock clock)
		{
			this.basketsRepository = basketsRepository;
			this.ordersRepository = ordersRepository;
			this.orderDescriptionsRepository = orderDescriptionsRepository;
			this.productsRepository = productsRepository;
			this.couponsRepository = couponsRepository;
			this.clock = clock;
		}

		public async Task<Guid> Handle(CreateOrderCommand cmd, CancellationToken cancellationToken)
		{
			var basket = basketsRepository.GetById(cmd.BasketId) ?? throw new AppException("Basket not found");

			var (order, description) = basket.CreateOrder(
				clock.Now,
				productId => productsRepository.GetById(productId) ?? throw new AppException("Product not found"),
				couponId => couponsRepository.GetById(couponId) ?? throw new AppException("Coupon not found"));

			ordersRepository.Save(order);
			orderDescriptionsRepository.Save(description);

			return order.Id.ToGuid();
		}
	}
}
