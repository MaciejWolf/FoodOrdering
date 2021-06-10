using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Common;
using FoodOrdering.Common.Functional;
using FoodOrdering.Modules.Auth.Contracts;
using FoodOrdering.Modules.Auth.Contracts.DTO;
using FoodOrdering.Modules.Auth.Contracts.Events;
using FoodOrdering.Modules.Auth.RavenDB.Entities;
using FoodOrdering.Modules.Auth.RavenDB.Helpers;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FoodOrdering.Modules.Auth.RavenDB.Services
{
	public class AuthService : IAuthService
	{
		private readonly UserManager<AppUser> userManager;
		private readonly SignInManager<AppUser> signInManager;
		private readonly ITokenFactory tokenFactory;
		private readonly IPublisher publisher;

		public AuthService(
			UserManager<AppUser> userManager, 
			SignInManager<AppUser> signInManager, 
			ITokenFactory tokenFactory, 
			IPublisher publisher)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
			this.tokenFactory = tokenFactory;
			this.publisher = publisher;
		}

		public async Task<bool> IsEmailTaken(string email)
		{
			return await userManager.FindByEmailAsync(email) != null;
		}

		public async Task<UserDTO> FindUserByEmail(string email)
		{
			var user = await userManager.FindByEmailAsync(email);

			if (user == null)
				return null;

			return new UserDTO(
				Id: user.UserId,
				Email: user.Email,
				DisplayName: user.DisplayName,
				Token: tokenFactory.CreateToken(user));
		}

		public async Task<UserDTO> Login(string email, string password)
		{
			if (await IsEmailTaken(email) is false)
			{
				throw new AppException("Email does not exists");
			}

			var user = await userManager.FindByEmailAsync(email);

			var result = await signInManager.CheckPasswordSignInAsync(user, password, false);

			if (!result.Succeeded)
			{
				throw new AppException("Unauthorized");
			}

			return new UserDTO(
				Id: user.UserId,
				Email: user.Email,
				DisplayName: user.DisplayName,
				Token: tokenFactory.CreateToken(user));
		}

		public async Task<Option<Error>> RegisterOrError(string displayName, string email, string password)
		{
			if (await FindUserByEmail(email) is not null)
			{
				return new Error("Email in use").AsOption();
			}

			var user = new AppUser
			{
				UserId = Guid.NewGuid(),
				DisplayName = displayName,
				Email = email,
				UserName = email
			};

			var result = await userManager.CreateAsync(user, password);

			if (!result.Succeeded)
			{
				//return BadRequest();
				return new Error("Creating user failed").AsOption();
			}

			await publisher.Publish(new UserRegisteredEvent(user.UserId));

			return Option<Error>.None();
		}

		public async Task Register(string displayName, string email, string password)
		{
			if (await FindUserByEmail(email) is not null)
			{
				throw new AppException("Email in use");
			}

			var user = new AppUser
			{
				UserId = Guid.NewGuid(),
				DisplayName = displayName,
				Email = email,
				UserName = email
			};

			var result = await userManager.CreateAsync(user, password);

			if (!result.Succeeded)
			{
				throw new AppException("Creating user failed");
			}

			await publisher.Publish(new UserRegisteredEvent(user.UserId));
		}
	}
}
