using System;

namespace AgileTeam.Core.Entities
{
	public class TaskEntity
	{
		public long Id { get; set; }

		public long UserId { get; set; }

		public string Title { get; set; }

		public string Description { get; set; }

		public DateTime DateCreated { get; set; }

		public TaskType TaskType { get; set; }

		public TaskEntity()
		{
		}

		public TaskEntity(long id, long userId)
		{
			Id = id;
			UserId = userId;
		}
	}
}