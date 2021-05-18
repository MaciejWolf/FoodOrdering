using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Common;

namespace FoodOrdering.Modules.Basket.ValueObjects
{
	record ClientId : IValueObject
	{
		private readonly Guid value;

		public ClientId(Guid value)
		{
			this.value = value;
		}

		public Guid ToGuid() => value;

		public static implicit operator ClientId(Guid guid) => new(guid);
	}
}
