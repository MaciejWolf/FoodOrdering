using System;
using System.Collections.Generic;
using System.Linq;
using FoodOrdering.Modules.Catalog.Core.DTO;
using FoodOrdering.Modules.Catalog.Core.Entities;

namespace FoodOrdering.Modules.Catalog.Core
{
	public class CatalogService
	{
		private readonly List<Region> regions = new();
		private readonly List<Restaurant> restaurants = new();
		private readonly List<Offer> offers = new();
		private readonly List<Meal> meals = new();

		public Guid CreateRegion(string name)
		{
			var region = new Region(Guid.NewGuid(), name);
			regions.Add(region);

			return region.Id;
		}

		public RegionDTO GetRegion(Guid regionId)
		{
			var region = regions.Single(r => r.Id == regionId);
			return new RegionDTO(region.Id, region.Name);
		}

		public Guid CreateRestaurant(string restaurantName, Guid regionId)
		{
			var restaurant = new Restaurant { Id = Guid.NewGuid(), Name = restaurantName, RegionId = regionId };
			var region = regions.Single(reg => reg.Id == regionId);

			region.AddRestaurant(restaurant.Id);
			restaurants.Add(restaurant);

			return restaurant.Id;
		}
		public RestaurantDTO GetRestaurant(Guid restaurantId)
		{
			var restaurant = restaurants.Single(res => res.Id == restaurantId);
			return new RestaurantDTO(restaurant.Id, restaurant.Name, restaurant.RegionId, restaurant.IsActive);
		}

		public Guid AddMeal(Guid offerId, string name)
		{
			var meal = new Meal { Id = Guid.NewGuid(), Name = name, OfferId = offerId };
			var offer = offers.Single(o => o.Id == offerId);

			offer.AddMeal(meal.Id);

			return meal.Id;
		}

		public MealDTO GetMeal(Guid mealId)
		{
			var meal = meals.Single(m => m.Id == mealId);
			return new MealDTO(meal.Id, meal.Name, meal.OfferId);
		}

		public Guid CreateOffer(string name, Guid regionId)
		{
			var offer = new Offer { Id = Guid.NewGuid(), Name = name, RegionId = regionId };
			var region = regions.Single(reg => reg.Id == regionId);

			region.AddOffer(offer.Id);

			offers.Add(offer);

			return offer.Id;
		}

		public bool ActivateOffer(Guid offerId)
		{
			var offer = offers.Single(o => o.Id == offerId);
			if (offer.Meals.Any())
			{
				offer.IsActive = true;
			}

			return offer.IsActive;
		}

		public OfferDTO GetOffer(Guid offerId)
		{
			var offer = offers.Single(o => o.Id == offerId);
			return new OfferDTO(offer.Id, offer.Name, offer.RegionId, offer.IsActive);
		}

		public bool ActivateRestaurant(Guid restaurantId)
		{
			var restaurant = restaurants.Single(r => r.Id == restaurantId);

			if (restaurant.Offers.Any(offerId => offers.Single(offer => offer.Id == offerId).IsActive))
			{
				restaurant.IsActive = true;
			}

			return restaurant.IsActive;
		}
	}
}
