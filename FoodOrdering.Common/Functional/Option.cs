using System;

namespace FoodOrdering.Common.Functional
{
	public partial class Option<T>
	{
		private readonly IState state;

		public Option(T value) => state = new SomeState(value);

		public Option() => state = new NoneState();

		public static Option<T> None() => new();

		public T Value => state.Value;

		public bool HasValue => state.HasValue;

		public override bool Equals(object obj)
			=> state.Equals(obj);

		public override int GetHashCode()
			=> state.GetHashCode();

		public TResult Match<TResult>(Func<T, TResult> some, Func<TResult> none)
			=> state.Match(some, none);
	}
}
