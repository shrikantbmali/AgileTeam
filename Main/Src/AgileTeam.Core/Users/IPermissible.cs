namespace AgileTeam.Core.Users
{
	internal interface IPermissible
	{
		IPermissions Permissions { get; }
	}
}