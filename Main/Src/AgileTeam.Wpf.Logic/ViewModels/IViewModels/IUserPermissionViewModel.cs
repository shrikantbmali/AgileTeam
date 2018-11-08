using AgileTeam.Core.Entities;

namespace AgileTeam.Wpf.Logic.ViewModels.IViewModels
{
	public interface IUserPermissionViewModel : IAgileTeamViewModel
	{
		bool CreateUserAsAdmin { get; }

		bool PermissionsEnabled { get; }

		bool CanCreateProject { get; set; }

		bool CanEditProject { get; set; }

		bool CanCreateTask { get; set; }

		bool CanEditTask { get; set; }

		bool CanAssignTask { get; set; }

		bool CanCloseTask { get; set; }

		UserPermissionsEntity Permissions { get; }
	}
}