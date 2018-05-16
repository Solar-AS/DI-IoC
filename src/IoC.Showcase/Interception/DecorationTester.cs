using System.Security.Principal;
using Lamar;
using NUnit.Framework;

namespace IoC.Showcase.Interception
{
	[TestFixture]
	public class DecorationTester
	{
		[Test]
		public void Normal_Resolution()
		{
			var container = new Container(cfg => { cfg.For<IQuery>().Use<FakeQuery>(); });

			var query = container.GetInstance<IQuery>();

			Assert.That(query.Get(42), Is.Not.Null);
		}

		[Test]
		public void Hand_Decorated_Resolution()
		{
			var container = new Container(cfg =>
			{
				cfg.For<IIdentity>().Use(new FakeIdentity(false));
				cfg.For<IQuery>().Use(ctx => 
					new SecurityDecorator(new FakeQuery(), ctx.GetInstance<IIdentity>()));
			});


			var query = container.GetInstance<IQuery>();

			// because we are not authenticated
			Assert.That(query.Get(42), Is.Null);
		}

		[Test]
		public void AlmostAutomatically_Decorated_Resolution()
		{
			var container = new Container(cfg =>
			{
				cfg.For<IIdentity>().Use(new FakeIdentity(false));
				cfg.For<IQuery>().Use<FakeQuery>();
				cfg.For<IQuery>().DecorateAllWith<SecurityDecorator>();
			});
			
			var query = container.GetInstance<IQuery>();

			// because we are not authenticated
			Assert.That(query.Get(42), Is.Null);
		}

		[Test]
		public void DecorationChain_HappensInRegistrationOrder()
		{
			var container = new Container(cfg =>
			{
				cfg.For<IIdentity>().Use(new FakeIdentity(true));
				cfg.For<IQuery>().Use<FakeQuery>();

				cfg.For<IQuery>().DecorateAllWith<SecurityDecorator>();
				cfg.For<IQuery>().DecorateAllWith<TranslatorDecorator>();
				cfg.For<IQuery>().DecorateAllWith<UpperCasingDecorator>();
			});
			
			var query = container.GetInstance<IQuery>();

			// first translate and the uppercase it
			Assert.That(query.Get(42), Does.StartWith("PRODUKT "));

			container = new Container(cfg =>
			{
				cfg.For<IIdentity>().Use(new FakeIdentity(true));
				cfg.For<IQuery>().Use<FakeQuery>();

				cfg.For<IQuery>().DecorateAllWith<SecurityDecorator>();
				cfg.For<IQuery>().DecorateAllWith<UpperCasingDecorator>();
				cfg.For<IQuery>().DecorateAllWith<TranslatorDecorator>();
			});

			query = container.GetInstance<IQuery>();

			// first uppercase that prevents translator to do its job
			Assert.That(query.Get(42), Does.StartWith("PRODUCT "));
		}
	}
}