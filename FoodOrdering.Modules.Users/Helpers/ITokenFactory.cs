using System.Collections.Generic;
using System.Threading.Tasks;
using FoodOrdering.Modules.Users.Entities;

namespace FoodOrdering.Modules.Users.Helpers
{
	public interface ITokenFactory
	{
		JsonWebToken CreateToken(string userId, string role = null, string audience = null,
			IDictionary<string, IEnumerable<string>> claims = null);
	}
}
