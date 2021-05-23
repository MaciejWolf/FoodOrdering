using System;
using FoodOrdering.Common;

namespace FoodOrdering.Modules.Basket.Domain.ValueObjects
{
	public record ClientId : IValueObject
	{
		private readonly Guid value;

		public ClientId(Guid value)
		{
			this.value = value;
		}

		public Guid ToGuid() => value;

		public static implicit operator ClientId(Guid guid) => new(guid);
	}
}
