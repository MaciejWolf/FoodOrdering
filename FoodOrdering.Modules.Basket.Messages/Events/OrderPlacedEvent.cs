using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Modules.Basket.Contracts.DTO;
using MediatR;

namespace FoodOrdering.Modules.Basket.Contracts.Events
{
	public record OrderPlacedEvent(Guid OrderId, Guid UserId, OrderDTO OrderDTO) : INotification;
}
