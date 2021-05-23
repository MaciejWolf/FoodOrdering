using FoodOrdering.Modules.Basket.Domain.Models.Order;
using FoodOrdering.Modules.Basket.Domain.Models.ValueObjects;

namespace FoodOrdering.Modules.Basket.Domain.Repositories
{
	public interface IOrdersRepository
	{
		void Save(OrderAggregate order);
		OrderAggregate GetById(OrderId id);
	}
}
