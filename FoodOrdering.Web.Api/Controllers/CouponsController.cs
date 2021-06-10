using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FoodOrdering.Modules.Auth.Contracts;
using FoodOrdering.Modules.Basket.Contracts.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrdering.Web.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CouponsController : ControllerBase
	{
		private readonly ISender sender;
		private readonly IAuthService authService;

		public CouponsController(ISender sender, IAuthService authService)
		{
			this.sender = sender;
			this.authService = authService;
		}

		private Guid UserId
		{
			get
			{
				var email = User.FindFirstValue(ClaimTypes.Email);
				var user = authService.FindUserByEmail(email).Result;
				return user.Id;
			}
		}

		[HttpGet]
		public async Task<IActionResult> GetAvailableCoupons()
		{
			return Ok(await sender.Send(new GetCouponsQuery(UserId)));
		}
	}
}
