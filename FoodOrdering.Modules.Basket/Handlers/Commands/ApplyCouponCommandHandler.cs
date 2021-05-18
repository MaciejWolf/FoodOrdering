using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FoodOrdering.Modules.Basket.Contracts.Commands;
using FoodOrdering.Modules.Basket.Repositories;
using MediatR;

namespace FoodOrdering.Modules.Basket.Handlers.Commands
{
	class ApplyCouponCommandHandler : IRequestHandler<ApplyCouponCommand>
	{
		private readonly IBasketsRepository basketsRepository;

		public ApplyCouponCommandHandler(IBasketsRepository basketsRepository)
		{
			this.basketsRepository = basketsRepository;
		}

		public async Task<Unit> Handle(ApplyCouponCommand cmd, CancellationToken cancellationToken)
		{
			var basket = basketsRepository.GetById(cmd.ClientId);

			basket.ApplyCoupon(cmd.CouponId);

			return Unit.Value;
		}
	}
}
