using System;
using System.Linq;
using System.Threading.Tasks;
using FoodOrdering.Common;
using FoodOrdering.Modules.Basket.Application.Handlers.Queries;
using FoodOrdering.Modules.Basket.Contracts.DTO;
using FoodOrdering.Modules.Basket.Contracts.Queries;
using FoodOrdering.Modules.Basket.Domain.Basket;
using FoodOrdering.Modules.Basket.Domain.Models;
using FoodOrdering.Modules.Basket.Domain.Repositories;
using FoodOrdering.Modules.Basket.Infrastructure.Repositories.InMemory;
using Xunit;

namespace FoodOrdering.Modules.Basket.Application.Tests.Handlers.Queries
{
	public class GetBasketQueryHandlerTests
	{
		private IBasketsRepository basketsRepository = new InMemoryBasketsRepository();
		private IViewModelsRepository repo = new InMemoryViewModelsRepository();

		private GetBasketQueryHandler Handler => new(repo);

		private async Task<BasketDTO> Send(GetBasketQuery query)
			=> await Handler.Handle(query, new System.Threading.CancellationToken());
	}
}
