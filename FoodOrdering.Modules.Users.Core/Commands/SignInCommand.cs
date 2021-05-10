using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrdering.Modules.Users.Contracts.Commands
{
	public record SignInCommand(string Email, string Password);
}
