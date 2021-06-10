using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FoodOrdering.Modules.Basket.Contracts.DTO;
using FoodOrdering.Modules.Basket.Contracts.Queries;
using MediatR;

namespace FoodOrdering.Modules.Basket.Application.Handlers.Queries
{
	public class GetBasketQueryHandler : IRequestHandler<GetBasketQuery, BasketDTO>
	{
		private readonly IViewModelsRepository repo;

		public GetBasketQueryHandler(IViewModelsRepository repo)
		{
			this.repo = repo;
		}

		public async Task<BasketDTO> Handle(GetBasketQuery query, CancellationToken cancellationToken)
		{
			var vm = repo.Get(query.BasketId);

			return new BasketDTO(vm.BasketItems.Select(bi => new BasketItemDTO(bi.ProductId, bi.Quantity)), vm.Price, vm.AppliedCoupon);
		}
	}
}
