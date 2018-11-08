using AgileTeam.Core.Users;

namespace AgileTeam.Core
{
	public struct LoginResult
	{
		public LoginResult(IUser user)
			: this()
		{
			User = user;
		}

		public bool Success
		{
			get { return User != null; }
		}

		private IUser User { get; set; }
	}
}