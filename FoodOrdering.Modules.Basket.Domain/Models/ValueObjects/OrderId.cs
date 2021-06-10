using System;
using FoodOrdering.Common;

namespace FoodOrdering.Modules.Basket.Domain.Models.ValueObjects
{
	public record OrderId : IValueObject
	{
		public Guid Value { get; private set; }

	public OrderId(Guid value)
	{
		Value = value;
	}

		public static OrderId New => new(Guid.NewGuid());
		public static implicit operator OrderId(Guid guid) => new(guid);

		public Guid ToGuid() => Value;

		public override string ToString() => Value.ToString();

		protected OrderId()
		{

		}
	}
}
