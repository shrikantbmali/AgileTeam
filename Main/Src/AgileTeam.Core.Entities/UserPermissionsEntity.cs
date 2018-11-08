namespace AgileTeam.Core.Entities
{
	public class UserPermissionsEntity
	{
		public bool CanCreateProject { get; set; }

		public bool CanCreateTask { get; set; }

		public bool CanCloseTask { get; set; }

		public bool CanEditTask { get; set; }

		public bool CanEditProject { get; set; }

		public bool CanAssignTask { get; set; }
	}
}