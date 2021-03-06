using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FoodOrdering.Modules.Basket.Contracts.Queries;
using FoodOrdering.Modules.Basket.Domain.Repositories;
using FoodOrdering.Modules.Basket.Messages.DTO;
using MediatR;

namespace FoodOrdering.Modules.Basket.Application.Handlers.Queries
{
	public class GetCouponsQueryHandler : IRequestHandler<GetCouponsQuery, IEnumerable<CouponDTO>>
	{
		private readonly IViewModels vms;

		public GetCouponsQueryHandler(IViewModels vms)
		{
			this.vms = vms;
		}

		public async Task<IEnumerable<CouponDTO>> Handle(GetCouponsQuery query, CancellationToken cancellationToken)
		{
			var coupons = vms.GetCouponsForClient(query.ClientId);

			return coupons.Select(c => new CouponDTO(c.Id, c.Value)).ToArray();

			
		}
	}
}
