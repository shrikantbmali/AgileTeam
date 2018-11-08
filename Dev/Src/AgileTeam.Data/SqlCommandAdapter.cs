using System.Common;
using System.Data;
using System.Data.SqlClient;

namespace AgileTeam.Data
{
	internal class SqlCommandAdapter : ISqlCommandAdapter
	{
		private SqlCommand _command;

		public IDbConnection Connection
		{
			get { return _command.Connection; }
			set { _command.Connection = (SqlConnection)value; }
		}

		public IDbTransaction Transaction
		{
			get { return _command.Transaction; }
			set { _command.Transaction = (SqlTransaction)value; }
		}

		public string CommandText
		{
			get { return _command.CommandText; }
			set { _command.CommandText = value; }
		}

		public int CommandTimeout
		{
			get { return _command.CommandTimeout; }
			set { _command.CommandTimeout = value; }
		}

		public CommandType CommandType
		{
			get { return _command.CommandType; }
			set { _command.CommandType = value; }
		}

		public IDataParameterCollection Parameters
		{
			get { return _command.Parameters; }
		}

		public UpdateRowSource UpdatedRowSource
		{
			get { return _command.UpdatedRowSource; }
			set { _command.UpdatedRowSource = value; }
		}

		public SqlCommandAdapter(SqlCommand command)
		{
			_command = command;
		}

		public void Dispose()
		{
			_command.Dispose();
			_command = null;
		}

		public void Prepare()
		{
			_command.Prepare();
		}

		public void Cancel()
		{
			_command.Cancel();
		}

		public IDbDataParameter CreateParameter()
		{
			return _command.CreateParameter();
		}

		public IResult<int, DBFailureReason> ExecuteNonQuery()
		{
			Result<int, DBFailureReason> result;

			try
			{
				var affectedRows = _command.ExecuteNonQuery();
				result = new Result<int, DBFailureReason>(affectedRows, DBFailureReason.None, true);
			}
			catch (SqlException ex)
			{
				result = new Result<int, DBFailureReason>(-1, DBFailureReason.SqlException, false) { Message = ex.Message };
			}

			return result;
		}

		public IResult<IDataReader, DBFailureReason> ExecuteReader()
		{
			Result<IDataReader, DBFailureReason> result;

			try
			{
				var sqlDataReader = _command.ExecuteReader();

				result = new Result<IDataReader, DBFailureReason>(sqlDataReader, DBFailureReason.None, true);
			}
			catch (SqlException ex)
			{
				result = new Result<IDataReader, DBFailureReason>(DBFailureReason.SqlException, false) { Message = ex.Message };
			}

			return result;
		}

		public IResult<IDataReader, DBFailureReason> ExecuteReader(CommandBehavior behavior)
		{
			return new Result<IDataReader, DBFailureReason>(_command.ExecuteReader(behavior), DBFailureReason.None, true);
		}

		public IResult<object, DBFailureReason> ExecuteScalar()
		{
			return new Result<object, DBFailureReason>(_command.ExecuteScalar(), DBFailureReason.None, true);
		}
	}
}