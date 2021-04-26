using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrdering.Modules.Catalog.Core.Entities
{
	class Restaurant
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public Guid RegionId { get; set; }
		public bool IsActive { get; set; }
		public List<Guid> Offers { get; set; } = new();

		public void AddOffer(Guid offerId)
		{
			Offers.Add(offerId);
		}
	}
}
