using System.Security.Claims;
using System.Threading.Tasks;
using FoodOrdering.Modules.Auth.Contracts.DTO;
using FoodOrdering.Modules.Auth.Services;
using FoodOrdering.Web.Api.Requests.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrdering.Web.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly IAuthService authService;

		public AccountController(IAuthService authService)
		{
			this.authService = authService;
		}

		[Authorize]
		[HttpGet]
		public async Task<ActionResult<UserDTO>> GetCurrentUser()
		{
			var email = User.FindFirstValue(ClaimTypes.Email);
			return await authService.FindUserByEmail(email);
		}

		[HttpGet("emailexists")]
		public async Task<ActionResult<bool>> IsEmailTaken(string email)
		{
			return await authService.IsEmailTaken(email);
		}

		[HttpPost("login")]
		public async Task<ActionResult<UserDTO>> Login(string email, string password)
		{
			return await authService.Login(email, password);
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register(RegisterRequest request)
		{
			var error = await authService.Register(request.DisplayName, request.Email, request.Password);

			return error.Match<IActionResult>(
				e => new BadRequestObjectResult(new { Error = e.Message }), 
				() => { return Ok(); } );
		}
	}
}
