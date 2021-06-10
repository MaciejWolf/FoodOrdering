using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Modules.Basket.Domain.Models;
using FoodOrdering.Modules.Basket.Domain.Repositories;
using FoodOrdering.Modules.Basket.Domain.ValueObjects;

namespace FoodOrdering.Modules.Basket.Infrastructure.Repositories.RavenDb
{
	class ProductsRepository : IProductsRepository
	{
		private readonly BasketDocumentStore documentStore;

		public ProductsRepository(BasketDocumentStore documentStore)
		{
			this.documentStore = documentStore;
		}

		public Product GetById(ProductId id)
		{
			using var session = documentStore.OpenSession();
			return session.Query<Product>().Single(m => m.Id == id);
		}

		public void Save(Product product)
		{
			using var session = documentStore.OpenSession();
			session.Store(product);
			session.SaveChanges();
		}
	}
}
