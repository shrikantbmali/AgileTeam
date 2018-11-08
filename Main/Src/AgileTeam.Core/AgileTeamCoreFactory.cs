using AgileTeam.Core.Users;
using System;

namespace AgileTeam.Core
{
	public class AgileTeamCoreFactory : IAgileTeamCoreFactory
	{
		private static readonly Lazy<IAgileTeamCoreFactory> _lazyInstance =
			new Lazy<IAgileTeamCoreFactory>(() => new AgileTeamCoreFactory());

		public static IAgileTeamCoreFactory Instance
		{
			get { return _lazyInstance.Value; }
		}

		private AgileTeamCoreFactory()
		{
		}

		public static IPermissionResolver PermissionResolver
		{
			get { return Users.PermissionResolver.Instance; }
		}

		public static IDatabaseServer DatabaseServer
		{
			get { return new DatabaseServer(); }
		}
	}
}