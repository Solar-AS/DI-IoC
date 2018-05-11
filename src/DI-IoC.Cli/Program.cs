using System;
using CommonServiceLocator;
using DI_IoC.Library;
using DI_IoC.Library.LowLevel;
using DI_IoC.Library.LowLevel.LowerLevel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DI_IoC
{
    class Program
    {
        static void Main(string[] args)
        {
			// explicit setup phase
			ServiceCollection services = new ServiceCollection();
	        services.AddTransient<IBar, InMemoryBar>(_ => new InMemoryBar());
	        services.Add(new ServiceDescriptor(typeof(IFoo), typeof(MaxFoo), ServiceLifetime.Transient));
	        services.AddTransient<TopLevel>();

			ServiceLocator.SetLocatorProvider(()=> new LocatorAdapter(services.BuildServiceProvider()));
			
			// runtime phase
	        TopLevel top = ServiceLocator.Current.GetInstance<TopLevel>();

	        sbyte fooBar = top.FooBar();
			Console.WriteLine(fooBar);

			Console.WriteLine("... press INTRO to exit ...");
	        Console.ReadLine();
        }
    }
}
