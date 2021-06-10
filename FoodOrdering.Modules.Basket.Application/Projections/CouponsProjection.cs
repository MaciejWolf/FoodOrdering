using System;
using System.Threading;
using System.Threading.Tasks;
using FoodOrdering.Modules.Basket.Application.ViewModels;
using FoodOrdering.Modules.Basket.Domain.Models.Basket;
using FoodOrdering.Modules.Basket.Domain.Repositories;
using MediatR;

namespace FoodOrdering.Modules.Basket.Application.Projections
{
	public class CouponsProjection :
		INotificationHandler<CouponGranted>,
		INotificationHandler<CouponDisabled>,
		INotificationHandler<CouponAppliedEvent>,
		INotificationHandler<AppliedCouponRemovedEvent>
	{
		private readonly IViewModelsRepository repo;
		private readonly ICouponsRepository couponsRepository;

		public CouponsProjection(IViewModelsRepository viewModelsRepo, ICouponsRepository couponsRepository)
		{
			repo = viewModelsRepo;
			this.couponsRepository = couponsRepository;
		}

		public async Task Handle(CouponGranted evnt, CancellationToken cancellationToken)
		{
			var c = couponsRepository.GetById(evnt.CouponId);

			var coupon = new CouponVm
			{
				Id = evnt.CouponId,
				ClientId = evnt.ClientId,
				IsApplied = false,
				Value = c.Value.ToDecimal()
			};

			repo.Save(coupon);
		}

		public async Task Handle(CouponDisabled evnt, CancellationToken cancellationToken)
		{
			repo.RemoveCoupon(evnt.CouponId);
		}

		public async Task Handle(CouponAppliedEvent evnt, CancellationToken cancellationToken)
		{
			repo.Update(evnt.CouponId, vm =>
			{
				vm.IsApplied = true;
			});
		}

		public async Task Handle(AppliedCouponRemovedEvent evnt, CancellationToken cancellationToken)
		{
			repo.Update(evnt.CouponId, vm =>
			{
				vm.IsApplied = false;
			});
		}
	}
}
