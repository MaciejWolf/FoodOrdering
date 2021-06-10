using System.Threading;
using System.Threading.Tasks;
using FoodOrdering.Modules.Basket.Domain.Models;
using FoodOrdering.Modules.Basket.Domain.Repositories;
using FoodOrdering.Modules.Coupons.Contracts.Events;
using MediatR;

namespace FoodOrdering.Modules.Basket.Application.Handlers.Events
{
	public class CouponGrantedEventHandler : INotificationHandler<CouponGrantedEvent>
	{
		private readonly ICouponsRepository couponsRepository;
		private readonly IBasketsRepository basketsRepository;

		public CouponGrantedEventHandler(ICouponsRepository couponsRepository, IBasketsRepository basketsRepository)
		{
			this.couponsRepository = couponsRepository;
			this.basketsRepository = basketsRepository;
		}

		public async Task Handle(CouponGrantedEvent evnt, CancellationToken cancellationToken)
		{
			var coupon = new Coupon
			{
				Id = evnt.CouponId,
				OwnerId = evnt.UserId,
				IsUsed = false,
				Value = evnt.Price
			};

			couponsRepository.Save(coupon);

			Thread.Sleep(5000);

			var basket = basketsRepository.GetById(evnt.UserId);
			basket.GrantCoupon(coupon.Id);
			basketsRepository.Update(basket);
		}
	}
}
