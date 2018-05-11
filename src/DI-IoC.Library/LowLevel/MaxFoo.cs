using System.Linq;
using CommonServiceLocator;

namespace DI_IoC.Library.LowLevel
{
    public class MaxFoo : IFoo
    {
	    private readonly IBar _bar;
	    public MaxFoo(IBar bar)
	    {
		    _bar = bar;
	    }

	    public MaxFoo() : this(ServiceLocator.Current.GetInstance<IBar>()) { }

	    public byte Foo()
	    {
		    byte[] bars = _bar.Bar();
		    byte foo = bars.Max();
		    return foo;
	    }
	}
}
