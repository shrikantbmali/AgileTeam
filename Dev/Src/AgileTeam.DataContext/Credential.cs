namespace AgileTeam.DataContext
{
	public class Credential
	{
		public string UserName { get; private set; }

		public string Password { get; private set; }

		public Credential(string userName, string password)
		{
			UserName = userName;
			Password = password;
		}
	}
}