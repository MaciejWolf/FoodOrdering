using System;
using System.Reflection;
using System.Text;
using FoodOrdering.Modules.Auth.Contracts;
using FoodOrdering.Modules.Auth.RavenDB.Entities;
using FoodOrdering.Modules.Auth.RavenDB.Helpers;
using FoodOrdering.Modules.Auth.RavenDB.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Raven.DependencyInjection;
using Raven.Identity;

namespace FoodOrdering.Modules.Auth.RavenDB
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddRavenDBAuthModule(this IServiceCollection services, IConfiguration config)
		{
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<ITokenFactory, TokenFactory>();
            services.AddIdentityServices(config);
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
		}

        private static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            var builder = services
                .AddRavenDbDocStore()
                .AddRavenDbAsyncSession()
                .AddIdentityCore<AppUser>(options =>
                {
                    options.Password.RequiredLength = 6;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireDigit = false;
                });

            builder = new IdentityBuilder(builder.UserType, builder.Services);
            builder.AddRavenDbIdentityStores<AppUser>();
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
