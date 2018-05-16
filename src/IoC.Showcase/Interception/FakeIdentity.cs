using System.Security.Principal;

namespace IoC.Showcase.Interception
{
	public class FakeIdentity : IIdentity
	{
		private readonly bool _authenticated;

		public FakeIdentity(bool authenticated)
		{
			_authenticated = authenticated;
		}

		public string Name { get; set; }
		public string AuthenticationType { get; set;  }
		public bool IsAuthenticated => _authenticated;
	}
}