using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Common;

namespace FoodOrdering.Modules.Basket.Domain.ValueObjects
{
	record Price : IValueObject
	{
		private readonly decimal value;

		public Price(decimal value)
		{
			if (value < 0)
			{
				throw new AppException("Price cannot be below 0");
			}

			this.value = value;
		}

		public static Price Zero => new(0);

		public decimal ToDecimal() => value;

		public static implicit operator Price(decimal value) => new(value);

		public static Price operator +(Price lhs, Price rhs) => new(lhs.value + rhs.value);

		public Price WithDiscount(Discount discount)
		{
			return new Price(value * discount.ToDecimal());
		}
	}
}
