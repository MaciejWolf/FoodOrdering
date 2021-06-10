using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Common.EventStore;
using FoodOrdering.Common.EventStore.RavenDb;
using FoodOrdering.Common.Time;
using Microsoft.Extensions.DependencyInjection;
using Raven.Client.Documents;
using Raven.Client.Documents.Operations;
using Raven.Client.Exceptions;
using Raven.Client.Exceptions.Database;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;

namespace FoodOrdering.Common
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddCommons(this IServiceCollection services)
		{
			services.AddTransient<IClock, UtcClock>();
			services.AddInMemoryEventStore();
			//services.AddRavenDbEventStore();

			return services;
		}

		public static void AddInMemoryEventStore(this IServiceCollection services)
		{
			services.AddSingleton<IEventStore, InMemoryEventStore>();
		}

		public static void AddRavenDbEventStore(this IServiceCollection services)
		{
			var store = new DocumentStore
			{
				Urls = new[] { "http://localhost:8080" },
				Database = "FoodOrdering.Db.EventStore"
			}.Initialize();

			EnsureDatabaseExists(store);

			services.AddScoped(c => store.OpenSession());
			services.AddScoped<IEventStore, RavenDbEventStore>();
		}

		private static void EnsureDatabaseExists(IDocumentStore store, string database = null, bool createDatabaseIfNotExists = true)
		{
			database ??= store.Database;

			if (string.IsNullOrWhiteSpace(database))
				throw new ArgumentException("Value cannot be null or whitespace.", nameof(database));

			try
			{
				store.Maintenance.ForDatabase(database).Send(new GetStatisticsOperation());
			}
			catch (DatabaseDoesNotExistException)
			{
				if (createDatabaseIfNotExists == false)
					throw;

				try
				{
					store.Maintenance.Server.Send(new CreateDatabaseOperation(new DatabaseRecord(database)));
				}
				catch (ConcurrencyException)
				{
					// The database was already created before calling CreateDatabaseOperation
				}

			}
		}
	}
}
