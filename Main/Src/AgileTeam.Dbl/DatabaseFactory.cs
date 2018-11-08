using AgileTeam.Dbl.Database;

namespace AgileTeam.Dbl
{
	public static class DatabaseFactory
	{
		public static IUserDatabaseManager GetUserDatabaseManager()
		{
			return new UserDatabaseManager(DatabaseConnection.Instance);
		}
	}
}