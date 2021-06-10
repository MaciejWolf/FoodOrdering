using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodOrdering.Modules.OrderProcessing.Contracts.Commands;
using FoodOrdering.Modules.OrderProcessing.Contracts.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrdering.Web.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[AllowAnonymous]
	public class OrderProcessingController : ControllerBase
	{
		private readonly ISender sender;

		public OrderProcessingController(ISender sender)
		{
			this.sender = sender;
		}

		[HttpGet]
		public async Task<IActionResult> GetActiveOrders()
		{
			var orders = await sender.Send(new GetActiveOrdersQuery());

			return Ok(orders);
		}

		[HttpPost]
		public async Task<IActionResult> CompleteOrder(Guid orderId)
		{
			await sender.Send(new CompleteOrderCommand { OrderId = orderId });

			return Ok();
		}
	}
}
