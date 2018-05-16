using System;
using DI_IoC.Library.LowLevel;
using DI_IoC.Library.LowLevel.LowerLevel;
using Lamar;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace IoC.Showcase.Delayed
{
	[TestFixture]
	public class DelayedTester
	{
		[Test]
		public void DelegatedDependency_AutomaticallyConstructed()
		{
			var container = new Container(cfg => cfg.AddScoped<IBar>(_ =>
			{
				var bar = new InMemoryBar();
				return bar;
			}));

			var func = container.GetInstance<Func<IBar>>();

			func().Bar();
			func().Bar();

			func = container.GetInstance<Func<IBar>>();
			func().Bar();

			var nested = container.GetNestedContainer();
			
			func = nested.GetInstance<Func<IBar>>();
			func().Bar();
			func().Bar();
		}

		[Test]
		public void LazyDependency_AutomaticallyConstructedOnce()
		{
			var container = new Container(cfg => cfg.AddScoped<IBar>(_ =>
			{
				var bar = new InMemoryBar();
				return bar;
			}));

			var lazy = container.GetInstance<Lazy<IBar>>();

			lazy.Value.Bar();
			lazy.Value.Bar();

			lazy = container.GetInstance<Lazy<IBar>>();
			lazy.Value.Bar();

			var nested = container.GetNestedContainer();
			
			lazy = nested.GetInstance<Lazy<IBar>>();
			lazy.Value.Bar();
			lazy.Value.Bar();
		}
	}
}