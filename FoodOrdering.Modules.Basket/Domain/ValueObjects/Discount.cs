using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Common;

namespace FoodOrdering.Modules.Basket.Domain.ValueObjects
{
	record Discount : IValueObject
	{
		private readonly double value;

		private Discount(double value)
		{
			if (value < 0 || value > 1)
			{
				throw new AppException("Invalid discount value");
			}

			this.value = value;
		}

		public static Discount Percent(int percent) => new(percent / 100);

		public double ToDouble() => value;
		public decimal ToDecimal() => (decimal)value;
	}
}
