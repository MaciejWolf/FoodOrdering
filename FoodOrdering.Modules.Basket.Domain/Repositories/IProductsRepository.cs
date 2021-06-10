using FoodOrdering.Modules.Basket.Domain.Models;
using FoodOrdering.Modules.Basket.Domain.ValueObjects;

namespace FoodOrdering.Modules.Basket.Domain.Repositories
{
	public interface IProductsRepository
	{
		Product GetById(ProductId product);

		void Save(Product product);
	}
}
