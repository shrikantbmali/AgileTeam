using System;
using System.Data.SqlClient;

namespace AgileTeam.Dbl.Database
{
	public class DatabaseConnection
	{
		private readonly string _connectionString;
		private static Lazy<DatabaseConnection> _lazyInstance;
		private static readonly object _intializerLock = new object();

		public static DatabaseConnection Instance
		{
			get
			{
				if (_lazyInstance == null)
					throw new InvalidOperationException("Please Initialize the instance!");

				return _lazyInstance.Value;
			}
		}

		private DatabaseConnection(string connectionString)
		{
			_connectionString = connectionString;
		}

		private SqlConnection GetOpennedtDbConnection()
		{
			var sqlConnection = new SqlConnection(_connectionString);
			sqlConnection.Open();
			return sqlConnection;
		}

		public SqlConnection GetConnection()
		{
			return GetOpennedtDbConnection();
		}

		public static void Initialize(string connectionString)
		{
			lock (_intializerLock)
			{
				if (_lazyInstance != null)
					throw new InvalidOperationException("Cannot reinitialize, instance is already Initialized");

				_lazyInstance = new Lazy<DatabaseConnection>(() => new DatabaseConnection(connectionString));
			}
		}
	}
}