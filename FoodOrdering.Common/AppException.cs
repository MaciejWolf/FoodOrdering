using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrdering.Common
{
	public class AppException : Exception
	{
		public AppException(): base()
		{

		}

		public AppException(string message): base(message)
		{

		}
	}
}
