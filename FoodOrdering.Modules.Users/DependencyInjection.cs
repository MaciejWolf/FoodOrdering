using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Modules.Users.Entities;
using FoodOrdering.Modules.Users.Helpers;
using FoodOrdering.Modules.Users.Repositories;
using FoodOrdering.Modules.Users.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace FoodOrdering.Modules.Users
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddUsersModule(this IServiceCollection services)
		{
			services
				.AddSingleton<IUserRepository, InMemoryUserRepository>()
				.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>()
				.AddTransient<ITokenFactory, TokenFactory>()
				.AddTransient<IIdentityService, IdentityService>()
				.AddMediatR(Assembly.GetExecutingAssembly());

			return services;
		}
	}
}
