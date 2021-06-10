using System;
using FoodOrdering.Modules.Basket.Domain.Models.Order;

namespace FoodOrdering.Modules.Basket.Domain.Repositories
{
	public interface IOrderDescriptionsRepository
	{
		OrderDescription GetById(Guid orderId);
		void Save(OrderDescription description);
	}
}
