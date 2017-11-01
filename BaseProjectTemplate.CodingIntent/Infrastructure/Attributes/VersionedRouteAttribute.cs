using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Linq;

namespace BaseProjectTemplate.CodingIntent.Infrastructure.Attributes
{

	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
	public class VersionedRouteAttribute : Attribute, IRouteTemplateProvider
	{
		private const string BaseRoute = "health/v{0}/{1}";
		private readonly int _version;
		private readonly string _template;

		public VersionedRouteAttribute(string template, int version)
		{
			if (template == null)
				throw new ArgumentNullException(nameof(template));
			if (template.EndsWith("/"))
				template = template.Substring(0, template.LastIndexOf('/'));
			_template = string.Format(BaseRoute, version, template);
			_version = version;
		}

		public string Template => _template;

		public int? Order => null;

		public string Name => _template;

		public int Version => _version;
	}
}
