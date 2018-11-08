using System.Infrastructure.Services;
using AgileTeam.Core.Entities;
using AgileTeam.Wpf.Logic.ViewModels.IViewModels;

namespace AgileTeam.Wpf.Logic.ViewModels
{
	internal class UserPermissionVM : AgileTeamVM, IUserPermissionViewModel
	{
		public bool CreateUserAsAdmin { get; private set; }

		public bool PermissionsEnabled { get { return !CreateUserAsAdmin; } }

		public bool CanCreateProject { get; set; }

		public bool CanEditProject { get; set; }

		public bool CanCreateTask { get; set; }

		public bool CanEditTask { get; set; }

		public bool CanAssignTask { get; set; }

		public bool CanCloseTask { get; set; }

		public UserPermissionsEntity Permissions { get { return GetPermissions(); } }

		public UserPermissionVM(IMessengerService messengerService, bool createUserAsAdmin)
			: base(messengerService)
		{
			CreateUserAsAdmin = createUserAsAdmin;

			CanCreateProject = true;
			CanEditProject = true;
			CanCreateTask = true;
			CanEditTask = true;
			CanAssignTask = true;
			CanCloseTask = true;
		}

		private UserPermissionsEntity GetPermissions()
		{
			var permissions = new UserPermissionsEntity
			{
				CanCreateProject = CanCreateProject,
				CanEditProject = CanEditProject,
				CanCreateTask = CanCreateTask,
				CanEditTask = CanEditTask,
				CanAssignTask = CanAssignTask,
				CanCloseTask = CanCloseTask
			};
			return permissions;
		}
	}
}