using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrdering.Modules.Catalog.Core.DTO
{
	public record OfferDTO(Guid Id, string Name, Guid RegionId, bool IsActive);
}
