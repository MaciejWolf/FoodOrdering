using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Common;

namespace FoodOrdering.Modules.Basket.Domain.ValueObjects
{
	public record CouponId : IValueObject
	{
		public Guid Value { get; private set; }

		public CouponId(Guid value)
		{
			Value = value;
		}

		public static implicit operator CouponId(Guid guid) => new(guid);
		public static implicit operator Guid(CouponId couponId) => couponId.Value;

		public Guid ToGuid() => Value;

		public override string ToString() => Value.ToString();

		protected CouponId() { }
	}
}
