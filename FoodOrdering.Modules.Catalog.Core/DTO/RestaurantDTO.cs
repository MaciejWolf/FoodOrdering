using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrdering.Modules.Catalog.Core.DTO
{
	public record RestaurantDTO(Guid Id, string Name, Guid RegionId, bool IsActive, Guid[] Offers);
}
