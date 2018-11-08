using AgileTeam.DataContext;
using System;
using System.Common;
using System.Data;
using System.Data.SqlClient;

namespace AgileTeam.Data
{
	public class DBConnection
	{
		public string GenerateConnectionString(string dataSource, string databaseName, DBAuthMode authMode, Credential credential = null)
		{
			var sqlConnectionStringBuilder = new SqlConnectionStringBuilder
			{
				DataSource = dataSource,
				InitialCatalog = databaseName,
				ConnectTimeout = 30,
				IntegratedSecurity = true,
				TrustServerCertificate = false,
				Encrypt = false,
			};

			if (authMode == DBAuthMode.DBServer)
			{
				if (credential == null)
					throw new InvalidOperationException("Credentials cannot be null if Authentication mode is DBServer!");

				sqlConnectionStringBuilder.UserID = credential.UserName;
				sqlConnectionStringBuilder.Password = credential.Password;
			}

			return sqlConnectionStringBuilder.ToString();
		}

		public IResult<DBFailureReason> CheckConnection(string connectionString)
		{
			IResult<DBFailureReason> result;

			try
			{
				using (var sqlConnection = GetNewConnection(connectionString))
				{
					try
					{
						sqlConnection.Open();
						sqlConnection.Close();

						result = new Result<DBFailureReason>(DBFailureReason.None, true);
					}
					catch (Exception)
					{
						result = new Result<DBFailureReason>(DBFailureReason.SqlException, false);
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
				result = new Result<DBFailureReason>(DBFailureReason.SqlException, false);
			}

			return result;
		}

		private static IDbConnection GetNewConnection(string connectionString)
		{
			return new SqlConnection(connectionString);
		}
	}
}