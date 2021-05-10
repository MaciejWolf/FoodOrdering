using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Common.Functional;
using FoodOrdering.Modules.Auth.Contracts.DTO;

namespace FoodOrdering.Modules.Auth.Services
{
	public interface IAuthService
	{
		Task<UserDTO> FindUserByEmail(string email);
		Task<UserDTO> Login(string email, string password);
		Task<Option<Error>> Register(string displayName, string email, string password);
		Task<bool> IsEmailTaken(string email);
	}
}
