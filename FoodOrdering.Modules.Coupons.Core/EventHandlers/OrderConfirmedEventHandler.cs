using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FoodOrdering.Modules.Basket.Messages.Events;
using FoodOrdering.Modules.Coupons.Contracts.Commands;
using MediatR;

namespace FoodOrdering.Modules.Coupons.Core.EventHandlers
{
	public class OrderConfirmedEventHandler : INotificationHandler<OrderConfirmedEvent>
	{
		private readonly ICouponsRepository repo;
		private readonly ISender mediator;

		public async Task Handle(OrderConfirmedEvent notification, CancellationToken cancellationToken)
		{
			foreach (var couponId in notification.UsedCoupons)
			{
				var coupon = repo.GetById(couponId);
				await mediator.Send(new MarkCouponUsedCommand() { Id = coupon.Id }, cancellationToken);
			}
		}
	}
}
