using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Modules.OrderProcessing.Entities;

namespace FoodOrdering.Modules.OrderProcessing.Repositories
{
	public interface IOrdersRepository
	{
		void Save(Order order);
		Order GetById(Guid orderId);
		IEnumerable<Order> GetAll();
		void Update(Guid orderId, Action<Order> updateOperation);
	}
}
