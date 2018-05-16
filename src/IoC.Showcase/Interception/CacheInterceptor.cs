using System;
using System.Collections.Generic;
using Castle.DynamicProxy;

namespace IoC.Showcase.Interception
{
	public class CacheInterceptor : IInterceptor
	{
		private readonly Dictionary<int, string> _cache;

		public CacheInterceptor()
		{
			_cache = new Dictionary<int, string>();
		}

		public void Intercept(IInvocation invocation)
		{
			if (invocation.Method.Name.Equals(nameof(IRepository.Get), StringComparison.Ordinal))
			{
				int key = (int) invocation.Arguments[0];
				if (!_cache.TryGetValue(key, out string value))
				{
					invocation.Proceed();
					value = (string) invocation.ReturnValue;
					_cache[key] = value;
				}

				invocation.ReturnValue = value;
			}
			// other methods are executed with no proxy intervention
			else
			{
				invocation.Proceed();
			}
		}
	}
}