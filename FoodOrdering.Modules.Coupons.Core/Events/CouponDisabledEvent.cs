using System;
using MediatR;

namespace FoodOrdering.Modules.Coupons.Core.Events
{
	public record CouponDisabledEvent(Guid CouponId, Guid OwnerId) : INotification;
}
