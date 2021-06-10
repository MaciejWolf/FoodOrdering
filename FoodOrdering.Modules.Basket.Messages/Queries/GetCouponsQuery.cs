using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Modules.Basket.Messages.DTO;
using MediatR;

namespace FoodOrdering.Modules.Basket.Contracts.Queries
{
	public record GetCouponsQuery(Guid ClientId) : IRequest<IEnumerable<CouponDTO>>;
}
