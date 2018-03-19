using System.Linq;
using DI_IoC.Library.LowLevel.LowerLevel;

namespace DI_IoC.Library.LowLevel
{
    public class MaxFoo
    {
	    private readonly InMemoryBar _bar;
	    public MaxFoo()
	    {
		    _bar = new InMemoryBar();
	    }

	    public byte Foo()
	    {
		    byte[] bars = _bar.Bar();
		    byte foo = bars.Max();
		    return foo;
	    }
	}
}
