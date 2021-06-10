using System;
using FoodOrdering.Common;

namespace FoodOrdering.Modules.Basket.Domain.ValueObjects
{
	public record ClientId : IValueObject
	{
		public Guid Value { get; private set; }

		public ClientId(Guid value)
		{
			Value = value;
		}

		public Guid ToGuid() => Value;

		public override string ToString() => Value.ToString();

		public static implicit operator ClientId(Guid guid) => new(guid);
		public static implicit operator Guid(ClientId clientId) => clientId.Value;

		protected ClientId() { }
	}
}
