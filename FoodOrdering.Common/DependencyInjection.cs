using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Common.Time;
using Microsoft.Extensions.DependencyInjection;

namespace FoodOrdering.Common
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddCommons(this IServiceCollection services)
		{
			services.AddTransient<IClock, UtcClock>();
			return services;
		}
	}
}
