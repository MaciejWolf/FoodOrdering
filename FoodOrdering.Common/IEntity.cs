using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrdering.Common
{
	public interface IEntity<T>
	{
		T Id { get; }
	}
}
