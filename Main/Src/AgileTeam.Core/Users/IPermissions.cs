namespace AgileTeam.Core.Users
{
	public interface IPermissions
	{
		bool CanCreateProject { get; }

		bool CanCreateTask { get; }

		bool CanCloseTask { get; }

		bool CanEditTask { get; }

		bool CanEditProject { get; }

		bool CanAssignTask { get; }
	}
}