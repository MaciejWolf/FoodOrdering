using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Common;

namespace FoodOrdering.Modules.Basket.Domain.ValueObjects
{
	public record Quantity : IValueObject
	{
		public int Value { get; private set; }

		public Quantity(int value)
		{
			if (value < 0)
				throw new AppException();

			Value = value;
		}

		public static Quantity Zero => new(0);

		public static implicit operator Quantity(int value) => new(value);
		public static implicit operator int(Quantity quantity) => quantity.Value;

		public static bool operator <(Quantity lhs, Quantity rhs) => lhs < rhs;
		public static bool operator >(Quantity lhs, Quantity rhs) => lhs > rhs;

		public int ToInt() => Value;

		protected Quantity() { }
	}
}
