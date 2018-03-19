using System;
using DI_IoC.Library;
using DI_IoC.Library.LowLevel;
using DI_IoC.Library.LowLevel.LowerLevel;

namespace DI_IoC
{
    class Program
    {
        static void Main(string[] args)
        {
	        var top = new TopLevel(
		        new MaxFoo(
			        new InMemoryBar()));
	        sbyte fooBar = top.FooBar();
			Console.WriteLine(fooBar);

			Console.WriteLine("... press INTRO to exit ...");
	        Console.ReadLine();
        }
    }
}
