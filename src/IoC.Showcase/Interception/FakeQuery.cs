namespace IoC.Showcase.Interception
{
	public class FakeQuery : IQuery
	{
		public string Get(int id)
		{
			return "product " + id;
		}
	}
}