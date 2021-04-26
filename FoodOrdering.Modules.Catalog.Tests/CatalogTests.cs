using FoodOrdering.Modules.Catalog.Core;
using Shouldly;
using Xunit;

namespace FoodOrdering.Modules.Catalog.Tests
{
	public class CatalogTests
	{
		private CatalogService Sut { get; }

		public CatalogTests()
		{
			Sut = new CatalogService();
		}

		[Fact]
		public void CanCreateRegion()
		{
			var id = Sut.CreateRegion("PL");
			var region = Sut.GetRegion(id);

			region.Name.ShouldBe("PL");
		}

		[Fact]
		public void CanCreateRestaurantInRegion()
		{
			var regionId = Sut.CreateRegion("PL");

			var restaurantId = Sut.CreateRestaurant("PL05", regionId);
			var restaurant = Sut.GetRestaurant(restaurantId);

			restaurant.Name.ShouldBe("PL");
			restaurant.RegionId.ShouldBe(regionId);
			restaurant.IsActive.ShouldBeFalse();
		}

		[Fact]
		public void CanCreateOfferInRegion()
		{
			var regionId = Sut.CreateRegion("PL");

			var offerId = Sut.CreateOffer("RegularOffer", regionId);
			var offer = Sut.GetOffer(offerId);

			offer.Name.ShouldBe("RegularOffer");
			offer.RegionId.ShouldBe(regionId);
			offer.IsActive.ShouldBeFalse();
		}

		[Fact]
		public void CanAddMealToOffer()
		{
			var regionId = Sut.CreateRegion("PL");
			var offerId = Sut.CreateOffer("RegularOffer", regionId);

			var mealId = Sut.AddMeal(offerId, "Hamburger");
			var meal = Sut.GetMeal(mealId);

			meal.Name.ShouldBe("Hamburger");
			meal.OfferId.ShouldBe(offerId);
		}

		[Fact]
		public void CanActivateOfferWithAtLeastOneMeal()
		{
			var regionId = Sut.CreateRegion("PL");
			var offerId = Sut.CreateOffer("RegularOffer", regionId);

			var success = Sut.ActivateOffer(offerId);
			var offer = Sut.GetOffer(offerId);

			success.ShouldBeTrue();
			offer.IsActive.ShouldBeTrue();
		}

		[Fact]
		public void CannotActivateOfferWithoutMeals()
		{
			var regionId = Sut.CreateRegion("PL");
			var offerId = Sut.CreateOffer("RegularOffer", regionId);

			var success = Sut.ActivateOffer(offerId);

			var offer = Sut.GetOffer(offerId);
			success.ShouldBeFalse();
			offer.IsActive.ShouldBe(false);
		}

		[Fact]
		public void CanActivateRestaurantWithAtLeastOneActiveOffer()
		{
			var regionId = Sut.CreateRegion("PL");
			var restaurantId = Sut.CreateRestaurant("XX", regionId);

			var success = Sut.ActivateRestaurant(restaurantId);
			var restaurant = Sut.GetRestaurant(restaurantId);

			success.ShouldBeTrue();
			restaurant.IsActive.ShouldBe(true);
		}

		[Fact]
		public void CannotActivateRestaurantWithoutActiveOffer()
		{
			var regionId = Sut.CreateRegion("PL");
			var restaurantId = Sut.CreateRestaurant("XX", regionId);

			var success = Sut.ActivateRestaurant(restaurantId);
			var restaurant = Sut.GetRestaurant(restaurantId);

			success.ShouldBeFalse();
			restaurant.IsActive.ShouldBe(false);
		}
	}
}
