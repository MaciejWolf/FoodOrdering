using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrdering.Modules.Basket.Contracts.DTO
{
	public record OrderDTO(IEnumerable<OrderItemDTO> OrderItems, Guid? UsedCoupon, decimal Price, DateTime ValidTo);
}
