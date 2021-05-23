using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FoodOrdering.Common;
using FoodOrdering.Modules.Basket.Contracts.DTO;
using FoodOrdering.Modules.Basket.Contracts.Queries;
using FoodOrdering.Modules.Basket.Domain.Repositories;
using MediatR;

namespace FoodOrdering.Modules.Basket.Application.Handlers.Queries
{
	public class GetBasketQueryHandler : IRequestHandler<GetBasketQuery, BasketDTO>
	{
		private readonly IBasketsRepository basketsRepository;

		public GetBasketQueryHandler(IBasketsRepository basketsRepository)
		{
			this.basketsRepository = basketsRepository;
		}

		public async Task<BasketDTO> Handle(GetBasketQuery request, CancellationToken cancellationToken)
		{
			var basket = basketsRepository.GetById(request.BasketId) ?? throw new AppException("Basket not found");

			var dto = new BasketDTO(
				BasketItems: basket.Products.Select(p => new BasketItemDTO(p.Id.ToGuid(), p.Quantity.ToInt())),
				TotalPrice: basket.TotalPrice.ToDecimal(),
				AppliedCoupon: basket.AppliedCoupon?.ToGuid());

			return dto;
		}
	}
}
