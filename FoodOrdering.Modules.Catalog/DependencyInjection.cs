using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Modules.Catalog.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FoodOrdering.Modules.Catalog
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddCatalogModule(this IServiceCollection services)
		{
			services.AddRavenDbRepository();
			//services.AddInMemoryRepository();

			services.AddMediatR(Assembly.GetExecutingAssembly());

			return services;
		}

		private static void AddRavenDbRepository(this IServiceCollection services)
		{
			var store = MealsDocumentStore.Create("http://localhost:8080", "FoodOrdering.Db.Catalog");
			store.EnsureDatabaseExists();

			services.AddSingleton(store);
			services.AddScoped<IMealsRepository, MealsRepository>();
		}

		private static void AddInMemoryRepository(this IServiceCollection services)
		{
			services.AddSingleton<IMealsRepository, InMemoryMealsRepository>();
		}
	}
}
