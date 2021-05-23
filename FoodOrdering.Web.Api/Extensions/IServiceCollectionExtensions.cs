using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodOrdering.Modules.Auth;
using FoodOrdering.Modules.Basket.Infrastructure;
using FoodOrdering.Modules.Catalog;
using FoodOrdering.Modules.Coupons;
using FoodOrdering.Modules.OrderProcessing;
using FoodOrdering.Modules.Surveys;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FoodOrdering.Web.Api.Extensions
{
	public static class IServiceCollectionExtensions
	{
		public static IServiceCollection AddModules(this IServiceCollection services, IConfiguration configuration)
			=> services
				.AddAuthModule(configuration)
				.AddBasketModule()
				.AddCatalogModule()
				.AddOrderProcessingModule()
				.AddSurveysModule()
				.AddCouponsModule();
	}
}
