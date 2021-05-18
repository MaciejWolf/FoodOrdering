using System;
using FoodOrdering.Common;

namespace FoodOrdering.Modules.Basket.ValueObjects
{
	record ProductId : IValueObject
	{
		private readonly Guid value;

		public ProductId(Guid value)
		{
			this.value = value;
		}

		public Guid ToGuid() => value;

		public static implicit operator ProductId(Guid guid) => new(guid);
		public static implicit operator Guid(ProductId productId) => productId.value;
		public static ProductId New => new(Guid.NewGuid());
	}
}
