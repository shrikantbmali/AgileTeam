using AgileTeam.Core.Tasks;

namespace AgileTeam.Core
{
	public static class TaskFactory
	{
		public static Task CreateProject(long projectId)
		{
			return new Project(projectId) {TaskType = TaskType.Project};
		}

		public static Task CreateSprint(long sprintId)
		{
			return new Sprint(sprintId) { TaskType = TaskType.Sprint };
		}
	}
}