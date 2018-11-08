namespace AgileTeam.Core.Entities
{
	/// <summary>
	/// Represents different task types;
	/// <remarks>
	/// Do not make changes to the values of the existing members of this class,
	/// and always assing different value for each new member added to this class.
	/// </remarks>
	/// </summary>
	public enum TaskType
	{
		Project = 1,
		Sprint = 2,
		UserStory = 3,
		Bug = 4,
		Feature = 5
	}
}