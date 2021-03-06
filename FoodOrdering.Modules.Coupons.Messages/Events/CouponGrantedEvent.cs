using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace FoodOrdering.Modules.Coupons.Contracts.Events
{
	public record CouponGrantedEvent(Guid CouponId, Guid UserId, decimal Price, DateTime ValidTo) : INotification;
}
