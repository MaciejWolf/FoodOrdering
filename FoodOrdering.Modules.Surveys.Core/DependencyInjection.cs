using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Modules.Surveys.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FoodOrdering.Modules.Surveys
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddSurveysModule(this IServiceCollection services)
		{
			//services.AddSingleton<ISurveyRepository, InMemorySurveyRepository>();
			services.AddRavenDbRepository();
			services.AddMediatR(Assembly.GetExecutingAssembly());

			return services;
		}

		private static void AddRavenDbRepository(this IServiceCollection services)
		{
			var store = SurveysDocumentStore.Create("http://localhost:8080", "FoodOrdering.Db.Surveys");
			store.EnsureDatabaseExists();

			services.AddSingleton(store);
			services.AddScoped<ISurveyRepository, SurveyRepository>();
		}
	}
}
