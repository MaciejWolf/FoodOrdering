using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrdering.Modules.Catalog.Core.Entities
{
	class Meal
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public Guid OfferId { get; set; }
	}
}
