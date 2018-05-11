using DI_IoC.Library;
using Lamar;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace IoC.Showcase.AutoWiring
{
	[TestFixture]
	public class AutoWiringTester
	{
		[Test]
		public void DefaultConvention_MostUsualTypes_Registered()
		{
			var container = new Container(cfg => cfg
				.Scan(scanner =>
				{
					scanner.AssemblyContainingType<AutoWiringTester>();
					scanner.WithDefaultConventions();
				}));

			ISomething something = container.TryGetInstance<ISomething>();
			// not null since it conforms to default convention
			Assert.That(something, Is.InstanceOf<Something>());

			ISomethingElse somethingElse = container.TryGetInstance<ISomethingElse>();
			// null since "NotSomethingElse" is not converered by the default convention
			Assert.That(somethingElse, Is.Null);
		}

		[Test]
		public void CustomConvention_AllowsEasyLibraryRegistration()
		{
			var container = new Container(cfg =>
			{
				cfg.Scan(scanner =>
				{
					scanner.AssemblyContainingType<TopLevel>();
					scanner.Convention<LibraryConvention>();
				});
			});

			var top = container.GetInstance<TopLevel>();
			
			Assert.That(top.FooBar(), Is.EqualTo(-5));
		}

		[Test]
		public void GenericClosing_OutOfTheBox()
		{
			var container = new Container(cfg => cfg.Scan(scan =>
			{
				scan.AssemblyContainingType <AutoWiringTester>();
				scan.ConnectImplementationsToTypesClosing(typeof(ICommandHandler<>));
			}));

			Assert.That(container.TryGetInstance<ICommandHandler<string>>(), Is.InstanceOf<TextCommand>());
			Assert.That(container.TryGetInstance<ICommandHandler<int>>(), Is.InstanceOf<NumberCommand>());
			Assert.That(container.TryGetInstance<ICommandHandler<char>>(), Is.Null);
		}
	}
}