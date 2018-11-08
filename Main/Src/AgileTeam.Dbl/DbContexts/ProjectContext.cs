using AgileTeam.Core.Entities;
using System;
using System.Collections.Generic;

namespace AgileTeam.Dbl.DbContexts
{
	// ReSharper disable once ClassNeverInstantiated.Global
	public sealed class ProjectContext : DbContext
	{
		private const string Id = "Id";
		private const string Title = "Title";
		private const string UserId = "UserId";
		private const string Description = "Description";
		private const string TaskType = "TaskType";
		private const string DateCreated = "DateCreated";

		static ProjectContext()
		{
			TableName = "PROJECTS";
		}

		public static long Create(long userId, TaskEntity taskEntity)
		{
			var createProjectQuery = string.Format("INSERT INTO {0} VALUES ({1}, '{2}', '{3}', '{4}', {5})", TableName, userId,
				taskEntity.Title, taskEntity.Description, taskEntity.DateCreated, (int)taskEntity.TaskType);

			var sqlCommand = GetSqlCommand(createProjectQuery);

			sqlCommand.ExecuteScalar();

			return GetProjectId(taskEntity.Title);
		}

		private static long GetProjectId(string title)
		{
			var selectProjectIdQuery = string.Format("SELECT {0} FROM {1} WHERE {2}='{3}'", Id, TableName, Title, title);

			var sqlCommand = GetSqlCommand(selectProjectIdQuery);

			var sqlDataReader = sqlCommand.ExecuteReader();

			sqlDataReader.Read();

			return (int)sqlDataReader[0];
		}

		public static IEnumerable<TaskEntity> GetAllProjects()
		{
			var selectAllProjectsQuery = string.Format("SELECT * FROM {0}", TableName);

			var sqlCommand = GetSqlCommand(selectAllProjectsQuery);

			var sqlDataReader = sqlCommand.ExecuteReader();

			while (sqlDataReader.Read())
			{
				var taskEntity = new TaskEntity((int)sqlDataReader[Id], (int)sqlDataReader[UserId])
				{
					Title = (string)sqlDataReader[Title],
					Description = (string)sqlDataReader[Description],
					DateCreated = (DateTime)sqlDataReader[DateCreated],
					TaskType = (TaskType)((int)sqlDataReader[TaskType])
				};

				yield return taskEntity;
			}
		}
	}
}