using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace FoodOrdering.Modules.OrderProcessing.Contracts.Events
{
	public class OrderCompletedEvent : INotification
	{
		public Guid OrderId { get; set; }
		public Guid ClientId { get; set; }
	}
}
