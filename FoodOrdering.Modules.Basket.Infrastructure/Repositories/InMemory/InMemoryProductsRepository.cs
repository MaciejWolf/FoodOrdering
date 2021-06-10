using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Common;
using FoodOrdering.Modules.Basket.Domain.Models;
using FoodOrdering.Modules.Basket.Domain.Repositories;
using FoodOrdering.Modules.Basket.Domain.ValueObjects;

namespace FoodOrdering.Modules.Basket.Infrastructure.Repositories.InMemory
{
	public class InMemoryProductsRepository : IProductsRepository
	{
		private readonly List<Product> products = new();

		public Product GetById(ProductId productId)
		{
			return products.SingleOrDefault(p => p.Id == productId);
		}

		public void Save(Product product)
		{
			if (products.Any(p => p.Id == product.Id))
			{
				throw new AppException($"Basket with {product.Id} already exists");
			}

			products.Add(product);
		}
	}
}
