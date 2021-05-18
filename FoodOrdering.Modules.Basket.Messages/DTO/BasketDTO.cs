using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Modules.Basket.Messages.DTO;

namespace FoodOrdering.Modules.Basket.Contracts.DTO
{
	public record BasketDTO(
		IEnumerable<BasketItemDTO> BasketItems, 
		decimal TotalPrice, 
		IEnumerable<Guid> AppliedDiscounts);
}
