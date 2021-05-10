using System;
using System.Collections.Generic;

namespace FoodOrdering.Modules.Users.Contracts.Commands
{
	public class SignUpCommand
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public Dictionary<string, IEnumerable<string>> Claims { get; set; }
    }
}
