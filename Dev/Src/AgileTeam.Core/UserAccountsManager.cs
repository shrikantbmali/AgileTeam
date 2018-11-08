using AgileTeam.Data;
using AgileTeam.DataContext;
using AgileTeam.DataContext.Entities;
using System.Common;
using System.Common.Linq;

namespace AgileTeam.Core
{
	internal class UserAccountsManager : IUserAccountsManager
	{
		private static IPasswordEncryptionService _passwordEncryptor;
		private readonly UserAccountsContext _userAccountsContext;

		public UserAccountsManager(IDatabaseManager databaseManager)
		{
			_passwordEncryptor = new PasswordEncryptionService();

			_userAccountsContext = new UserAccountsContext(databaseManager);
		}

		public IResult<IUser, UserAccountFailureReason> Login(Credential credential)
		{
			Result<IUser, UserAccountFailureReason> loginResult;

			var userSessionResult =
				_userAccountsContext.GetUserSession(GetHashedCredential(credential));

			if (userSessionResult.IsSuccessful)
			{
				var newUser = User.NewUser(new UserEntity { Session = userSessionResult.Value });

				loginResult = new Result<IUser, UserAccountFailureReason>(newUser, newUser != null);
			}
			else
			{
				loginResult =
					new Result<IUser, UserAccountFailureReason>(userSessionResult.FailureReason.ToUserAccountsFailureReason(), false)
					{
						Message = userSessionResult.Message
					};
			}

			return loginResult;
		}

		private static Credential GetHashedCredential(Credential credential)
		{
			return new Credential(credential.UserName, _passwordEncryptor.Hash(credential.Password));
		}

		public IResult<UserAccountFailureReason> CreateUser(Credential credential)
		{
			IResult<UserAccountFailureReason> accountCreationResult;

			var result = _userAccountsContext.CreateUser(GetHashedCredential(credential));

			if (result.IsSuccessful && result.Value == DBFailureReason.None)
			{
				accountCreationResult = new Result<UserAccountFailureReason>(UserAccountFailureReason.None, true)
				{
					Message = result.Message
				};
			}
			else
			{
				accountCreationResult = new Result<UserAccountFailureReason>(result.Value.ToUserAccountsFailureReason(), false)
				{
					Message = result.Message
				};
			}

			return accountCreationResult;
		}
	}
}