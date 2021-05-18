using System;
using FoodOrdering.Modules.Basket.Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FoodOrdering.Modules.Basket.Tests
{
	public abstract class TestBase
	{
		protected IServiceProvider serviceProvider;
		protected ServiceCollection services;

		public TestBase()
		{
			services.AddBasketModule();
			serviceProvider = services.BuildServiceProvider();
		}

		protected IMediator Mediator => serviceProvider.GetRequiredService<IMediator>();
	}
}
