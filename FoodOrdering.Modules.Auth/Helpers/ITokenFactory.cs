using System.Linq;
using System.Threading.Tasks;
using FoodOrdering.Modules.Auth.Entities;

namespace FoodOrdering.Modules.Auth.Helpers
{
	interface ITokenFactory
	{
		string CreateToken(AppUser user);
	}
}
