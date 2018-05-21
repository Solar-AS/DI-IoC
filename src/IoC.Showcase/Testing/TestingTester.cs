using IoC.Showcase.AutoWiring;
using IoC.Showcase.Lifestyles;
using Lamar;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace IoC.Showcase.Testing
{
	[TestFixture]
	public class TestingTester
	{
		[Test]
		public void WhatXX_GivesInformation()
		{
			var container = new Container(cfg =>
			{
				cfg.AddTransient<I1, One>();
				cfg.AddTransient<I2, Two>();
				cfg.AddSingleton<Simpleton>();

				cfg.Scan(scan =>
				{
					scan.AssemblyContainingType<AutoWiringTester>();
					scan.ConnectImplementationsToTypesClosing(typeof(ICommandHandler<>));
					scan.WithDefaultConventions();
				});
			});

			System.Diagnostics.Debug.WriteLine(container.WhatDoIHave());

			System.Diagnostics.Debug.WriteLine(container.WhatDoIHave(typeof(Simpleton)));

			System.Diagnostics.Debug.WriteLine(container.WhatDidIScan());
		}

		[Test]
		public void AssertConfigurationIsValid_ChecksForInconsistencies()
		{
			new Container(cfg =>
			{
				cfg.AddTransient<I1, One>();
				cfg.AddTransient<I2, Two>();
				cfg.AddSingleton<Simpleton>();
			})
			.AssertConfigurationIsValid();

			new Container(cfg =>
			{
				cfg.AddTransient<I1, One>();
				cfg.AddSingleton<Simpleton>();
			})
			.AssertConfigurationIsValid();
		}

		[Test]
		public void UseModel_ForAssertions()
		{
			var container = new Container(cfg =>
			{
				cfg.AddTransient<I1, One>();
				cfg.AddTransient<I2, Two>();
				cfg.AddSingleton<Simpleton>();
			});
			Assert.That(container.Model.HasRegistrationFor<Simpleton>(), Is.True);
			Assert.That(container.Model.For<Simpleton>().Default.Lifetime, Is.EqualTo(ServiceLifetime.Singleton));
			
			System.Diagnostics.Debug.WriteLine(container.Model.For<Simpleton>().Default.GetBuildPlan());
			System.Diagnostics.Debug.WriteLine(container.Model.For<I1>().Default.GetBuildPlan());
			System.Diagnostics.Debug.WriteLine(container.Model.For<I2>().Default.GetBuildPlan());
		}

		
	}
}