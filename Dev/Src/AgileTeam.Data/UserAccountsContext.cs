using AgileTeam.DataContext;
using System;
using System.Common;
using System.Data;

namespace AgileTeam.Data
{
	public class UserAccountsContext : DBContext
	{
		public UserAccountsContext(IDatabaseManager dbContext)
			: base(dbContext)
		{
		}

		public IResult<IUserLoginSession, DBFailureReason> GetUserSession(Credential credential)
		{
			IResult<IUserLoginSession, DBFailureReason> result;

			var dbSessionResult = DBManager.OpenDBSession();

			if (dbSessionResult.IsSuccessful)
			{
				var sqlCommand = string.Format("SELECT ID FROM USERS WHERE USERNAME='{0}' AND PASSWORD='{1}'", credential.UserName,
					credential.Password);

				using (var dbCommand = DBManager.GetCommand(sqlCommand, dbSessionResult.Value))
				{
					var loginData = dbCommand.ExecuteReader();

					result = loginData.IsSuccessful
						? CreateSessionResult(loginData.Value)
						: new Result<IUserLoginSession, DBFailureReason>(loginData.FailureReason, false) { Message = loginData.Message };
				}
			}
			else
			{
				result = new Result<IUserLoginSession, DBFailureReason>(dbSessionResult.FailureReason, false)
				{
					Message = dbSessionResult.Message
				};
			}

			DBManager.CloseSession();

			return result;
		}

		private static IResult<IUserLoginSession, DBFailureReason> CreateSessionResult(IDataReader value)
		{
			IResult<IUserLoginSession, DBFailureReason> result;

			if (value.Read())
			{
				result =
					new Result<IUserLoginSession, DBFailureReason>(
						new UserLoginSession(GetSessionId(), long.Parse(value[0].ToString()),
							TimeSpan.FromHours(1)), DBFailureReason.None, true);
			}
			else
			{
				result = new Result<IUserLoginSession, DBFailureReason>(DBFailureReason.InvalidDetails, false)
				{
					Message = "Could not read the data."
				};
			}

			return result;
		}

		private static string GetSessionId()
		{
			return DateTime.UtcNow + Guid.NewGuid().ToString();
		}

		public IResult<DBFailureReason> CreateUser(Credential credential)
		{
			IResult<DBFailureReason> accountCreationResult;

			var sqlCommand = string.Format("INSERT INTO USERS (USERNAME, PASSWORD) VALUES('{0}','{1}')", credential.UserName,
				credential.Password);

			var dbSession = DBManager.OpenDBSession();

			if (dbSession.IsSuccessful)
			{
				var sqlCommandAdapter = DBManager.GetCommand(sqlCommand, dbSession.Value);

				var result = sqlCommandAdapter.ExecuteNonQuery();

				accountCreationResult = result.IsSuccessful
					? new Result<DBFailureReason>(DBFailureReason.None, true) { Message = result.Message }
					: new Result<DBFailureReason>(result.FailureReason, false) { Message = result.Message };
			}
			else
			{
				accountCreationResult = new Result<DBFailureReason>(dbSession.FailureReason, false);
			}

			return accountCreationResult;
		}
	}
}