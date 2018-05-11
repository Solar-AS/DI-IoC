using System;
using System.Collections.Generic;
using CommonServiceLocator;
using Microsoft.Extensions.DependencyInjection;

namespace DI_IoC
{
	public class LocatorAdapter : ServiceLocatorImplBase
	{
		private readonly IServiceProvider _provider;

		public LocatorAdapter(IServiceProvider provider)
		{
			_provider = provider;
		}

		protected override object DoGetInstance(Type serviceType, string key)
		{
			return _provider.GetService(serviceType);
		}

		protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
		{
			return _provider.GetServices(serviceType);
		}
	}
}