using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Modules.Auth.RavenDB.Entities;

namespace FoodOrdering.Modules.Auth.RavenDB.Helpers
{
	public interface ITokenFactory
	{
		string CreateToken(AppUser user);
	}
}
