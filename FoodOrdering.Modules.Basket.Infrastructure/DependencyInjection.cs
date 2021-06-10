using FoodOrdering.Modules.Basket.Application;
using FoodOrdering.Modules.Basket.Domain.Repositories;
using FoodOrdering.Modules.Basket.Infrastructure.Repositories;
using FoodOrdering.Modules.Basket.Infrastructure.Repositories.InMemory;
using FoodOrdering.Modules.Basket.Infrastructure.Repositories.RavenDb;
using Microsoft.Extensions.DependencyInjection;

namespace FoodOrdering.Modules.Basket.Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddBasketModule(this IServiceCollection services)
		{
			//services.AddSingleton<IBasketsRepository, InMemoryBasketsRepository>();
			services.AddScoped<IBasketsRepository, BasketsRepository>();

			//services.AddSingleton<IProductsRepository, InMemoryProductsRepository>();
			//services.AddSingleton<ICouponsRepository, InMemoryCouponsRepository>();
			//services.AddSingleton<IOrdersRepository, InMemoryOrdersRepository>();
			//services.AddSingleton<IViewModelsRepository, InMemoryViewModelsRepository>();
			//services.AddSingleton<IOrderDescriptionsRepository, InMemoryOrderDescriptionsRepository>();

			var store = BasketDocumentStore.Create("http://localhost:8080", "FoodOrdering.Db.Basket");
			store.EnsureDatabaseExists();

			services.AddSingleton(store);
			services.AddScoped<IProductsRepository, ProductsRepository>();
			services.AddScoped<ICouponsRepository, CouponsRepository>();
			services.AddScoped<IOrdersRepository, OrdersRepository>();
			services.AddScoped<IViewModelsRepository, ViewModelsRepository>();
			services.AddScoped<IOrderDescriptionsRepository, OrderDescriptionsRepository>();

			services.AddApplication();

			return services;
		}
	}
}
