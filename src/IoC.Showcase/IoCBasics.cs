using DI_IoC.Library;
using DI_IoC.Library.LowLevel;
using DI_IoC.Library.LowLevel.LowerLevel;
using Lamar;
using NUnit.Framework;

namespace IoC.Showcase
{
	[TestFixture]
	public class IoCBasics
	{
		[Test]
		public void Resolve_ManualConfiguration_Instance()
		{
			var container = new Container(cfg =>
			{
				cfg.For<TopLevel>().Use<TopLevel>();
				cfg.For<IFoo>().Use<MaxFoo>();
				cfg.For<IBar>().Use<InMemoryBar>();
			});

			var top = container.GetInstance<TopLevel>();
			
			Assert.That(top.FooBar(), Is.EqualTo(-5));
		}
	}
}
