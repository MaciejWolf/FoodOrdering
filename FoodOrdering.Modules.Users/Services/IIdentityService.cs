using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoodOrdering.Common.Time;
using FoodOrdering.Modules.Users.Contracts.Commands;
using FoodOrdering.Modules.Users.Contracts.DTO;
using FoodOrdering.Modules.Users.Contracts.Events;
using FoodOrdering.Modules.Users.Entities;
using FoodOrdering.Modules.Users.Helpers;
using FoodOrdering.Modules.Users.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FoodOrdering.Modules.Users.Services
{
	public interface IIdentityService
	{
		Task<AccountDTO> GetAsync(Guid id);
		Task<JsonWebTokenDTO> SignInAsync(SignInCommand dto);
		Task SignUpAsync(SignUpCommand dto);
	}

	public class IdentityService : IIdentityService
	{
		private readonly IUserRepository userRepository;
		private readonly IPasswordHasher<User> passwordHasher;
		private readonly ITokenFactory tokenFactory;
		private readonly IClock clock;
		private readonly IPublisher publisher;

		public IdentityService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, ITokenFactory tokenFactory, IClock clock, IPublisher publisher)
		{
			this.userRepository = userRepository;
			this.passwordHasher = passwordHasher;
			this.tokenFactory = tokenFactory;
			this.clock = clock;
			this.publisher = publisher;
		}

		public async Task<AccountDTO> GetAsync(Guid id)
		{
			var user = await userRepository.GetAsync(id);

			return user is null
				? null
				: new AccountDTO
				{
					Id = user.Id,
					Email = user.Email,
					Role = user.Role,
					Claims = user.Claims,
					CreatedAt = user.CreatedAt
				};
		}

		public async Task<JsonWebTokenDTO> SignInAsync(SignInCommand dto)
		{
			var user = await userRepository.GetAsync(dto.Email.ToLowerInvariant());
			if (user is null)
			{
				//throw new InvalidCredentialsException();
				throw new Exception("Invalid credentials");
			}

			if (passwordHasher.VerifyHashedPassword(default, user.Password, dto.Password) ==
				PasswordVerificationResult.Failed)
			{
				//throw new InvalidCredentialsException();
				throw new Exception("Invalid credentials");
			}

			if (!user.IsActive)
			{
				//throw new UserNotActiveException(user.Id);
				throw new Exception("User not active");
			}

			var jwt = tokenFactory.CreateToken(user.Id.ToString(), user.Role, claims: user.Claims);
			jwt.Email = user.Email;
			await publisher.Publish(new UserSignedInEvent(user.Id));

			return ToDTO(jwt);
		}

		public async Task SignUpAsync(SignUpCommand dto)
		{
			var email = dto.Email.ToLowerInvariant();
			var user = await userRepository.GetAsync(email);
			if (user is not null)
			{
				//throw new EmailInUseException();
				throw new Exception("Email in use");
			}

			var password = passwordHasher.HashPassword(default, dto.Password);
			user = new User
			{
				Id = Guid.NewGuid(),
				Email = email,
				Password = password,
				Role = dto.Role?.ToLowerInvariant() ?? "user",
				CreatedAt = clock.Now,
				IsActive = true,
				Claims = dto.Claims ?? new Dictionary<string, IEnumerable<string>>()
			};
			await userRepository.AddAsync(user);
			await publisher.Publish(new UserSignedUpEvent(user.Id, user.Email));
		}

		private static JsonWebTokenDTO ToDTO(JsonWebToken jwt)
		{
			return new JsonWebTokenDTO
			{
				AccessToken = jwt.AccessToken,
				RefreshToken = jwt.RefreshToken,
				Expires = jwt.Expires,
				Id = jwt.Id,
				Role = jwt.Role,
				Email = jwt.Email,
				Claims = jwt.Claims
			};
		}
	}
}
