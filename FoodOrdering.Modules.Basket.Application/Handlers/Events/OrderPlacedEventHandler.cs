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
			if (evnt.OrderDTO.UsedCoupon.HasValue)
			{
				MarkCouponUsed(evnt.OrderDTO.UsedCoupon.Value);
			}
			ClearBasket(evnt.UserId);
		}

		private void ClearBasket(Guid clientId)
		{
			var basket = basketsRepository.GetById(clientId);
			basket.Reset();
			basketsRepository.Update(basket);
		}

		private void MarkCouponUsed(Guid couponId)
		{
			couponsRepository.Update(couponId, coupon =>
			{
				coupon.IsUsed = true;
			});
		}
	}
}
