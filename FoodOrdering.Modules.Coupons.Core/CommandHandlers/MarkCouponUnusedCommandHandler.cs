using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FoodOrdering.Modules.Coupons.Contracts.Commands;
using FoodOrdering.Modules.Coupons.Core.EventHandlers;
using MediatR;

namespace FoodOrdering.Modules.Coupons.Core.CommandHandlers
{
	class MarkCouponUnusedCommandHandler : IRequestHandler<MarkCouponUnusedCommand>
	{
		private readonly ICouponsRepository repo;
		private readonly IPublisher publisher;

		public async Task<Unit> Handle(MarkCouponUnusedCommand request, CancellationToken cancellationToken)
		{
			var coupon = repo.GetById(request.Id);
			coupon.IsUsed = false;

			repo.Update(coupon);

			return Unit.Value;
		}
	}
}
