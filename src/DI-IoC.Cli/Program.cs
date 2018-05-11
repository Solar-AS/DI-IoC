using System;
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

	        services.AddSingleton<Func<string>>(_ => Console.ReadLine);
	        services.AddSingleton(_ => Console.Out);
	        services.AddSingleton<CompositionRoot>();

			IServiceProvider provider = services.BuildServiceProvider();
			
			// runtime phase: use on composition root
	        CompositionRoot root = provider.GetService<CompositionRoot>();
	        root.Run();
        }
    }
}
