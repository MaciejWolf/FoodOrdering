using System.Linq;
using System.Threading.Tasks;
using FoodOrdering.Common.TestUtilities;
using FoodOrdering.Modules.Catalog.Contracts.Commands;
using FoodOrdering.Modules.Catalog.Contracts.Events;
using FoodOrdering.Modules.Catalog.Handlers.Commands;
using FoodOrdering.Modules.Catalog.Repositories;
using Shouldly;
using Xunit;

namespace FoodOrdering.Modules.Catalog.Tests.Handlers.Commands
{
	public class CreateMealCommandHandlerTests
	{
		private PublisherMock publisherMock = new PublisherMock();
		private IMealsRepository mealsRepository = new InMemoryMealsRepository();

		private CreateMealCommandHandler Handler => new(mealsRepository, publisherMock.Object);

		[Fact]
		public async Task MealIsPersistedInRepository()
		{
			var cmd = new CreateMealCommand("Hamburger", 4.99m);
			var handler = Handler;

			var id = await handler.Handle(cmd, new System.Threading.CancellationToken());

			var result = await mealsRepository.GetById(id);

			result.Name.ShouldBe("Hamburger");
			result.Price.ShouldBe(4.99m);
		}

		[Fact]
		public async Task WhenMealIsCreated_MealBecameAvailableEventIsPublished()
		{
			var cmd = new CreateMealCommand("Hamburger", 4.99m);
			var handler = Handler;

			var id = await handler.Handle(cmd, new System.Threading.CancellationToken());

			var result = await mealsRepository.GetById(id);

			var evnt = publisherMock.PublishedNotifications
				.OfType<MealBecameAvailableEvent>()
				.Single(e => e.Id == id && e.Name == "Hamburger" && e.Price == 4.99m);
		}
	}
}
