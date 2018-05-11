using DI_IoC.Library.LowLevel;
using DI_IoC.Library.LowLevel.LowerLevel;
using Lamar;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace IoC.Showcase.Lifestyles
{
	[TestFixture]
	public class LifestyleTester
	{
		[Test]
		public void Singleton_SameObject_EveryTime()
		{
			var container = new Container(cfg => cfg.For<IBar>().Use<InMemoryBar>().Singleton());

			IBar one = container.GetInstance<IBar>(), another = container.GetInstance<IBar>();

			Assert.That(one, Is.SameAs(another));
		}

		[Test]
		public void Transient_NewObject_EveryTime()
		{
			var container = new Container(cfg => cfg.AddTransient<IBar, InMemoryBar>());

			IBar one = container.GetInstance<IBar>(), another = container.GetInstance<IBar>();

			Assert.That(one, Is.Not.SameAs(another));

			using (var nested = container.GetNestedContainer())
			{
				one = nested.GetInstance<IBar>();
				Assert.That(one, Is.Not.SameAs(another));

				another = nested.GetInstance<IBar>();
				Assert.That(one, Is.Not.SameAs(another));
			}
		}

		[Test]
		public void Scoped_NewObject_OncePerContainer()
		{
			var container = new Container(cfg => cfg.AddScoped<IBar, InMemoryBar>());

			IBar one = container.GetInstance<IBar>(), another = container.GetInstance<IBar>();

			// same instance as they are coming from same container
			Assert.That(one, Is.SameAs(another));

			using (var nested = container.GetNestedContainer())
			{
				one = nested.GetInstance<IBar>();
				// it is a different instance since it is coming from nested
				Assert.That(one, Is.Not.SameAs(another));

				another = nested.GetInstance<IBar>();
				// same instance as before
				Assert.That(one, Is.SameAs(another));
			}
		}

		[Test]
		public void Beware_Singleton_BrainFreeze()
		{
			var container = new Container(cfg =>
			{
				cfg.AddTransient<I1, One>();
				cfg.AddTransient<I2, Two>();
				cfg.AddSingleton<Simpleton>();
			});

			I1 one = container.GetInstance<I1>(),
				another = container.GetInstance<I1>();

			// transient as they are
			Assert.That(one, Is.Not.SameAs(another));

			var simpleton = container.GetInstance<Simpleton>();
			Assert.That(simpleton.One, Is.Not.SameAs(one));

			var anotherSimpleton = container.GetInstance<Simpleton>();
			// transients are no more
			Assert.That(simpleton.One, Is.SameAs(anotherSimpleton.One));
		}
	}

	public class Simpleton
	{
		public I1 One { get; }
		public I2 Two { get; }

		public Simpleton(I1 one, I2 two)
		{
			One = one;
			Two = two;
		}
	}

	public interface I1 { }
	public class One : I1{ }
	public interface I2 { }
	public class Two : I2 { }
}