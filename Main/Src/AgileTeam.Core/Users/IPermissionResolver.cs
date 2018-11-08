namespace AgileTeam.Core.Users
{
	public interface IPermissionResolver
	{
		bool CanCreateNewProject(IUser user);
	}
}