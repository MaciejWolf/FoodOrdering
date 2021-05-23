using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Modules.OrderProcessing.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FoodOrdering.Modules.OrderProcessing
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddOrderProcessingModule(this IServiceCollection services)
		{
			services.AddSingleton<IOrdersRepository, InMemoryOrdersRepository>();
			services.AddMediatR(Assembly.GetExecutingAssembly());

			return services;
		}
	}
}
