using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raven.Identity;

namespace FoodOrdering.Modules.Auth.RavenDB.Entities
{
	public class AppUser : IdentityUser
	{
		public Guid UserId { get; set; }
		public string DisplayName { get; set; }
	}
}
