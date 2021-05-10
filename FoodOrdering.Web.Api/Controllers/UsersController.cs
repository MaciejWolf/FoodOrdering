using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodOrdering.Modules.Users.Contracts.Commands;
using FoodOrdering.Modules.Users.Contracts.DTO;
using FoodOrdering.Modules.Users.Services;
using FoodOrdering.Web.Api.Contexts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrdering.Web.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IIdentityService identityService;
        private readonly IContext context;

		//public UsersController(IIdentityService identityService, IContext context)
		//{
		//	this.identityService = identityService;
		//	this.context = context;
		//}

		[HttpGet]
        [Authorize]
        public async Task<ActionResult<AccountDTO>> GetAsync()
            => OkOrNotFound(await identityService.GetAsync(context.Identity.Id));

        [HttpPost("sign-up")]
        public async Task<ActionResult> SignUpAsync(SignUpCommand dto)
        {
            await identityService.SignUpAsync(dto);
            return NoContent();
        }

        [HttpPost("sign-in")]
        public async Task<ActionResult<JsonWebTokenDTO>> SignInAsync(SignInCommand dto)
            => Ok(await identityService.SignInAsync(dto));

        private ActionResult<T> OkOrNotFound<T>(T model)
        {
            if (model is not null)
            {
                return Ok(model);
            }

            return NotFound();
        }
    }
}
