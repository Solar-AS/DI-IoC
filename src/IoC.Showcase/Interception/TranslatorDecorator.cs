namespace IoC.Showcase.Interception
{
	public class TranslatorDecorator : IQuery
	{
		private readonly IQuery _decoree;

		public TranslatorDecorator(IQuery decoree)
		{
			_decoree = decoree;
		}

		public string Get(int id)
		{
			string result = _decoree.Get(id);
			return result?.Replace("product", "produkt");
		}
	}
}