using System.Threading.Tasks;
using FoodOrdering.Modules.Surveys.Entities;
using Raven.Client.Documents;
using Raven.Client.Documents.Operations;
using Raven.Client.Documents.Session;
using Raven.Client.Exceptions;
using Raven.Client.Exceptions.Database;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;

namespace FoodOrdering.Modules.Surveys.Repositories
{
	public class SurveysDocumentStore
	{
		private readonly IDocumentStore store;

		private SurveysDocumentStore(IDocumentStore store)
		{
			this.store = store;
		}

		public static SurveysDocumentStore Create(string url, string dbName)
		{
			var store = new DocumentStore
			{
				Urls = new[] { url },
				Database = dbName,
				Conventions =
				{
					FindIdentityProperty = m => m.Name == "_databaseId"
				}
			};

			store.Conventions.RegisterAsyncIdConvention<Survey>(
				(dbName, entity) => Task.FromResult("Survey/" + entity.Id.ToString()));
			store.Initialize();

			return new SurveysDocumentStore(store);
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
