using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Common;

namespace FoodOrdering.Modules.Basket.ValueObjects
{
	record Quantity : IValueObject
	{
		private readonly int value;

		public Quantity(int value)
		{
			if (value < 0)
				throw new Exception();

			this.value = value;
		}

		public static Quantity Zero => new(0);

		public static implicit operator Quantity(int value) => new(value);

		public static bool operator <(Quantity lhs, Quantity rhs) => lhs < rhs;
		public static bool operator >(Quantity lhs, Quantity rhs) => lhs > rhs;

		public int ToInt() => value;
	}
}
