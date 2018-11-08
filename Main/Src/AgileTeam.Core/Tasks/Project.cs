using AgileTeam.Core.Entities;
using AgileTeam.Core.Users;
using AgileTeam.Dbl.DbContexts;
using System;
using System.Collections.Generic;

namespace AgileTeam.Core.Tasks
{
	public class Project : TaskEntity
	{
		private Project()
		{
			TaskType = TaskType.Project;
		}

		private Project(IUser user, string projectTitle, string projectDescription)
			: this()
		{
			UserId = user.Id;
			Title = projectTitle;
			Description = projectDescription;

			DateCreated = DateTime.Now;
		}

		public static Project New(IUser user, string projectTitle, string projectDescription)
		{
			if (string.IsNullOrEmpty(projectTitle) || string.IsNullOrEmpty(projectDescription))
				throw new InvalidOperationException("Neither project title nor description can be empty or null!");

			return InsertProjectInDB(new Project(user, projectTitle, projectDescription));
		}

		private static Project InsertProjectInDB(Project project)
		{
			project.Id = ProjectContext.Create(project.UserId, project);
			return project;
		}

		public static IEnumerable<Project> GetAllProjects()
		{
			foreach (var taskEntity in ProjectContext.GetAllProjects())
			{
				var project = new Project
				{
					Id = taskEntity.Id,
					UserId = taskEntity.UserId,
					Title = taskEntity.Title,
					Description = taskEntity.Description,
					DateCreated = taskEntity.DateCreated
				};

				yield return project;
			}
		}
	}
}