using Autofac;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;

namespace BaseProjectTemplate.CodingIntent.Infrastructure.Security
{
	public class RegisteredUser : IRegisteredUser
	{
		public RegisteredUser(IHttpContextAccessor context)
		{
			var claimsIdentity = context.HttpContext.User.Identity as ClaimsIdentity;

			if (claimsIdentity == null) return;

			Identifier = claimsIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
			IsAuthenticated = context.HttpContext.User.Identity.IsAuthenticated;
		}

		public string Identifier { get; }
		public bool IsAuthenticated { get; }
	}
}
