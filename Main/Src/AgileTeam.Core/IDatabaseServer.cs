using System.Collections.Generic;

namespace AgileTeam.Core
{
	public interface IDatabaseServer
	{
		IEnumerable<string> GetLocalSqlServerNames();

		bool IsSqlServerAlive(string connectionString);

		string GetSqlConnectionString(string sqlServerName);
	}
}