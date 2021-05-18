using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Common;

namespace FoodOrdering.Modules.Basket.ValueObjects
{
	record CouponId : IValueObject
	{
		private readonly Guid value;

		public CouponId(Guid value)
		{
			this.value = value;
		}

		public static implicit operator CouponId(Guid guid) => new(guid);

		public Guid ToGuid() => value;
	}
}
