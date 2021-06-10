using System.Threading.Tasks;
using FoodOrdering.Common.Functional;
using FoodOrdering.Modules.Auth.Contracts.DTO;

namespace FoodOrdering.Modules.Auth.Contracts
{
	public interface IAuthService
	{
		Task<UserDTO> FindUserByEmail(string email);
		Task<UserDTO> Login(string email, string password);
		Task Register(string displayName, string email, string password);
		Task<Option<Error>> RegisterOrError(string displayName, string email, string password);
		Task<bool> IsEmailTaken(string email);
	}
}
