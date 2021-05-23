using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FoodOrdering.Modules.Catalog.Contracts.Queries;
using FoodOrdering.Modules.Catalog.Handlers.Queries;
using FoodOrdering.Modules.Catalog.Models;
using FoodOrdering.Modules.Catalog.Repositories;
using Shouldly;
using Xunit;

namespace FoodOrdering.Modules.Catalog.Tests.Handlers.Queries
{
	public class GetAllMealsQueryHandlerTests
	{
		private IMealsRepository mealsRepository = new InMemoryMealsRepository();

		private GetAllMealsQueryHandler Handler => new GetAllMealsQueryHandler(mealsRepository);

		[Fact]
		public async Task ReturnsAllMeals()
		{
			// Arrange
			var hamburger = new Meal { Name = "Hamburger", Price = 4.99m };
			var hotdog = new Meal { Name = "HotDog", Price = 3.99m };

			await mealsRepository.AddAsync(hamburger);
			await mealsRepository.AddAsync(hotdog);

			// Act
			var result = await Handler.Handle(new GetAllMealsQuery(), new CancellationToken());

			// Assert
			result.Count.ShouldBe(2);
			result.Single(m => m.Id == hamburger.Id && m.Name == "Hamburger" && m.Price == 4.99m);
			result.Single(m => m.Id == hotdog.Id && m.Name == "HotDog" && m.Price == 3.99m);
		}
	}
}
