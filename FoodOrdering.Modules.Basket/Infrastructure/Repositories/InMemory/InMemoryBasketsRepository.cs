using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Common;
using FoodOrdering.Modules.Basket.Repositories;
using FoodOrdering.Modules.Basket.ValueObjects;

namespace FoodOrdering.Modules.Basket.Infrastructure.Repositories
{
	class InMemoryBasketsRepository : IBasketsRepository
	{
		private readonly List<Entities.Basket> baskets = new();

		public IEnumerable<Entities.Basket> GetAll() => baskets;

		public Entities.Basket GetById(ClientId id)
		{
			return baskets.Single(WithId(id));
		}

		public async Task Save(Entities.Basket basket)
		{
			if (baskets.Any(WithId(basket.Id)))
			{
				throw new AppException($"Basket with id {basket.Id} already exists");
			}

			baskets.Add(basket);
		}

		public async Task Update(Entities.Basket basket)
		{
			if (!baskets.Any(WithId(basket.Id)))
			{
				throw new AppException($"Basket with id {basket.Id} does not exists");
			}
		}

		private static Func<Entities.Basket, bool> WithId(ClientId id)
			=> basket => basket.Id == id;
	}
}
