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
			services.AddSingleton<IMealsRepository, InMemoryMealsRepository>();
			services.AddMediatR(Assembly.GetExecutingAssembly());

			return services;
		}
	}
}
