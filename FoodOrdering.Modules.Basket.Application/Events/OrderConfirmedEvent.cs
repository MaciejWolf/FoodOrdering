using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Modules.Basket.Application.DTO;
using MediatR;

namespace FoodOrdering.Modules.Basket.Application.Events
{
	public record OrderConfirmedEvent(
		Guid OrderId, 
		Guid UserId, 
		IEnumerable<CouponDTO> UsedCoupons) : INotification;
}
