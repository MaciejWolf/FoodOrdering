using System;
using MediatR;

namespace FoodOrdering.Modules.Coupons.Contracts.Commands
{
	public class MarkCouponUsedCommand : IRequest
	{
		public Guid Id { get; set; }
	}
}
