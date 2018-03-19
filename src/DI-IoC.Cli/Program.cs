using System;
using DI_IoC.Library;

namespace DI_IoC
{
    class Program
    {
        static void Main(string[] args)
        {
	        var top = new TopLevel();
	        sbyte fooBar = top.FooBar();
			Console.WriteLine(fooBar);

			Console.WriteLine("... press INTRO to exit ...");
	        Console.ReadLine();
        }
    }
}
