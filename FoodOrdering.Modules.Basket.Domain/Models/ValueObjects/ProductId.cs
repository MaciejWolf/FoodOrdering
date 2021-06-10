using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Common;

namespace FoodOrdering.Modules.Basket.Domain.ValueObjects
{
	public record ProductId : IValueObject
	{
		public Guid Value { get; private set; }

		public ProductId(Guid value)
		{
			this.Value = value;
		}

		public Guid ToGuid() => Value;

		public override string ToString() => Value.ToString();

		public static implicit operator ProductId(Guid guid) => new(guid);
		public static implicit operator Guid(ProductId productId) => productId.Value;

		protected ProductId() { }
	}
}
