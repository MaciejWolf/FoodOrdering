using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace FoodOrdering.Common
{
	public abstract class AggregateRoot<T> : IEntity<T>
	{
		public T Id { get; protected set; }
    }
}
