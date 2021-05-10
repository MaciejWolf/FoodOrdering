using System;
using System.Collections.Generic;

namespace FoodOrdering.Common.Functional
{
	public class Result<T>
	{
		private readonly Either<Error[], T> either;

		public Result(params Error[] errors)
		{
			either = new Either<Error[], T>(errors);
		}

		public Result(T value)
		{
			either = new Either<Error[], T>(value);
		}

		public bool IsSuccess => either.IsRight;
		public bool IsFailure => either.IsLeft;

		public Option<T> AsOption() => either.Right;
		public Option<Error[]> Errors => either.Left;

		public TResult Match<TResult>(Func<T, TResult> onSuccess, Func<Error[], TResult> onFailure) 
			=> either.Match(onFailure, onSuccess);
	}
}
