using System.Threading;
using System.Threading.Tasks;
using FoodOrdering.Modules.Coupons.Contracts.Commands;
using FoodOrdering.Modules.Coupons.Core.EventHandlers;
using MediatR;

namespace FoodOrdering.Modules.Coupons.Core.CommandHandlers
{
	class MarkCouponUsedCommandHandler : IRequestHandler<MarkCouponUsedCommand>
	{
		private readonly ICouponsRepository repo;

		public async Task<Unit> Handle(MarkCouponUsedCommand request, CancellationToken cancellationToken)
		{
			var coupon = repo.GetById(request.Id);

			coupon.IsUsed = true;

			repo.Update(coupon);

			return Unit.Value;
		}
	}
}
