using FoodOrdering.Common;

namespace FoodOrdering.Modules.Basket.Domain.ValueObjects
{
	public class Price : IValueObject
	{
		public decimal Value { get; private set; }

		public Price(decimal value)
		{
			if (Value < 0)
			{
				throw new AppException("Price cannot be below 0");
			}

			Value = value;
		}

		// Serialization requirement
		protected Price()
		{ }

		public static Price Zero => new((decimal)0);

		public Price LoweredBy(Price price)
		{
			if (Value - price.Value <= 0)
			{
				return Zero;
			}
			else
			{
				return new Price(Value - price.Value);
			}
		}

		public decimal ToDecimal() => Value;

		public static implicit operator Price(decimal value) => new(value);
		public static implicit operator decimal(Price price) => price.Value;

		public static Price operator +(Price lhs, Price rhs) => new(lhs.Value + rhs.Value);

		public static Price operator -(Price lhs, Price rhs) => lhs.LoweredBy(rhs);
		public static Price operator *(Price lhs, Quantity rhs) => new(lhs.Value * rhs.ToInt());
	}
}
