using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;

namespace AgileTeam.Core
{
	internal class DatabaseServer : IDatabaseServer
	{
		public IEnumerable<string> GetLocalSqlServerNames()
		{
			var dataSources = SqlDataSourceEnumerator.Instance.GetDataSources();
			var sqlConnectioNNames = new List<string>();

			if (dataSources.Rows.Count > 0)
				sqlConnectioNNames.AddRange(from DataRow row in dataSources.Rows select row[0] + "\\" + row[1]);

			return sqlConnectioNNames;
		}

		public bool IsSqlServerAlive(string connectionString)
		{
			try
			{
				var sqlConnection = new SqlConnection(connectionString);
				sqlConnection.Open();

				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public string GetSqlConnectionString(string sqlServerName)
		{
			return new SqlConnectionStringBuilder
			{
				ConnectRetryCount = 3,
				ConnectTimeout = 30,
				DataSource = sqlServerName,
				InitialCatalog = "AgileTeam",
				TrustServerCertificate = false,
				IntegratedSecurity = true,
			}.ConnectionString;
		}
	}
}