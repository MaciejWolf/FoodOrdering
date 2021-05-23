using System;

namespace FoodOrdering.Common
{
	public abstract class Entity : Entity<Guid>
	{
		public Entity(Guid id) : base(id)
		{

		}
	}

	public abstract class Entity<T> : IEntity<T>
	{
		public T Id { get; }

		protected Entity(T id)
		{
			Id = id;
		}
	}
}
