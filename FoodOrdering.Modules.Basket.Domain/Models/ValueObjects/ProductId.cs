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
		private readonly Guid value;

		public ProductId(Guid value)
		{
			this.value = value;
		}

		public Guid ToGuid() => value;

		public static implicit operator ProductId(Guid guid) => new(guid);
		public static implicit operator Guid(ProductId productId) => productId.value;
	}
}
