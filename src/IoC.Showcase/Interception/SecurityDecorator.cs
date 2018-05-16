using System.Security.Principal;

namespace IoC.Showcase.Interception
{
	public class SecurityDecorator : IQuery
	{
		private readonly IQuery _decoree;
		private readonly IIdentity _identity;

		public SecurityDecorator(IQuery decoree, IIdentity identity)
		{
			_decoree = decoree;
			_identity = identity;
		}

		public string Get(int id)
		{
			return _identity.IsAuthenticated ? _decoree.Get(id) : null;
		}
	}
}