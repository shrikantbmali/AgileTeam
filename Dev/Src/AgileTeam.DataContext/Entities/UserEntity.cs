using System.Common;

namespace AgileTeam.DataContext.Entities
{
	public class UserEntity : IIdentifiable<long>
	{
		public long ID { get; private set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public IUserLoginSession Session { get; set; }
	}
}