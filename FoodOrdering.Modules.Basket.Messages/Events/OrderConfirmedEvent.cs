using System;
using System.Collections.Generic;
using FoodOrdering.Modules.Basket.Messages.DTO;
using MediatR;

namespace FoodOrdering.Modules.Basket.Messages.Events
{
	public record OrderConfirmedEvent(
		Guid OrderId, 
		Guid UserId, 
		IEnumerable<Guid> UsedCoupons) : INotification;
}
