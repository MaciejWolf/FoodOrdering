using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrdering.Common.Time
{
	public interface IClock
	{
		DateTime Now { get; }
	}

	class UtcClock : IClock
	{
		public DateTime Now => DateTime.UtcNow;
	}
}
