using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FoodOrdering.Modules.Basket.Contracts.DTO;
using FoodOrdering.Modules.Basket.Contracts.Queries;
using FoodOrdering.Modules.Basket.Messages.DTO;
using FoodOrdering.Modules.Basket.Repositories;
using MediatR;

namespace FoodOrdering.Modules.Basket.Handlers.Queries
{
	class GetBasketQueryHandler : IRequestHandler<GetBasketQuery, BasketDTO>
	{
		private readonly IBasketsRepository basketsRepository;

		public GetBasketQueryHandler(IBasketsRepository basketsRepository)
		{
			this.basketsRepository = basketsRepository;
		}

		public async Task<BasketDTO> Handle(GetBasketQuery request, CancellationToken cancellationToken)
		{
			var basket = basketsRepository.GetById(request.BasketId);

			var dto = new BasketDTO(
				BasketItems: basket.BasketItems.Select(bi => new BasketItemDTO(
					MealId: bi.ProductId,
					Quantity: bi.Quantity.ToInt(),
					Price: bi.Price.ToDecimal()
				)),
				TotalPrice: basket.TotalPrice.ToDecimal(),
				AppliedDiscounts: basket.AppliedCouponsIds.Select(id => id.ToGuid()));

			return dto;
		}
	}
}
