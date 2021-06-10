using System.Threading.Tasks;
using FoodOrdering.Modules.OrderProcessing.Entities;
using Raven.Client.Documents;
using Raven.Client.Documents.Operations;
using Raven.Client.Documents.Session;
using Raven.Client.Exceptions;
using Raven.Client.Exceptions.Database;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;

namespace FoodOrdering.Modules.OrderProcessing.Repositories
{
	public class OrderProcessingDocumentStore
	{
		private readonly IDocumentStore store;

		private OrderProcessingDocumentStore(IDocumentStore store)
		{
			this.store = store;
		}

		public static OrderProcessingDocumentStore Create(string url, string dbName)
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

			store.Conventions.RegisterAsyncIdConvention<Order>(
				(dbName, entity) => Task.FromResult("Order/" + entity.Id.ToString()));
			store.Initialize();

			return new OrderProcessingDocumentStore(store);
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
