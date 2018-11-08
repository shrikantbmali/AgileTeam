using AgileTeam.Core.Entities;

namespace AgileTeam.Core.Users
{
	internal class User : IUser, IPermissible
	{
		private readonly UserEntity _userEntity;

		public long Id
		{
			get { return _userEntity.Id; }
		}

		public string Title
		{
			get { return _userEntity.UserPropertiesEntity.Title; }
		}

		public string FirstName
		{
			get { return _userEntity.UserPropertiesEntity.FirstName; }
		}

		public string LastName
		{
			get { return _userEntity.UserPropertiesEntity.LastName; }
		}

		public byte[] Image
		{
			get { return _userEntity.UserPropertiesEntity.Image; }
		}

		public IPermissions Permissions { get; private set; }

		private User(UserEntity userEntity)
		{
			_userEntity = userEntity;
			Permissions = new UserPermissions(userEntity.UserPermissionsEntity);
		}

		public static IUser New(UserEntity userPropertiesContext)
		{
			return IsValideDataProvided(userPropertiesContext)
				? new User(userPropertiesContext)
				: null;
		}

		private static bool IsValideDataProvided(UserEntity userPropertiesContext)
		{
			return userPropertiesContext != null && userPropertiesContext.Id > 0;
		}
	}
}