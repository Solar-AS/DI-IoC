using System.Threading;

namespace IoC.Showcase.Interception
{
	public class SlowRepository : IRepository{
		public string Get(int key)
		{
			Thread.Sleep(1500);
			return "something";
		}

		public string Add(string item)
		{
			// whatever
			return item;
		}

		public void Save()
		{
			// nothing to see, move along
		}
	}
}