using DI_IoC.Library;
using DI_IoC.Library.LowLevel;
using DI_IoC.Library.LowLevel.LowerLevel;
using Lamar;
using NUnit.Framework;

namespace IoC.Showcase.Factories
{
	[TestFixture]
	public class FactoriesTester
	{
		[Test]
		public void Context_CanBeUser_WhenConfiguringObjects()
		{
			var container = new Container(cfg =>
			{
				cfg.For<IBar>().Use<InMemoryBar>();

				cfg.For<IFoo>().Use(ctx => new MaxFoo(ctx.GetInstance<IBar>()));
			});

			Assert.That(container.GetInstance<IFoo>(), Is.InstanceOf<MaxFoo>());
		}
	}
}