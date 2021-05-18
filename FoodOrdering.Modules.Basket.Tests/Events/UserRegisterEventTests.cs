using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Modules.Auth.Contracts.Events;
using Xunit;

namespace FoodOrdering.Modules.Basket.Tests.Events
{
	public class UserRegisterEventTests : TestBase
	{
		[Fact]
		public async Task RegisteredUserCanAddItemsToBasket()
		{
			var userId = Guid.NewGuid();

			await Mediator.Publish(new UserRegisteredEvent(userId));
		}
	}
}
