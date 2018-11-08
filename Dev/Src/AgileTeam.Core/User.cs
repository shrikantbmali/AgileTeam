using AgileTeam.DataContext.Entities;
using System.Common;

namespace AgileTeam.Core
{
	internal class User : IUser
	{
		private readonly UserEntity _userEntity;

		public long ID
		{
			get { return _userEntity.ID; }
		}

		public string FirstName
		{
			get { return _userEntity.FirstName; }
		}

		public string LastName
		{
			get { return _userEntity.LastName; }
		}

		public IUserLoginSession Session
		{
			get { return _userEntity.Session; }
		}

		private User(UserEntity userEntity)
		{
			_userEntity = userEntity;
		}

		public static IUser NewUser(UserEntity userEntity)
		{
			return ValidateSession(userEntity.Session) ? new User(userEntity) : null;
		}

		private static bool ValidateSession(IUserLoginSession session)
		{
			return session != null && !string.IsNullOrEmpty(session.SessionID) && session.ID > 0;
		}
	}
}