using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FoodOrdering.Modules.Basket.Domain.Repositories;
using FoodOrdering.Modules.Basket.Domain.ValueObjects;
using FoodOrdering.Modules.Basket.Repositories;
using FoodOrdering.Modules.OrderProcessing.Contracts.Events;
using MediatR;

namespace FoodOrdering.Modules.Basket.Handlers.Events
{
	class OrderRejectedEventHandler : INotificationHandler<OrderRejectedEvent>
	{
		private readonly IOrdersRepository ordersRepository;
		private readonly IBasketsRepository basketsRepository;
		private readonly IUsedCouponsRepository usedCouponsRepository;
		private readonly IProductsRepository productsRepository;

		public OrderRejectedEventHandler(
			IOrdersRepository ordersRepository, 
			IBasketsRepository basketsRepository, 
			IProductsRepository productsRepository)
		{
			this.ordersRepository = ordersRepository;
			this.basketsRepository = basketsRepository;
			this.productsRepository = productsRepository;
		}

		public async Task Handle(OrderRejectedEvent evnt, CancellationToken cancellationToken)
		{
			var order = await ordersRepository.GetById(evnt.Id);
			var couponIds = order.UsedCoupons;
			var clientId = order.ClientId;

			var usedCoupons = couponIds.Select(id => usedCouponsRepository.Get(id));

			foreach (var usedCoupon in usedCoupons)
			{
				usedCouponsRepository.Remove(usedCoupon.Id);
			}

			var basket = basketsRepository.GetById(clientId);

			foreach (var usedCoupon in usedCoupons)
			{
				basket.AddCoupon(
					usedCoupon.Id, 
					usedCoupon.ProductId, 
					(await productsRepository.GetById(usedCoupon.ProductId)).BasePrice, 
					Discount.Percent(usedCoupon.PercentageDiscount), 
					usedCoupon.ValidTo);
			}

			await basketsRepository.Update(basket);
		}
	}
}
