using System;

namespace FoodOrdering.Common.Functional
{
	public partial class Option<T>
	{
		private class NoneState : IState
		{
			public T Value => throw new InvalidOperationException("Option has no value");

			public bool HasValue => false;

			public TResult Match<TResult>(Func<T, TResult> some, Func<TResult> none) => none();

			public override bool Equals(object other)
				=> other is Option<T> option && option.state is NoneState;

			public override int GetHashCode() => 0;
		}
	}
}
