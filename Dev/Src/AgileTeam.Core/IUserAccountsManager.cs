using AgileTeam.DataContext;
using System.Common;

namespace AgileTeam.Core
{
	public interface IUserAccountsManager
	{
		IResult<IUser, UserAccountFailureReason> Login(Credential credential);

		IResult<UserAccountFailureReason> CreateUser(Credential credential);
	}
}