using FoodOrdering.Modules.Basket.Application;
using FoodOrdering.Modules.Basket.Domain.Repositories;
using FoodOrdering.Modules.Basket.Infrastructure.Repositories.InMemory;
using Microsoft.Extensions.DependencyInjection;

namespace FoodOrdering.Modules.Basket.Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddBasketModule(this IServiceCollection services)
		{
			services.AddSingleton<IBasketsRepository, InMemoryBasketsRepository>();
			services.AddSingleton<IProductsRepository, InMemoryProductsRepository>();
			services.AddSingleton<ICouponsRepository, InMemoryCouponsRepository>();
			services.AddSingleton<IOrdersRepository, InMemoryOrdersRepository>();

			services.AddApplication();

			return services;
		}
	}
}
