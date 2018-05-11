using System;
using System.Linq;
using Lamar.Scanning;
using Lamar.Scanning.Conventions;
using Microsoft.Extensions.DependencyInjection;

namespace IoC.Showcase.AutoWiring
{
	public class LibraryConvention : IRegistrationConvention
	{
		public void ScanTypes(TypeSet types, IServiceCollection services)
		{
			var classes = types.FindTypes(TypeClassification.Concretes | TypeClassification.Closed);
			foreach (Type @class in classes)
			{
				Type service = @class.GetInterfaces()
					.Where(i => i.Name.StartsWith("I", StringComparison.Ordinal))
					.FirstOrDefault(i => @class.Name.EndsWith(trimmedName(i)));

				if (service != null)
				{
					services.Add(new ServiceDescriptor(service, @class, ServiceLifetime.Transient));
				}
			}
		}

		private string trimmedName(Type @interface)
		{
			return @interface.Name.Substring(1);
		}
	}
}