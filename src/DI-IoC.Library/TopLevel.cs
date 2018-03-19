﻿using System;

namespace DI_IoC.Library
{
    public class TopLevel
    {
	    private readonly IFoo _foo;
	    public TopLevel(IFoo foo)
	    {
		    _foo = foo;
	    }

	    public sbyte FooBar()
	    {
		    byte foo = _foo.Foo();
		    sbyte fooBar = Convert.ToSByte(-foo);
		    return fooBar;
	    }
	}
}
