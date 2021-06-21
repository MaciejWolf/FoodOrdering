using FoodOrdering.Common;
using FoodOrdering.Web.Api.Contexts;
using FoodOrdering.Web.Api.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Raven.Client.Documents;

namespace FoodOrdering.Web.Api
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{

			services.AddControllers();

			services.AddCommons();

			services.AddModules(Configuration);

			services.AddSingleton<IContextFactory, ContextFactory>();
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddTransient(sp => sp.GetRequiredService<IContextFactory>().Create());

			services.AddSwaggerDocumentation();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwaggerDocumentation();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			var docStore = app.ApplicationServices.GetRequiredService<IDocumentStore>();
			docStore.EnsureExists();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
