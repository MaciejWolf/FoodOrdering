using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Modules.Catalog.Models;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;

namespace FoodOrdering.Modules.Catalog.Repositories
{
	public class MealsRepository : IMealsRepository
	{
		private readonly MealsDocumentStore documentStore;

		public MealsRepository(MealsDocumentStore documentStore)
		{
			this.documentStore = documentStore;
		}

		public async Task AddAsync(Meal meal)
		{
			using var session = documentStore.OpenSession();
			session.Store(meal);
			session.SaveChanges();
		}

		public async Task<Meal[]> GetAll()
		{
			using var session = documentStore.OpenSession();
			return session.Query<Meal>().ToArray();
		}

		public async Task<Meal> GetById(Guid id)
		{
			using var session = documentStore.OpenSession();
			return session.Query<Meal>().Single(m => m.Id == id);
		}

		public void Update(Meal meal, Action<Meal> updateOperation)
		{
			throw new NotImplementedException();
		}

		public void Update(Guid mealId, Action<Meal> updateOperation)
		{
			using var session = documentStore.OpenSession();
			var meal = session.Query<Meal>().Single(m => m.Id == mealId);
			updateOperation(meal);
			session.SaveChanges();
		}
	}
}
