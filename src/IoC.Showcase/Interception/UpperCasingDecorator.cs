namespace IoC.Showcase.Interception
{
	public class UpperCasingDecorator : IQuery
	{
		private readonly IQuery _decoree;

		public UpperCasingDecorator(IQuery decoree)
		{
			_decoree = decoree;
		}

		public string Get(int id)
		{
			string result = _decoree.Get(id);
			return result?.ToUpperInvariant();
		}
	}
}