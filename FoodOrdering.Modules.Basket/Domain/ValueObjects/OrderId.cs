using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Common;

namespace FoodOrdering.Modules.Basket.Domain.ValueObjects
{
	record OrderId : IValueObject
	{
		private readonly Guid value;

		public OrderId(Guid value)
		{
			this.value = value;
		}

		public static OrderId New => new(Guid.NewGuid());
		public static implicit operator OrderId(Guid guid) => new(guid);

		public Guid ToGuid() => value;
	}
}
