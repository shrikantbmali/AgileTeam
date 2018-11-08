namespace AgileTeam.Core.Entities
{
	public class UserEntity
	{
		public long Id { get; set; }

		public UserPropertiesEntity UserPropertiesEntity { get; private set; }

		public UserPermissionsEntity UserPermissionsEntity { get; private set; }

		public UserEntity(int id, UserPropertiesEntity userPropertiesEntity, UserPermissionsEntity userPermissionsEntity)
		{
			Id = id;
			UserPropertiesEntity = userPropertiesEntity;
			UserPermissionsEntity = userPermissionsEntity;
		}
	}
}