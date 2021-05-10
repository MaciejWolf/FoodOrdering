using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace FoodOrdering.Modules.Catalog.Core.Events
{
	public class MealBecameAvailableEvent : INotification
	{
		public Guid MealId { get; set; }
	}
}
