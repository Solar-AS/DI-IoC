using System.Linq;

namespace DI_IoC.Library.LowLevel
{
    public class MaxFoo : IFoo
    {
	    private readonly IBar _bar;
	    public MaxFoo(IBar bar)
	    {
		    _bar = bar;
	    }

	    public byte Foo()
	    {
		    byte[] bars = _bar.Bar();
		    byte foo = bars.Max();
		    return foo;
	    }
	}
}
