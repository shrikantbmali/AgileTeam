using System;

namespace AgileTeam.Core.Users
{
	internal class PermissionResolver : IPermissionResolver
	{
		private static readonly Lazy<IPermissionResolver> _lazyInstance =
			new Lazy<IPermissionResolver>(() => new PermissionResolver());

		public static IPermissionResolver Instance
		{
			get { return _lazyInstance.Value; }
		}

		public bool CanCreateNewProject(IUser user)
		{
			var userWithPermission = GetPermissionProvider(user);

			return userWithPermission.CanCreateProject;
		}

		private static IPermissions GetPermissionProvider(IUser user)
		{
			return ((IPermissible)user).Permissions;
		}
	}
}