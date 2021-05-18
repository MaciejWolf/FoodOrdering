using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FoodOrdering.Modules.Basket.Contracts.Commands;
using FoodOrdering.Modules.Basket.Repositories;
using MediatR;

namespace FoodOrdering.Modules.Basket.Handlers.Commands
{
	class ChangeItemsQuantityCommandHandler : IRequestHandler<ChangeItemsQuantityCommand>
	{
		private readonly IBasketsRepository basketsRepository;
		private readonly IProductsRepository productsRepository;

		public ChangeItemsQuantityCommandHandler(IBasketsRepository basketsRepository, IProductsRepository productsRepository)
		{
			this.basketsRepository = basketsRepository;
			this.productsRepository = productsRepository;
		}

		public async Task<Unit> Handle(ChangeItemsQuantityCommand cmd, CancellationToken cancellationToken)
		{
			var basket = basketsRepository.GetById(cmd.BasketId);
			var product = await productsRepository.GetById(cmd.ProductId);

			basket.UpdateProduct(cmd.ProductId, product.Price, cmd.Quantity);

			return Unit.Value;
		}
	}
}
