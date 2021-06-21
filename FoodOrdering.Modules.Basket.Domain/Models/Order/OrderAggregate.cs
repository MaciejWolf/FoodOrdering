using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Common;
using FoodOrdering.Modules.Basket.Domain.Models.ValueObjects;
using FoodOrdering.Modules.Basket.Domain.ValueObjects;

namespace FoodOrdering.Modules.Basket.Domain.Models.Order
{
	public class OrderAggregate : AggregateRoot<OrderId>
	{
		public DateTime ValidTo { get; private set; }
		public ClientId ClientId { get; private set; }

		public OrderAggregate(OrderId id, ClientId clientId, DateTime createdAt)
		{
			Id = id;
			ClientId = clientId;
			ValidTo = createdAt.AddMinutes(5);
		}

		public static OrderAggregate Create(ClientId clientId, DateTime createdAt) 
			=> new(Guid.NewGuid(), clientId, createdAt);

		public bool IsPlaced { get; private set; }

		public void PlaceOrder(DateTime placingTime)
		{
			if (IsPlaced || placingTime > ValidTo)
			{
				throw new AppException("Order expired");
			}

			IsPlaced = true;
		}

		public override void ApplyEvent(IEvent evnt)
		{
			throw new NotImplementedException();
		}

		protected OrderAggregate() { }
	}
}
