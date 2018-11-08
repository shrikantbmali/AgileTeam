using AgileTeam.Dbl.Database;
using System.Data.SqlClient;

namespace AgileTeam.Dbl.DbContexts
{
	public class DbContext
	{
		protected static string TableName { get; set; }

		protected static SqlCommand GetSqlCommand(string query)
		{
			return new SqlCommand(query, DatabaseConnection.Instance.GetConnection());
		}
	}
}