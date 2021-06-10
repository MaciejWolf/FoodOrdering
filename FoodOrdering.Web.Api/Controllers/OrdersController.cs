using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FoodOrdering.Modules.Auth.Contracts;
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
	public class OrdersController : ControllerBase
	{
		private readonly ISender sender;
		private readonly IAuthService authService;

		public OrdersController(ISender sender, IAuthService authService)
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
		public async Task<IActionResult> GetOrder(Guid id)
		{
			var order = await sender.Send(new GetOrderQuery(id));

			return Ok(order);
		}

		[HttpPost("createorder")]
		public async Task<IActionResult> CreateOrder()
		{
			var userId = GetUserId();

			var orderId = await sender.Send(new CreateOrderCommand(userId));

			return Ok(orderId);
		}

		[HttpPost("placeorder")]
		public async Task<IActionResult> PlaceOrder(Guid orderId)
		{
			await sender.Send(new PlaceOrderCommand(orderId));

			return Ok();
		}
	}
}
