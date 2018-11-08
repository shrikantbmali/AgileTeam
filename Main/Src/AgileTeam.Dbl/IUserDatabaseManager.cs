using AgileTeam.Core.Entities;

namespace AgileTeam.Dbl
{
	public interface IUserDatabaseManager
	{
		bool IsAccountSetupCompleted();

		bool IsUsernameExists(string userName);

		UserEntity GetUserEntity(CredentialsEntity credentials);

		bool InsertUser(CredentialsEntity credentials, UserPropertiesEntity userProperties, UserPermissionsEntity userPermissions);
	}
}