using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FoodOrdering.Common;
using FoodOrdering.Modules.Basket.Contracts.Commands;
using FoodOrdering.Modules.Basket.Domain.Repositories;
using MediatR;

namespace FoodOrdering.Modules.Basket.Application.Handlers.Commands
{
	public class UpdateProductInBasketCommandHandler : IRequestHandler<UpdateProductInBasketCommand>
	{
		private IBasketsRepository basketsRepository;
		private IProductsRepository productsRepository;

		public UpdateProductInBasketCommandHandler(IBasketsRepository basketsRepository, IProductsRepository productsRepository)
		{
			this.basketsRepository = basketsRepository;
			this.productsRepository = productsRepository;
		}

		public async Task<Unit> Handle(UpdateProductInBasketCommand request, CancellationToken cancellationToken)
		{
			var basket = basketsRepository.GetById(request.BasketId) ?? throw new AppException("Basket not found");
			var _ = productsRepository.GetById(request.ProductId) ?? throw new AppException("Product not found");

			basket.UpdateProduct(request.ProductId, request.Quantity);

			basketsRepository.Update(basket);

			return Unit.Value;
		}
	}
}
