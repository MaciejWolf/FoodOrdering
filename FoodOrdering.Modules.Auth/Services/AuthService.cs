using System;
using System.Threading.Tasks;
using FoodOrdering.Common.Functional;
using FoodOrdering.Modules.Auth.Contracts.DTO;
using FoodOrdering.Modules.Auth.Entities;
using FoodOrdering.Modules.Auth.Helpers;
using Microsoft.AspNetCore.Identity;

namespace FoodOrdering.Modules.Auth.Services
{
	class AuthService : IAuthService
	{
		private readonly UserManager<AppUser> userManager;
		private readonly SignInManager<AppUser> signInManager;
		private readonly ITokenFactory tokenFactory;

		public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenFactory tokenFactory)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
			this.tokenFactory = tokenFactory;
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
				Id: user.Id,
				Email: user.Email,
				DisplayName: user.DisplayName,
				Token: tokenFactory.CreateToken(user));
		}

		public async Task<UserDTO> Login(string email, string password)
		{
			if (await IsEmailTaken(email) is false)
			{
				//return Unauthorized();
				throw new Exception("Email does not exists");
			}

			var user = await userManager.FindByEmailAsync(email);

			var result = await signInManager.CheckPasswordSignInAsync(user, password, false);

			if (!result.Succeeded)
			{
				//return Unauthorized();
				throw new Exception("Unauthorized");
			}

			return new UserDTO(
				Id: user.Id,
				Email: user.Email,
				DisplayName: user.DisplayName,
				Token: tokenFactory.CreateToken(user));
		}

		public async Task<Option<Error>> Register(string displayName, string email, string password)
		{
			if (await FindUserByEmail(email) is not null)
			{
				return new Error("Email in use").AsOption();
			}

			var user = new AppUser
			{
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

			return Option<Error>.None();
		}
	}
}
