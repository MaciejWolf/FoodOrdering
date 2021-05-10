using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace FoodOrdering.Modules.Coupons.Core.Events
{
	public record CouponGrantedToUserEvent(Guid CouponId, Guid OwnerId, DateTime ValidTo, int DiscountInPercentage) : INotification;
}
