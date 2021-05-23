using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Common;
using FoodOrdering.Modules.Basket.Domain.Basket;
using FoodOrdering.Modules.Basket.Domain.Repositories;
using FoodOrdering.Modules.Basket.Domain.ValueObjects;

namespace FoodOrdering.Modules.Basket.Infrastructure.Repositories.InMemory
{
	public class InMemoryBasketsRepository : IBasketsRepository
	{
		private readonly List<BasketAggregate> baskets = new();

		public IEnumerable<BasketAggregate> GetAll() => baskets;

		public BasketAggregate GetById(ClientId clientId) => baskets.SingleOrDefault(WithId(clientId));

		public void Save(BasketAggregate basket)
		{
			if (baskets.Any(WithId(basket.Id)))
			{
				throw new AppException($"Basket with {basket.Id} already exists");
			}

			baskets.Add(basket);
		}

		public void Update(BasketAggregate basket)
		{
			var existingBasket = baskets.SingleOrDefault(WithId(basket.Id));

			if (existingBasket is null)
			{
				throw new AppException($"Basket with {basket.Id} doesn't exists");
			}

			baskets.Remove(existingBasket);
			baskets.Add(basket);
		}

		private static Func<BasketAggregate, bool> WithId(ClientId clientId)
			=> b => b.Id == clientId;
	}
}
