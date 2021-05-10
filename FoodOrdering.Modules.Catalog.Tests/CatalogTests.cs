using Shouldly;
using Xunit;

namespace FoodOrdering.Modules.Catalog.Tests
{
	public class CatalogTests
	{
		//private CatalogService Sut { get; }

		//public CatalogTests()
		//{
		//	// Arrange
		//	Sut = new CatalogService();
		//}

		//[Fact]
		//public void CanCreateRegion()
		//{
		//	// Act
		//	var id = Sut.CreateRegion("PL");

		//	// Assert
		//	var region = Sut.GetRegion(id);

		//	region.Name.ShouldBe("PL");
		//}

		//[Fact]
		//public void CanCreateRestaurantInRegion()
		//{
		//	// Arrange
		//	var regionId = Sut.CreateRegion("PL");

		//	// Act
		//	var restaurantId = Sut.CreateRestaurant("PL05", regionId);

		//	// Assert
		//	var restaurant = Sut.GetRestaurant(restaurantId);

		//	restaurant.Name.ShouldBe("PL05");
		//	restaurant.RegionId.ShouldBe(regionId);
		//	restaurant.IsActive.ShouldBeFalse();
		//}

		//[Fact]
		//public void CanCreateOfferInRegion()
		//{
		//	// Arrange
		//	var regionId = Sut.CreateRegion("PL");

		//	// Act
		//	var offerId = Sut.CreateOffer("RegularOffer", regionId);

		//	// Assert
		//	var offer = Sut.GetOffer(offerId);

		//	offer.Name.ShouldBe("RegularOffer");
		//	offer.RegionId.ShouldBe(regionId);
		//	offer.IsActive.ShouldBeFalse();
		//}

		//[Fact]
		//public void CanAddMealToOffer()
		//{
		//	// Arrange
		//	var regionId = Sut.CreateRegion("PL");
		//	var offerId = Sut.CreateOffer("RegularOffer", regionId);

		//	// Act
		//	var mealId = Sut.AddMeal(offerId, "Hamburger");

		//	// Assert
		//	var meal = Sut.GetMeal(mealId);

		//	meal.Name.ShouldBe("Hamburger");
		//	meal.OfferId.ShouldBe(offerId);
		//}

		//[Fact]
		//public void CanActivateOfferWithAtLeastOneMeal()
		//{
		//	// Arrange
		//	var regionId = Sut.CreateRegion("PL");
		//	var offerId = Sut.CreateOffer("RegularOffer", regionId);
		//	Sut.AddMeal(offerId, "Hamburger");

		//	// Act
		//	var success = Sut.ActivateOffer(offerId);

		//	// Assert
		//	var offer = Sut.GetOffer(offerId);

		//	success.ShouldBeTrue();
		//	offer.IsActive.ShouldBeTrue();
		//}

		//[Fact]
		//public void CannotActivateOfferWithoutMeals()
		//{
		//	// Arrange
		//	var regionId = Sut.CreateRegion("PL");
		//	var offerId = Sut.CreateOffer("RegularOffer", regionId);

		//	// Act
		//	var success = Sut.ActivateOffer(offerId);

		//	// Assert
		//	var offer = Sut.GetOffer(offerId);
		//	success.ShouldBeFalse();
		//	offer.IsActive.ShouldBe(false);
		//}

		//[Fact]
		//public void CanAddOfferToRestaurantInTheSameRegion()
		//{
		//	// Arrange
		//	var regionId = Sut.CreateRegion("PL");
		//	var offerId = Sut.CreateOffer("RegularOffer", regionId);
		//	var restaurantId = Sut.CreateRestaurant("RestaurantName", regionId);

		//	// Act
		//	var success = Sut.AddOfferToRestaurant(offerId, restaurantId);

		//	// Assert
		//	var restaurant = Sut.GetRestaurant(restaurantId);

		//	success.ShouldBeTrue();
		//	restaurant.Offers.ShouldContain(offerId);
		//}

		//[Fact]
		//public void CannotAddOfferToRestaurantFromAnotherRegion()
		//{
		//	// Arrange
		//	var regionId = Sut.CreateRegion("PL");
		//	var anotherRegionId = Sut.CreateRegion("US");

		//	var offerId = Sut.CreateOffer("RegularOffer", regionId);
		//	var restaurantId = Sut.CreateRestaurant("RestaurantName", anotherRegionId);

		//	// Act
		//	var success = Sut.AddOfferToRestaurant(offerId, restaurantId);

		//	// Assert
		//	var restaurant = Sut.GetRestaurant(restaurantId);

		//	success.ShouldBeFalse();
		//	restaurant.Offers.ShouldNotContain(offerId);
		//}

		//[Fact]
		//public void CanActivateRestaurantWithAtLeastOneActiveOffer()
		//{
		//	// Arrange
		//	var regionId = Sut.CreateRegion("PL");
		//	var restaurantId = Sut.CreateRestaurant("XX", regionId);
		//	var offerId = Sut.CreateOffer("RegularOffer", regionId);
		//	Sut.AddOfferToRestaurant(offerId, restaurantId);
		//	Sut.AddMeal(offerId, "Hamburger");
		//	Sut.ActivateOffer(offerId);

		//	// Act
		//	var success = Sut.ActivateRestaurant(restaurantId);

		//	// Assert
		//	var restaurant = Sut.GetRestaurant(restaurantId);

		//	success.ShouldBeTrue();
		//	restaurant.IsActive.ShouldBe(true);
		//}

		//[Fact]
		//public void CannotActivateRestaurantWithoutActiveOffer()
		//{
		//	// Arrange
		//	var regionId = Sut.CreateRegion("PL");
		//	var restaurantId = Sut.CreateRestaurant("XX", regionId);

		//	// Act
		//	var success = Sut.ActivateRestaurant(restaurantId);

		//	// Assert
		//	var restaurant = Sut.GetRestaurant(restaurantId);

		//	success.ShouldBeFalse();
		//	restaurant.IsActive.ShouldBe(false);
		//}
	}
}
