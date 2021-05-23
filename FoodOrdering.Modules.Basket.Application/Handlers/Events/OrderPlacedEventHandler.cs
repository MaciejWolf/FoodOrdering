using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FoodOrdering.Modules.Basket.Contracts.Events;
using FoodOrdering.Modules.Basket.Domain.Repositories;
using MediatR;

namespace FoodOrdering.Modules.Basket.Application.Handlers.Events
{
	public class OrderPlacedEventHandler : INotificationHandler<OrderPlacedEvent>
	{
		private readonly ICouponsRepository couponsRepository;
		private readonly IBasketsRepository basketsRepository;

		public OrderPlacedEventHandler(ICouponsRepository couponsRepository, IBasketsRepository basketsRepository)
		{
			this.couponsRepository = couponsRepository;
			this.basketsRepository = basketsRepository;
		}

		public async Task Handle(OrderPlacedEvent evnt, CancellationToken cancellationToken)
		{
			MarkCouponUsed(evnt.OrderDTO.UsedCoupon);
			ClearBasket(evnt.OrderDTO.ClientId);
		}

		private void ClearBasket(Guid clientId)
		{
			var basket = basketsRepository.GetById(clientId);
			basket.Reset();
			basketsRepository.Update(basket);
		}

		private void MarkCouponUsed(Guid couponId)
		{
			var coupon = couponsRepository.GetById(couponId);
			coupon.IsUsed = true;

			couponsRepository.Update(coupon);
		}
	}
}
