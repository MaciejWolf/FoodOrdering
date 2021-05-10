using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrdering.Modules.Auth.Contracts.DTO
{
	public record UserDTO(Guid Id, string Email, string DisplayName, string Token);
}
