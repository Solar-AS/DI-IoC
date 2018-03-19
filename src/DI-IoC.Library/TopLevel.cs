using System;
using DI_IoC.Library.LowLevel;

namespace DI_IoC.Library
{
    public class TopLevel
    {
	    private readonly MaxFoo _foo;
	    public TopLevel()
	    {
		    _foo = new MaxFoo();
	    }

	    public sbyte FooBar()
	    {
		    byte foo = _foo.Foo();
		    sbyte fooBar = Convert.ToSByte(-foo);
		    return fooBar;
	    }
	}
}
