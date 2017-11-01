using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace BaseProjectTemplate.CodingIntent.Infrastructure.Security
{
	public static class Auth0SecurityExtensions
	{
		public static void AddSecurityServices(this IServiceCollection services, IConfiguration configuration)
		{
			var domain = $"https://{configuration["Auth0:Domain"]}/";
			services
				.AddAuthentication(options =>
				{
					options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
					options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				})
				.AddJwtBearer(options =>
				{
					options.Authority = domain;
					options.Audience = configuration["Auth0:ApiIdentifier"];
				});

			services.AddAuthorization(options =>
			{
				options.AddPolicy("api:user", policy => policy.Requirements.Add(new HasScopeRequirement("api:user", domain)));
				options.AddPolicy("api:service", policy => policy.Requirements.Add(new HasScopeRequirement("api:service", domain)));
			});
		}
	}
}
