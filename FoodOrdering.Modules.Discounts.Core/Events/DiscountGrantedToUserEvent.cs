using System;
using MediatR;

namespace FoodOrdering.Modules.Discounts.Core.Events
{
	public record DiscountGrantedToUserEvent(Guid DiscountId, Guid UserId, int DiscountInPercentage) : INotification;
}
