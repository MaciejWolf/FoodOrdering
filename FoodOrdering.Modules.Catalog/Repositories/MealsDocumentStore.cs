using System.Threading.Tasks;
using FoodOrdering.Modules.Catalog.Models;
using Raven.Client.Documents;
using Raven.Client.Documents.Operations;
using Raven.Client.Documents.Session;
using Raven.Client.Exceptions;
using Raven.Client.Exceptions.Database;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;

namespace FoodOrdering.Modules.Catalog.Repositories
{
	public class MealsDocumentStore
	{
		private readonly IDocumentStore store;

		private MealsDocumentStore(IDocumentStore store)
		{
			this.store = store;
		}

		public static MealsDocumentStore Create(string url, string dbName)
		{
			var store = new DocumentStore
			{
				Urls = new[] { url },
				Database = dbName,
				Conventions =
				{
					FindIdentityProperty = m => m.Name == "_databeseId"
				}
			};
			store.Conventions.RegisterAsyncIdConvention<Meal>(
				(dbName, entity) => Task.FromResult("Meal/" + entity.Id.ToString()));
			store.Initialize();

			return new MealsDocumentStore(store);
		}

		public void EnsureDatabaseExists(bool createDatabaseIfNotExists = true)
		{
			try
			{
				store.Maintenance.ForDatabase(store.Database).Send(new GetStatisticsOperation());
			}
			catch (DatabaseDoesNotExistException)
			{
				if (createDatabaseIfNotExists == false)
					throw;

				try
				{
					store.Maintenance.Server.Send(new CreateDatabaseOperation(new DatabaseRecord(store.Database)));
				}
				catch (ConcurrencyException)
				{
					// The database was already created before calling CreateDatabaseOperation
				}

			}
		}

		public IDocumentSession OpenSession()
			=> store.OpenSession();
	}
}
