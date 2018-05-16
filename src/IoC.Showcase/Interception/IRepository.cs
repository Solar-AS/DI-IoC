namespace IoC.Showcase.Interception
{
	public interface IRepository
	{
		string Get(int key);

		string Add(string item);

		void Save();
	}
}