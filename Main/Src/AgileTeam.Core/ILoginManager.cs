using AgileTeam.Core.Entities;
using AgileTeam.Core.Users;

namespace AgileTeam.Core
{
	public interface ILoginManager
	{
		IUser CurrentUser { get; }

		LoginResult TryLogin(UserCredentials credentials);

		bool IsUserAccountSetupCompleted();

		bool TryCreateFirstUser(UserCredentials userCredentials, UserPropertiesEntity userProperties, UserPermissionsEntity userPermissions);
	}
}