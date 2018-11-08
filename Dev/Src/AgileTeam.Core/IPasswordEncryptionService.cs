namespace AgileTeam.Core
{
	internal interface IPasswordEncryptionService
	{
		string Hash(string password);
	}
}