using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Common.Time;
using FoodOrdering.Modules.Basket.Contracts.Queries;
using FoodOrdering.Modules.Basket.Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace FoodOrdering.Modules.Basket.Tests.Queries
{
	public class DummyQueryTests
	{
		[Fact]
		public async Task Test()
		{
			var services = new ServiceCollection();

			services.AddBasketModule();
			services.AddSingleton(_ => Mock.Of<IClock>());

			var sp = services.BuildServiceProvider();

			var mediator = sp.GetRequiredService<IMediator>();

			var response = await mediator.Send(new DummyQuery());

			Assert.Equal("Great!", response);
		}
	}
}
