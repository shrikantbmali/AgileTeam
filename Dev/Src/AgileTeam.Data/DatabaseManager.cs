using System;
using System.Common;
using System.Data;
using System.Data.SqlClient;

namespace AgileTeam.Data
{
	public class DatabaseManager : IDatabaseManager
	{
		private string _connectionString;

		private IDbConnection _currentConnection;

		public IResult<DBFailureReason> CheckConnection(string connectionString)
		{
			IResult<DBFailureReason> result;

			try
			{
				using (var sqlConnection = GetNewConnection())
				{
					try
					{
						sqlConnection.Open();
						sqlConnection.Close();

						result = new Result<DBFailureReason, string>(DBFailureReason.None, true);
					}
					catch (Exception)
					{
						result = new Result<DBFailureReason, string>(DBFailureReason.SqlException, false);
					}
					finally
					{
						if (sqlConnection.State == ConnectionState.Open)
							sqlConnection.Close();
					}
				}
			}
			catch (Exception)
			{
				result = new Result<DBFailureReason, string>(DBFailureReason.SqlException, false);
			}

			return result;
		}

		public IResult<IDbConnection, DBFailureReason> OpenDBSession()
		{
			Result<IDbConnection, DBFailureReason> result;

			if (Validate())
			{
				_currentConnection = GetNewConnection();

				_currentConnection.Open();

				result = new Result<IDbConnection, DBFailureReason>(_currentConnection, true);
			}
			else
			{
				result = new Result<IDbConnection, DBFailureReason>(DBFailureReason.CannotOpenAConnection, false);
			}

			return result;
		}

		private bool Validate()
		{
			return _currentConnection == null || _currentConnection.State == ConnectionState.Closed;
		}

		private IDbConnection GetNewConnection()
		{
			return new SqlConnection(_connectionString);
		}

		public void CloseSession()
		{
			_currentConnection.Close();
			_currentConnection.Dispose();
			_currentConnection = null;
		}

		public ISqlCommandAdapter GetCommand(string command, IDbConnection connection)
		{
			return new SqlCommandAdapter(new SqlCommand(command, (SqlConnection)connection));
		}

		public bool IsAccountSetupCompleted()
		{
			var result = false;

			var openDBSessionResult = OpenDBSession();

			if (openDBSessionResult.IsSuccessful)
			{
				const string sqlCommand = "SELECT ID FROM USERS";

				var command = GetCommand(sqlCommand, openDBSessionResult.Value);

				var executeReader = command.ExecuteReader();

				if (executeReader.IsSuccessful)
				{
					if (executeReader.Value.Read())
					{
						var o = executeReader.Value.FieldCount;
						result = o > 0;
					}
				}
			}

			CloseSession();

			return result;
		}

		public void SetDatabaseConnection(string connectionString)
		{
			_connectionString = connectionString;
		}
	}
}