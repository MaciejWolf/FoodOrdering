using FoodOrdering.Modules.Auth;
using FoodOrdering.Modules.Auth.RavenDB;
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
		{
			services.AddRavenDBAuthModule(configuration);
			//services.AddEfCoreAuthModule(configuration);
			services.AddBasketModule();
			services.AddCatalogModule();
			services.AddOrderProcessingModule();
			services.AddSurveysModule();
			services.AddCouponsModule();
			return services;
		}
	}
}
