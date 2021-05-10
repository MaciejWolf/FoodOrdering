using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Modules.Users.Entities;

namespace FoodOrdering.Modules.Users.Repositories
{
	class InMemoryUserRepository : IUserRepository
	{
		private List<User> users = new();

		public async Task AddAsync(User user)
		{
			users.Add(user);
		}

		public async Task<User> GetAsync(Guid id)
		{
			return users.SingleOrDefault(u => u.Id == id);
		}

		public async Task<User> GetAsync(string email)
		{
			return users.SingleOrDefault(u => u.Email == email);
		}

		public async Task UpdateAsync(User user)
		{
			var toRemove = users.Single(u => u.Id == user.Id);
			users.Remove(toRemove);
			users.Add(user);
		}
	}
}
