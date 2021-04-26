using Xunit;

namespace FoodOrdering.Modules.Catalog.Tests
{
	public class CatalogTests
	{
		private CatalogService Sut { get; }

		public CatalogTests()
		{
			CatalogService = new CatalogService();
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
		}

		[Fact]
		public void CanCreateOfferInRegion()
		{
			var regionId = Sut.CreateRegion("PL");

			var offerId = Sut.CreateOffer("RegularOffer", regionId);
			var offer = Sut.GetOffer(offerId);

			offer.Name.ShouldBe("RegularOffer");
		}

		[Fact]
		public void CanAddMealToOffer()
		{
			var regionId = Sut.CreateRegion("PL");
			var offerId = Sut.CreateOffer("RegularOffer", regionId);

			var mealId = Sut.AddMeal(offerId, "Hamburger");
			var meal = Sut.GetMeal(mealId);

			meal.Name.ShouldBe("Hamburger");
		}

		[Fact]
		public void CanActivateOfferWithAtLeastOneMeal()
		{
			var regionId = Sut.CreateRegion("PL");
			var offerId = Sut.CreateOffer("RegularOffer", regionId);

			var error = Sut.ActivateOffer(offerId);
			var offer = Sut.GetOffer(offerId);

			error.Failure.HasValue.ShouldBe(false);
			offer.IsActive.ShouldBe(true);
		}

		[Fact]
		public void CannotActivateOfferWithoutMeals()
		{
			var regionId = Sut.CreateRegion("PL");
			var offerId = Sut.CreateOffer("RegularOffer", regionId);

			var error = Sut.ActivateOffer(offerId);

			var offer = Sut.GetOffer(offerId);
			error.Failure.HasValue.ShouldBe(true);
			offer.IsActive.ShouldBe(false);
		}

		[Fact]
		public void CanActivateRestaurantWithAtLeastOneActiveOffer()
		{
			var regionId = Sut.CreateRegion("PL");
			var restaurantId = Sut.CreateRestaurant("XX");

			var error = Sut.ActivateRestaurant(restaurantId);
			var restaurant = Sut.GetRestaurant(restaurantId);

			error.Failure.HasValue.ShouldBe(false);
			restaurant.IsActive.ShouldBe(false);
		}

		[Fact]
		public void CannotActivateRestaurantWithoutActiveOffer()
		{
			var regionId = Sut.CreateRegion("PL");
			var restaurantId = Sut.CreateRestaurant("XX");

			var error = Sut.ActivateRestaurant(restaurantId);
			var restaurant = Sut.GetRestaurant(restaurantId);

			error.Failure.HasValue.ShouldBe(true);
			restaurant.IsActive.ShouldBe(true);
		}
	}
}
