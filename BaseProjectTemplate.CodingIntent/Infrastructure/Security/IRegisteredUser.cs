using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BaseProjectTemplate.CodingIntent.Infrastructure.Security
{
	public interface IRegisteredUser
	{
		string Identifier { get; }
		bool IsAuthenticated { get; }
	}
}
