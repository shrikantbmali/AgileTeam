using AgileTeam.Core.Entities;
using AgileTeam.Core.Users;
using AgileTeam.Dbl;
using System;

namespace AgileTeam.Core
{
	public class LoginManager : ILoginManager
	{
		private static Lazy<ILoginManager> _lazyInstance =
			new Lazy<ILoginManager>(() => new LoginManager(DatabaseFactory.GetUserDatabaseManager()));

		public static ILoginManager Instance { get { return _lazyInstance.Value; } }

		private readonly IUserDatabaseManager _userDatabaseManager;

		public IUser CurrentUser { get; private set; }

		private LoginManager(IUserDatabaseManager userDatabaseManager)
		{
			_userDatabaseManager = userDatabaseManager;
		}

		public static void Initialize(IUserDatabaseManager userDatabaseManager)
		{
			_lazyInstance = new Lazy<ILoginManager>(() => new LoginManager(userDatabaseManager));
		}

		public static void Initialize()
		{
			_lazyInstance = new Lazy<ILoginManager>(() => new LoginManager(DatabaseFactory.GetUserDatabaseManager()));
		}

		public LoginResult TryLogin(UserCredentials credentials)
		{
			Validate();

			CurrentUser = User.New(_userDatabaseManager.GetUserEntity(credentials));

			return new LoginResult(CurrentUser);
		}

		public bool IsUserAccountSetupCompleted()
		{
			return _userDatabaseManager.IsAccountSetupCompleted();
		}

		public bool TryCreateFirstUser(UserCredentials userCredentials, UserPropertiesEntity userProperties,
			UserPermissionsEntity userPermissions)
		{
			Validate();

			if (_userDatabaseManager.IsUsernameExists(userCredentials.UserName) && CurrentUser != null)
				return false;

			return _userDatabaseManager.InsertUser(userCredentials, userProperties, userPermissions);
		}

		private void Validate()
		{
			if (CurrentUser != null)
				throw new InvalidOperationException(
					"A user is already logged in, cannot try to re-logging with different or even same user!");
		}
	}
}