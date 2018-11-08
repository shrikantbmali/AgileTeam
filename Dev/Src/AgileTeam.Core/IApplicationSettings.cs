using AgileTeam.Data;

namespace AgileTeam.Core
{
	public interface IApplicationSettings
	{
		string ConnectionString { get; }

		bool IsAccountSetupCompleted();

		void SetupDatabaseConnection(string connectionString);
	}

	internal class ApplicationSettings : IApplicationSettings
	{
		public string ConnectionString { get; private set; }

		public bool IsAccountSetupCompleted()
		{
			var dbBridge = new DBBridge();
			var databaseManager = new DatabaseManager();
			databaseManager.SetDatabaseConnection(ConnectionString);
			DBBridge.Initialize(databaseManager);

			return dbBridge.IsAccountSetupCompleted();
		}

		public void SetupDatabaseConnection(string connectionString)
		{
			ConnectionString = connectionString;
		}
	}
}