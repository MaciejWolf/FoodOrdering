using System;
using FoodOrdering.Modules.Basket.Core.Entities;

namespace FoodOrdering.Modules.Basket.Core.Repositories
{
	public interface IUsersRepository
	{
		void Save(User user);
		User Get(Guid id);
		void Update(User user);
	}
}
