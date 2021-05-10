using System;
using System.Collections.Generic;
using FoodOrdering.Modules.Basket.Application.DTO;
using FoodOrdering.Modules.Basket.Core.DTO;
using MediatR;

namespace FoodOrdering.Modules.Basket.Core.Events
{
	public record OrderPlacedEvent(
		Guid UserId, 
		IEnumerable<OrderPositionDto> OrderPositions,
		DateTime? RealisationDate,
		DeliveryType DeliveryType
		) : INotification;
}
