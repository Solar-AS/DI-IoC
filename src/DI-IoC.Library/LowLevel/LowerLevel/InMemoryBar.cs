namespace DI_IoC.Library.LowLevel.LowerLevel
{
	public class InMemoryBar : IBar
	{
		public byte[] Bar()
		{
			return new byte[] { 1, 2, 3, 4, 5 };
		}
	}
}