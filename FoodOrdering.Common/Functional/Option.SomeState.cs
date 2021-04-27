using System;

namespace FoodOrdering.Common.Functional
{
	public partial class Option<T>
	{
		private class SomeState : IState
		{
			public SomeState(T value) => Value = value ?? throw new NullReferenceException("Value cannot be null");

			public T Value { get; }

			public bool HasValue => true;

			public TResult Match<TResult>(Func<T, TResult> some, Func<TResult> none) => some(Value);

			public override bool Equals(object other)
				=> other is Option<T> some && Equals(some);

			private bool Equals(Option<T> other)
				=> other.state is SomeState && (ReferenceEquals(this, other) || Value.Equals(other.Value));

			public override int GetHashCode() => HashCode.Combine(Value);
		}
	}
}
