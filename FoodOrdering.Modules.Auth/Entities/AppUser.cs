using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace FoodOrdering.Modules.Auth.Entities
{
	class AppUser : IdentityUser<Guid>
	{
		public string DisplayName { get; set; }
	}
}
