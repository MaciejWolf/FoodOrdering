using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodOrdering.Modules.Catalog.Contracts.Commands;
using FoodOrdering.Modules.Catalog.Contracts.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrdering.Web.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[AllowAnonymous]
	public class CatalogController : ControllerBase
	{
		private readonly ISender sender;

		public CatalogController(ISender sender)
		{
			this.sender = sender;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var result = await sender.Send(new GetAllMealsQuery());
			return Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> CreateMeal(CreateMealCommand cmd)
		{
			var result = await sender.Send(cmd);
			return Ok(result);
		}
	}
}
