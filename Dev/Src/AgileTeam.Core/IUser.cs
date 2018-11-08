using System.Common;

namespace AgileTeam.Core
{
	public interface IUser : IIdentifiable<long>
	{
		string FirstName { get; }

		string LastName { get; }

		IUserLoginSession Session { get; }
	}
}