using Autofac;
using System;
using System.Linq;

namespace BaseProjectTemplate.CodingIntent.Infrastructure.Security
{
	public class IdentityModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<RegisteredUser>()
				.As<IRegisteredUser>()
				.InstancePerLifetimeScope();
		}
	}
}
