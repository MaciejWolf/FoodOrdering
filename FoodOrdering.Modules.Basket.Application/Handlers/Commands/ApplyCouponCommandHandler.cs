using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FoodOrdering.Modules.Basket.Contracts.Commands;
using FoodOrdering.Modules.Basket.Domain.Repositories;
using MediatR;

namespace FoodOrdering.Modules.Basket.Application.Handlers.Commands
{
	public class ApplyCouponCommandHandler : IRequestHandler<ApplyCouponCommand>
	{
		private readonly IBasketsRepository basketsRepository;

		public ApplyCouponCommandHandler(IBasketsRepository basketsRepository, ICouponsRepository couponsRepository)
		{
			this.basketsRepository = basketsRepository;
		}

		public async Task<Unit> Handle(ApplyCouponCommand request, CancellationToken cancellationToken)
		{
			var basket = basketsRepository.GetById(request.ClientId);

			basket.ApplyCoupon(request.CouponId);

			basketsRepository.Update(basket);

			return Unit.Value;
		}
	}
}
