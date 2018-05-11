using System.Linq;
using DI_IoC.Library.LowLevel.LowerLevel;

namespace DI_IoC.Library.LowLevel
{
    public class MaxFoo : IFoo
    {
	    private readonly IBar _bar;
	    public MaxFoo(IBar bar)
	    {
		    _bar = bar;
	    }

	    public MaxFoo() : this(new InMemoryBar()) { }

	    public byte Foo()
	    {
		    byte[] bars = _bar.Bar();
		    byte foo = bars.Max();
		    return foo;
	    }
	}
}
