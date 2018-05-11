using System;
using System.IO;
using DI_IoC.Library;

namespace DI_IoC
{
	public class CompositionRoot
	{
		private readonly TextWriter _out;
		private readonly Func<string> _readLine;
		private readonly TopLevel _top;

		public CompositionRoot(TextWriter @out, Func<string> readLine, TopLevel top)
		{
			_out = @out;
			_readLine = readLine;
			_top = top;
		}

		public void Run()
		{
			sbyte fooBar = _top.FooBar();
			_out.WriteLine(fooBar);

			_out.WriteLine("... press INTRO to exit ...");
			_readLine();
		}
	}
}