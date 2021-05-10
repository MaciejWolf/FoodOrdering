using System.Text;
using FoodOrdering.Modules.Auth.DbContext;
using FoodOrdering.Modules.Auth.Entities;
using FoodOrdering.Modules.Auth.Helpers;
using FoodOrdering.Modules.Auth.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace FoodOrdering.Modules.Auth
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddAuthModule(this IServiceCollection services, IConfiguration config)
		{
			services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<ITokenFactory, TokenFactory>();
            services.AddIdentityServices(config);
            services.AddDbContext<AppIdentityDbContext>(x =>
            {
				x.UseInMemoryDatabase("InMemoryIdentityDatabase");
				//x.UseNpgsql(config.GetConnectionString("IdentityConnection"));
			});
			return services;
		}

		private static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
		{
            var builder = services.AddIdentityCore<AppUser>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
            });

            builder = new IdentityBuilder(builder.UserType, builder.Services);
            builder.AddEntityFrameworkStores<AppIdentityDbContext>();
            builder.AddSignInManager<SignInManager<AppUser>>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Token:Key"])),
                        ValidIssuer = config["Token:Issuer"],
                        ValidateIssuer = true,
                        ValidateAudience = false
                    };
                });

            return services;
        }
	}
}
