using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrdering.Common.Functional
{
	public static class ResultExtensions
	{
		//public static Result<TFailure, TResult> Select<TFailure, TSuccess, TResult>(this Result<TFailure, TSuccess> result, Func<TSuccess, TResult> selector)
		//{
		//	return result.Match<Result<TFailure, TResult>>(
		//		onSuccess: v => new Result(selector(v)),
		//		onFailure: v => v);
		//}
	}
}
