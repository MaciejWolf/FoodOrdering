using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodOrdering.Modules.Auth.RavenDB.Entities;
using Raven.Client.Documents;

namespace FoodOrdering.Web.Api.Extensions
{
	public static class RavenExtensions
	{
        public static IDocumentStore EnsureExists(this IDocumentStore store)
        {
            try
            {
                using var dbSession = store.OpenSession();
                dbSession.Query<AppUser>().Take(0).ToList();
            }
            catch (Raven.Client.Exceptions.Database.DatabaseDoesNotExistException)
            {
                store.Maintenance.Server.Send(new Raven.Client.ServerWide.Operations.CreateDatabaseOperation(new Raven.Client.ServerWide.DatabaseRecord
                {
                    DatabaseName = store.Database
                }));
            }

            return store;
        }
    }
}
