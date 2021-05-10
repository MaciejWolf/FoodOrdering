using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Modules.Users.Entities;

namespace FoodOrdering.Modules.Users.Repositories
{
	public interface IUserRepository
	{
		Task<User> GetAsync(Guid id);
		Task<User> GetAsync(string email);
		Task AddAsync(User user);
		Task UpdateAsync(User user);
	}
}
