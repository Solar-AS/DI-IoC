using Castle.DynamicProxy;
using Lamar;
using NUnit.Framework;

namespace IoC.Showcase.Interception
{
	[TestFixture]
	public class InterceptionTester
	{
		[Test]
		public void Slow_Implementation_WaitForEveryInvocation()
		{
			var container = new Container(cfg =>
			{
				cfg.For<IRepository>()
					.Use<SlowRepository>();
			});

			var repo = container.GetInstance<IRepository>();
			// we wait
			Assert.That(repo.Get(1), Is.EqualTo("something"));
			System.Diagnostics.Debug.WriteLine("first invocation");

			// and wait again
			Assert.That(repo.Get(1), Is.EqualTo("something"));
			System.Diagnostics.Debug.WriteLine("second invocation");
		}

		[Test]
		public void ProxyingCache_WaitsForFirstInvocation()
		{
			var container = new Container(cfg =>
			{
				cfg.For<SlowRepository>();
				cfg.For<IRepository>().Add(ctx =>
				{
					var intercepted = ctx.GetInstance<SlowRepository>();
					var generator = new ProxyGenerator();
					var proxy = (IRepository) generator.CreateInterfaceProxyWithTarget(
						typeof(IRepository),
						intercepted,
						new CacheInterceptor());
					return proxy;
				});
			});

			var repo = container.GetInstance<IRepository>();
			// we wait
			Assert.That(repo.Get(1), Is.EqualTo("something"));
			System.Diagnostics.Debug.WriteLine("first invocation");

			// and wait again
			Assert.That(repo.Get(1), Is.EqualTo("something"));
			System.Diagnostics.Debug.WriteLine("second invocation");

			repo.Add("something");
			repo.Save();
		}
	}
}