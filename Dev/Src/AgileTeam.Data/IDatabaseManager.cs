using System.Common;
using System.Data;

namespace AgileTeam.Data
{
	public interface IDatabaseManager
	{
		IResult<DBFailureReason> CheckConnection(string connectionString);

		IResult<IDbConnection, DBFailureReason> OpenDBSession();

		void CloseSession();

		ISqlCommandAdapter GetCommand(string command, IDbConnection connection);

		bool IsAccountSetupCompleted();
	}
}