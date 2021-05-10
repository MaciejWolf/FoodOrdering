using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrdering.Common.Functional
{
	public class Either<L, R>
	{
		private readonly L leftValue;
		private readonly R rightValue;

		public Either(L leftValue)
		{
			if (leftValue is null)
				throw new ArgumentNullException();

			this.leftValue = leftValue;
			IsRight = false;
		}

		public Either(R rightValue)
		{
			if (rightValue is null)
				throw new ArgumentNullException();

			this.rightValue = rightValue;
			IsRight = true;
		}

		public bool IsRight { get; }
		public bool IsLeft => !IsRight;

		public Option<L> Left => leftValue.AsOption();
		public Option<R> Right => rightValue.AsOption();

		public T Match<T>(Func<L, T> onLeft, Func<R, T> onRight)
		{
			return IsRight ? onRight(rightValue) : onLeft(leftValue);
		}
	}
}
