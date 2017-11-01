using BaseProjectTemplate.CodingIntent.Infrastructure.Security;
using BaseProjectTemplate.CodingIntent.Infrastructure.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace BaseProjectTemplate.CodingIntent
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
			services.AddMvc();
			services.AddOptions();

			services.AddSecurityServices(Configuration);

			services.AddSwaggerGen(c =>
			{
				var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;

				var commentsFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, assemblyName + ".xml");
				c.IncludeXmlComments(commentsFile);

				c.TagActionsBy(api =>
				{
					var attributes = api.ControllerAttributes().Union(api.ActionAttributes()).OfType<SwaggerGroupAttribute>();
					return attributes.Any() ? attributes.First().GroupName : api.GroupName;
				});

				c.AddSecurityDefinition("oauth2", new OAuth2Scheme
				{
					Type = "oauth2",
					Flow = "implicit",
					TokenUrl = $"https://{Configuration["Auth0:Domain"]}/oauth/token",
					AuthorizationUrl = $"https://{Configuration["Auth0:Domain"]}/authorize",
					Scopes = new Dictionary<string, string> {
						{ "api:user", "User api" },
						{ "api:service", "Service api" },
						{ "openid", "Open Id" },
						{ "profile", "Profile" }
					}
				});

				c.SwaggerDoc("v1", new Info { Title = "42 Buckets", Version = "v1" });
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseAuthentication();

			app.UseMvc();
			app.UseSwagger();

			app.UseSwaggerUI(c =>
			{
				c.ShowRequestHeaders();
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "42 Buckets API");
				c.ConfigureOAuth2(
					Configuration["Auth0ClientId"],
					Configuration["Auth0ClientSecret"],
					string.Empty,
					Configuration["Auth0:ApiIdentifier"],
					" ",
					new { Audience = Configuration["Auth0:ApiIdentifier"] }
				);
			});
		}
	}
}
