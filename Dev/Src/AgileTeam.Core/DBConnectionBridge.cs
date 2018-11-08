using AgileTeam.Data;
using AgileTeam.DataContext;
using System.Common;

namespace AgileTeam.Core
{
	public class DBConnectionBridge
	{
		private readonly DBConnection _dbConnection;

		public string GetConnectionString(string dataSource, string databaseName, DBAuthMode authMode, Credential credential = null)
		{
			return _dbConnection.GenerateConnectionString(dataSource, databaseName, authMode, credential);
		}

		public DBConnectionBridge()
		{
			_dbConnection = new DBConnection();
		}

		public IResult<DBFailureReason> CheckConnection(string connectionString)
		{
			return _dbConnection.CheckConnection(connectionString);
		}
	}
}