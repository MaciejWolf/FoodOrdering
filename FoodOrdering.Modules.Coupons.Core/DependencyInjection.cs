using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FoodOrdering.Modules.Coupons
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddCouponsModule(this IServiceCollection services)
		{
			services.AddMediatR(Assembly.GetExecutingAssembly());

			return services;
		}
	}
}
