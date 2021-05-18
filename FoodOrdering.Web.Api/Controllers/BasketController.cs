using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FoodOrdering.Modules.Auth.Services;
using FoodOrdering.Modules.Basket.Contracts.Commands;
using FoodOrdering.Modules.Basket.Contracts.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrdering.Web.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BasketController : ControllerBase
	{
		private readonly ISender sender;
		private readonly IAuthService authService;

		public BasketController(ISender sender, IAuthService authService)
		{
			this.sender = sender;
			this.authService = authService;
		}

		private Guid GetUserId()
		{
			var email = User.FindFirstValue(ClaimTypes.Email);
			var user = authService.FindUserByEmail(email).Result;
			return user.Id;
		}

		[HttpGet]
		public async Task<IActionResult> GetBasket()
		{
			var id = GetUserId();

			var basket = await sender.Send(new GetBasketQuery(id));

			return basket != null ? Ok(basket) : NotFound();
		}

		[HttpPost("additem")]
		public async Task<IActionResult> ChangeItemsQuantity(Guid itemId, int quantity)
		{
			var userId = GetUserId();

			await sender.Send(new ChangeItemsQuantityCommand(userId, itemId, quantity));
			return NoContent();
		}
	}
}
