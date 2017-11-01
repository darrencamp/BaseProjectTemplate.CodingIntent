using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseProjectTemplate.CodingIntent.Infrastructure.Swagger
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	public class SwaggerGroupAttribute : Attribute
	{
		public string GroupName { get; set; }

		public SwaggerGroupAttribute(string groupName)
		{
			if (string.IsNullOrEmpty(groupName))
			{
				throw new ArgumentNullException("groupName");
			}
			GroupName = groupName;
		}
	}
}
