using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Modules.Basket.Domain.Repositories;
using FoodOrdering.Modules.Basket.Infrastructure.Repositories;
using FoodOrdering.Modules.Basket.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FoodOrdering.Modules.Basket.Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddBasketModule(this IServiceCollection services)
		{
			services.AddMediatR(Assembly.GetExecutingAssembly());
			services.AddPersistence();

			return services;
		}

		private static IServiceCollection AddPersistence(this IServiceCollection services)
		{
			services.AddSingleton<IBasketsRepository, InMemoryBasketsRepository>();
			services.AddSingleton<IOrdersRepository, InMemoryOrdersRepository>();
			services.AddSingleton<IProductsRepository, InMemoryProductsRepository>();
			services.AddSingleton<IUsedCouponsRepository, InMemoryUsedCouponsRepository>();

			return services;
		}
	}
}
