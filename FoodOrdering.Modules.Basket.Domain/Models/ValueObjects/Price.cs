using FoodOrdering.Common;

namespace FoodOrdering.Modules.Basket.Domain.ValueObjects
{
	public record Price : IValueObject
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

		public Price LoweredBy(Price price)
		{
			if (value - price.value <= 0)
			{
				return Zero;
			}
			else
			{
				return new Price(value - price.value);
			}
		}

		public decimal ToDecimal() => value;

		public static implicit operator Price(decimal value) => new(value);

		public static Price operator +(Price lhs, Price rhs) => new(lhs.value + rhs.value);

		public static Price operator -(Price lhs, Price rhs) => lhs.LoweredBy(rhs);
		public static Price operator *(Price lhs, Quantity rhs) => new(lhs.value * rhs.ToInt());
	}
}
