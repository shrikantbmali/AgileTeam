namespace AgileTeam.Core.Entities
{
	public class CredentialsEntity
	{
		public string UserName { get; set; }

		public string Password { get; set; }

		protected CredentialsEntity(string userName, string password)
		{
			UserName = userName;
			Password = password;
		}
	}
}