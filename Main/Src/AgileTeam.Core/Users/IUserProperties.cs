namespace AgileTeam.Core.Users
{
	public interface IUserProperties
	{
		string Title { get; }

		string FirstName { get; }

		string LastName { get; }

		byte[] Image { get; }
	}
}