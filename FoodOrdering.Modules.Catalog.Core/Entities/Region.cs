using System;
using System.Collections.Generic;

namespace FoodOrdering.Modules.Catalog.Core.Entities
{
	class Region
	{
		public Guid Id { get; }
		public string Name { get; }

		public List<Guid> Restaurants { get; } = new();
		public List<Guid> Offers { get; } = new();

		public Region(Guid id, string name)
		{
			Id = id;
			Name = name;
		}

		public void AddRestaurant(Guid restaurantId)
		{
			Restaurants.Add(restaurantId);
		}

		public void AddOffer(Guid offerId)
		{
			Offers.Add(offerId);
		}
	}
}
