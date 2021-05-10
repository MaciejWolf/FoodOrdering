using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace FoodOrdering.Modules.Coupons.Contracts.Commands
{
	public class MarkCouponUnusedCommand : IRequest
	{
		public Guid Id { get; set; }
	}
}
