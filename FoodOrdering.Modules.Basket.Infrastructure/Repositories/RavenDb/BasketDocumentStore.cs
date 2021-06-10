using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Modules.Basket.Application.ViewModels;
using FoodOrdering.Modules.Basket.Domain.Models;
using FoodOrdering.Modules.Basket.Domain.Models.Order;
using Raven.Client.Documents;
using Raven.Client.Documents.Operations;
using Raven.Client.Documents.Session;
using Raven.Client.Exceptions;
using Raven.Client.Exceptions.Database;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;

namespace FoodOrdering.Modules.Basket.Infrastructure.Repositories.RavenDb
{
	public class BasketDocumentStore
	{
		private readonly IDocumentStore store;

		private BasketDocumentStore(IDocumentStore store)
		{
			this.store = store;
		}

		public static BasketDocumentStore Create(string url, string dbName)
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

			store.Conventions.RegisterAsyncIdConvention<Coupon>(
				(dbName, entity) => Task.FromResult("Coupon/" + entity.Id.ToString()));

			store.Conventions.RegisterAsyncIdConvention<Domain.Models.Order.OrderAggregate>(
				(dbName, entity) => Task.FromResult("Order/" + entity.Id.ToString()));

			store.Conventions.RegisterAsyncIdConvention<OrderDescription>(
				(dbName, entity) => Task.FromResult("OrderDescription/" + entity.Id.ToString()));

			store.Conventions.RegisterAsyncIdConvention<Product>(
				(dbName, entity) => Task.FromResult("Product/" + entity.Id.ToString()));

			store.Conventions.RegisterAsyncIdConvention<BasketVm>(
				(dbName, entity) => Task.FromResult("BasketVm/" + entity.Id.ToString()));

			store.Conventions.RegisterAsyncIdConvention<CouponVm>(
				(dbName, entity) => Task.FromResult("CouponVm/" + entity.Id.ToString()));

			store.Initialize();

			return new BasketDocumentStore(store);
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
