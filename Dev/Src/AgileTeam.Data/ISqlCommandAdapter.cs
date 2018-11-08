using System;
using System.Common;
using System.Data;

namespace AgileTeam.Data
{
	public interface ISqlCommandAdapter : IDisposable
	{
		void Prepare();

		void Cancel();

		IDbDataParameter CreateParameter();

		IResult<int, DBFailureReason> ExecuteNonQuery();

		IResult<IDataReader, DBFailureReason> ExecuteReader();

		IResult<IDataReader, DBFailureReason> ExecuteReader(CommandBehavior behavior);

		IResult<object, DBFailureReason> ExecuteScalar();

		IDbConnection Connection { get; set; }

		IDbTransaction Transaction { get; set; }

		string CommandText { get; set; }

		int CommandTimeout { get; set; }

		CommandType CommandType { get; set; }

		IDataParameterCollection Parameters { get; }

		UpdateRowSource UpdatedRowSource { get; set; }
	}
}