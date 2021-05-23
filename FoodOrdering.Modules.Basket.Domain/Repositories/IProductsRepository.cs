using FoodOrdering.Modules.Basket.Domain.Models.Product;
using FoodOrdering.Modules.Basket.Domain.ValueObjects;

namespace FoodOrdering.Modules.Basket.Domain.Repositories
{
	public interface IProductsRepository
	{
		ProductAggregate GetById(ProductId product);

		void Save(ProductAggregate productAggregate);
	}
}
