using AgileTeam.Core.Entities;

namespace AgileTeam.Core.Users
{
	internal class UserPermissions : IPermissions
	{
		private readonly UserPermissionsEntity _permissionsContext;

		public bool CanCreateProject
		{
			get { return _permissionsContext.CanCreateProject; }
		}

		public bool CanEditProject
		{
			get { return _permissionsContext.CanEditProject; }
		}

		public bool CanCreateTask
		{
			get { return _permissionsContext.CanCreateTask; }
		}

		public bool CanEditTask
		{
			get { return _permissionsContext.CanEditTask; }
		}

		public bool CanAssignTask
		{
			get { return _permissionsContext.CanAssignTask; }
		}

		public bool CanCloseTask
		{
			get { return _permissionsContext.CanCloseTask; }
		}

		public UserPermissions(UserPermissionsEntity permissionsContext)
		{
			_permissionsContext = permissionsContext;
		}
	}
}