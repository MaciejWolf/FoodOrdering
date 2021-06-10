using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FoodOrdering.Modules.Auth.Contracts;
using FoodOrdering.Modules.Auth.Services;
using FoodOrdering.Modules.Surveys.Contracts.Commands;
using FoodOrdering.Modules.Surveys.Contracts.Queries;
using FoodOrdering.Web.Api.Requests.Surveys;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrdering.Web.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SurveysController : ControllerBase
	{
		private readonly ISender sender;
		private readonly IAuthService authService;

		public SurveysController(ISender sender, IAuthService authService)
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
		public async Task<IActionResult> GetActiveSurveys()
		{
			return Ok(await sender.Send(new GetActiveSurveysQuery(UserId)));
		}

		[HttpGet("survey/{id}")]
		public async Task<IActionResult> GetSurvey(Guid id)
		{
			return Ok(await sender.Send(new GetSurveyQuery(id)));
		}

		[HttpPost("/completesurvey")]
		public async Task<IActionResult> CompleteSurvey(CompleteSurveyCommand cmd)
		{
			return Ok(await sender.Send(cmd));
		}
	}
}
