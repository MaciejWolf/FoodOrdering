using System;

namespace FoodOrdering.Common.Functional
{
	public partial class Option<T>
	{
		private interface IState
		{
			T Value { get; }
			bool HasValue { get; }
			TResult Match<TResult>(Func<T, TResult> some, Func<TResult> none);
		}
	}
}
